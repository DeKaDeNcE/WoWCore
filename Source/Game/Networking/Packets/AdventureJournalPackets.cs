// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using Framework.Constants;

namespace Game.Networking.Packets;

    public class AdventureJournalOpenQuest : ClientPacket
    {
        public uint AdventureJournalID;

        public AdventureJournalOpenQuest(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            AdventureJournalID = _worldPacket.ReadUInt32();
        }
    }

    public class AdventureJournalUpdateSuggestions : ClientPacket
    {
        public bool OnLevelUp;

        public AdventureJournalUpdateSuggestions(WorldPacket packet) : base(packet) { }

        public override void Read()
        {
            OnLevelUp = _worldPacket.HasBit();
        }
    }

    public class AdventureJournalDataResponse : ServerPacket
    {
        public bool OnLevelUp;
        public List<AdventureJournalEntry> AdventureJournalData = new();

        public AdventureJournalDataResponse() : base(ServerOpcodes.AdventureJournalDataResponse) { }

        public override void Write()
        {
            _worldPacket.WriteBit(OnLevelUp);
            _worldPacket.FlushBits();
            _worldPacket.WriteInt32(AdventureJournalData.Count);

            foreach (var adventureJournal in AdventureJournalData)
            {
                _worldPacket.WriteInt32(adventureJournal.AdventureJournalID);
                _worldPacket.WriteInt32(adventureJournal.Priority);
            }
        }
    }

    public struct AdventureJournalEntry
    {
        public int AdventureJournalID;
        public int Priority;
    }