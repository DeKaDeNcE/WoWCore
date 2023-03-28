// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System;

namespace Framework.IO
{
    public class SocketBuffer
    {
        byte[] _storage;
        int _wpos;

        public SocketBuffer(int initialSize = 0)
        {
            _storage = new byte[initialSize];
        }

        public void Resize(int bytes)
        {
            _storage = new byte[bytes];
        }

        public byte[] GetData()
        {
            return _storage;
        }

        public void Write(byte[] data, int index, int size)
        {
            Buffer.BlockCopy(data, index, _storage, _wpos, size);
            _wpos += size;
        }

        public int GetRemainingSpace() { return _storage.Length - _wpos; }

        public void Reset()
        {
            _wpos = 0;
        }
    }
}
