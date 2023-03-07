// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Local

using System;
using System.Collections.Generic;
using Framework.Constants;
using Game.Chat;
using Game.DataStorage;
using Game.Networking;
using Game.Networking.Packets;

namespace Game
{
    public partial class WorldSession
    {
        const uint MaxChannelNameLength = 31;
        const uint MaxChannelPassLength = 127;

        [WorldPacketHandler(ClientOpcodes.ChatJoinChannel)]
        void HandleJoinChannel(JoinChannel packet)
        {
            Log.outDebug(LogFilter.ChatSystem, $"CMSG_JOIN_CHANNEL {GetPlayerInfo()} ChatChannelId: {packet.ChatChannelId}, CreateVoiceSession: {packet.CreateVoiceSession}, Internal: {packet.Internal}, ChannelName: {packet.ChannelName}, Password: {packet.Password}");

            AreaTableRecord zone = CliDB.AreaTableStorage.LookupByKey(GetPlayer().GetZoneId());
            if (packet.ChatChannelId != 0)
            {
                ChatChannelsRecord channel = CliDB.ChatChannelsStorage.LookupByKey(packet.ChatChannelId);
                if (channel == null)
                    return;

                if (zone == null || !GetPlayer().CanJoinConstantChannelInZone(channel, zone))
                    return;
            }

            ChannelManager cMgr = ChannelManager.ForTeam(GetPlayer().GetTeam());
            if (cMgr == null)
                return;

            if (packet.ChatChannelId != 0)
            { // system channel
                Channel channel = cMgr.GetSystemChannel((uint)packet.ChatChannelId, zone);
                if (channel != null)
                    channel.JoinChannel(GetPlayer());
            }
            else
            { // custom channel
                if (string.IsNullOrEmpty(packet.ChannelName) || Char.IsDigit(packet.ChannelName[0]))
                {
                    ChannelNotify channelNotify = new();
                    channelNotify.Type = ChatNotify.InvalidNameNotice;
                    channelNotify.Channel = packet.ChannelName;
                    SendPacket(channelNotify);
                    return;
                }

                if (packet.ChannelName.Length > MaxChannelNameLength)
                {
                    ChannelNotify channelNotify = new();
                    channelNotify.Type = ChatNotify.InvalidNameNotice;
                    channelNotify.Channel = packet.ChannelName;
                    SendPacket(channelNotify);
                    Log.outError(LogFilter.Network, $"Player {GetPlayer().GetGUID()} tried to create a channel with a name more than {MaxChannelNameLength} characters long - blocked");
                    return;
                }

                if (packet.Password.Length > MaxChannelPassLength)
                {
                    Log.outError(LogFilter.Network, $"Player {GetPlayer().GetGUID()} tried to create a channel with a password more than {MaxChannelPassLength} characters long - blocked");
                    return;
                }
                if (!DisallowHyperlinksAndMaybeKick(packet.ChannelName))
                    return;

                Channel channel = cMgr.GetCustomChannel(packet.ChannelName);
                if (channel != null)
                    channel.JoinChannel(GetPlayer(), packet.Password);
                else
                {
                    channel = cMgr.CreateCustomChannel(packet.ChannelName);
                    if (channel != null)
                    {
                        channel.SetPassword(packet.Password);
                        channel.JoinChannel(GetPlayer(), packet.Password);
                    }
                }
            }
        }

        [WorldPacketHandler(ClientOpcodes.ChatLeaveChannel)]
        void HandleLeaveChannel(LeaveChannel packet)
        {
            Log.outDebug(LogFilter.ChatSystem, $"CMSG_LEAVE_CHANNEL {GetPlayerInfo()} ChannelName: {packet.ChannelName}, ZoneChannelID: {packet.ZoneChannelID}");

            if (string.IsNullOrEmpty(packet.ChannelName) && packet.ZoneChannelID == 0)
                return;

            AreaTableRecord zone = CliDB.AreaTableStorage.LookupByKey(GetPlayer().GetZoneId());
            if (packet.ZoneChannelID != 0)
            {
                ChatChannelsRecord channel = CliDB.ChatChannelsStorage.LookupByKey(packet.ZoneChannelID);
                if (channel == null)
                    return;

                if (zone == null || !GetPlayer().CanJoinConstantChannelInZone(channel, zone))
                    return;
            }

            ChannelManager cMgr = ChannelManager.ForTeam(GetPlayer().GetTeam());
            if (cMgr != null)
            {
                Channel channel = cMgr.GetChannel((uint)packet.ZoneChannelID, packet.ChannelName, GetPlayer(), true, zone);
                if (channel != null)
                    channel.LeaveChannel(GetPlayer(), true);

                if (packet.ZoneChannelID != 0)
                    cMgr.LeftChannel((uint)packet.ZoneChannelID, zone);
            }
        }

        [WorldPacketHandler(ClientOpcodes.ChatChannelAnnouncements)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelDeclineInvite)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelDisplayList)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelList)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelOwner)]
        void HandleChannelCommand(ChannelCommand packet)
        {
            Log.outDebug(LogFilter.ChatSystem, $"{Enum.GetName(typeof(ClientOpcodes), packet.GetOpcode())} {GetPlayerInfo()} ChannelName: {packet.ChannelName}");

            Channel channel = ChannelManager.GetChannelForPlayerByNamePart(packet.ChannelName, GetPlayer());
            if (channel == null)
                return;

            switch (packet.GetOpcode())
            {
                case ClientOpcodes.ChatChannelAnnouncements:
                    channel.Announce(GetPlayer());
                    break;
                case ClientOpcodes.ChatChannelDeclineInvite:
                    channel.DeclineInvite(GetPlayer());
                    break;
                case ClientOpcodes.ChatChannelDisplayList:
                case ClientOpcodes.ChatChannelList:
                    channel.List(GetPlayer());
                    break;
                case ClientOpcodes.ChatChannelOwner:
                    channel.SendWhoOwner(GetPlayer());
                    break;
            }
        }

        [WorldPacketHandler(ClientOpcodes.ChatChannelBan)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelInvite)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelKick)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelModerator)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelSetOwner)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelSilenceAll)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelUnban)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelUnmoderator)]
        [WorldPacketHandler(ClientOpcodes.ChatChannelUnsilenceAll)]
        void HandleChannelPlayerCommand(ChannelPlayerCommand packet)
        {
            if (packet.Name.Length >= MaxChannelNameLength)
            {
                Log.outDebug(LogFilter.ChatSystem, $"{Enum.GetName(typeof(ClientOpcodes), packet.GetOpcode())} {GetPlayerInfo()} ChannelName: {packet.ChannelName}, Name: {packet.Name}, Name too long.");
                return;
            }

            if (!ObjectManager.NormalizePlayerName(ref packet.Name))
                return;

            Channel channel = ChannelManager.GetChannelForPlayerByNamePart(packet.ChannelName, GetPlayer());
            if (channel == null)
                return;

            switch (packet.GetOpcode())
            {
                case ClientOpcodes.ChatChannelBan:
                    channel.Ban(GetPlayer(), packet.Name);
                    break;
                case ClientOpcodes.ChatChannelInvite:
                    channel.Invite(GetPlayer(), packet.Name);
                    break;
                case ClientOpcodes.ChatChannelKick:
                    channel.Kick(GetPlayer(), packet.Name);
                    break;
                case ClientOpcodes.ChatChannelModerator:
                    channel.SetModerator(GetPlayer(), packet.Name);
                    break;
                case ClientOpcodes.ChatChannelSetOwner:
                    channel.SetOwner(GetPlayer(), packet.Name);
                    break;
                case ClientOpcodes.ChatChannelSilenceAll:
                    channel.SilenceAll(GetPlayer(), packet.Name);
                    break;
                case ClientOpcodes.ChatChannelUnban:
                    channel.UnBan(GetPlayer(), packet.Name);
                    break;
                case ClientOpcodes.ChatChannelUnmoderator:
                    channel.UnsetModerator(GetPlayer(), packet.Name);
                    break;
                case ClientOpcodes.ChatChannelUnsilenceAll:
                    channel.UnsilenceAll(GetPlayer(), packet.Name);
                    break;
            }
        }

        [WorldPacketHandler(ClientOpcodes.ChatChannelPassword)]
        void HandleChannelPassword(ChannelPassword packet)
        {
            if (packet.Password.Length > MaxChannelPassLength)
            {
                Log.outDebug(LogFilter.ChatSystem, $"{Enum.GetName(typeof(ClientOpcodes), packet.GetOpcode())} {GetPlayerInfo()} ChannelName: {packet.ChannelName}, Password: {packet.Password}, Password too long.");
                return;
            }

            Log.outDebug(LogFilter.ChatSystem, $"{Enum.GetName(typeof(ClientOpcodes), packet.GetOpcode())} {GetPlayerInfo()} ChannelName: {packet.ChannelName}, Password: {packet.Password}");

            Channel channel = ChannelManager.GetChannelForPlayerByNamePart(packet.ChannelName, GetPlayer());
            if (channel != null)
                channel.Password(GetPlayer(), packet.Password);
        }
    }
}