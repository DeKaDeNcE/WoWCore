// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

namespace Scripts.Maps.Azeroth.Kalimdor.BloodmystIsle;

using System;
using System.Collections.Generic;
using System.Numerics;
using Framework.Constants;
using Framework.Dynamic;
using Game;
using Game.AI;
using Game.Entities;
using Game.Groups;
using Game.Scripting;
using Google.Protobuf.WellKnownTypes;

//imported from trinitycore
struct SpellIds
{
    public const uint SPELL_BLOODMYST_TESLA = 31611;
    public const uint SPELL_SIRONAS_CHANNELING = 31612;

    public const uint SPELL_UPPERCUT = 10966;
    public const uint SPELL_IMMOLATE = 12742;
    public const uint SPELL_CURSE_OF_BLOOD = 8282;

    public const uint SPELL_FROST_SHOCK = 8056;
    public const uint SPELL_HEALING_SURGE = 8004;
    public const uint SPELL_SEARING_TOTEM = 38116;
    public const uint SPELL_STRENGTH_OF_EARTH_TOTEM = 31633;
}

struct QuestIds
{
    public const uint QUEST_ENDING_THEIR_WORLD = 9759;
}

struct CreatureIds
{
    public const uint NPC_SIRONAS = 17678;
    public const uint NPC_BLOODMYST_TESLA_COIL = 17979;
    public const uint NPC_LEGOSO = 17982;
}

struct Texts
{
    public const uint SAY_SIRONAS_1 = 0;

    public const uint SAY_LEGOSO_1 = 0;
    public const uint SAY_LEGOSO_2 = 1;
    public const uint SAY_LEGOSO_3 = 2;
    public const uint SAY_LEGOSO_4 = 3;
    public const uint SAY_LEGOSO_5 = 4;
    public const uint SAY_LEGOSO_6 = 5;
    public const uint SAY_LEGOSO_7 = 6;
    public const uint SAY_LEGOSO_8 = 7;
    public const uint SAY_LEGOSO_9 = 8;
    public const uint SAY_LEGOSO_10 = 9;
    public const uint SAY_LEGOSO_11 = 10;
    public const uint SAY_LEGOSO_12 = 11;
    public const uint SAY_LEGOSO_13 = 12;
    public const uint SAY_LEGOSO_14 = 13;
    public const uint SAY_LEGOSO_15 = 14;
    public const uint SAY_LEGOSO_16 = 15;
    public const uint SAY_LEGOSO_17 = 16;
    public const uint SAY_LEGOSO_18 = 17;
    public const uint SAY_LEGOSO_19 = 18;
    public const uint SAY_LEGOSO_20 = 19;
    public const uint SAY_LEGOSO_21 = 20;
}

struct GameObjectIds
{
    public const uint GO_DRAENEI_EXPLOSIVES_1 = 182088;
    public const uint GO_DRAENEI_EXPLOSIVES_2 = 182091;
    public const uint GO_FIRE_EXPLOSION = 182071;
}

struct Actions
{
    public const int ACTION_SIRONAS_CHANNEL_START = 1;
    public const int ACTION_SIRONAS_CHANNEL_STOP = 2;

    public const int ACTION_LEGOSO_SIRONAS_KILLED = 1;
}

struct Events
{
    public const uint EVENT_UPPERCUT = 1;
    public const uint EVENT_IMMOLATE = 2;
    public const uint EVENT_CURSE_OF_BLOOD = 3;

    public const uint EVENT_FROST_SHOCK = 1;
    public const uint EVENT_HEALING_SURGE = 2;
    public const uint EVENT_SEARING_TOTEM = 3;
    public const uint EVENT_STRENGTH_OF_EARTH_TOTEM = 4;
}

struct Waypoints
{
    public const uint WP_START = 1;
    public const uint WP_EXPLOSIVES_FIRST_POINT = 21;
    public const uint WP_EXPLOSIVES_FIRST_PLANT = 22;
    public const uint WP_EXPLOSIVES_FIRST_RUNOFF = 23;
    public const uint WP_EXPLOSIVES_FIRST_DETONATE = 24;
    public const uint WP_DEBUG_1 = 25;
    public const uint WP_DEBUG_2 = 26;
    public const uint WP_SIRONAS_HILL = 33;
    public const uint WP_EXPLOSIVES_SECOND_BATTLEROAR = 35;
    public const uint WP_EXPLOSIVES_SECOND_PLANT = 39;
    public const uint WP_EXPLOSIVES_SECOND_DETONATE = 40;
}

struct Phases
{
    public const uint PHASE_NONE = 0;
    public const uint PHASE_CONTINUE = 0; //was -1;
    public const uint PHASE_WP_26 = 1;
    public const uint PHASE_WP_22 = 2;
    public const uint PHASE_PLANT_FIRST_KNEEL = 3;
    public const uint PHASE_PLANT_FIRST_STAND = 4;
    public const uint PHASE_PLANT_FIRST_WORK = 5;
    public const uint PHASE_PLANT_FIRST_FINISH = 6;
    public const uint PHASE_PLANT_FIRST_TIMER_1 = 7;
    public const uint PHASE_PLANT_FIRST_TIMER_2 = 8;
    public const uint PHASE_PLANT_FIRST_TIMER_3 = 9;
    public const uint PHASE_PLANT_FIRST_DETONATE = 10;
    public const uint PHASE_PLANT_FIRST_SPEECH = 11;
    public const uint PHASE_PLANT_FIRST_ROTATE = 12;
    public const uint PHASE_PLANT_FIRST_POINT = 13;
    public const uint PHASE_FEEL_SIRONAS_1 = 14;
    public const uint PHASE_FEEL_SIRONAS_2 = 15;
    public const uint PHASE_MEET_SIRONAS_ROAR = 16;
    public const uint PHASE_MEET_SIRONAS_TURN = 17;
    public const uint PHASE_MEET_SIRONAS_SPEECH = 18;
    public const uint PHASE_PLANT_SECOND_KNEEL = 19;
    public const uint PHASE_PLANT_SECOND_SPEECH = 20;
    public const uint PHASE_PLANT_SECOND_STAND = 21;
    public const uint PHASE_PLANT_SECOND_FINISH = 22;
    public const uint PHASE_PLANT_SECOND_WAIT = 23;
    public const uint PHASE_PLANT_SECOND_TIMER_1 = 24;
    public const uint PHASE_PLANT_SECOND_TIMER_2 = 25;
    public const uint PHASE_PLANT_SECOND_TIMER_3 = 26;
    public const uint PHASE_PLANT_SECOND_DETONATE = 27;
    public const uint PHASE_FIGHT_SIRONAS_STOP = 28;
    public const uint PHASE_FIGHT_SIRONAS_SPEECH_1 = 29;
    public const uint PHASE_FIGHT_SIRONAS_SPEECH_2 = 30;
    public const uint PHASE_FIGHT_SIRONAS_START = 31;
    public const uint PHASE_SIRONAS_SLAIN_SPEECH_1 = 32;
    public const uint PHASE_SIRONAS_SLAIN_EMOTE_1 = 33;
    public const uint PHASE_SIRONAS_SLAIN_EMOTE_2 = 34;
    public const uint PHASE_SIRONAS_SLAIN_SPEECH_2 = 35;
}

struct Misc
{
    public const uint DATA_EVENT_STARTER_GUID = 0;

    public const uint MAX_EXPLOSIVES = 5;
}

struct Positions
{
    public static Position[][] ExplosivesPos =
    {
            new Position[]
            {
                new Position(-1954.946f, -10654.714f, 110.448f),
                new Position( -1956.331f, -10654.494f, 110.869f),
                new Position( -1955.906f, -10656.221f, 110.791f),
                new Position(-1957.294f, -10656.000f, 111.219f),
                new Position(-1954.462f, -10656.451f, 110.404f)
            },
            new Position[]
            {
                new Position( -1915.137f, -10583.651f, 178.365f),
                new Position( -1914.006f, -10582.964f, 178.471f),
                new Position(-1912.717f, -10582.398f, 178.658f),
                new Position(-1915.056f, -10582.251f, 178.162f),
                new Position(-1913.883f, -10581.778f, 178.346f)
            }
        };
}

[Script]
class npc_sironas : ScriptedAI
{
    public npc_sironas(Creature creature) : base(creature) { }
    public override void Reset()
    {
        _events.Reset();
        me.SetDisplayFromModel(1);
    }

    public override void JustEngagedWith(Unit who)
    {
        _events.ScheduleEvent(Events.EVENT_UPPERCUT, TimeSpan.FromSeconds(15));
        _events.ScheduleEvent(Events.EVENT_IMMOLATE, TimeSpan.FromSeconds(10));
        _events.ScheduleEvent(Events.EVENT_CURSE_OF_BLOOD, TimeSpan.FromSeconds(5));
    }

    public override void JustDied(Unit killer)
    {
        me.SetObjectScale(1.0f);
        _events.Reset();

        if (!killer)
            return;

        Creature legoso = me.FindNearestCreature(CreatureIds.NPC_LEGOSO, 533.3333f); //SIZE_OF_GRIDS which is 533.3333f
        if (legoso != null)
        {
            var players = me.GetMap().GetPlayers();
            Group group = players[0].GetGroup();

            if (killer.GetGUID() == legoso.GetGUID() ||
                (group && group.IsMember(killer.GetGUID())) ||
                killer.GetGUID() == legoso.GetAI().GetGUID((int)Misc.DATA_EVENT_STARTER_GUID))
                legoso.GetAI().DoAction(Actions.ACTION_LEGOSO_SIRONAS_KILLED);
        }
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        _events.Update(diff);

        uint eventId = _events.ExecuteEvent();

        while (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_UPPERCUT:
                    DoCastVictim(SpellIds.SPELL_UPPERCUT);
                    _events.ScheduleEvent(Events.EVENT_UPPERCUT, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(12));
                    break;
                case Events.EVENT_IMMOLATE:
                    DoCastVictim(SpellIds.SPELL_IMMOLATE);
                    _events.ScheduleEvent(Events.EVENT_IMMOLATE, TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(20));
                    break;
                case Events.EVENT_CURSE_OF_BLOOD:
                    DoCastVictim(SpellIds.SPELL_CURSE_OF_BLOOD);
                    _events.ScheduleEvent(Events.EVENT_CURSE_OF_BLOOD, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(25));
                    break;
                default:
                    break;
            }
        }

        DoMeleeAttackIfReady();
    }

    public override void DoAction(int action)
    {
        switch (action)
        {
            case Actions.ACTION_SIRONAS_CHANNEL_START:
                {
                    DoCast(me, SpellIds.SPELL_SIRONAS_CHANNELING);
                    List<Creature> BeamList = me.GetCreatureListWithEntryInGrid(CreatureIds.NPC_BLOODMYST_TESLA_COIL, 5333.3333f);
                    if (!BeamList.Empty())
                        foreach (Creature creature in BeamList)
                            creature.CastSpell(creature, SpellIds.SPELL_BLOODMYST_TESLA);
                    break;
                }
            case Actions.ACTION_SIRONAS_CHANNEL_STOP:
                {
                    me.InterruptNonMeleeSpells(true, SpellIds.SPELL_SIRONAS_CHANNELING);
                    List<Creature> creatureList = me.GetCreatureListWithEntryInGrid(CreatureIds.NPC_BLOODMYST_TESLA_COIL, 500.0f);
                    if (!creatureList.Empty())
                        foreach (Creature creature in creatureList)
                            creature.InterruptNonMeleeSpells(true, SpellIds.SPELL_BLOODMYST_TESLA);
                    break;
                }
            default:
                break;
        }
    }
}

[Script]
class npc_demolitionist_legoso : EscortAI
{
    public npc_demolitionist_legoso(Creature creature) : base(creature)
    {
        Initialize();
    }

    byte _phase;
    uint _moveTimer;
    ObjectGuid _eventStarterGuid;
    List<ObjectGuid> _explosivesGuids = new();

    void Initialize()
    {
        _phase = (byte)Phases.PHASE_NONE;
        _moveTimer = 0;
    }

    public override void OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIds.QUEST_ENDING_THEIR_WORLD)
        {
            SetGUID(player.GetGUID(), (int)Misc.DATA_EVENT_STARTER_GUID);
            Start(true, player.GetGUID(), quest);
        }
    }

    public override ObjectGuid GetGUID(int type = 0)
    {
        if (type == Misc.DATA_EVENT_STARTER_GUID)
            return _eventStarterGuid;

        return ObjectGuid.Empty;
    }

    public override void SetGUID(ObjectGuid guid, int type = 0)
    {
        switch (type)
        {
            case (int)Misc.DATA_EVENT_STARTER_GUID:
                _eventStarterGuid = guid;
                break;
            default:
                break;
        }
    }

    public override void Reset()
    {
        me.SetCanDualWield(true);
        Initialize();
        _events.Reset();
        _events.ScheduleEvent(Events.EVENT_FROST_SHOCK, TimeSpan.FromSeconds(1));
        _events.ScheduleEvent(Events.EVENT_HEALING_SURGE, TimeSpan.FromSeconds(5));
        _events.ScheduleEvent(Events.EVENT_SEARING_TOTEM, TimeSpan.FromSeconds(15));
        _events.ScheduleEvent(Events.EVENT_STRENGTH_OF_EARTH_TOTEM, TimeSpan.FromSeconds(20));
    }

    public override void UpdateAI(uint diff)
    {
        _events.Update(diff);

        if (UpdateVictim())
        {
            uint eventId = _events.ExecuteEvent();
            while (eventId != 0)
            {
                switch (eventId)
                {
                    case Events.EVENT_FROST_SHOCK:
                        DoCastVictim(SpellIds.SPELL_FROST_SHOCK);
                        _events.DelayEvents(TimeSpan.FromSeconds(1), 1);
                        _events.ScheduleEvent(Events.EVENT_FROST_SHOCK, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15));
                        break;
                    case Events.EVENT_SEARING_TOTEM:
                        DoCast(me, SpellIds.SPELL_SEARING_TOTEM);
                        _events.DelayEvents(TimeSpan.FromMilliseconds(1), 1);
                        _events.ScheduleEvent(Events.EVENT_SEARING_TOTEM, TimeSpan.FromMilliseconds(RandomHelper.URand(110, 130)));
                        break;
                    case Events.EVENT_STRENGTH_OF_EARTH_TOTEM:
                        DoCast(me, SpellIds.SPELL_STRENGTH_OF_EARTH_TOTEM);
                        _events.DelayEvents(TimeSpan.FromMilliseconds(1), 1);
                        _events.ScheduleEvent(Events.EVENT_STRENGTH_OF_EARTH_TOTEM, TimeSpan.FromMilliseconds(RandomHelper.URand(110, 130)));
                        break;
                    case Events.EVENT_HEALING_SURGE:
                        {
                            Player player = GetPlayerForEscort();
                            Unit target = null;
                            if (me.GetHealthPct() < 85)
                                target = me;
                            else if (player != null)
                                if (player.GetHealthPct() < 85)
                                    target = player;
                            if (target)
                            {
                                DoCast(target, SpellIds.SPELL_HEALING_SURGE);
                                _events.ScheduleEvent(Events.EVENT_HEALING_SURGE, TimeSpan.FromSeconds(10));
                            }
                            else
                                _events.ScheduleEvent(Events.EVENT_HEALING_SURGE, TimeSpan.FromSeconds(2));
                            break;
                        }
                    default:
                        break;
                }
            }

            DoMeleeAttackIfReady();
        }

        if (HasEscortState(EscortState.None))
            return;

        UpdateAI(diff);

        if (_phase != 0)
        {
            if (_moveTimer <= diff)
            {
                switch (_phase)
                {
                    case (byte)Phases.PHASE_WP_26: //debug skip path to point 26, buggy path calculation
                        me.GetMotionMaster().MovePoint(Waypoints.WP_DEBUG_2, -2021.77f, -10648.8f, 129.903f, false);
                        _moveTimer = 2 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_CONTINUE;
                        break;
                    case (byte)Phases.PHASE_CONTINUE: // continue escort
                        SetEscortPaused(false);
                        _moveTimer = 0 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_NONE;
                        break;
                    case (byte)Phases.PHASE_WP_22: //debug skip path to point 22, buggy path calculation
                        me.GetMotionMaster().MovePoint(Waypoints.WP_EXPLOSIVES_FIRST_PLANT, -1958.026f, -10660.465f, 111.547f, false);
                        Talk(Texts.SAY_LEGOSO_3);
                        _moveTimer = 2 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_KNEEL;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_KNEEL: // plant first explosives stage 1 kneel
                        me.SetStandState(UnitStandStateType.Kneel);
                        _moveTimer = 10 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_STAND;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_STAND: // plant first explosives stage 1 stand
                        me.SetStandState(UnitStandStateType.Stand);
                        _moveTimer = (uint)(0.5 * Time.InMilliseconds);
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_WORK;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_WORK: // plant first explosives stage 2 work
                        Talk(Texts.SAY_LEGOSO_4);
                        _moveTimer = (uint)(17.5 * Time.InMilliseconds);
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_FINISH;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_FINISH: // plant first explosives finish
                        _explosivesGuids.Clear();
                        for (byte i = 0; i != Misc.MAX_EXPLOSIVES; ++i)
                        {
                            GameObject explosive = me.SummonGameObject(GameObjectIds.GO_DRAENEI_EXPLOSIVES_1, Positions.ExplosivesPos[0][i], Quaternion.CreateFromRotationMatrix(Extensions.fromEulerAnglesZYX(Positions.ExplosivesPos[0][i].GetOrientation(), 0.0f, 0.0f)), TimeSpan.FromSeconds(0));
                            if (explosive != null)
                                _explosivesGuids.Add(explosive.GetGUID());
                        }
                        me.HandleEmoteCommand(Emote.OneshotNone); // reset anim state
                                                                  // force runoff movement so he will not screw up next waypoint
                        me.GetMotionMaster().MovePoint(Waypoints.WP_EXPLOSIVES_FIRST_RUNOFF, -1955.6f, -10669.8f, 110.65f, false);
                        Talk(Texts.SAY_LEGOSO_5);
                        _moveTimer = (uint)(1.5 * Time.InMilliseconds);
                        _phase = (byte)Phases.PHASE_CONTINUE;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_TIMER_1: // first explosives detonate timer 1
                        Talk(Texts.SAY_LEGOSO_6);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_TIMER_2;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_TIMER_2: // first explosives detonate timer 2
                        Talk(Texts.SAY_LEGOSO_7);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_TIMER_3;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_TIMER_3: // first explosives detonate timer 3
                        Talk(Texts.SAY_LEGOSO_8);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_DETONATE;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_DETONATE: // first explosives detonate finish
                        foreach (var explosivesGuid in _explosivesGuids)
                        {
                            GameObject explosive = ObjectAccessor.GetGameObject(me, explosivesGuid);
                            if (explosive != null)
                                me.RemoveGameObject(explosive, true);
                        }
                        _explosivesGuids.Clear();
                        me.HandleEmoteCommand(Emote.OneshotCheer);
                        _moveTimer = 2 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_SPEECH;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_SPEECH: // after detonation 1 speech
                        Talk(Texts.SAY_LEGOSO_9);
                        _moveTimer = 4 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_ROTATE;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_ROTATE: // after detonation 1 rotate to next point
                        me.SetFacingTo(2.272f);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_FIRST_POINT;
                        break;
                    case (byte)Phases.PHASE_PLANT_FIRST_POINT: // after detonation 1 send point anim and go on to next point
                        me.HandleEmoteCommand(Emote.OneshotPoint);
                        _moveTimer = 2 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_CONTINUE;
                        break;
                    case (byte)Phases.PHASE_FEEL_SIRONAS_1: // legoso exclamation before sironas 1.1
                        Talk(Texts.SAY_LEGOSO_10);
                        _moveTimer = 4 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_FEEL_SIRONAS_2;
                        break;
                    case (byte)Phases.PHASE_FEEL_SIRONAS_2: // legoso exclamation before sironas 1.2
                        Talk(Texts.SAY_LEGOSO_11);
                        _moveTimer = 4 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_CONTINUE;
                        break;
                    case (byte)Phases.PHASE_MEET_SIRONAS_ROAR: // legoso exclamation before sironas 2.1
                        Talk(Texts.SAY_LEGOSO_12);
                        _moveTimer = 4 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_MEET_SIRONAS_TURN;
                        break;
                    case (byte)Phases.PHASE_MEET_SIRONAS_TURN: // legoso exclamation before sironas 2.2
                        Player player = GetPlayerForEscort();
                        if (player != null)
                            me.SetFacingToObject(player);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_MEET_SIRONAS_SPEECH;
                        break;
                    case (byte)Phases.PHASE_MEET_SIRONAS_SPEECH: // legoso exclamation before sironas 2.3
                        Talk(Texts.SAY_LEGOSO_13);
                        _moveTimer = 7 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_CONTINUE;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_KNEEL: // plant second explosives stage 1 kneel
                        me.SetStandState(UnitStandStateType.Kneel);
                        _moveTimer = 11 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_SECOND_SPEECH;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_SPEECH: // plant second explosives stage 2 kneel
                        Talk(Texts.SAY_LEGOSO_14);
                        _moveTimer = 13 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_SECOND_STAND;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_STAND: // plant second explosives finish
                        me.SetStandState(UnitStandStateType.Stand);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_SECOND_FINISH;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_FINISH: // plant second explosives finish - create explosives
                        _explosivesGuids.Clear();
                        for (byte i = 0; i != Misc.MAX_EXPLOSIVES; ++i)
                        {
                            GameObject explosive = me.SummonGameObject(GameObjectIds.GO_DRAENEI_EXPLOSIVES_2, Positions.ExplosivesPos[1][i], Quaternion.CreateFromRotationMatrix(Extensions.fromEulerAnglesZYX(Positions.ExplosivesPos[1][i].GetOrientation(), 0.0f, 0.0f)), TimeSpan.FromSeconds(0));
                            if (explosive != null)
                                _explosivesGuids.Add(explosive.GetGUID());
                        }
                        Talk(Texts.SAY_LEGOSO_15);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_SECOND_WAIT;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_WAIT: // plant second explosives finish - proceed to next point
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_CONTINUE;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_TIMER_1: // second explosives detonate timer 1
                        Talk(Texts.SAY_LEGOSO_16);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_SECOND_TIMER_2;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_TIMER_2: // second explosives detonate timer 2
                        Talk(Texts.SAY_LEGOSO_17);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_SECOND_TIMER_3;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_TIMER_3: // second explosives detonate timer 3
                        Talk(Texts.SAY_LEGOSO_18);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_PLANT_SECOND_DETONATE;
                        break;
                    case (byte)Phases.PHASE_PLANT_SECOND_DETONATE: // second explosives detonate finish
                        foreach (var explosivesGuids in _explosivesGuids)
                        {
                            GameObject explosive = ObjectAccessor.GetGameObject(me, explosivesGuids);
                            if (explosive != null)
                                me.RemoveGameObject(explosive, true);
                        }
                        _explosivesGuids.Clear();
                        Creature sironas = me.FindNearestCreature(CreatureIds.NPC_SIRONAS, 533.3333f);
                        if (sironas != null)
                        {
                            sironas.SetImmuneToAll(false);
                            me.SetFacingToObject(sironas);
                        }
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_FIGHT_SIRONAS_STOP;
                        break;
                    case (byte)Phases.PHASE_FIGHT_SIRONAS_STOP: // sironas channel stop
                        Creature sironas2 = me.FindNearestCreature(CreatureIds.NPC_SIRONAS, 533.3333f);
                        if (sironas2 != null)
                            sironas2.GetAI().DoAction(Actions.ACTION_SIRONAS_CHANNEL_STOP);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_FIGHT_SIRONAS_SPEECH_1;
                        break;
                    case (byte)Phases.PHASE_FIGHT_SIRONAS_SPEECH_1: // sironas exclamation before aggro
                        Creature sironas3 = me.FindNearestCreature(CreatureIds.NPC_SIRONAS, 533.333f);
                        if (sironas3 != null)
                            sironas3.GetAI().Talk(Texts.SAY_SIRONAS_1);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_FIGHT_SIRONAS_SPEECH_2;
                        break;
                    case (byte)Phases.PHASE_FIGHT_SIRONAS_SPEECH_2: // legoso exclamation before aggro
                        Creature sironas4 = me.FindNearestCreature(CreatureIds.NPC_SIRONAS, 533.3333f);
                        if (sironas4 != null)
                            sironas4.SetObjectScale(3.0f);
                        Talk(Texts.SAY_LEGOSO_19);
                        _moveTimer = 1 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_FIGHT_SIRONAS_START;
                        break;
                    case (byte)Phases.PHASE_FIGHT_SIRONAS_START: // legoso exclamation at aggro
                        Creature sironas5 = me.FindNearestCreature(CreatureIds.NPC_SIRONAS, 533.3333f);
                        if (sironas5 != null)
                        {
                            Unit target = GetPlayerForEscort();
                            if (!target)
                                target = me;

                            AddThreat(sironas5, 0.001f, target);
                            sironas5.Attack(target, true);
                            sironas5.GetMotionMaster().MoveChase(target);
                        }
                        _moveTimer = 10 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_CONTINUE;
                        break;
                    case (byte)Phases.PHASE_SIRONAS_SLAIN_SPEECH_1: // legoso exclamation after battle - stage 1.1
                        Talk(Texts.SAY_LEGOSO_20);
                        _moveTimer = 2 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_SIRONAS_SLAIN_EMOTE_1;
                        break;
                    case (byte)Phases.PHASE_SIRONAS_SLAIN_EMOTE_1: // legoso exclamation after battle - stage 1.2
                        me.HandleEmoteCommand(Emote.OneshotExclamation);
                        _moveTimer = 2 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_SIRONAS_SLAIN_EMOTE_2;
                        break;
                    case (byte)Phases.PHASE_SIRONAS_SLAIN_EMOTE_2: // legoso exclamation after battle - stage 1.3
                        Player player2 = GetPlayerForEscort();
                        if (player2 != null)
                            player2.GroupEventHappens(QuestIds.QUEST_ENDING_THEIR_WORLD, me);
                        me.HandleEmoteCommand(Emote.OneshotCheer);
                        _moveTimer = 5 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_SIRONAS_SLAIN_SPEECH_2;
                        break;
                    case (byte)Phases.PHASE_SIRONAS_SLAIN_SPEECH_2: // legoso exclamation after battle - stage 2
                        Talk(Texts.SAY_LEGOSO_21);
                        _moveTimer = 30 * Time.InMilliseconds;
                        _phase = (byte)Phases.PHASE_CONTINUE;
                        break;
                    default:
                        break;
                }
            }
            else if (!me.IsInCombat())
                _moveTimer -= diff;
        }
    }

    public override void DoAction(int action)
    {
        switch (action)
        {
            case Actions.ACTION_LEGOSO_SIRONAS_KILLED:
                _phase = (byte)Phases.PHASE_SIRONAS_SLAIN_SPEECH_1;
                _moveTimer = 5 * Time.InMilliseconds;
                break;
            default:
                break;
        }
    }
}