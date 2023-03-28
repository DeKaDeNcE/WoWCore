﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace

using System.Linq;

namespace System.Collections.Generic;

    public sealed class MultiMap<TKey, TValue> : IMultiMap<TKey, TValue>, IDictionary<TKey, TValue>
    {
        public MultiMap() { }

        public MultiMap(IEnumerable<KeyValuePair<TKey, TValue>> initialData)
        {
            foreach (var item in initialData)
            {
                Add(item);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (!_interalStorage.ContainsKey(key))
                _interalStorage.Add(key, new List<TValue>());

            _interalStorage[key].Add(value);
        }

        public void AddRange(TKey key, IEnumerable<TValue> valueList)
        {
            if (!_interalStorage.ContainsKey(key))
            {
                _interalStorage.Add(key, new List<TValue>());
            }
            foreach (TValue value in valueList)
            {
                _interalStorage[key].Add(value);
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (!_interalStorage.ContainsKey(item.Key))
            {
                _interalStorage.Add(item.Key, new List<TValue>());
            }
            _interalStorage[item.Key].Add(item.Value);
        }

        public bool Remove(TKey key)
        {
            return _interalStorage.Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!ContainsKey(item.Key))
                return false;

            bool val = _interalStorage[item.Key].Remove(item.Value);

            if (!val)
                return false;

            if (_interalStorage[item.Key].Empty())
                _interalStorage.Remove(item.Key);

            return true;
        }

        public bool Remove(TKey key, TValue value)
        {
            if (!ContainsKey(key))
                return false;

            bool val = _interalStorage[key].Remove(value);
            if (!val)
                return false;

            if (_interalStorage[key].Empty())
                _interalStorage.Remove(key);

            return true;
        }

        public bool ContainsKey(TKey key)
        {
            return _interalStorage.ContainsKey(key);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            List<TValue> valueList;
            if (_interalStorage.TryGetValue(item.Key, out valueList))
                return valueList.Contains(item.Value);
            return false;
        }

        public bool Contains(TKey key, TValue item)
        {
            if (!_interalStorage.ContainsKey(key)) return false;
            return _interalStorage[key].Contains(item);
        }

        public List<TValue> LookupByKey(TKey key)
        {
            if (_interalStorage.ContainsKey(key))
                return _interalStorage[key];

            return new List<TValue>();
        }

        public List<TValue> LookupByKey(object key)
        {
            TKey newkey = (TKey)Convert.ChangeType(key, typeof(TKey));
            if (_interalStorage.ContainsKey(newkey))
                return _interalStorage[newkey];

            return new List<TValue>();
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            if (!_interalStorage.ContainsKey(key))
            {
                value = default;
                return false;
            }
            value = _interalStorage[key].Last();
            return true;
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return _interalStorage[key].LastOrDefault();
            }
            set
            {
                Add(key, value);
            }
        }

        public List<TValue> this[TKey key]
        {
            get
            {
                if (!_interalStorage.ContainsKey(key))
                    return new List<TValue>();
                return _interalStorage[key];
            }
            set
            {
                if (!_interalStorage.ContainsKey(key))
                    _interalStorage.Add(key, value);
                else
                    _interalStorage[key] = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get { return _interalStorage.Keys; }
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> retVal = new();
                foreach (var item in _interalStorage)
                {
                    retVal.AddRange(item.Value);
                }
                return retVal;
            }
        }

        public List<KeyValuePair<TKey, TValue>> KeyValueList
        {
            get
            {
                List<KeyValuePair<TKey, TValue>> retVal = new();
                foreach (var pair in _interalStorage)
                {
                    foreach (var value in pair.Value)
                        retVal.Add(new KeyValuePair<TKey, TValue>(pair.Key, value));
                }
                return retVal;
            }
        }

        public void Clear()
        {
            _interalStorage.Clear();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "argument 'arrayIndex' cannot be negative");

            if (arrayIndex >= array.Length || Count > array.Length - arrayIndex)
                array = new KeyValuePair<TKey, TValue>[Count];

            int index = arrayIndex;
            foreach (KeyValuePair<TKey, TValue> pair in this)
                array[index++] = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);

        }

        public int Count
        {
            get
            {
                int count = 0;
                foreach (var item in _interalStorage)
                {
                    count += item.Value.Count;
                }
                return count;
            }
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count
        {
            get { return _interalStorage.Count; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new MultiMapEnumerator<TKey, TValue>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MultiMapEnumerator<TKey, TValue>(this);
        }

        private Dictionary<TKey, List<TValue>> _interalStorage = new();
    }

    public sealed class SortedMultiMap<TKey, TValue> : IMultiMap<TKey, TValue>, IDictionary<TKey, TValue>
    {
        public SortedMultiMap() { }

        public SortedMultiMap(IEnumerable<KeyValuePair<TKey, TValue>> initialData)
        {
            foreach (var item in initialData)
            {
                Add(item);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (!_interalStorage.ContainsKey(key))
                _interalStorage.Add(key, new List<TValue>());

            _interalStorage[key].Add(value);
        }

        public void AddRange(TKey key, IEnumerable<TValue> valueList)
        {
            if (!_interalStorage.ContainsKey(key))
            {
                _interalStorage.Add(key, new List<TValue>());
            }
            foreach (TValue value in valueList)
            {
                _interalStorage[key].Add(value);
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (!_interalStorage.ContainsKey(item.Key))
            {
                _interalStorage.Add(item.Key, new List<TValue>());
            }
            _interalStorage[item.Key].Add(item.Value);
        }

        public bool Remove(TKey key)
        {
            return _interalStorage.Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!ContainsKey(item.Key))
                return false;

            bool val = _interalStorage[item.Key].Remove(item.Value);

            if (!val)
                return false;

            if (_interalStorage[item.Key].Empty())
                _interalStorage.Remove(item.Key);

            return true;
        }

        public bool Remove(TKey key, TValue value)
        {
            if (!ContainsKey(key))
                return false;

            bool val = _interalStorage[key].Remove(value);

            if (!val)
                return false;

            if (_interalStorage[key].Empty())
                _interalStorage.Remove(key);

            return true;
        }

        public bool ContainsKey(TKey key)
        {
            return _interalStorage.ContainsKey(key);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            List<TValue> valueList;
            if (_interalStorage.TryGetValue(item.Key, out valueList))
                return valueList.Contains(item.Value);
            return false;
        }

        public bool Contains(TKey key, TValue item)
        {
            if (!_interalStorage.ContainsKey(key)) return false;
            return _interalStorage[key].Contains(item);
        }

        public List<TValue> LookupByKey(TKey key)
        {
            if (_interalStorage.ContainsKey(key))
                return _interalStorage[key];

            return new List<TValue>();
        }

        public List<TValue> LookupByKey(object key)
        {
            TKey newkey = (TKey)Convert.ChangeType(key, typeof(TKey));
            if (_interalStorage.ContainsKey(newkey))
                return _interalStorage[newkey];

            return new List<TValue>();
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            if (!_interalStorage.ContainsKey(key))
            {
                value = default;
                return false;
            }
            value = _interalStorage[key].Last();
            return true;
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return _interalStorage[key].LastOrDefault();
            }
            set
            {
                Add(key, value);
            }
        }

        public List<TValue> this[TKey key]
        {
            get
            {
                if (!_interalStorage.ContainsKey(key))
                    return new List<TValue>();
                return _interalStorage[key];
            }
            set
            {
                if (!_interalStorage.ContainsKey(key))
                    _interalStorage.Add(key, value);
                else _interalStorage[key] = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get { return _interalStorage.Keys; }
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> retVal = new();
                foreach (var item in _interalStorage)
                {
                    retVal.AddRange(item.Value);
                }
                return retVal;
            }
        }

        public List<KeyValuePair<TKey, TValue>> KeyValueList
        {
            get
            {
                List<KeyValuePair<TKey, TValue>> retVal = new();
                foreach (var pair in _interalStorage)
                {
                    foreach (var value in pair.Value)
                        retVal.Add(new KeyValuePair<TKey, TValue>(pair.Key, value));
                }
                return retVal;
            }
        }

        public void Clear()
        {
            _interalStorage.Clear();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "argument 'arrayIndex' cannot be negative");

            if (arrayIndex >= array.Length || Count > array.Length - arrayIndex)
                array = new KeyValuePair<TKey, TValue>[Count];

            int index = arrayIndex;
            foreach (KeyValuePair<TKey, TValue> pair in this)
                array[index++] = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);

        }

        public int Count
        {
            get
            {
                int count = 0;
                foreach (var item in _interalStorage)
                {
                    count += item.Value.Count;
                }
                return count;
            }
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count
        {
            get { return _interalStorage.Count; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new SortedMultiMapEnumerator<TKey, TValue>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SortedMultiMapEnumerator<TKey, TValue>(this);
        }

        private SortedDictionary<TKey, List<TValue>> _interalStorage = new();
    }