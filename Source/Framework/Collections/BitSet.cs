﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace

namespace System.Collections;

    public class BitSet : ICollection, ICloneable
    {
        public BitSet(int length, bool defaultValue = false)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            _mArray = new uint[GetArrayLength(length, BitsPerInt32)];
            _mLength = length;

            var fillValue = defaultValue ? 0xffffffff : 0;

            for (int i = 0; i < _mArray.Length; i++)
                _mArray[i] = fillValue;

            _version = 0;
        }

        public BitSet(uint[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            // this value is chosen to prevent overflow when computing m_length
            if (values.Length > uint.MaxValue / BitsPerInt32)
                throw new ArgumentException();

            _mArray = new uint[values.Length];
            _mLength = values.Length * BitsPerInt32;

            Array.Copy(values, _mArray, values.Length);

            _version = 0;
        }

        public BitSet(BitSet bits)
        {
            if (bits == null)
                throw new ArgumentNullException(nameof(bits));

            var arrayLength = GetArrayLength(bits._mLength, BitsPerInt32);
            _mArray = new uint[arrayLength];
            _mLength = bits._mLength;

            Array.Copy(bits._mArray, _mArray, arrayLength);

            _version = bits._version;
        }

        public bool this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public bool Get(int index)
        {
            if (index < 0 || index >= Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            return (Convert.ToInt64(_mArray[index / 32]) & (1 << (index % 32))) != 0;
        }

        public void Set(int index, bool value)
        {
            if (index < 0 || index >= Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (value)
                _mArray[index / 32] |= 1u << (index % 32);
            else
                _mArray[index / 32] &= ~(1u << (index % 32));

            _version++;
        }

        public void SetAll(bool value)
        {
            uint fillValue = value ? 0xffffffff : 0u;
            int ints = GetArrayLength(_mLength, BitsPerInt32);

            for (int i = 0; i < ints; i++)
                _mArray[i] = fillValue;

            _version++;
        }

        public BitSet And(BitSet value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (Length != value.Length)
                throw new ArgumentException();

            int ints = GetArrayLength(_mLength, BitsPerInt32);

            for (int i = 0; i < ints; i++)
                _mArray[i] &= value._mArray[i];

            _version++;
            return this;
        }

        public BitSet Or(BitSet value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (Length != value.Length)
                throw new ArgumentException();

            var ints = GetArrayLength(_mLength, BitsPerInt32);

            for (int i = 0; i < ints; i++)
                _mArray[i] |= value._mArray[i];

            _version++;
            return this;
        }

        public BitSet Xor(BitSet value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (Length != value.Length)
                throw new ArgumentException();

            var ints = GetArrayLength(_mLength, BitsPerInt32);

            for (int i = 0; i < ints; i++)
                _mArray[i] ^= value._mArray[i];

            _version++;
            return this;
        }

        public BitSet Not()
        {
            var ints = GetArrayLength(_mLength, BitsPerInt32);

            for (int i = 0; i < ints; i++)
                _mArray[i] = ~_mArray[i];

            _version++;
            return this;
        }

        public bool Any()
        {
            for (var i  = 0; i < Length; ++i)
                if (Get(i))
                    return true;

            return false;
        }

        public int Length
        {
            get => _mLength;

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                var newints = GetArrayLength(value, BitsPerInt32);

                if (newints > _mArray.Length || newints + ShrinkThreshold < _mArray.Length)
                {
                    // grow or shrink (if wasting more than _ShrinkThreshold ints)
                    uint[] newarray = new uint[newints];
                    Array.Copy(_mArray, newarray, newints > _mArray.Length ? _mArray.Length : newints);
                    _mArray = newarray;
                }

                if (value > _mLength)
                {
                    // clear high bit values in the last int
                    int last = GetArrayLength(_mLength, BitsPerInt32) - 1;
                    int bits = _mLength % 32;

                    if (bits > 0)
                        _mArray[last] &= (1u << bits) - 1;

                    // clear remaining int values
                    Array.Clear(_mArray, last + 1, newints - last - 1);
                }

                _mLength = value;
                _version++;
            }
        }

        // ICollection implementation
        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (array.Rank != 1)
                throw new ArgumentException();

            if (array is uint[])
                Array.Copy(_mArray, 0, array, index, GetArrayLength(_mLength, BitsPerInt32));
            else if (array is byte[])
            {
                var arrayLength = GetArrayLength(_mLength, BitsPerByte);

                if (array.Length - index < arrayLength)
                    throw new ArgumentException();

                byte[] b = (byte[])array;

                for (int i = 0; i < arrayLength; i++)
                    b[index + i] = (byte)((_mArray[i / 4] >> ((i % 4) * 8)) & 0x000000FF); // Shift to bring the required byte to LSB, then mask
            }
            else if (array is bool[])
            {
                if (array.Length - index < _mLength)
                    throw new ArgumentException();

                bool[] b = (bool[])array;

                for (int i = 0; i < _mLength; i++)
                    b[index + i] = ((_mArray[i / 32] >> (i % 32)) & 0x00000001) != 0;
            }
            else
                throw new ArgumentException();
        }

        public int Count => _mLength;

        public object Clone()
        {
            BitSet bitArray = new(_mArray)
            {
                _version = _version,
                _mLength = _mLength
            };

            return bitArray;
        }

        public object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                    Threading.Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);

                return _syncRoot;
            }
        }

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public IEnumerator GetEnumerator() => new BitArrayEnumeratorSimple(this);

        // XPerY=n means that n Xs can be stored in 1 Y.
        private const int BitsPerInt32 = 32;
        private const int BytesPerInt32 = 4;
        private const int BitsPerByte = 8;

        private static int GetArrayLength(int n, int div)
        {
            Cypher.Assert(div > 0, "GetArrayLength: div arg must be greater than 0");
            return n > 0 ? (n - 1) / div + 1 : 0;
        }

        [Serializable]
        private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
        {
            private BitSet _bitarray;
            private int _index;
            private int _version;
            private bool _currentElement;

            internal BitArrayEnumeratorSimple(BitSet bitarray)
            {
                _bitarray = bitarray;
                _index = -1;
                _version = bitarray._version;
            }

            public object Clone() => MemberwiseClone();

            public bool MoveNext()
            {
                if (_index < _bitarray.Count - 1)
                {
                    _index++;
                    _currentElement = _bitarray.Get(_index);
                    return true;
                }

                _index = _bitarray.Count;
                return false;
            }

            public virtual object Current
            {
                get
                {
                    if (_index == -1)
                        throw new InvalidOperationException();

                    if (_index >= _bitarray.Count)
                        throw new InvalidOperationException();

                    return _currentElement;
                }
            }

            public void Reset()
            {
                //if (version != bitarray._version) throw new InvalidOperationException(Environment.GetResourceString(ResId.InvalidOperation_EnumFailedVersion));
                _index = -1;
            }
        }

        private uint[] _mArray;
        private int _mLength;
        private int _version;
        [NonSerialized]
        private object _syncRoot;

        private const int ShrinkThreshold = 256;
    }