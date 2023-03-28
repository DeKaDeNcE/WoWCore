// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Game.Entities;

namespace Game.Networking.Packets;

    public class AutoBankItem : ClientPacket
    {
        public InvUpdate Inv;
        public byte Bag;
        public byte Slot;

        public AutoBankItem(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Inv = new InvUpdate(_worldPacket);
            Bag = _worldPacket.ReadUInt8();
            Slot = _worldPacket.ReadUInt8();
        }
    }

    public class AutoStoreBankItem : ClientPacket
    {
        public InvUpdate Inv;
        public byte Bag;
        public byte Slot;

        public AutoStoreBankItem(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Inv = new InvUpdate(_worldPacket);
            Bag = _worldPacket.ReadUInt8();
            Slot = _worldPacket.ReadUInt8();
        }
    }

    public class BuyBankSlot : ClientPacket
    {
        public ObjectGuid Guid;

        public BuyBankSlot(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Guid = _worldPacket.ReadPackedGuid();
        }
    }

    public class AutoBankReagent : ClientPacket
    {
        public InvUpdate Inv;
        public byte Slot;
        public byte PackSlot;

        public AutoBankReagent(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Inv = new(_worldPacket);
            PackSlot = _worldPacket.ReadUInt8();
            Slot = _worldPacket.ReadUInt8();
        }
    }

    public class AutoStoreBankReagent : ClientPacket
    {
        public InvUpdate Inv;
        public byte Slot;
        public byte PackSlot;

        public AutoStoreBankReagent(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Inv = new(_worldPacket);
            Slot = _worldPacket.ReadUInt8();
            PackSlot = _worldPacket.ReadUInt8();
        }
    }

    // CMSG_BUY_REAGENT_BANK
    // CMSG_REAGENT_BANK_DEPOSIT
    public class ReagentBank : ClientPacket
    {
        public ObjectGuid Banker;

        public ReagentBank(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            Banker = _worldPacket.ReadPackedGuid();
        }
    }