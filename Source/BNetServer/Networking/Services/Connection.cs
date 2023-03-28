// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Local

using System;
using Framework.Constants;
using Bgs.Protocol;
using Bgs.Protocol.Connection.V1;

namespace BNetServer.Networking;

    public partial class Session
    {
        [Service(OriginalHash.ConnectionService, 1)]
        BattlenetRpcErrorCode HandleConnect(ConnectRequest request, ConnectResponse response)
        {
            if (request.ClientId != null)
                response.ClientId.MergeFrom(request.ClientId);

            response.ServerId = new ProcessId
            {
                Label = (uint)Environment.ProcessId,
                Epoch = (uint)Time.UnixTime
            };

            response.ServerTime = (ulong)Time.UnixTimeMilliseconds;

            response.UseBindlessRpc = request.UseBindlessRpc;

            return BattlenetRpcErrorCode.Ok;
        }

        [Service(OriginalHash.ConnectionService, 5)]
        BattlenetRpcErrorCode HandleKeepAlive(NoData request)
        {
            return BattlenetRpcErrorCode.Ok;
        }

        [Service(OriginalHash.ConnectionService, 7)]
        BattlenetRpcErrorCode HandleRequestDisconnect(DisconnectRequest request)
        {
            var disconnectNotification = new DisconnectNotification
            {
                ErrorCode = request.ErrorCode
            };

            SendRequest((uint)OriginalHash.ConnectionService, 4, disconnectNotification);

            CloseSocket();
            return BattlenetRpcErrorCode.Ok;
        }
    }