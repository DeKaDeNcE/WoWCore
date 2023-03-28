﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.Web
{
    [DataContract]
    public class RealmCharacterCountList
    {
        [DataMember(Name = "counts")]
        public IList<RealmCharacterCountEntry> Counts { get; set; } = new List<RealmCharacterCountEntry>();
    }
}
