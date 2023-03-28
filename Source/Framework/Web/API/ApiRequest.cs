// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Web.API
{
    public class ApiRequest<T>
    {
        public uint? SearchId { get; set; }
        public Func<T, bool> SearchFunc { get; set; }
    }
}
