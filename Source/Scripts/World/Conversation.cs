﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Game.Entities;
using Game.Scripting;

namespace Scripts.World
{
    [Script]
    class conversation_allied_race_dk_defender_of_azeroth : ConversationScript
    {
        const uint NpcTalkToYourCommanderCredit = 161709;
        const uint NpcListenToYourCommanderCredit = 163027;
        const uint ConversationLinePlayer = 32926;

        public conversation_allied_race_dk_defender_of_azeroth() : base("conversation_allied_race_dk_defender_of_azeroth") { }

        public override void OnConversationCreate(Conversation conversation, Unit creator)
        {
            Player player = creator.ToPlayer();
            if (player != null)
                player.KilledMonsterCredit(NpcTalkToYourCommanderCredit);
        }

        public override void OnConversationLineStarted(Conversation conversation, uint lineId, Player sender)
        {
            if (lineId != ConversationLinePlayer)
                return;

            sender.KilledMonsterCredit(NpcListenToYourCommanderCredit);
        }
    }
}