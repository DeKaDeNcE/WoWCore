﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace

using BNetServer;

public static class Global
{
    public static RealmManager RealmMgr => RealmManager.Instance;
    public static LoginServiceManager LoginServiceMgr => LoginServiceManager.Instance;
}