﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace

using System.IO;

namespace System.Collections.Generic;

    public class Array<T> : List<T>
    {
        private readonly int _limit;

        public Array(int size) : base(size)
        {
            _limit = size;
        }

        public Array(params T[] args) : base(args)
        {
            _limit = args.Length;
        }

        public Array(int size, T defaultFillValue) : base(size)
        {
            _limit = size;
            Fill(defaultFillValue);
        }

        public void Fill(T value)
        {
            for (var i = 0; i < _limit; ++i)
                Add(value);
        }

        public new T this[int index]
        {
            get => base[index];
            set
            {
                if (index >= Count)
                {
                    if (Count >= _limit)
                        throw new InternalBufferOverflowException("Attempted to read more array elements from packet " + Count + 1 + " than allowed " + _limit);

                    Insert(index, value);
                }
                else
                    base[index] = value;
            }
        }

        public int GetLimit() => _limit;

        public static implicit operator T[] (Array<T> array) => array.ToArray();
    }