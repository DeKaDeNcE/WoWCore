// Copyright (C) Arctium <https://arctium.io> All rights reserved.
// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

namespace Framework.Cryptography
{
    //Thx Fabian over at Arctium.
    public sealed class SARC4
    {
        public SARC4()
        {
            _s = new byte[0x100];
            _tmp = 0;
            _tmp2 = 0;
        }

        public void PrepareKey(byte[] key)
        {
            for (int i = 0; i < 0x100; i++)
                _s[i] = (byte)i;

            var j = 0;
            for (int i = 0; i < 0x100; i++)
            {
                j = (byte)((j + key[i % key.Length] + _s[i]) & 255);

                var tempS = _s[i];

                _s[i] = _s[j];
                _s[j] = tempS;
            }
        }

        public void ProcessBuffer(byte[] data, int length)
        {
            for (int i = 0; i < length; i++)
            {
                _tmp = (byte)((_tmp + 1) % 0x100);
                _tmp2 = (byte)((_tmp2 + _s[_tmp]) % 0x100);

                var sTemp = _s[_tmp];

                _s[_tmp] = _s[_tmp2];
                _s[_tmp2] = sTemp;

                data[i] = (byte)(_s[(_s[_tmp] + _s[_tmp2]) % 0x100] ^ data[i]);
            }
        }

        byte[] _s;
        byte _tmp;
        byte _tmp2;
    }
}
