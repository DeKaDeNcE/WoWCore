﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Local

using System.Collections.Generic;
using Framework.Constants;
using Bgs.Protocol.Account.V1;

namespace BNetServer.Networking
{
    public partial class Session
    {
        [Service(OriginalHash.AccountService, 30)]
        BattlenetRpcErrorCode HandleGetAccountState(GetAccountStateRequest request, GetAccountStateResponse response)
        {
            if (!authed)
                return BattlenetRpcErrorCode.Denied;

            if (request.Options.FieldPrivacyInfo)
            {
                response.State = new AccountState();
                response.State.PrivacyInfo = new PrivacyInfo();
                response.State.PrivacyInfo.IsUsingRid = false;
                response.State.PrivacyInfo.IsVisibleForViewFriends = false;
                response.State.PrivacyInfo.IsHiddenFromFriendFinder = true;

                response.Tags = new AccountFieldTags();
                response.Tags.PrivacyInfoTag = 0xD7CA834D;
            }

            return BattlenetRpcErrorCode.Ok;
        }

        [Service(OriginalHash.AccountService, 31)]
        BattlenetRpcErrorCode HandleGetGameAccountState(GetGameAccountStateRequest request, GetGameAccountStateResponse response)
        {
            if (!authed)
                return BattlenetRpcErrorCode.Denied;

            if (request.Options.FieldGameLevelInfo)
            {
                var gameAccountInfo = accountInfo.GameAccounts.LookupByKey(request.GameAccountId.Low);
                if (gameAccountInfo != null)
                {
                    response.State = new GameAccountState();
                    response.State.GameLevelInfo = new GameLevelInfo();
                    response.State.GameLevelInfo.Name = gameAccountInfo.DisplayName;
                    response.State.GameLevelInfo.Program = 5730135; // WoW
                }

                response.Tags = new GameAccountFieldTags();
                response.Tags.GameLevelInfoTag = 0x5C46D483;
            }

            if (request.Options.FieldGameStatus)
            {
                if (response.State == null)
                    response.State = new GameAccountState();

                response.State.GameStatus = new GameStatus();

                var gameAccountInfo = accountInfo.GameAccounts.LookupByKey(request.GameAccountId.Low);
                if (gameAccountInfo != null)
                {
                    response.State.GameStatus.IsSuspended = gameAccountInfo.IsBanned;
                    response.State.GameStatus.IsBanned = gameAccountInfo.IsPermanentlyBanned;
                    response.State.GameStatus.SuspensionExpires = gameAccountInfo.UnbanDate * 1000000;
                }

                response.State.GameStatus.Program = 5730135; // WoW
                response.Tags.GameStatusTag = 0x98B75F99;
            }

            return BattlenetRpcErrorCode.Ok;
        }
    }
}