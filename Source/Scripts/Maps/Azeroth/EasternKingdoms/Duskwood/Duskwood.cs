// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game;
using Game.AI;
using Game.DataStorage;
using Game.Entities;
using Game.Scripting;
using Game.Spells;
using System;
using System.Collections.Generic;

namespace Scripts.Maps.Azeroth.EasternKingdoms.Duskwood
{
    struct QuestIDS
    {
        public const uint QUEST_NIGHTMARES_CORRUPTION = 8735;
        public const uint QUEST_CRY_FOR_THE_MOON = 26760;
        public const uint QUEST_MISTMANTLES_REVENGE = 26674;
        public const uint QUEST_EMBALMERS_REVENGE = 26727;
        public const uint QUEST_A_CURSE_WE_CANNOT_LIFT = 26720;
    }

    struct SpellIDS
    {
        public const uint SPELL_SOUL_CORRUPTION = 25805;
        public const uint SPELL_CREATURE_OF_NIGHTMARE = 25806;
        public const uint SPELL_LEVEL_UP = 24312;
        public const uint SPELL_WORGEN_TRANSFORMATION = 81908;
        public const uint SPELL_SACRED_CLEANSING = 82130;
        public const uint SPELL_BLAZE_PERIODIC_TRIGGER = 82182;
        public const uint SPELL_AURA_OF_ROT = 3106;

        public const uint SPELL_INVISIBILITY_7 = 82288;
        public const uint SPELL_INVISIBILITY_8 = 82289;
        public const uint SPELL_KILL_CREDIT = 82286;  // Regain Quest Invis Detection and give Kill Credit (43969)
        public const uint SPELL_CHOKING_JITTERS = 82262;  // Not used
        public const uint SPELL_CHOKED_BY_SVEN = 82266;  // Not used
        public const uint SPELL_EJECT_PASSENGERS = 65785;  // Not used
        public const uint SPELL_SUMMON_OLIVER_HARRIS = 82055; // //cast on quest taken and summons npc 43858
        public const uint SPELL_FORCE_SUMMON_JITTERS = 82236; 
        public const uint SPELL_SUMMON_JITTERS = 82056; //cast on quest taken & summons npc 43859
        public const uint SPELL_GENERIC_QUEST_INVISIBLITY = 49414; //cast on quest taken on npc 228

        public const uint SPELL_LURKING_WORGEN_KILL_CREDIT = 43860;
        public const uint SPELL_STUNNING_POUNCE = 81957;
        public const uint SPELL_HARRIS_AMPULE = 82058; // Used by item ID: 60206
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

        public const uint ACTION_START_EVENT = 1;

        public const uint EVENT_WORGEN_1 = 1;
        public const uint EVENT_WORGEN_2 = 2;
        public const uint EVENT_WORGEN_3 = 3;
        public const uint EVENT_WORGEN_4 = 4;
        public const uint EVENT_WORGEN_5 = 5;
        public const uint EVENT_WORGEN_6 = 6;
        public const uint EVENT_WORGEN_7 = 7;
        public const uint EVENT_WORGEN_8 = 8;
        public const uint EVENT_WORGEN_9 = 9;
        public const uint EVENT_WORGEN_10 = 10;
        public const uint EVENT_WORGEN_11 = 11;
        public const uint EVENT_WORGEN_12 = 12;
        public const uint EVENT_WORGEN_13 = 13;
        public const uint EVENT_WORGEN_14 = 14;
        public const uint EVENT_WORGEN_15 = 15;
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

        public const string SAY_0 = "Here we go...";
        public const string SAY_1 = "It's working. Hold him still, Jitters.";
        public const string SAY_2 = "I...I can't";
        public const string SAY_3 = "Jitters...";
        public const string SAY_4 = "Damn it, Jitters. I said HOLD!";
        public const string SAY_5 = "JITTERS!";
        public const string SAY_6 = "I remember now...it's all your fault!";
        public const string SAY_7 = "You brought the worgen to Duskwood! You led the Dark Riders to my farm, and hid while they murdered my family!";
        public const string SAY_8 = "Every speak of suffering in my life is YOUR PATHETIC FAULT! I SHOULD KILL YOU!";
        public const string SAY_9 = "Letting him go is the only thing that's going to separateyou from the beasts now, my friend.";
        public const string SAY_10 = "You've got a lot to make up for, Jitters. I won't give you the easy way out.";
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
        public const uint NPC_NIGHT_WATCH_GUARD = 43903;
        public const uint NPC_OLIVER_HARRIS = 43730; 
        public const uint NPC_OLIVER_HARRIS_SUMMONED = 43858; //This is the summend one 43858
        public const uint NPC_JITTERS = 43859;
        public const uint NPC_LUR_WORGEN = 43950;
        public const uint NPC_LURKING_WORGEN = 43814;
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
            return SpellCastResult.SpellCastOK;
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
            OnEffectHit.Add(new EffectHandler(HandleSendEvent, 0, SpellEffectName.SendEvent));
            OnCheckCast.Add(new CheckCastHandler(CheckRequirement));
        }
    }

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
                        //_events.ScheduleEvent(Events.EVENT_SOUL_CORRUPTION, RandomHelper.URand(15000, 19000));
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

    [Script]
    class at_twilight_grove : AreaTriggerScript
    {
        public at_twilight_grove() : base("at_twilight_grove") { }

        public override bool OnTrigger(Player player, AreaTriggerRecord trigger)
        {
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

        Creature GetTobias()
        {
            Creature tobias = me.FindNearestCreature(CreatureIDS.NPC_TOBIAS, 30, true); 
            if (tobias != null)
                return tobias;

            return me.FindNearestCreature(CreatureIDS.NPC_WORGEN_TOBIAS, 30, true);
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

        public override void UpdateAI(uint diff)
        {

            _events.Update(diff);

            _events.ExecuteEvents(eventId =>
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
                        tobias2?.GetAI().Talk(Texts.SAY_01, me);
                        break;

                    case Events.EVENT_TOBIAS_STEP_3:
                        Creature tobias3 = GetTobias();
                        tobias3?.GetAI().Talk(Texts.SAY_02, me);
                        break;

                    case Events.EVENT_TOBIAS_STEP_4:
                        Creature tobias4 = GetTobias();
                        if (tobias4 != null)
                        {
                            tobias4.CastSpell(tobias4, SpellIDS.SPELL_WORGEN_TRANSFORMATION, true);
                            tobias4.SetReactState(ReactStates.Aggressive);

                            me.RemoveUnitFlag(UnitFlags.NotAttackable1 | UnitFlags.ImmuneToNpc);
                            AddThreat(tobias4, 10);
                            AddThreat(me, 10);
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
            });

            if (!UpdateVictim())
                return;

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

            _events.ExecuteEvents(eventId =>
            {
                switch (eventId)
                {
                    case Events.EVENT_SELECT_TARGET:
                        SelectTargets();
                        KillSelectedCreaturesAndRewardPlayer();
                        break;
                }
            });
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
                return SpellCastResult.SpellCastOK;

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

    //[Quest] - Embalmer's Revenge - 26727
    [Script]
    class npc_blaze_darkshire : ScriptedAI //43918 - find spawn locations
    {
        public npc_blaze_darkshire(Creature creature) : base(creature) { }

        public override void Reset()
        {
            me.CastSpell(me, SpellIDS.SPELL_BLAZE_PERIODIC_TRIGGER, true);
        }
    }

    [Script]
    class npc_ebenlocke : ScriptedAI //263
    {
        public npc_ebenlocke(Creature creature) : base(creature) { }

        ObjectGuid PlayerGUID;
        uint SummonTimer;

        bool bSummoned;

        public override void OnQuestAccept(Player player, Quest quest)
        {

            if(quest.Id == QuestIDS.QUEST_EMBALMERS_REVENGE)
            {
                me.GetAI().SetGUID(player.GetGUID());
                me.GetAI().DoAction((int)Events.ACTION_START_EVENT);
                SummonSpawns();
            }
        }

        public override void Reset()
        {
            bSummoned = false;
            SummonTimer = 2000;
        }


        public override void DoAction(int action)
        {
            switch (action)
            {
                case (int)Events.ACTION_START_EVENT:
                    SummonTimer = 2000;
                    break;
            }
        }

        void SummonSpawns()
        {
            if (!bSummoned)
            {
                Creature stitches = me.SummonCreature(CreatureIDS.NPC_STITCHES, -10553.90f, -1171.27f, 27.8604f, 1.48514f, TempSummonType.TimedDespawnOutOfCombat, TimeSpan.FromMilliseconds(90000));
                if (stitches != null)
                    bSummoned = true;
            }
        }

        public override void UpdateAI(uint diff)
        {
            if (SummonTimer < diff)
            {
                Player player = Global.ObjAccessor.GetPlayer(me, PlayerGUID);
                if (player != null)
                {
                    SummonSpawns();
                }
            }
            else SummonTimer -= diff;
        }
    }


    /// Stitches - 43862
    [Script]
    class npc_stitches : ScriptedAI
    {
        public npc_stitches(Creature creature) : base(creature) { }

        uint Timer;

        public override void Reset()
        {
            me.SetRespawnTime(10000);
            Timer = 5000;
        }

        //info script
        //this script makes npc to deal 0 damage to all other npc's
        public override void DamageTaken(Unit attacker, ref uint damage, DamageEffectType damageType, SpellInfo spellInfo = null)
        {
            if (attacker.GetTypeId() == TypeId.Player)
            {
                me.GetThreatManager().ResetAllThreat();
                attacker.GetThreatManager().AddThreat(me, 100000);
                me.GetThreatManager().AddThreat(attacker, 100000);
                me.GetAI().AttackStart(attacker);
            }
            else if (attacker.IsPet() || attacker.IsGuardian())
            {
                me.GetThreatManager().ResetAllThreat();
                me.GetThreatManager().AddThreat(attacker, 100000);
                me.GetAI().AttackStart(attacker);
            }
            else damage = 0;
        }

        public override void DamageDealt(Unit victim, ref uint damage, DamageEffectType damageType)
        {
            if(victim.GetTypeId() != TypeId.Player)
            {
                damage = 0; //do not do damage to ncps
            }
        }

        public override void JustDied(Unit killer)
        {
            me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(40000));
            me.SetRespawnTime(10);
        }

        public override void UpdateAI(uint diff)
        {
            if(me.IsAlive() && me.IsInCombat())
            {
                Creature enemy = me.FindNearestCreature(CreatureIDS.NPC_NIGHT_WATCH_GUARD, 16.0f, true);
                if (enemy != null)
                    me.GetAI().AttackStart(enemy);
            }

            if (!UpdateVictim())
                return;

            DoMeleeAttackIfReady();

            if (me.HasReactState(ReactStates.Passive) && Timer <= diff)
            {
                DoCastVictim(SpellIDS.SPELL_AURA_OF_ROT, new CastSpellExtraArgs(false));
                Timer = 50000;
            }
            else Timer -= diff;
        }
    }


    /// Night Watch Guard - 43903
    [Script]
    class npc_nightwatch_guard : ScriptedAI
    {
        public npc_nightwatch_guard(Creature creature) : base(creature) { }

        public override void DamageTaken(Unit attacker, ref uint damage, DamageEffectType damageType, SpellInfo spellInfo = null)
        {
            if (attacker.GetTypeId() == TypeId.Player)
            {
                me.GetThreatManager().ResetAllThreat();
                attacker.GetThreatManager().AddThreat(me, 100000);
                me.GetThreatManager().AddThreat(attacker, 100000);
                me.GetAI().AttackStart(attacker);
            }
            else if (attacker.IsPet() || attacker.IsGuardian())
            {
                me.GetThreatManager().ResetAllThreat();
                me.GetThreatManager().AddThreat(attacker, 100000);
                me.GetAI().AttackStart(attacker);
            }
            else damage = 0;
        }

        public override void DamageDealt(Unit victim, ref uint damage, DamageEffectType damageType)
        {
            if (victim.GetEntry() == CreatureIDS.NPC_STITCHES)
            {
                damage = 0;
            }
        }

        public override void JustDied(Unit killer)
        {
            me.SetRespawnTime(10000);
        }

        public override void UpdateAI(uint diff)
        {
            if (me.IsAlive() && me.IsInCombat())
            {
                Creature enemy = me.FindNearestCreature(CreatureIDS.NPC_STITCHES, 16.0f, true);
                if (enemy != null)
                    me.GetAI().AttackStart(enemy);
            }

            if (!UpdateVictim())
                return;

            DoMeleeAttackIfReady();

        }
    }


    /// Oliver Harris - 43730
    [Script]
    class npc_oliver_harris : ScriptedAI
    {
        public npc_oliver_harris (Creature creature) : base(creature) { }

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

    /// Oliver Harris - 43730
    [Script]
    class npc_oliver_harris1 : ScriptedAI
    {
        public npc_oliver_harris1 (Creature creature) : base(creature) { }

        ObjectGuid PlayerGUID;

        public override void Reset()
        {
            _events.Reset();
        }

        public override void OnQuestAccept(Player player, Quest quest)
        {
            if (quest.Id == QuestIDS.QUEST_CRY_FOR_THE_MOON)
            {
                player.RemoveAurasDueToSpell(SpellIDS.SPELL_INVISIBILITY_7);
                player.RemoveAurasDueToSpell(SpellIDS.SPELL_INVISIBILITY_8);

                Creature jitters = player.FindNearestCreature(CreatureIDS.NPC_JITTERS, 20.0f, true);
                if (jitters != null)
                {
                    player.SummonCreature(CreatureIDS.NPC_JITTERS, -10748.52f, 333.62f, 37.46f, 5.37f, TempSummonType.TimedOrDeadDespawn, TimeSpan.FromMicroseconds(90000));
                    player.SummonCreature(CreatureIDS.NPC_OLIVER_HARRIS, -10752.87f, 338.19f, 37.294f, 5.48f, TempSummonType.TimedOrDeadDespawn, TimeSpan.FromMicroseconds(90000));
                    player.SummonCreature(CreatureIDS.NPC_LUR_WORGEN, -10747.40f, 332.28f, 37.74f, 4.48f, TempSummonType.TimedOrDeadDespawn, TimeSpan.FromMicroseconds(90000));
                }

            }
        }

        public override void UpdateAI(uint diff)
        {

            _events.Update(diff);

            Player player = Global.ObjAccessor.GetPlayer(me, PlayerGUID);
            Creature Jitters = me.FindNearestCreature(CreatureIDS.NPC_JITTERS, 20.0f, true);
            Creature Worgen = me.FindNearestCreature(CreatureIDS.NPC_LUR_WORGEN, 20.0f, true);
            Creature Harris = me.FindNearestCreature(CreatureIDS.NPC_OLIVER_HARRIS, 20.0f, true);


            _events.ExecuteEvents(eventId =>
            {
                switch (eventId)
                {
                    case Events.EVENT_WORGEN_1:
                        Harris.GetMotionMaster().MovePoint(0, -10745.14f, 331.53f, 37.86f);
                        Jitters.HandleEmoteCommand(Emote.StateUseStanding);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_2, TimeSpan.FromMilliseconds(3500));
                        break;
                    case Events.EVENT_WORGEN_2:
                        Harris.Say(Texts.SAY_0, 0);
                        Harris.SetFacingToObject(Worgen);
                        Harris.HandleEmoteCommand(Emote.OneshotTalkNoSheathe);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_3, TimeSpan.FromMilliseconds(2500));
                        break;
                    case Events.EVENT_WORGEN_3:
                        Harris.Say(Texts.SAY_1, 0);
                        Harris.HandleEmoteCommand(Emote.OneshotAttackOffPierce);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_4, TimeSpan.FromMilliseconds(2000));
                        break;
                    case Events.EVENT_WORGEN_4:
                        Jitters.Say(Texts.SAY_2, 0);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_5, TimeSpan.FromMilliseconds(1000));
                        break;
                    case Events.EVENT_WORGEN_5:
                        Worgen.Say(Texts.SAY_3, 0);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_6, TimeSpan.FromMilliseconds(3000));
                        break;
                    case Events.EVENT_WORGEN_6:
                        Harris.Say(Texts.SAY_4, 0);
                        Harris.HandleEmoteCommand(Emote.OneshotExclamation);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_7, TimeSpan.FromMilliseconds(4000));
                        break;
                    case Events.EVENT_WORGEN_7:
                        Worgen.Yell(Texts.SAY_5, 0);
                        Worgen.HandleEmoteCommand(Emote.OneshotExclamation);
                        Worgen.SetOrientation(2.3997f); //Check if this is supposed to be Jitters
                        _events.ScheduleEvent(Events.EVENT_WORGEN_8, TimeSpan.FromMilliseconds(4500));
                        break;
                    case Events.EVENT_WORGEN_8:
                        Worgen.Say(Texts.SAY_6, 0);
                        Worgen.HandleEmoteCommand(Emote.OneshotExclamation);
                        Jitters.HandleEmoteCommand(Emote.OneshotExclamation);
                        Worgen.CastSpell(Jitters, SpellIDS.SPELL_CHOKING_JITTERS);
                        Jitters.CastSpell(me, SpellIDS.SPELL_CHOKED_BY_SVEN);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_9, TimeSpan.FromMilliseconds(4500));
                        break;
                    case Events.EVENT_WORGEN_9:
                        Worgen.Say(Texts.SAY_7, 0);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_10, TimeSpan.FromMilliseconds(9000));
                        break;
                    case Events.EVENT_WORGEN_10:
                        Worgen.Yell(Texts.SAY_8, 0);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_11, TimeSpan.FromMilliseconds(1500));
                        break;
                    case Events.EVENT_WORGEN_11:
                        Harris.Say(Texts.SAY_9, 0);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_12, TimeSpan.FromMilliseconds(2000));
                        break;
                    case Events.EVENT_WORGEN_12:
                        Worgen.Say(Texts.SAY_10, 0);
                        Worgen.CastSpell(Jitters, SpellIDS.SPELL_EJECT_PASSENGERS);
                        Worgen.CastStop();
                        Jitters.CastStop();
                        _events.ScheduleEvent(Events.EVENT_WORGEN_13, TimeSpan.FromMilliseconds(6000));
                        break;
                    case Events.EVENT_WORGEN_13:
                        Worgen.GetMotionMaster().MovePoint(1, -10761.66f, 338.77f, 37.82f);
                        Worgen.HandleEmoteCommand(Emote.OneshotExclamation);
                        Jitters.HandleEmoteCommand(Emote.OneshotExclamation);
                        _events.ScheduleEvent(Events.EVENT_WORGEN_14, TimeSpan.FromMilliseconds(3000));
                        break;
                    case Events.EVENT_WORGEN_14:
                        Worgen.SetFacingToObject(Jitters);
                        Harris.GetMotionMaster().MoveTargetedHome();
                        _events.ScheduleEvent(Events.EVENT_WORGEN_15, TimeSpan.FromMilliseconds(1000));
                        break;
                    case Events.EVENT_WORGEN_15:
                        Jitters.DespawnOrUnsummon();
                        Harris.DespawnOrUnsummon();
                        Worgen.DespawnOrUnsummon();
                        player.CastSpell(player, SpellIDS.SPELL_INVISIBILITY_7, true);
                        player.CastSpell(player, SpellIDS.SPELL_INVISIBILITY_8, true);
                        player.CastSpell(player, SpellIDS.SPELL_KILL_CREDIT, true);
                        break;

                    default:
                        break;
                }
            });

            if (!UpdateVictim())
                return;
        }
    }

    [Script]
    class at_addele_stead : AreaTriggerScript
    {
        public at_addele_stead() : base("at_addele_stead") { }

        public override bool OnTrigger(Player player, AreaTriggerRecord trigger)
        {
            if (player.GetQuestStatus(QuestIDS.QUEST_A_CURSE_WE_CANNOT_LIFT) == QuestStatus.Incomplete && player.IsAlive())
            {
                Creature worgen = player.FindNearestCreature(CreatureIDS.NPC_LURKING_WORGEN, 500.0f, true);
                if(worgen != null)
                {
                    worgen.RemoveUnitFlag(UnitFlags.Pacified);
                    worgen.RemoveUnitFlag(UnitFlags.NonAttackable);
                    worgen.RemoveUnitFlag(UnitFlags.ImmuneToPc);
                    //worgen.GetMotionMaster().MoveJump(player.GetPosition(), 30.0f, 3.00f); //this
                    worgen.JumpTo(player, 30); //or this..
                    worgen.CastSpell(player, SpellIDS.SPELL_STUNNING_POUNCE, true);
                    worgen.GetAI().AttackStart(player);
                }
            }
            return false;
        }
    }

    [Script]
    class npc_lurking_worgen_curse : ScriptedAI
    {
        public npc_lurking_worgen_curse(Creature creature) : base(creature) { }

        bool Cast;
        bool Say;

        public override void Reset()
        {
            Say = false;
            Cast = false;
            me.SetRespawnTime(10000);
        }

        public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
        {
            if (spellInfo.Id == SpellIDS.SPELL_HARRIS_AMPULE && HealthBelowPct(20) && caster.ToPlayer().GetQuestStatus(QuestIDS.QUEST_A_CURSE_WE_CANNOT_LIFT) == QuestStatus.Incomplete)
            {
                if (!Say)
                {
                    caster.ToPlayer().KilledMonsterCredit(SpellIDS.SPELL_LURKING_WORGEN_KILL_CREDIT);
                    caster.ToPlayer().AttackStop();

                    me.TextEmote("The worgen stares and hesitates!");
                    me.SetReactState(ReactStates.Passive);
                    me.AttackStop();
                    me.SetUnitFlag(UnitFlags.Allowed);
                    me.SetUnitFlag(UnitFlags.Pacified);
                    me.SetUnitFlag(UnitFlags.NonAttackable);

                    me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(5000));

                    Say = true;
                }
            }
        }

        public override void JustEnteredCombat(Unit who)
        {
            Player player = who.ToPlayer();
            me.GetAI().AttackStart(player);
        }
    }
}