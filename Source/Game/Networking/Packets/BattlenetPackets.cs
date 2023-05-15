// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Framework.IO;
using Framework.Constants;

namespace Game.Networking.Packets
{
    public class Notification : ServerPacket
    {
        public MethodCall Method;
        public ByteBuffer Data = new();

        public Notification() : base(ServerOpcodes.BattlenetNotification) { }

        public override void Write()
        {
            Method.Write(_worldPacket);
            _worldPacket.WriteUInt32(Data.GetSize());
            _worldPacket.WriteBytes(Data);
        }
    }

    public class Response : ServerPacket
    {
        public BattlenetRpcErrorCode BnetStatus = BattlenetRpcErrorCode.Ok;
        public MethodCall Method;
        public ByteBuffer Data = new();

        public Response() : base(ServerOpcodes.BattlenetResponse) { }

        public override void Write()
        {
            _worldPacket.WriteUInt32((uint)BnetStatus);
            Method.Write(_worldPacket);
            _worldPacket.WriteUInt32(Data.GetSize());
            _worldPacket.WriteBytes(Data);
        }
    }

    public class ConnectionStatus : ServerPacket
    {
        public byte State;
        public bool SuppressNotification = true;

        public ConnectionStatus() : base(ServerOpcodes.BattleNetConnectionStatus) { }

        public override void Write()
        {
            _worldPacket.WriteBits(State, 2);
            _worldPacket.WriteBit(SuppressNotification);
            _worldPacket.FlushBits();
        }
    }

    public class ChangeRealmTicketResponse : ServerPacket
    {
        public uint Token;
        public bool Allow = true;
        public ByteBuffer Ticket;

        public ChangeRealmTicketResponse() : base(ServerOpcodes.ChangeRealmTicketResponse) { }

        public override void Write()
        {
            _worldPacket.WriteUInt32(Token);
            _worldPacket.WriteBit(Allow);
            _worldPacket.WriteUInt32(Ticket.GetSize());
            _worldPacket.WriteBytes(Ticket);
        }
    }

    public class BattlenetRequest : ClientPacket
    {
        public MethodCall Method;
        public byte[] Data;

        public BattlenetRequest(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Method.Read(_worldPacket);
            var protoSize = _worldPacket.ReadUInt32();

            Data = _worldPacket.ReadBytes(protoSize);
        }
    }

    public class ChangeRealmTicket : ClientPacket
    {
        public uint Token;
        public Array<byte> Secret = new(32);

        public ChangeRealmTicket(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Token = _worldPacket.ReadUInt32();

            for (var i = 0; i < Secret.GetLimit(); ++i)
                Secret[i] = _worldPacket.ReadUInt8();
        }
    }

    public struct MethodCall
    {

        public ulong Type;
        public ulong ObjectId;
        public uint Token;

        public uint GetServiceHash() { return (uint)(Type >> 32); }
        public uint GetMethodId() { return (uint)(Type & 0xFFFFFFFF); }

        public void Read(ByteBuffer data)
        {
            Type = data.ReadUInt64();
            ObjectId = data.ReadUInt64();
            Token = data.ReadUInt32();
        }

        public void Write(ByteBuffer data)
        {
            data.WriteUInt64(Type);
            data.WriteUInt64(ObjectId);
            data.WriteUInt32(Token);
        }
    }
}