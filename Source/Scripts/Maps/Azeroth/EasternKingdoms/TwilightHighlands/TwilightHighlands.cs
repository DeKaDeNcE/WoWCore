// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Framework.Dynamic;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game.Spells;
using System.Collections.Generic;
using System;
using Game;

namespace Scripts.Maps.Azeroth.EasternKingdoms.TwilightHighlands;

struct QuestIDS
{
    public const uint QUEST_THE_CRUCIBLE_OF_CARNAGE_THEBLOODEYE_BRUISER = 27863;
    public const uint QUEST_THE_CRUCIBLE_OF_CARNAGE_SPECIAL_H = 27865; // second quest
    public const uint QUEST_THE_CRUCIBLE_OF_CARNAGE_SPECIAL_A = 27864; // second quest
    public const uint QUEST_THE_CRUCIBLE_OF_CARNAGE_CALDERS_CREATION = 27866;
    public const uint QUEST_THE_CRUCIBLE_OF_CARNAGE_THE_EARL_OF_EVIS = 27867;
    public const uint QUEST_THE_CRUCIBLE_OF_CARNAGE_THE_TWILIGHT_TERROR = 27868;
}

struct CreatureIDS
{
    public const uint NPC_HURP_DERP = 46944;
    public const uint NPC_SULLY_KNEECAPPER = 46946; // Horde Quest Boss
    public const uint NPC_TORG_DRAKEFLAYER = 46945; // Ally Quest Boss
    public const uint NPC_CADAVER_COLLAGE = 46947;
    public const uint NPC_LORD_GEOFFERY_TULVAN = 46948;
    public const uint NPC_EMBERSCAR_THE_DEVOURER = 46949;

    // Trash
    public const uint NPC_GLOOMWING = 47476;
    public const uint NPC_GURGTHOCK = 46935;

    public const uint NPC_POISON_CLOUD = 48591;
}

struct SpellIDS
{
    public const uint SPELL_INTIMIDATING_ROAR = 91933;
    public const uint SPELL_OVERHEAD_SMASH = 88482;
    public const uint SPELL_WHIRLWIND = 83016;

    public const uint SPELL_CHARGE = 88288;
    public const uint SPELL_UPPERCUT = 80182;

    public const uint SPELL_BELCH = 88607;
    public const uint SPELL_INHALE = 88615;
    public const uint SPELL_PLAGUE_EXPLOSION = 88614;
    public const uint SPELL_POISON_CLOUD = 90447;
    public const uint SPELL_REPULSIVE_KICK = 88605;
    public const uint SPELL_CADAVER_HOVER = 95868;

    public const uint SPELL_DEATH_BY_PEASANT = 88619;
    public const uint SPELL_UNDYING_FRENZY = 88616;
    public const uint SPELL_GEOF_UCUT = 80182;
    public const uint SPELL_TRANSFORM_VISUAL = 24085;

    public const uint SPELL_ACTIVATE_POOLS = 90409;
    public const uint SPELL_FIREBALL = 90446;
    public const uint SPELL_MAGMATIC_FAULT = 9033;

    public const uint SPELL_GLOOM_BALL = 88515;
}

struct Misc
{
    public const uint DISPLAY_GEOF_WORGEN = 34367;
}

struct Events
{
    public const uint EVENT_INTIMIDATING_ROAR = 1;
    public const uint EVENT_OVERHEAD_SMASH = 2;
    public const uint EVENT_WHIRLWIND = 3;

    public const uint EVENT_S_CHARGE = 1;
    public const uint EVENT_UPPERCUT = 2;

    public const uint EVENT_BELCH = 1;
    public const uint EVENT_INHALE = 2;
    public const uint EVENT_PLAGUE_EXPLOSION = 3;
    public const uint EVENT_POISON_CLOUD = 4;
    public const uint EVENT_REPULSIVE_KICK = 5;
    public const uint EVENT_BELOW5 = 6;
    public const uint EVENT_TAKEOFF = 7;
    public const uint EVENT_EXPLODE = 8;

    public const uint EVENT_DEATH_BY_PEASANT = 1;
    public const uint EVENT_UNDYING_FRENZY = 2;
    public const uint EVENT_GEOF_UCUT = 3;
    public const uint EVENT_TRANSFORM = 4;

    public const uint EVENT_ACTIVATE_POOLS = 1;
    public const uint EVENT_FIREBALL = 2;
    public const uint EVENT_MAG_FAULT = 3;

    public const uint EVENT_GLOOM_BALL = 1;
}

struct Texts
{
    public const uint SAY_QUEST_ACCEPT_BLOODEYE = 0;
}

struct Positions
{
    public static Position[] CenterPos =
    {
            new Position (-4190.416f, -5145.781f, -7.7363f, 2.070863f)
        };

    public static Position[] SpawnPosition =
    {
            new Position (-4182.385f, -5111.955f, -7.7334f, 4.479527f), // general spawnpos
            new Position (-4194.154f, -5139.351f, -7.7364f, 1.865049f), // direct center pos
            new Position (-4108.251f, -5221.142f, -9.5933f, 2.296299f), // outside arena
            new Position (-4187.152f, -5157.176f, 8.42034f, 1.428060f) // gloomwing fly pos
        };
}

[Script]
class npc_gurgthock_cata : ScriptedAI
{
    public npc_gurgthock_cata(Creature creature) : base(creature) { }

    ObjectGuid SummonGUID;
    ObjectGuid uiPlayerGUID;
    uint uiTimer;
    uint uiPhase;
    uint uiRemoveFlagTimer;
    bool bRemoveFlag;

    public override void Reset()
    {
        me.SetNpcFlag(NPCFlags.QuestGiver);
        uiTimer = 0;
        uiPhase = 0;
        uiRemoveFlagTimer = 5000;

        bRemoveFlag = false;
    }

    public override void SetGUID(ObjectGuid guid, int id = 0)
    {
        uiPlayerGUID = guid;
    }

    public override void SetData(uint id, uint value)
    {
        bRemoveFlag = true;
        me.RemoveNpcFlag(NPCFlags.QuestGiver);

        switch (id)
        {
            case 1:
                switch (value)
                {
                    case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THEBLOODEYE_BRUISER:
                        uiPhase = 1;
                        uiTimer = 4000;
                        break;
                    case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_SPECIAL_H:
                    case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_SPECIAL_A:
                        uiPhase = 3;
                        uiTimer = 3000;
                        break;
                    case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_CALDERS_CREATION:
                        uiPhase = 6;
                        uiTimer = 3000;
                        break;
                    case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THE_EARL_OF_EVIS:
                        uiPhase = 8;
                        uiTimer = 3000;
                        break;
                    case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THE_TWILIGHT_TERROR:
                        uiPhase = 10;
                        uiTimer = 3000;
                        break;
                }
                break;
        }
    }

    public override void UpdateAI(uint diff)
    {
        if (bRemoveFlag)
        {
            if (uiRemoveFlagTimer <= diff)
            {
                me.SetNpcFlag(NPCFlags.QuestGiver);
                bRemoveFlag = false;

                uiRemoveFlagTimer = 10000;
            }
            else uiRemoveFlagTimer -= diff;
        }

        if (uiPhase != 0)
        {
            Player player = Global.ObjAccessor.GetPlayer(me, uiPlayerGUID);

            if (uiTimer <= diff)
            {
                switch (uiPhase)
                {
                    case 1:
                        {
                            Creature summon = me.SummonCreature(CreatureIDS.NPC_HURP_DERP, Positions.SpawnPosition[0], TempSummonType.CorpseDespawn, TimeSpan.FromMilliseconds(5000));
                            if (summon != null)
                                SummonGUID = summon.GetGUID();
                            uiPhase = 2;
                            uiTimer = 4000;
                            break;
                        }
                    case 2:
                        {
                            Creature summon = ObjectAccessor.GetCreature(me, SummonGUID);
                            if (summon != null)
                                summon.GetMotionMaster().MovePoint(0, Positions.CenterPos[0]);
                            uiPhase = 0;
                            SummonGUID = ObjectGuid.Empty;
                            break;
                        }
                    case 3:
                        {
                            uiTimer = 3000;
                            uiPhase = 4;
                            break;
                        }
                    case 4:
                        {
                            if (player.GetTeamId() == TeamId.Alliance)
                            {
                                Creature summon = me.SummonCreature(CreatureIDS.NPC_TORG_DRAKEFLAYER, Positions.SpawnPosition[1], TempSummonType.CorpseDespawn, TimeSpan.FromMilliseconds(5000));
                                if (summon != null)
                                    SummonGUID = summon.GetGUID();
                            }
                            else if (player.GetTeamId() == TeamId.Horde)
                            {
                                Creature summon = me.SummonCreature(CreatureIDS.NPC_SULLY_KNEECAPPER, Positions.SpawnPosition[1], TempSummonType.CorpseDespawn, TimeSpan.FromMilliseconds(5000));
                                if (summon != null)
                                    SummonGUID = summon.GetGUID();
                            }
                            uiTimer = 3000;
                            uiPhase = 0;
                            break;
                        }
                    case 6:
                        {
                            Creature summon = me.SummonCreature(CreatureIDS.NPC_CADAVER_COLLAGE, Positions.SpawnPosition[0], TempSummonType.CorpseDespawn, TimeSpan.FromMilliseconds(5000));
                            if (summon != null)
                                SummonGUID = summon.GetGUID();
                            uiPhase = 7;
                            uiTimer = 4000;
                            break;
                        }
                    case 7:
                        {
                            Creature summon = ObjectAccessor.GetCreature(me, SummonGUID);
                            if (summon != null)
                                summon.GetMotionMaster().MovePoint(0, Positions.CenterPos[0]);
                            uiPhase = 0;
                            SummonGUID = ObjectGuid.Empty;
                            break;
                        }
                    case 8:
                        {
                            Creature summon = me.SummonCreature(CreatureIDS.NPC_LORD_GEOFFERY_TULVAN, Positions.SpawnPosition[2], TempSummonType.CorpseDespawn, TimeSpan.FromMilliseconds(5000));
                            if (summon != null)
                                SummonGUID = summon.GetGUID();
                            uiPhase = 9;
                            uiTimer = 4000;
                            break;
                        }
                    case 9:
                        {
                            Creature summon = ObjectAccessor.GetCreature(me, SummonGUID);
                            if (summon != null)
                                summon.GetMotionMaster().MovePoint(0, Positions.CenterPos[0]);
                            uiPhase = 0;
                            SummonGUID = ObjectGuid.Empty;
                            break;
                        }
                    case 10:
                        {
                            Creature summon = me.SummonCreature(CreatureIDS.NPC_EMBERSCAR_THE_DEVOURER, Positions.SpawnPosition[1], TempSummonType.CorpseDespawn, TimeSpan.FromMilliseconds(5000));
                            if (summon != null)
                                SummonGUID = summon.GetGUID();
                            uiPhase = 11;
                            uiTimer = 4000;
                            break;
                        }
                    case 11:
                        {
                            Creature summon = ObjectAccessor.GetCreature(me, SummonGUID);
                            if (summon != null)
                                summon.GetMotionMaster().MovePoint(0, Positions.CenterPos[0]);
                            uiPhase = 0;
                            SummonGUID = ObjectGuid.Empty;
                            break;
                        }
                }
            }
            else uiTimer -= diff;
        }
    }


    public override void OnQuestAccept(Player player, Quest quest)
    {
        Creature creature = player.FindNearestCreature(CreatureIDS.NPC_GURGTHOCK, 10.0f, true);
        switch (quest.Id)
        {
            case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THEBLOODEYE_BRUISER:
                creature.GetAI().SetData(1, quest.Id);
                break;
            case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_SPECIAL_H:
            case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_SPECIAL_A:
                creature.GetAI().SetData(1, quest.Id);
                break;
            case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_CALDERS_CREATION:
                creature.GetAI().SetData(1, quest.Id);
                break;
            case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THE_EARL_OF_EVIS:
                creature.GetAI().SetData(1, quest.Id);
                break;
            case QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THE_TWILIGHT_TERROR:
                creature.GetAI().SetData(1, quest.Id);
                break;
        }

        creature.GetAI().SetGUID(player.GetGUID());
    }
}

[Script]
class npc_hurp_derp : ScriptedAI
{
    public npc_hurp_derp(Creature creature) : base(creature) { }

    EventMap events = new();

    public override void Reset()
    {
        //empty
    }

    public override void EnterEvadeMode(EvadeReason why = EvadeReason.Other)
    {
        me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(3000));
    }

    public override void JustDied(Unit killer)
    {
        Player player = killer.GetCharmerOrOwnerPlayerOrPlayerItself();
        if (player != null)
            player.GroupEventHappens(QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THEBLOODEYE_BRUISER, killer);
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        uint eventId = events.ExecuteEvent();

        if (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_INTIMIDATING_ROAR:
                    DoCastVictim(SpellIDS.SPELL_INTIMIDATING_ROAR);
                    events.ScheduleEvent(Events.EVENT_INTIMIDATING_ROAR, RandomHelper.RandTime(TimeSpan.FromMilliseconds(15), TimeSpan.FromMilliseconds(25)));
                    break;
                case Events.EVENT_OVERHEAD_SMASH:
                    DoCastVictim(SpellIDS.SPELL_OVERHEAD_SMASH);
                    events.ScheduleEvent(Events.EVENT_OVERHEAD_SMASH, RandomHelper.RandTime(TimeSpan.FromMilliseconds(20), TimeSpan.FromMilliseconds(30)));
                    break;
                case Events.EVENT_WHIRLWIND:
                    Unit target = SelectTarget(SelectTargetMethod.MaxThreat, 0);
                    if (target != null)
                    {
                        DoCast(target, SpellIDS.SPELL_WHIRLWIND);
                    }
                    events.ScheduleEvent(Events.EVENT_WHIRLWIND, RandomHelper.RandTime(TimeSpan.FromMilliseconds(15), TimeSpan.FromMilliseconds(25)));
                    break;
            }
        }
        DoMeleeAttackIfReady();
    }
}

[Script]
class npc_sully_kneecapper : ScriptedAI
{
    public npc_sully_kneecapper(Creature creature) : base(creature) { }

    EventMap events = new();

    public override void Reset()
    {
        DespawnCreatures(CreatureIDS.NPC_GLOOMWING);
    }

    public override void EnterEvadeMode(EvadeReason why = EvadeReason.Other)
    {
        me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(3000));
        DespawnCreatures(CreatureIDS.NPC_GLOOMWING);
    }

    void DespawnCreatures(uint entry)
    {
        List<Creature> creatures = new();
        me.GetCreatureListWithEntryInGrid(entry, 1000.0f);
        if (creatures.Empty())
            return;
        foreach (var creature in creatures)
        {
            creature.DespawnOrUnsummon();
        }
        //help me port this..
        //for (std::list<Creature*>::iterator iter = creatures.begin(); iter != creatures.end(); ++iter)
        //    (*iter)->DespawnOrUnsummon();
    }

    public override void JustEnteredCombat(Unit who)
    {
        events.ScheduleEvent(Events.EVENT_S_CHARGE, TimeSpan.FromMilliseconds(10000));
        events.ScheduleEvent(Events.EVENT_UPPERCUT, TimeSpan.FromMilliseconds(5000));

        me.SummonCreature(CreatureIDS.NPC_GLOOMWING, Positions.SpawnPosition[3], TempSummonType.CorpseDespawn, TimeSpan.FromMilliseconds(5000));
    }

    public override void JustDied(Unit killer)
    {
        Player player = killer.GetCharmerOrOwnerPlayerOrPlayerItself();
        if (player != null)
        {
            player.GroupEventHappens(QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_SPECIAL_H, killer);
        }
        DespawnCreatures(CreatureIDS.NPC_GLOOMWING);
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        uint eventId = events.ExecuteEvent();

        if (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_S_CHARGE:
                    DoCastSelf(SpellIDS.SPELL_CHARGE); //was DoCastRandom(SPELL_CHARGE, 150.0f); --> check SLCore
                    events.ScheduleEvent(Events.EVENT_S_CHARGE, RandomHelper.RandTime(TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(20)));
                    break;
                case Events.EVENT_UPPERCUT:
                    DoCastVictim(SpellIDS.SPELL_UPPERCUT);
                    events.ScheduleEvent(Events.EVENT_UPPERCUT, RandomHelper.RandTime(TimeSpan.FromMilliseconds(15), TimeSpan.FromMilliseconds(25)));
                    break;
            }
        }
        DoMeleeAttackIfReady();
    }
}

[Script]
class npc_torg_drakeflayer : ScriptedAI
{
    public npc_torg_drakeflayer(Creature creature) : base(creature) { }

    EventMap events = new();

    void DespawnCreatures(uint entry)
    {
        List<Creature> creatures = new();
        me.GetCreatureListWithEntryInGrid(entry, 1000.0f);
        if (creatures.Empty())
            return;
        foreach (var creature in creatures)
        {
            creature.DespawnOrUnsummon();
        }
        //for (std::list<Creature*>::iterator iter = creatures.begin(); iter != creatures.end(); ++iter)
        //    (*iter)->DespawnOrUnsummon();
    }

    public override void Reset()
    {
        DespawnCreatures(CreatureIDS.NPC_GLOOMWING);
    }

    public override void EnterEvadeMode(EvadeReason why = EvadeReason.Other)
    {
        me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(3000));
        DespawnCreatures(CreatureIDS.NPC_GLOOMWING);
    }

    public override void JustEnteredCombat(Unit who)
    {
        events.ScheduleEvent(Events.EVENT_S_CHARGE, TimeSpan.FromMilliseconds(10000));
        events.ScheduleEvent(Events.EVENT_UPPERCUT, TimeSpan.FromMilliseconds(5000));

        me.SummonCreature(CreatureIDS.NPC_GLOOMWING, Positions.SpawnPosition[3], TempSummonType.CorpseDespawn, TimeSpan.FromMilliseconds(5000));
    }

    public override void JustDied(Unit killer)
    {
        Player player = killer.GetCharmerOrOwnerPlayerOrPlayerItself();
        if (player != null)
        {
            player.GroupEventHappens(QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_SPECIAL_H, killer);
        }
        DespawnCreatures(CreatureIDS.NPC_GLOOMWING);
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        uint eventId = events.ExecuteEvent();
        if (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_S_CHARGE:
                    DoCastSelf(SpellIDS.SPELL_CHARGE); //same as the other.. was -> DoCastRandom(SPELL_CHARGE, 150.0f);
                    events.ScheduleEvent(Events.EVENT_S_CHARGE, RandomHelper.RandTime(TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(20)));
                    break;
                case Events.EVENT_UPPERCUT:
                    DoCastVictim(SpellIDS.SPELL_UPPERCUT);
                    events.ScheduleEvent(Events.EVENT_UPPERCUT, RandomHelper.RandTime(TimeSpan.FromMilliseconds(15), TimeSpan.FromMilliseconds(25)));
                    break;
            }
        }
        DoMeleeAttackIfReady();
    }
}

[Script]
class npc_cadaver_collage : ScriptedAI
{
    public npc_cadaver_collage(Creature creature) : base(creature) { }

    EventMap events = new();
    bool inhaled;

    public override void Reset()
    {
        DespawnTriggers(CreatureIDS.NPC_POISON_CLOUD);
        inhaled = false;
    }

    void DespawnTriggers(uint entry)
    {
        List<Creature> creatures = new();
        me.GetCreatureListWithEntryInGrid(entry, 1000.0f);
        if (creatures.Empty())
            return;
        foreach (var creature in creatures)
        {
            creature.DespawnOrUnsummon();
        }
    }

    public override void DamageTaken(Unit attacker, ref uint damage, DamageEffectType damageType, SpellInfo spellInfo = null)
    {
        if (!inhaled && !me.IsNonMeleeSpellCast(false) && HealthBelowPct(20))
        {
            Player player = attacker.GetCharmerOrOwnerPlayerOrPlayerItself();
            if (player != null)
            {
                player.GroupEventHappens(QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_CALDERS_CREATION, attacker);
            }

            inhaled = true;
            me.SetUnitFlag(UnitFlags.NonAttackable);
            me.RemoveAllAuras();
            me.SetReactState(ReactStates.Passive);
            me.AttackStop();
            DoCast(SpellIDS.SPELL_INHALE);
            events.CancelEvent(Events.EVENT_BELCH); // just to be sure
            events.CancelEvent(Events.EVENT_POISON_CLOUD);
            events.CancelEvent(Events.EVENT_REPULSIVE_KICK);
            events.ScheduleEvent(Events.EVENT_BELOW5, TimeSpan.FromMilliseconds(6000));
        }
    }

    //no need to port JustDied sience it's empty...

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        uint eventId = events.ExecuteEvent();

        if (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_BELCH:
                    DoCastVictim(SpellIDS.SPELL_BELCH);
                    events.ScheduleEvent(Events.EVENT_BELCH, RandomHelper.RandTime(TimeSpan.FromMilliseconds(15), TimeSpan.FromMilliseconds(60)));
                    break;
                case Events.EVENT_POISON_CLOUD:
                    DoCast(SpellIDS.SPELL_POISON_CLOUD);
                    events.ScheduleEvent(Events.EVENT_POISON_CLOUD, RandomHelper.RandTime(TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(15)));
                    break;
                case Events.EVENT_REPULSIVE_KICK:
                    DoCastVictim(SpellIDS.SPELL_REPULSIVE_KICK);
                    events.ScheduleEvent(Events.EVENT_REPULSIVE_KICK, RandomHelper.RandTime(TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(15)));
                    break;
                case Events.EVENT_BELOW5:
                    me.SetUnitFlag2(UnitFlags2.FeignDeath);
                    me.AddAura(SpellIDS.SPELL_CADAVER_HOVER, me);
                    events.ScheduleEvent(Events.EVENT_TAKEOFF, TimeSpan.FromMilliseconds(5200));
                    break;
                case Events.EVENT_TAKEOFF:
                    me.SetCanFly(true);
                    //                    me->SetAnimTier(UnitBytes1_Flags(UNIT_BYTE1_FLAG_ALWAYS_STAND | UNIT_BYTE1_FLAG_HOVER), true);
                    me.SetDisableGravity(true);
                    me.SetSpeed(UnitMoveType.Flight, 0.5f);
                    me.GetMotionMaster().MovePoint(0, me.GetPositionX(), me.GetPositionY(), me.GetPositionZ() + 20.0f);
                    events.ScheduleEvent(Events.EVENT_EXPLODE, TimeSpan.FromMilliseconds(5600));
                    break;
                case Events.EVENT_EXPLODE:
                    DoCast(SpellIDS.SPELL_PLAGUE_EXPLOSION);
                    me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(700));
                    break;
            }
        }
        DoMeleeAttackIfReady();
    }
}

[Script]
class npc_lord_geoffery : ScriptedAI
{
    public npc_lord_geoffery(Creature creature) : base(creature) { }

    EventMap events = new();
    bool transformed;

    public override void Reset()
    {
        transformed = false;
    }

    public override void EnterEvadeMode(EvadeReason why = EvadeReason.Other)
    {
        me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(3000));
    }

    public override void JustEnteredCombat(Unit who)
    {
        events.ScheduleEvent(Events.EVENT_DEATH_BY_PEASANT, TimeSpan.FromMilliseconds(15000));
        events.ScheduleEvent(Events.EVENT_GEOF_UCUT, TimeSpan.FromMilliseconds(5000));
    }

    public override void DamageTaken(Unit attacker, ref uint damage, DamageEffectType damageType, SpellInfo spellInfo = null)
    {
        if (!transformed && !me.IsNonMeleeSpellCast(false) && HealthBelowPct(20))
        {
            transformed = true;
            DoCast(me, SpellIDS.SPELL_TRANSFORM_VISUAL);
            me.SetDisplayId(Misc.DISPLAY_GEOF_WORGEN);
            me.SetSpeed(UnitMoveType.Run, 2.5f); // his speed changes when transforming (Retail 1.5 | PWS: 2.5)
            events.ScheduleEvent(Events.EVENT_UNDYING_FRENZY, TimeSpan.FromMilliseconds(2000));
        }
    }

    public override void JustDied(Unit killer)
    {
        Player player = killer.GetCharmerOrOwnerPlayerOrPlayerItself();
        if (player != null)
        {
            player.GroupEventHappens(QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THE_EARL_OF_EVIS, killer);
        }
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        uint eventId = events.ExecuteEvent();

        if (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_DEATH_BY_PEASANT:
                    DoCast(SpellIDS.SPELL_DEATH_BY_PEASANT);
                    events.ScheduleEvent(Events.EVENT_DEATH_BY_PEASANT, RandomHelper.RandTime(TimeSpan.FromMilliseconds(30), TimeSpan.FromMilliseconds(40)));
                    break;
                case Events.EVENT_UNDYING_FRENZY: // execute this event at 20% (needs retail info)
                    DoCastVictim(SpellIDS.SPELL_UNDYING_FRENZY);
                    events.ScheduleEvent(Events.EVENT_UNDYING_FRENZY, RandomHelper.RandTime(TimeSpan.FromMilliseconds(10), TimeSpan.FromMilliseconds(15)));
                    break;
                case Events.EVENT_GEOF_UCUT:
                    DoCastVictim(SpellIDS.SPELL_UPPERCUT);
                    events.ScheduleEvent(Events.EVENT_GEOF_UCUT, RandomHelper.RandTime(TimeSpan.FromMilliseconds(15), TimeSpan.FromMilliseconds(20)));
                    break;
            }
        }
        DoMeleeAttackIfReady();
    }
}

[Script]
class npc_emberscar_devourer : ScriptedAI
{
    public npc_emberscar_devourer(Creature creature) : base(creature) { }

    EventMap events = new();

    public override void Reset()
    {
        //empty
    }

    public override void EnterEvadeMode(EvadeReason why = EvadeReason.Other)
    {
        me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(3000));
    }

    public override void JustEnteredCombat(Unit who)
    {
        events.ScheduleEvent(Events.EVENT_FIREBALL, TimeSpan.FromMilliseconds(1000));
    }

    public override void JustDied(Unit killer)
    {
        Player player = killer.GetCharmerOrOwnerPlayerOrPlayerItself();
        if (player != null)
        {
            player.GroupEventHappens(QuestIDS.QUEST_THE_CRUCIBLE_OF_CARNAGE_THE_TWILIGHT_TERROR, killer);
        }
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        uint eventId = events.ExecuteEvent();

        if (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_FIREBALL:
                    DoCastVictim(SpellIDS.SPELL_FIREBALL);
                    events.ScheduleEvent(Events.EVENT_FIREBALL, TimeSpan.FromMilliseconds(4000));
                    break;
            }
        }
    }
}

[Script]
class npc_gloomwing : ScriptedAI
{
    public npc_gloomwing(Creature creature) : base(creature)
    {
        me.SetUnitFlag(UnitFlags.Uninteractible | UnitFlags.NonAttackable);
        me.SetHover(true);
        me.SetCanFly(true);
    }

    EventMap events = new();

    public override void Reset()
    {
        Unit playersearch = me.SelectNearestPlayer(300.0f);
        if (playersearch != null)
            AttackStartNoMove(playersearch);
    }

    public override void JustEnteredCombat(Unit who)
    {
        events.ScheduleEvent(Events.EVENT_GLOOM_BALL, TimeSpan.FromMilliseconds(2000));
    }

    public override void AttackStart(Unit target)
    {
        if (!target)
            return;

        AttackStartNoMove(target);
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        uint eventId = events.ExecuteEvent();

        if (eventId != 0)
        {
            switch (eventId)
            {
                case Events.EVENT_GLOOM_BALL:
                    DoCast(SpellIDS.SPELL_GLOOM_BALL);
                    events.ScheduleEvent(Events.EVENT_GLOOM_BALL, RandomHelper.RandTime(TimeSpan.FromMilliseconds(3), TimeSpan.FromMilliseconds(6)));
                    break;
            }
        }
    }
}