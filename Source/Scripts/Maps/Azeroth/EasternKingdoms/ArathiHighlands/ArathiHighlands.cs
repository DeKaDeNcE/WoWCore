// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game;
using System;

namespace Scripts.Maps.Azeroth.EasternKingdoms.ArathiHighlands;

struct Texts
{
    public const uint SAY_PROGRESS_1 = 0;
    public const uint SAY_PROGRESS_2 = 1;
    public const uint SAY_PROGRESS_3 = 2;
    public const uint SAY_AGGRO = 4;
    public const uint SAY_PROGRESS_5 = 5;
    public const uint SAY_PROGRESS_6 = 6;
    public const uint SAY_PROGRESS_7 = 7;
    public const uint SAY_PROGRESS_9 = 9;
}

struct Emotes
{
    public const uint EMOTE_PROGRESS_8 = 8;
    public const uint EMOTE_PROGRESS_4 = 3;
}

struct Events
{
    public const uint EVENT_SAY_3 = 1;
    public const uint EVENT_SAY_6 = 2;
    public const uint EVENT_SAY_8 = 3;
}

struct CreatureIDS
{
    public const uint NPC_VENGEFUL_SURGE = 2776;
    public const uint NPC_MYZRAEL = 2755;
}

struct QuestIDS
{
    public const uint QUEST_SUNKEN_TREASURE = 665;
    public const uint QUEST_GOGGLE_BOGGLE = 26050;
}

struct FactionIDS
{
    public const uint FACTION_SUNKEN_TREASURE = 113;
}

[Script]
class npc_professor_phizzlethorpe : EscortAI
{
    public npc_professor_phizzlethorpe(Creature creature) : base(creature) { }

    public override void WaypointReached(uint waypointId, uint pathId)
    {
        Player player = GetPlayerForEscort();
        if (!player)
            return;

        switch (waypointId)
        {
            case 6:
                Talk(Texts.SAY_PROGRESS_2, player);
                _events.ScheduleEvent(Events.EVENT_SAY_3, TimeSpan.FromMicroseconds(3000));
                break;
            case 8:
                Talk(Emotes.EMOTE_PROGRESS_4);
                me.SummonCreature(CreatureIDS.NPC_VENGEFUL_SURGE, -2065.505f, -2136.88f, 22.20362f, 1.0f, TempSummonType.CorpseDespawn);
                me.SummonCreature(CreatureIDS.NPC_VENGEFUL_SURGE, -2059.249f, -2134.88f, 21.51582f, 1.0f, TempSummonType.CorpseDespawn);
                break;
            case 11:
                Talk(Texts.SAY_PROGRESS_5, player);
                _events.ScheduleEvent(Events.EVENT_SAY_6, TimeSpan.FromMilliseconds(11000));
                break;
            case 17:
                Talk(Texts.SAY_PROGRESS_7, player);
                _events.ScheduleEvent(Events.EVENT_SAY_8, TimeSpan.FromMilliseconds(11000));
                break;
        }
    }

    public override void JustSummoned(Creature summon)
    {
        summon.GetAI().AttackStart(me);
    }

    public override void JustEnteredCombat(Unit who)
    {
        Talk(Texts.SAY_AGGRO);
    }

    public override void OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIDS.QUEST_SUNKEN_TREASURE)
        {
            Talk(Texts.SAY_PROGRESS_1, player);
            Start(false, player.GetGUID(), quest);
            me.SetFaction(FactionIDS.FACTION_SUNKEN_TREASURE);
        }
    }


    public override void UpdateAI(uint diff)
    {
        Player player = GetPlayerForEscort();
        if (!player)
            return;

        _events.Update(diff);
        uint eventId = _events.ExecuteEvent();

        while (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_SAY_3:
                    Talk(Texts.SAY_PROGRESS_3, player);
                    break;
                case Events.EVENT_SAY_6:
                    Talk(Texts.SAY_PROGRESS_6, player);
                    me.SetWalk(true);
                    break;
                case Events.EVENT_SAY_8:
                    Talk(Emotes.EMOTE_PROGRESS_8);
                    Talk(Texts.SAY_PROGRESS_9, player);
                    player.GroupEventHappens(QuestIDS.QUEST_GOGGLE_BOGGLE, me);
                    break;
            }
        }
        UpdateAI(diff);
    }
}

/*
 * I decided not to delete a piece of code, it may come in handy in the future.
* This code creates a second copy of Myzrael and appears to be outdated for the BFA 
* since Myzrael appears without this script.
[Script]
class spell_summon_myzrael : SpellScript
{
    void HandleDummy(uint effindex)
    {
        Creature myzrael = GetCaster().SummonCreature(CreatureIDS.NPC_MYZRAEL, -948.493f, -3113.98f, 50.4207f, 3.14159f, TempSummonType.TimedDespawnOutOfCombat, 90000);
        if (myzrael != null)
        {
            myzrael.SetReactState(ReactStates.Aggressive);
            myzrael.SetFaction(14);
            myzrael.AddUnitFlag(UnitFlags.PvpAttackable);
        }
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleDummy, 1, SpellEffectName.Dummy));
    }
}
*/

[Script]
class arathi_highlands : PlayerScript
{
    public arathi_highlands() : base("arathi_highlands") { }

    public override void OnUpdateZone(Player player, uint newZone, uint newArea)
    {
        if (player.GetLevel() == 60 && player.GetZoneId() == 9734 && newZone == 9734)
            PhasingHandler.AddPhase(player, 11292, true);

        if (player.GetLevel() == 60 && player.GetZoneId() == 9734 && newZone == 9734)
            PhasingHandler.RemovePhase(player, 11292, true);
    }
}