// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using System;

namespace Scripts.Maps.Azeroth.EasternKingdoms.Ghostlands;
    struct QuestIDS
{
    public const uint QUEST_ESCAPE_FROM_THE_CATACOMBS = 9212;
}

struct CreatureIDS
{
    public const uint NPC_CAPTAIN_HELIOS = 16220;
    public const uint NPC_MUMMIFIED_HEADHUNTER = 16342;
    public const uint NPC_SHADOWPINE_ORACLE = 16343;
    public const uint NPC_RANGER_LILATHA = 16295;
    public const uint NPC_RATHIS_TOMBER = 16224;
}

struct Texts
{
    public const uint SAY_START = 0;
    public const uint SAY_PROGRESS1 = 1;
    public const uint SAY_PROGRESS2 = 2;
    public const uint SAY_PROGRESS3 = 3;
    public const uint SAY_END1 = 4;
    public const uint SAY_END2 = 5;
    public const uint SAY_CAPTAIN_ANSWER = 0;
}

struct GameObjects
{
    public const uint GO_CAGE = 181152;
}

struct GossipOption
{
    public const string Option1 = "I'd like to browse your goods.";
}

struct Misc
{
    public const uint FACTION_QUEST_ESCAPE = 113;
}

[Script]
class npc_ranger_lilatha : EscortAI
{
    public npc_ranger_lilatha(Creature creature) : base(creature) { }

    public override void WaypointReached(uint waypointId, uint pathId)
    {
        Player player = GetPlayerForEscort();
        if (!player)
            return;

        switch (waypointId)
        {
            case 0:
                me.SetStandState(UnitStandStateType.Stand);
                GameObject Cage = me.FindNearestGameObject(GameObjects.GO_CAGE, 20);
                if (Cage != null)
                    Cage.SetGoState(GameObjectState.Active);
                Talk(Texts.SAY_START, player);
                break;
            case 5:
                Talk(Texts.SAY_PROGRESS1, player);
                break;
            case 11:
                Talk(Texts.SAY_PROGRESS2, player);
                me.SetFacingTo(4.762841f);
                break;
            case 18:
                {
                    Talk(Texts.SAY_PROGRESS3, player);
                    TempSummon Summ1 = me.SummonCreature(CreatureIDS.NPC_MUMMIFIED_HEADHUNTER, 7627.083984f, -7532.538086f, 152.128616f, 1.082733f, TempSummonType.DeadDespawn, TimeSpan.FromMilliseconds(0));
                    TempSummon Summ2 = me.SummonCreature(CreatureIDS.NPC_SHADOWPINE_ORACLE, 7620.432129f, -7532.550293f, 152.454865f, 0.827478f, TempSummonType.DeadDespawn, TimeSpan.FromMilliseconds(0));
                    if (Summ1 && Summ2)
                    {
                        Summ1.Attack(me, true);
                        Summ2.Attack(player, true);
                    }
                    AttackStart(Summ1);
                }
                break;
            case 19:
                me.SetWalk(false);
                break;
            case 25:
                me.SetWalk(true);
                break;
            case 30:
                player.GroupEventHappens(QuestIDS.QUEST_ESCAPE_FROM_THE_CATACOMBS, me);
                break;
            case 32:
                me.SetFacingTo(2.978281f);
                Talk(Texts.SAY_END1, player);
                break;
            case 33:
                me.SetFacingTo(5.858011f);
                Talk(Texts.SAY_END2, player);
                Creature CaptainHelios = me.FindNearestCreature(CreatureIDS.NPC_CAPTAIN_HELIOS, 50);
                if (CaptainHelios != null)
                    CaptainHelios.GetAI().Talk(Texts.SAY_CAPTAIN_ANSWER, player);
                break;
        }
    }

    public override void OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIDS.QUEST_ESCAPE_FROM_THE_CATACOMBS)
        {
            Creature creature = GetClosestCreatureWithEntry(me, CreatureIDS.NPC_RANGER_LILATHA, SharedConst.InteractionDistance * 2);
            if (creature != null)
                creature.SetFaction(Misc.FACTION_QUEST_ESCAPE);

            npc_ranger_lilatha pEscortAI = creature.GetAI<npc_ranger_lilatha>();
            if (pEscortAI != null)
                pEscortAI.Start(true, player.GetGUID());
        }
    }
}

[Script]
class npc_rathis_tomber : ScriptedAI
{
    public npc_rathis_tomber(Creature creature) : base(creature) { }

    public override bool OnGossipSelect(Player player, uint menuId, uint gossipListId)
    {
        uint action = player.PlayerTalkClass.GetGossipOptionAction(gossipListId);
        player.PlayerTalkClass.ClearMenus();
        if (action == eTradeskill.GossipActionTrade)
            player.GetSession().SendListInventory(me.GetGUID());
        return true;
    }

    public override bool OnGossipHello(Player player)
    {
        if (me.IsQuestGiver())
            player.PrepareQuestMenu(me.GetGUID());

        if (me.IsVendor() && player.GetQuestRewardStatus(9152))
        {
            player.AddGossipItem(GossipOptionNpc.Vendor, GossipOption.Option1, eTradeskill.GossipSenderMain, eTradeskill.GossipActionTrade);
            player.SendGossipMenu(player.GetGossipTextId(me), me.GetGUID());
        }
        else
            player.SendGossipMenu(player.GetGossipTextId(me), me.GetGUID());

        return true;
    }
}