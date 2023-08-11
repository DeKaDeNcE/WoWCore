// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using System;

namespace Scripts.Maps.Azeroth.Kalimdor.NorthernBarrens;


struct CreatureIds
{
    public const uint Mercenary = 3282;
    public const uint PilotWizz = 3451;
}

struct FactionIds
{
    public const uint Ratchet = 637;
}

struct QuestIds
{
    public const uint TheEscape = 863;
}

struct Say
{
    public const uint SAY_MERCENARY = 0;
    public const uint SAY_START = 0;
    public const uint SAY_STARTUP1 = 1;
    public const uint SAY_STARTUP2 = 2;
    public const uint SAY_PROGRESS_1 = 3;
    public const uint SAY_PROGRESS_2 = 4;
    public const uint SAY_PROGRESS_3 = 5;
    public const uint SAY_END = 6;
}

[Script]
class npc_wizzlecrank_shredder : EscortAI
{
    public npc_wizzlecrank_shredder(Creature creature) : base(creature)
    {
        Initialize();
    }

    void Initialize()
    {
        IsPostEvent = false;
        PostEventTimer = 1000;
        PostEventCount = 0;
    }

    bool IsPostEvent;
    uint PostEventTimer;
    uint PostEventCount;

    public override void Reset()
    {
        if (!HasEscortState(EscortState.Escorting))
        {
            if (me.GetStandState() == UnitStandStateType.Dead)
                me.SetStandState(UnitStandStateType.Stand);

            Initialize();
        }
    }

    public override void OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIds.TheEscape)
        {
            me.SetFaction(FactionIds.Ratchet);
            me.GetAI().Talk(Say.SAY_START);
            Start(true, player.GetGUID());
        }
    }

    public override void WaypointReached(uint waypointId, uint pathId)
    {
        switch (waypointId)
        {
            case 0:
                Talk(Say.SAY_STARTUP1);
                break;
            case 1:
                me.SetWalk(false);
                break;
            case 17:
                Creature temp = me.SummonCreature(CreatureIds.Mercenary, 1128.489f, -3037.611f, 92.701f, 1.472f, TempSummonType.TimedDespawnOutOfCombat, TimeSpan.FromMilliseconds(120000));
                if (temp != null)
                {
                    temp.GetAI().Talk(Say.SAY_MERCENARY);
                    me.SummonCreature(CreatureIds.Mercenary, 1160.172f, -2980.168f, 97.313f, 3.690f, TempSummonType.TimedDespawnOutOfCombat, TimeSpan.FromMilliseconds(120000));
                }
                break;
            case 24:
                IsPostEvent = true;
                break;
        }
    }

    public override void WaypointStarted(uint waypointId, uint pathId)
    {
        Player player = GetPlayerForEscort();

        if (!player)
            return;

        switch (waypointId)
        {
            case 9:
                Talk(Say.SAY_STARTUP2, player);
                break;
            case 18:
                Talk(Say.SAY_PROGRESS_1, player);
                me.SetWalk(true);
                break;
        }
    }

    public override void JustSummoned(Creature summon)
    {
        if (summon.GetEntry() == CreatureIds.PilotWizz)
            me.SetStandState(UnitStandStateType.Dead);

        if (summon.GetEntry() == CreatureIds.Mercenary)
            summon.GetAI().AttackStart(me);
    }

    public override void UpdateEscortAI(uint diff)
    {
        if (!UpdateVictim())
        {
            if (IsPostEvent)
            {
                if (PostEventTimer <= diff)
                {
                    switch (PostEventCount)
                    {
                        case 0:
                            Talk(Say.SAY_PROGRESS_2);
                            break;
                        case 1:
                            Talk(Say.SAY_PROGRESS_3);
                            break;
                        case 2:
                            Talk(Say.SAY_END);
                            break;
                        case 3:
                            Player player = GetPlayerForEscort();
                            if (player != null)
                            {
                                player.GroupEventHappens(QuestIds.TheEscape, me);
                                me.SummonCreature(CreatureIds.PilotWizz, 0.0f, 0.0f, 0.0f, 0.0f, TempSummonType.TimedDespawn, TimeSpan.FromMilliseconds(180000));
                            }
                            break;
                    }
                    ++PostEventCount;
                    PostEventTimer = 5000;
                }
                else
                    PostEventTimer -= diff;
            }
            return;
        }
        DoMeleeAttackIfReady();
    }
}