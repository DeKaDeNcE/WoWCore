// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Maps;
using Game.Scripting;
using Game.Spells;
using System.Collections.Generic;
using System;
using Game;

namespace Scripts.Maps.Azeroth.Kalimdor.AzuremystIsle;

struct Say
{
    public const uint SAY_THANK_FOR_HEAL = 0;
    public const uint SAY_ASK_FOR_HELP = 1;
    public const uint SAY_TEXT = 0;
    public const uint SAY_EMOTE = 1;
    public const uint ATTACK_YELL = 2;
    public const uint SAY_START = 0;
    public const uint SAY_AGGRO = 1;
    public const uint SAY_PROGRESS = 2;
    public const uint SAY_END1 = 3;
    public const uint SAY_END2 = 4;
    public const uint EMOTE_HUG = 5;
    public const uint SAY_COWLEN = 0;

    public const uint GEEZLE_SAY_1 = 0;
    public const uint SPARK_SAY_2 = 3;
    public const uint SPARK_SAY_3 = 4;
    public const uint GEEZLE_SAY_4 = 1;
    public const uint SPARK_SAY_5 = 5;
    public const uint SPARK_SAY_6 = 6;
    public const uint GEEZLE_SAY_7 = 2;

    public const uint EMOTE_SPARK = 7;
}

struct CreatureIds
{
    public const uint NPC_COWLEN = 17311;
    public const uint NPC_SPARK = 17243;
    public const uint NPC_DEATH_RAVAGER = 17556;
}

struct SpellIds
{
    public const uint Irridation = 35046;
    public const uint Stunned = 28630;
    public const uint SPELL_DYNAMITE = 7978;
    public const uint SPELL_TREE_DISGUISE = 30298;
    public const uint SPELL_REND = 13443;
    public const uint SPELL_ENRAGING_BITE = 30736;
}

struct QuestIds
{
    public const uint QUEST_GNOMERCY = 9537;
    public const uint QUEST_A_CRY_FOR_HELP = 9528;
    public const uint QUEST_TREES_COMPANY = 9531;
    public const uint QUEST_STRENGTH_ONE = 9582;
}

struct EventIds
{
    public const uint CanAskForHelp = 1;
    public const uint ThankPlayer = 2;
    public const uint RunAway = 3;
    public const uint EVENT_ACCEPT_QUEST = 1;
    public const uint EVENT_START_ESCORT = 2;
    public const uint EVENT_STAND = 3;
    public const uint EVENT_TALK_END = 4;
    public const uint EVENT_COWLEN_TALK = 5;
}

struct GameObjectIds
{
    public const uint GO_NAGA_FLAG = 181694;
}

struct CrashSite
{
    public static Position[] POS =
    {
            new Position(-4115.25f, -13754.75f)
        };
}

struct SparkPos
{
    public static Position[] POS =
    {
            new Position(-5029.91f, -11291.79f, 8.096f, 0.0f)
        };
}

struct Misc
{
    public const uint AREA_COVE = 3579;
    public const uint AREA_ISLE = 3639;
}

class npc_draenei_survivor : ScriptedAI
{
    public npc_draenei_survivor(Creature creature) : base(creature)
    {
        Initialize();
    }

    bool _canUpdateEvents;
    bool _tappedBySpell;
    bool _canAskForHelp;
    ObjectGuid _playerGUID;

    void Initialize()
    {
        _playerGUID.Clear();
        _canAskForHelp = true;
        _canUpdateEvents = false;
        _tappedBySpell = false;
    }

    public override void Reset()
    {
        Initialize();
        _events.Reset();

        DoCastSelf(SpellIds.Irridation);

        me.SetUnitFlag(UnitFlags.PvpEnabling);
        me.SetUnitFlag(UnitFlags.InCombat);
        me.SetHealth(me.CountPctFromCurHealth(10));
        me.SetStandState(UnitStandStateType.Sleep);
    }

    public override void MoveInLineOfSight(Unit who)
    {
        if (_canAskForHelp && who.GetTypeId() == TypeId.Player && me.IsFriendlyTo(who) && me.IsWithinDistInMap(who, 25.0f))
        {
            Talk(Say.SAY_ASK_FOR_HELP);

            _events.ScheduleEvent(EventIds.CanAskForHelp, TimeSpan.FromSeconds(16), TimeSpan.FromSeconds(24));
            _canAskForHelp = false;
            _canUpdateEvents = true;
        }
    }

    public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
    {
        if (spellInfo.SpellFamilyFlags[2] == 0x80000000 && !_tappedBySpell)
        {
            _events.Reset();
            _tappedBySpell = true;
            _canAskForHelp = false;
            _canUpdateEvents = true;

            me.RemoveUnitFlag(UnitFlags.PvpEnabling);
            me.SetStandState(UnitStandStateType.Stand);

            _playerGUID = caster.GetGUID();
            Player player = caster.ToPlayer();
            if (player != null)
                player.KilledMonsterCredit(me.GetEntry());

            me.SetFacingToObject(caster);
            DoCastSelf(SpellIds.Stunned);
            _events.ScheduleEvent(EventIds.ThankPlayer, TimeSpan.FromSeconds(4));
        }
    }

    public override void UpdateAI(uint diff)
    {
        if (!_canUpdateEvents)
            return;

        _events.Update(diff);

        uint eventId = _events.ExecuteEvent();
        switch (eventId)
        {
            case EventIds.CanAskForHelp:
                _canAskForHelp = true;
                _canUpdateEvents = false;
                break;
            case EventIds.ThankPlayer:
                me.RemoveAurasDueToSpell(SpellIds.Irridation);
                Player player = Global.ObjAccessor.GetPlayer(me, _playerGUID);
                if (player != null)
                    Talk(Say.SAY_THANK_FOR_HEAL, player);
                _events.ScheduleEvent(EventIds.RunAway, TimeSpan.FromSeconds(10));
                break;
            case EventIds.RunAway:
                me.GetMotionMaster().Clear();
                me.GetMotionMaster().MovePoint(0, me.GetPositionX() + (float)(Math.Cos(me.GetAbsoluteAngle(CrashSite.POS[0])) * 28.0f), me.GetPositionY() + (float)(Math.Sin(me.GetAbsoluteAngle(CrashSite.POS[0])) * 28.0f), me.GetPositionZ() + 1.0f);
                me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(4000));
                break;
            default:
                break;
        }
    }
}

[Script]
class npc_engineer_spark_overgrind : ScriptedAI
{
    public npc_engineer_spark_overgrind(Creature creature) : base(creature)
    {
        Initialize();
        NormFaction = creature.GetFaction();
        NpcFlags = (NPCFlags)creature.m_unitData.NpcFlags[0];
    }

    uint NormFaction;
    NPCFlags NpcFlags;
    uint DynamiteTimer;
    uint EmoteTimer;
    bool IsTreeEvent;

    void Initialize()
    {
        DynamiteTimer = 8000;
        EmoteTimer = RandomHelper.URand(120000, 150000);

        if (me.GetAreaId() == Misc.AREA_COVE || me.GetAreaId() == Misc.AREA_ISLE)
            IsTreeEvent = true;
        else
            IsTreeEvent = false;
    }

    public override void Reset()
    {
        Initialize();

        me.SetFaction(NormFaction);
        me.SetNpcFlag(NpcFlags);
    }

    public override void JustEngagedWith(Unit who)
    {
        Talk(Say.ATTACK_YELL, who);
    }

    public override bool OnGossipSelect(Player player, uint menuId, uint gossipListId)
    {
        player.CloseGossipMenu();
        me.SetFaction((uint)FactionTemplates.Monster);
        me.Attack(player, true);
        return false;
    }

    public override void UpdateAI(uint diff)
    {
        if (!me.IsInCombat() && !IsTreeEvent)
        {
            if (EmoteTimer <= diff)
            {
                Talk(Say.SAY_TEXT);
                Talk(Say.SAY_EMOTE);
                EmoteTimer = RandomHelper.URand(120000, 150000);
            }
            else EmoteTimer -= diff;
        }
        else if (IsTreeEvent)
            return;

        if (!UpdateVictim())
            return;

        if (DynamiteTimer <= diff)
        {
            DoCastVictim(SpellIds.SPELL_DYNAMITE);
            DynamiteTimer = 8000;
        }
        else DynamiteTimer -= diff;

        DoMeleeAttackIfReady();
    }
}

[Script]
class npc_injured_draenei : ScriptedAI
{
    public npc_injured_draenei(Creature creature) : base(creature) { }

    public override void Reset()
    {
        me.SetUnitFlag(UnitFlags.InCombat);
        me.SetHealth(me.CountPctFromMaxHealth(15));
        switch (RandomHelper.URand(0, 1))
        {
            case 0:
                me.SetStandState(UnitStandStateType.Sit);
                break;

            case 1:
                me.SetStandState(UnitStandStateType.Sleep);
                break;
        }
    }

    public override void JustEngagedWith(Unit who) { }
    public override void MoveInLineOfSight(Unit who) { }
    public override void UpdateAI(uint diff) { }
}

[Script]
class npc_magwin : EscortAI
{
    public npc_magwin(Creature creature) : base(creature) { }

    ObjectGuid _player;

    public override void Reset()
    {
        _events.Reset();
    }

    public override void JustEngagedWith(Unit who)
    {
        Talk(Say.SAY_AGGRO, who);
    }

    public override void OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIds.QUEST_A_CRY_FOR_HELP)
        {
            _player = player.GetGUID();
            _events.ScheduleEvent(EventIds.EVENT_ACCEPT_QUEST, TimeSpan.FromSeconds(2));
        }
    }

    public override void WaypointReached(uint waypointId, uint pathId)
    {
        Player player = GetPlayerForEscort();
        if (player != null)
        {
            switch (waypointId)
            {
                case 17:
                    Talk(Say.SAY_PROGRESS, player);
                    break;
                case 28:
                    player.GroupEventHappens(QuestIds.QUEST_A_CRY_FOR_HELP, me);
                    _events.ScheduleEvent(EventIds.EVENT_TALK_END, TimeSpan.FromSeconds(2));
                    me.SetSpeed(UnitMoveType.Run, 1.14f);
                    break;
                case 29:
                    Creature cowlen = me.FindNearestCreature(CreatureIds.NPC_COWLEN, 50.0f, true);
                    if (cowlen != null)
                        Talk(Say.EMOTE_HUG, cowlen);
                    Talk(Say.SAY_END2, player);
                    break;
            }
        }
    }

    public override void UpdateEscortAI(uint diff)
    {
        _events.Update(diff);

        uint eventId = _events.ExecuteEvent();

        if (eventId != 0)
        {
            switch (eventId)
            {
                case EventIds.EVENT_ACCEPT_QUEST:
                    {
                        Player player = Global.ObjAccessor.GetPlayer(me, _player);
                        if (player != null)
                            Talk(Say.SAY_START, player);
                        me.SetFaction((uint)FactionTemplates.EscorteeNNeutralPassive);
                        _events.ScheduleEvent(EventIds.EVENT_START_ESCORT, TimeSpan.FromSeconds(1));
                        break;
                    }
                case EventIds.EVENT_START_ESCORT:
                    {
                        Player player = Global.ObjAccessor.GetPlayer(me, _player);
                        if (player != null)
                            Start(true, player.GetGUID());
                        _events.ScheduleEvent(EventIds.EVENT_STAND, TimeSpan.FromSeconds(2));
                        break;
                    }
                case EventIds.EVENT_STAND: // Remove kneel standstate. Using a separate delayed event because it causes unwanted delay before starting waypoint movement.
                    me.SetStandState(UnitStandStateType.Stand);
                    break;
                case EventIds.EVENT_TALK_END:
                    {
                        Player player = Global.ObjAccessor.GetPlayer(me, _player);
                        if (player != null)
                            Talk(Say.SAY_END1, player);
                        _events.ScheduleEvent(EventIds.EVENT_COWLEN_TALK, TimeSpan.FromSeconds(2));
                        break;
                    }
                case EventIds.EVENT_COWLEN_TALK:
                    Creature cowlen = me.FindNearestCreature(CreatureIds.NPC_COWLEN, 50.0f, true);
                    if (cowlen != null)
                        cowlen.GetAI().Talk(Say.SAY_COWLEN);
                    break;
            }
        }
    }
}

[Script]
class npc_geezle : ScriptedAI
{
    public npc_geezle(Creature creature) : base(creature)
    {
        Initialize();
    }

    ObjectGuid SparkGUID;
    byte Step;
    uint SayTimer;
    bool EventStarted;

    void Initialize()
    {
        SparkGUID.Clear();
        Step = 0;
        EventStarted = false;
        SayTimer = 0;
    }

    public override void Reset()
    {
        Initialize();
        StartEvent();
    }

    public override void JustEngagedWith(Unit who) { }

    void StartEvent()
    {
        Step = 0;
        EventStarted = true;

        Creature Spark = me.SummonCreature(CreatureIds.NPC_SPARK, SparkPos.POS[0], TempSummonType.CorpseTimedDespawn, TimeSpan.FromMilliseconds(1000));
        if (Spark != null)
        {
            SparkGUID = Spark.GetGUID();
            Spark.SetActive(true);
            Spark.RemoveNpcFlag(NPCFlags.Gossip);
        }
        SayTimer = 8000;
    }

    uint NextStep(byte step)
    {
        Creature Spark = ObjectAccessor.GetCreature(me, SparkGUID);
        if (Spark != null)
            return 99999999;

        switch (step)
        {
            case 0:
                Spark.GetMotionMaster().MovePoint(0, -5080.70f, -11253.61f, 0.56f);
                me.GetMotionMaster().MovePoint(0, -5092.26f, -11252, 0.71f);
                return 9000;
            case 1:
                DespawnNagaFlag(true);
                Spark.GetAI().Talk(Say.EMOTE_SPARK);
                return 1000;
            case 2:
                Talk(Say.GEEZLE_SAY_1, Spark);
                Spark.SetFacingToObject(me);
                me.SetFacingToObject(Spark);
                return 5000;
            case 3:
                Spark.GetAI().Talk(Say.SPARK_SAY_2);
                return 7000;
            case 4:
                Spark.GetAI().Talk(Say.SPARK_SAY_3);
                return 8000;
            case 5:
                Talk(Say.GEEZLE_SAY_4, Spark);
                return 8000;
            case 6:
                Spark.GetAI().Talk(Say.SPARK_SAY_5);
                return 9000;
            case 7:
                Spark.GetAI().Talk(Say.SPARK_SAY_6);
                return 8000;
            case 8:
                Talk(Say.GEEZLE_SAY_7, Spark);
                return 2000;
            case 9:
                me.GetMotionMaster().MoveTargetedHome();
                Spark.GetMotionMaster().MovePoint(0, SparkPos.POS[0]);
                CompleteQuest();
                return 9000;
            case 10:
                Spark.DisappearAndDie();
                DespawnNagaFlag(false);
                me.DisappearAndDie();
                return 99999999;
            default:
                return 99999999;
        }
    }

    void CompleteQuest()
    {
        float radius = 50.0f;
        List<Unit> playerList = new List<Unit>();
        var check = new AnyPlayerInObjectRangeCheck(me, radius);
        var searcher = new PlayerListSearcher(me, playerList, check);
        Cell.VisitWorldObjects(me, searcher, radius);
        foreach (Player player in playerList)
        {
            if (player.GetQuestStatus(QuestIds.QUEST_TREES_COMPANY) == QuestStatus.Incomplete && player.HasAura(SpellIds.SPELL_TREE_DISGUISE))
                player.KilledMonsterCredit(CreatureIds.NPC_SPARK);
        }
    }

    void DespawnNagaFlag(bool despawn)
    {
        List<GameObject> FlagList = new();
        me.GetGameObjectListWithEntryInGrid(GameObjectIds.GO_NAGA_FLAG, 100.0f);

        if (!FlagList.Empty())
        {
            foreach (var Flag in FlagList)
            {
                if (despawn)
                    Flag.SetLootState(LootState.JustDeactivated);
                else
                    Flag.Respawn();
            }
        }
        else
            Log.outError(LogFilter.Scripts, "SD2 ERROR: FlagList is empty!");
    }

    public override void UpdateAI(uint diff)
    {
        if (SayTimer <= diff)
        {
            if (EventStarted)
                SayTimer = NextStep(Step++);
        }
        else
            SayTimer -= diff;
    }
}

[Script]
class go_ravager_cage : GameObjectAI
{
    public go_ravager_cage(GameObject go) : base(go) { }

    public override bool OnGossipHello(Player player)
    {
        me.UseDoorOrButton();
        if (player.GetQuestStatus(QuestIds.QUEST_STRENGTH_ONE) == QuestStatus.Incomplete)
        {
            Creature ravager = me.FindNearestCreature(CreatureIds.NPC_DEATH_RAVAGER, 5.0f, true);
            if (ravager != null)
            {
                ravager.RemoveUnitFlag(UnitFlags.NonAttackable);
                ravager.SetReactState(ReactStates.Aggressive);
                ravager.GetAI().AttackStart(player);
            }
        }
        return true;
    }
}

[Script]
class npc_death_ravager : ScriptedAI
{
    public npc_death_ravager(Creature creature) : base(creature)
    {
        Initialize();
    }

    uint RendTimer;
    uint EnragingBiteTimer;

    void Initialize()
    {
        RendTimer = 30000;
        EnragingBiteTimer = 20000;
    }

    public override void Reset()
    {
        Initialize();

        me.SetUnitFlag(UnitFlags.NonAttackable);
        me.SetReactState(ReactStates.Passive);
    }

    public override void UpdateAI(uint diff)
    {
        if (!UpdateVictim())
            return;

        if (RendTimer <= diff)
        {
            DoCastVictim(SpellIds.SPELL_REND);
            RendTimer = 30000;
        }
        else RendTimer -= diff;

        if (EnragingBiteTimer <= diff)
        {
            DoCastVictim(SpellIds.SPELL_ENRAGING_BITE);
            EnragingBiteTimer = 15000;
        }
        else EnragingBiteTimer -= diff;

        DoMeleeAttackIfReady();
    }
}

[Script]
class spell_inoculate_nestlewood : AuraScript
{
    void PeriodicTick(AuraEffect aurEff)
    {
        if (GetTarget().GetTypeId() != TypeId.Unit) // prevent error reports in case ignored player target
            PreventDefaultAction();
    }

    public override void Register()
    {
        OnEffectPeriodic.Add(new EffectPeriodicHandler(PeriodicTick, 0, AuraType.PeriodicTriggerSpell));
    }
}