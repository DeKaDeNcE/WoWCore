// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.DataStorage;
using Game.Entities;
using Game.Scripting;
using static Game.Scripting.SpellScript;
using System.Collections.Generic;
using System;
using Game;

namespace Scripts.Maps.Azeroth.EasternKingdoms.Duskwood;

struct QuestIDS
{
    public const uint QUEST_NIGHTMARES_CORRUPTION = 8735;
    public const uint QUEST_CRY_FOR_THE_MOON = 26760;
    public const uint QUEST_MISTMANTLES_REVENGE = 26674;
    public const uint QUEST_EMBALMERS_REVENGE = 26727;
}

struct SpellIDS
{
    public const uint SPELL_SOUL_CORRUPTION = 25805;
    public const uint SPELL_CREATURE_OF_NIGHTMARE = 25806;
    public const uint SPELL_LEVEL_UP = 24312;
    public const uint SPELL_WORGEN_TRANSFORMATION = 81908;
    public const uint SPELL_SACRED_CLEANSING = 82130;
}

struct Events
{
    //boss_twilight_corrupter
    public const uint EVENT_SOUL_CORRUPTION = 1;
    public const uint EVENT_CREATURE_OF_NIGHTMARE = 2;

    //npc_stalvan
    public const uint EVENT_STALVAN_STEP_1 = 1;
    public const uint EVENT_STALVAN_STEP_2 = 2;
    public const uint EVENT_STALVAN_STEP_3 = 3;
    public const uint EVENT_STALVAN_STEP_4 = 4;
    public const uint EVENT_STALVAN_STEP_5 = 5;
    public const uint EVENT_STALVAN_STEP_6 = 6;

    public const uint EVENT_TOBIAS_STEP_1 = 7;
    public const uint EVENT_TOBIAS_STEP_2 = 8;
    public const uint EVENT_TOBIAS_STEP_3 = 9;
    public const uint EVENT_TOBIAS_STEP_4 = 10;

    public const uint EVENT_SELECT_TARGET = 1;
}

struct DisplayID
{
    public const uint DISPLAYID_WORGEN_TOBIAS = 33508;
}

struct Texts
{
    //boss_twilight_corrupter
    public const uint YELL_TWILIGHT_CORRUPTOR_RESPAWN = 0;
    public const uint YELL_TWILIGHT_CORRUPTOR_AGGRO = 1;
    public const uint YELL_TWILIGHT_CORRUPTOR_KILL = 2;

    //npc_stalvan
    public const uint SAY_00 = 0;
    public const uint SAY_01 = 1;
    public const uint SAY_02 = 2;
    public const uint SAY_03 = 3;
    public const uint SAY_04 = 4;
    public const uint SAY_05 = 5;
    public const uint SAY_06 = 6;
}

struct SpawnPos
{
    public static Position[] TwillightCorrupter =
    {
            new Position(-10328.16f, -489.57f, 49.95f, 0.0f)
        };

    public static Position[] stalvanPosition =
    {
            new Position(-10371.72f, -1251.92f, 35.99339f)
        };

    public static Position[] stalvanDestination =
    {
            new Position(-10369.932617f, -1253.7677f, 35.909294f)
        };
}

struct Tobias
{
    public static Position[] Pos =
    {
            new Position( -10351.5f, -1256.7f, 34.8566f ),
            new Position( -10357.5f, -1256.8f, 35.3863f ),
            new Position( -10363.5f, -1257.0f, 35.9107f ),
            new Position( -10365.8f, -1255.7f, 35.9098f )
        };
}

//public const float stalvanOrientation = 5.532694f;

struct CreatureIDS
{
    public const uint NPC_TWILIGHT_CORRUPTER = 15625;
    public const uint NPC_STALVAN = 315;
    public const uint NPC_TOBIAS = 43453;
    public const uint NPC_WORGEN_TOBIAS = 43797;
    public const uint NPC_FORLORN_SPIRIT = 43923;
    public const uint NPC_FORLORN_SPIRIT_KILLCREDIT = 43930;
    public const uint NPC_WEAKENED_MORBENT_FEL = 43762;
    public const uint NPC_MORBENT_FEL = 43761;
    public const uint NPC_STITCHES = 43862;
}

//Scripting time..
//boss_twilight_corrupter
[Script]
class boss_twilight_corrupter : ScriptedAI
{
    uint KillCount;

    public boss_twilight_corrupter(Creature creature) : base(creature)
    {
        Initialize();
    }

    void Initialize()
    {
        KillCount = 0;
    }

    public override void Reset()
    {
        _events.Reset();
        Initialize();
    }

    public override void JustEnteredCombat(Unit who)
    {
        Talk(Texts.YELL_TWILIGHT_CORRUPTOR_AGGRO);
        _events.ScheduleEvent(Events.EVENT_SOUL_CORRUPTION, TimeSpan.FromMilliseconds(15000));
        _events.ScheduleEvent(Events.EVENT_CREATURE_OF_NIGHTMARE, TimeSpan.FromMilliseconds(30000));
    }

    public override void KilledUnit(Unit victim)
    {
        if (victim.GetTypeId() == TypeId.Player)
        {
            ++KillCount;
            Talk(Texts.YELL_TWILIGHT_CORRUPTOR_KILL, victim);

            if (KillCount == 3)
            {
                DoCast(me, SpellIDS.SPELL_LEVEL_UP);
                KillCount = 0;
            }
        }
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        _events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        uint eventId = _events.ExecuteEvent();
        while (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_SOUL_CORRUPTION:
                    DoCastAOE(SpellIDS.SPELL_SOUL_CORRUPTION);
                    _events.ScheduleEvent(Events.EVENT_SOUL_CORRUPTION, TimeSpan.FromMilliseconds(RandomHelper.URand(15000, 19000)));
                    break;
                case Events.EVENT_CREATURE_OF_NIGHTMARE:
                    Unit target = SelectTarget(SelectTargetMethod.Random, 0, 0.0f, true);
                    if (target != null)
                        DoCast(target, SpellIDS.SPELL_CREATURE_OF_NIGHTMARE);
                    _events.ScheduleEvent(Events.EVENT_CREATURE_OF_NIGHTMARE, TimeSpan.FromMilliseconds(45000));
                    break;
                default:
                    break;
            }
        }
        DoMeleeAttackIfReady();
    }
}

//at_twilight_grove
[Script]
class at_twilight_grove : AreaTriggerScript
{
    public at_twilight_grove() : base("at_twilight_grove") { }

    public override bool OnTrigger(Player player, AreaTriggerRecord trigger)
    {
        if (trigger != null)
            return true;

        if (player.GetQuestStatus(QuestIDS.QUEST_NIGHTMARES_CORRUPTION) == QuestStatus.Incomplete)
            if (!player.FindNearestCreature(CreatureIDS.NPC_TWILIGHT_CORRUPTER, 500.0f, true))
            {
                Creature corrupter = player.SummonCreature(CreatureIDS.NPC_TWILIGHT_CORRUPTER, SpawnPos.TwillightCorrupter[0], TempSummonType.ManualDespawn, TimeSpan.FromMilliseconds(60000));
                if (corrupter != null)
                    corrupter.GetAI().Talk(Texts.YELL_TWILIGHT_CORRUPTOR_RESPAWN, player);
            }
        return false;
    }
}

[Script]
class npc_stalvan : ScriptedAI
{
    public npc_stalvan(Creature creature) : base(creature) { }

    public override void Reset()
    {
        _events.Reset();
        _events.ScheduleEvent(Events.EVENT_STALVAN_STEP_1, TimeSpan.FromMilliseconds(3000));
        _events.ScheduleEvent(Events.EVENT_STALVAN_STEP_2, TimeSpan.FromMilliseconds(8000));
        _events.ScheduleEvent(Events.EVENT_STALVAN_STEP_3, TimeSpan.FromMilliseconds(15000));
        _events.ScheduleEvent(Events.EVENT_STALVAN_STEP_4, TimeSpan.FromMilliseconds(23000));
        _events.ScheduleEvent(Events.EVENT_STALVAN_STEP_5, TimeSpan.FromMilliseconds(26000));
        _events.ScheduleEvent(Events.EVENT_STALVAN_STEP_6, TimeSpan.FromMilliseconds(32000));

        _events.ScheduleEvent(Events.EVENT_TOBIAS_STEP_1, TimeSpan.FromMilliseconds(5000));
        _events.ScheduleEvent(Events.EVENT_TOBIAS_STEP_2, TimeSpan.FromMilliseconds(9000));
        _events.ScheduleEvent(Events.EVENT_TOBIAS_STEP_3, TimeSpan.FromMilliseconds(16000));
        _events.ScheduleEvent(Events.EVENT_TOBIAS_STEP_4, TimeSpan.FromMilliseconds(27000));
    }

    public override void JustDied(Unit killer)
    {
        Creature tobias = GetTobias();
        if (tobias != null)
        {
            Talk(Texts.SAY_06, tobias);
            tobias.GetAI().Talk(Texts.SAY_04);
            tobias.DespawnOrUnsummon(TimeSpan.FromMilliseconds(4000));
        }
    }

    Creature GetTobias()
    {
        Creature tobias = me.FindNearestCreature(CreatureIDS.NPC_TOBIAS, 30, true);
        if (tobias != null)
            return tobias;

        return me.FindNearestCreature(CreatureIDS.NPC_WORGEN_TOBIAS, 30, true);
    }

    public override void UpdateAI(uint diff)
    {
        _events.Update(diff);

        uint eventId = _events.ExecuteEvent();
        while (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_STALVAN_STEP_1:
                    me.SetWalk(true);
                    me.SetSpeed(UnitMoveType.Walk, 2);
                    me.GetMotionMaster().MovePoint(0, SpawnPos.stalvanDestination[0], true);
                    Talk(Texts.SAY_00, GetTobias());
                    break;
                case Events.EVENT_STALVAN_STEP_2:
                    Talk(Texts.SAY_01, GetTobias());
                    break;

                case Events.EVENT_STALVAN_STEP_3:
                    Talk(Texts.SAY_02, GetTobias());
                    break;

                case Events.EVENT_STALVAN_STEP_4:
                    Talk(Texts.SAY_03, GetTobias());
                    break;

                case Events.EVENT_STALVAN_STEP_5:
                    Talk(Texts.SAY_04, GetTobias());
                    break;

                case Events.EVENT_STALVAN_STEP_6:
                    Talk(Texts.SAY_05, GetTobias());
                    break;
                case Events.EVENT_TOBIAS_STEP_1:
                    Creature tobias = GetTobias();
                    if (tobias != null)
                    {
                        tobias.SetFacingToObject(me, true);
                        me.SetFacingToObject(tobias, true);
                        tobias.GetAI().Talk(Texts.SAY_00, me);
                    }
                    break;
                case Events.EVENT_TOBIAS_STEP_2:
                    Creature tobias2 = GetTobias();
                    if (tobias2 != null)
                        tobias2.GetAI().Talk(Texts.SAY_01, me);
                    break;
                case Events.EVENT_TOBIAS_STEP_3:
                    Creature tobias3 = GetTobias();
                    if (tobias3 != null)
                        tobias3.GetAI().Talk(Texts.SAY_02, me);
                    break;
                case Events.EVENT_TOBIAS_STEP_4:
                    Creature tobias4 = GetTobias();
                    if (tobias4 != null)
                    {
                        tobias4.CastSpell(tobias4, SpellIDS.SPELL_WORGEN_TRANSFORMATION, true);
                        tobias4.SetReactState(ReactStates.Aggressive);

                        me.RemoveUnitFlag(UnitFlags.NonAttackable | UnitFlags.ImmuneToPc);
                        me.GetThreatManager().AddThreat(tobias4, 10);
                        tobias4.GetThreatManager().AddThreat(me, 10);
                        tobias4.SetInCombatWith(me);

                        if (me.Attack(tobias4, true))
                            me.GetMotionMaster().MoveChase(tobias4);

                        if (tobias4.Attack(me, true))
                            tobias4.GetMotionMaster().MoveChase(me);

                        me.SetReactState(ReactStates.Aggressive);

                        tobias4.GetAI().Talk(Texts.SAY_03, tobias4.GetOwner());
                    }
                    break;
                default:
                    break;
            }
        }
        DoMeleeAttackIfReady();
    }
}

[Script]
class npc_soothing_incense_cloud : ScriptedAI
{
    public npc_soothing_incense_cloud(Creature creature) : base(creature) { }

    public override void Reset()
    {
        _events.Reset();
        _events.ScheduleEvent(Events.EVENT_SELECT_TARGET, TimeSpan.FromMilliseconds(500));
    }

    Player GetOwner()
    {
        return me.ToTempSummon().GetSummoner().ToPlayer();
    }

    List<Creature> _selectedTargets = new();
    void SelectTargets()
    {
        _selectedTargets = me.GetCreatureListWithEntryInGrid(CreatureIDS.NPC_FORLORN_SPIRIT, 5.0f);
    }

    void KillSelectedCreaturesAndRewardPlayer()
    {
        foreach (var creature in _selectedTargets)
        {
            if (!creature.IsAlive())
                continue;

            GetOwner().RewardPlayerAndGroupAtEvent(CreatureIDS.NPC_FORLORN_SPIRIT_KILLCREDIT, GetOwner());
            creature.DisappearAndDie();
        }
    }

    public override void UpdateAI(uint diff)
    {
        _events.Update(diff);

        uint eventId = _events.ExecuteEvent();
        while (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_SELECT_TARGET:
                    SelectTargets();
                    KillSelectedCreaturesAndRewardPlayer();
                    break;
            }
        }
    }
}

[Script]
class spell_sacred_cleansing : SpellScript
{
    public void SelectTarget(ref WorldObject target)
    {
        target = GetCaster().FindNearestCreature(CreatureIDS.NPC_MORBENT_FEL, 15.0f, true);
    }

    SpellCastResult CheckRequirement()
    {
        if (GetCaster().FindNearestCreature(CreatureIDS.NPC_MORBENT_FEL, 15.0f, true))
            return SpellCastResult.SpellCastOk;

        return SpellCastResult.IncorrectArea;
    }

    void HandleDummy(uint effindex)
    {
        Unit hitUnit = GetHitUnit();
        if (!hitUnit || !GetCaster().IsPlayer())
            return;

        Creature target = hitUnit.ToCreature();
        if (target != null)
        {
            if (target.GetEntry() == CreatureIDS.NPC_MORBENT_FEL)
            {
                GetCaster().SummonCreature(CreatureIDS.NPC_WEAKENED_MORBENT_FEL, target.GetPosition());
                target.DespawnOrUnsummon();
            }
        }
    }

    public override void Register()
    {
        OnObjectTargetSelect.Add(new ObjectTargetSelectHandler(SelectTarget, 0, Targets.UnitNearbyEntry));
        OnEffectHitTarget.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.Dummy));
        OnCheckCast.Add(new CheckCastHandler(CheckRequirement));
    }
}

[Script]
class spell_summon_stalvan : SpellScript
{
    bool IsEventRunning()
    {
        return GetCaster().FindNearestCreature(CreatureIDS.NPC_STALVAN, 20, true) != null;
    }

    SpellCastResult CheckRequirement()
    {
        return SpellCastResult.SpellCastOk;
    }

    void HandleSendEvent(uint effindex)
    {
        if (!GetCaster().IsPlayer())
            return;

        if (GetCaster().ToPlayer().GetQuestStatus(QuestIDS.QUEST_MISTMANTLES_REVENGE) != QuestStatus.Incomplete)
            return;

        if (IsEventRunning())
            return;

        SummonStalvan();
        SummonTobias();
    }

    void SummonStalvan()
    {
        TempSummon stalvan = GetCaster().SummonCreature(CreatureIDS.NPC_STALVAN, SpawnPos.stalvanPosition[0]);
        if (stalvan != null)
        {
            stalvan.SetFacingTo(5.532694f, true);
            stalvan.SetReactState(ReactStates.Passive);
        }
    }

    void SummonTobias()
    {
        TempSummon tobias = GetCaster().SummonCreature(CreatureIDS.NPC_TOBIAS, Tobias.Pos[0]);
        if (tobias != null)
        {
            tobias.RemoveNpcFlag(NPCFlags.QuestGiver | NPCFlags.Gossip);
            tobias.SetWalk(true);
            tobias.SetSpeed(UnitMoveType.Walk, 3);
            tobias.GetMotionMaster().MovePoint(0, Tobias.Pos[3], true);
            tobias.SetReactState(ReactStates.Passive);
        }
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleSendEvent, 0, SpellEffectName.Dummy));
        OnCheckCast.Add(new CheckCastHandler(CheckRequirement));
    }
}

[Script]
class npc_ebenlocke : ScriptedAI
{
    public npc_ebenlocke(Creature creature) : base(creature) { }

    public override void OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIDS.QUEST_EMBALMERS_REVENGE)
        {
            _scheduler.Schedule(TimeSpan.FromSeconds(2), task =>
            {
                me.SummonCreature(CreatureIDS.NPC_STITCHES, -10553.90f, -1171.27f, 27.8604f, 1.48514f, TempSummonType.TimedDespawnOutOfCombat, TimeSpan.FromMilliseconds(90000));
            });
        }
    }
}

[Script]
class npc_oliver_harris : ScriptedAI
{
    public npc_oliver_harris(Creature creature) : base(creature) { }

    public override void OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIDS.QUEST_CRY_FOR_THE_MOON)
            me.SummonCreature(me.GetEntry(), me.GetPosition(), TempSummonType.ManualDespawn);
    }


    public override void IsSummonedBy(WorldObject summoner)
    {
        if (!summoner.IsCreature())
            return;

        ObjectGuid ownerGuid = summoner.GetGUID();
        me.GetOwnerGUID();
        me.SetWalk(true);
        me.GetMotionMaster().MovePoint(1, -10745.0f, 330.0f, 37.87f, true);
    }

    public override void MovementInform(MovementGeneratorType movementType, uint pointId)
    {
        if (movementType != MovementGeneratorType.Point)
            return;

        if (pointId == 1)
        {
            me.SetFacingTo(2.949f, true);
            me.Say("Here we go...", Language.Universal);

            _scheduler.Schedule(TimeSpan.FromSeconds(3), task =>
            {
                me.Say("It's working. Hold him still, Jitters", Language.Universal);
            });
            _scheduler.Schedule(TimeSpan.FromSeconds(10), task =>
            {
                me.GetMotionMaster().MovePoint(2, -10746.0f, 331.0f, 37.88f, true);
            });
        }
        if (pointId == 2)
        {
            Unit owner = me.GetOwner();
            if (owner != null)
            {
                owner.ToPlayer().KilledMonsterCredit(43969);
                me.DespawnOrUnsummon();
            }
        }
    }
}