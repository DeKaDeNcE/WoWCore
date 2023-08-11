// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

namespace Scripts.Maps.Azeroth.Kalimdor.Durotar;

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Maps;
using Game.Scripting;
using Game.Spells;
using System.Collections.Generic;
using System;
using Game;
using Framework.Dynamic;
using static Game.AI.SmartEvent;

struct QuestIds
{
    public const uint WatershedPatrol = 25188;
    public const uint LostButNotForgotten = 25193; //credit for Misha Tor'kren
    public const uint RaggaransFury = 25192; //kill credit for Raggaran
    public const uint ThatsTheEndOfThatRaptor = 25195; //kill credit for ZenTaji
    public const uint LostInTheFloods = 25187;
    public const uint UnbiddenVisitors = 25194;
}

struct CreatureIds
{
    public const uint DrownedThunderLizard = 39464;
    public const uint Raggaran = 39326;
    public const uint DurotarTelescopeQuestKC1 = 39357;
    public const uint DurotarTelescopeQuestKC2 = 39358;
    public const uint DurotarTelescopeQuestKC3 = 39359;
    public const uint DurotarTelescopeQuestKC4 = 39360;
    public const uint ScaredWaywardPlainstriderKC = 39336;
}

struct SpellIds
{
    public const uint RushingCharge = 6268;
    public const uint WaterWalk = 73757;
    public const uint LightningDischarge = 73958;
    public const uint SummonTelescope01Raggaran = 73741;
    public const uint SummonTelescope02Tekla = 73763;
    public const uint SummonTelescope03Misha = 73764;
    public const uint SummonTelescope04Zentaji = 73765;
}

struct EventIds
{
    public const uint TelescopeAIEventTimeout = 1;
    public const uint TelescopeAIEventFinale = 2;
    public const uint TelescopeAiEventEmote = 3;
}

struct ObjectIds
{
    public const uint ThonkSpellFocus = 203084;
}

[Script]
class npc_lazy_peon : NullCreatureAI
{
    public npc_lazy_peon(Creature creature) : base(creature) { }

    public override void InitializeAI()
    {
        me.SetWalk(true);

        scheduler.Schedule(TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(120000), task =>
        {
            GameObject Lumberpile = me.FindNearestGameObject(GoLumberpile, 20);

            if (Lumberpile != null)
                me.GetMotionMaster().MovePoint(1, Lumberpile.GetPositionX() - 1, Lumberpile.GetPositionY(), Lumberpile.GetPositionZ());

            task.Repeat();
        });

        scheduler.Schedule(TimeSpan.FromMilliseconds(300000), task =>
        {
            me.HandleEmoteCommand(Framework.Constants.Emote.StateNone);
            me.GetMotionMaster().MovePoint(2, me.GetHomePosition());
            task.Repeat();
        });
    }

    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        switch (id)
        {
            case 1:
                me.HandleEmoteCommand(Framework.Constants.Emote.StateWorkChopwood);
                break;
            case 2:
                DoCast(me, SpellBuffSleep);
                break;
        }
    }


    public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
    {
        if (spellInfo.Id != SpellAwakenPeon)
            return;

        Player player = caster.ToPlayer();

        if (player != null && player.GetQuestStatus(QuestLazyPeons) == QuestStatus.Incomplete)
        {
            player.KilledMonsterCredit(me.GetEntry(), me.GetGUID());
            Talk(SaySpellHit, caster);
            me.RemoveAllAuras();
            GameObject Lumberpile = me.FindNearestGameObject(GoLumberpile, 20);

            if (Lumberpile != null)
                me.GetMotionMaster().MovePoint(1, Lumberpile.GetPositionX() - 1, Lumberpile.GetPositionY(), Lumberpile.GetPositionZ());
        }
    }

    public override void UpdateAI(uint diff)
    {
        scheduler.Update(diff);

        //if (!UpdateVictim())
        //return;

        //DoMeleeAttackIfReady();
    }

    const int QuestLazyPeons = 37446;
    const int GoLumberpile = 175784;
    const uint SpellBuffSleep = 17743;
    const int SpellAwakenPeon = 19938;
    const int SaySpellHit = 0;

    TaskScheduler scheduler = new TaskScheduler();
}

[Script]
class spell_voodoo : SpellScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellBrew, SpellGhostly, SpellHex1, SpellHex2, SpellHex3, SpellGrow, SpellLaunch);
    }

    void HandleDummy(uint effIndex)
    {
        uint spellid = RandomHelper.RAND(SpellBrew, SpellGhostly, RandomHelper.RAND(SpellHex1, SpellHex2, SpellHex3), SpellGrow, SpellLaunch);

        Unit caster = GetCaster();
        Unit target = GetHitUnit();

        if (caster != null && target != null)
            caster.CastSpell(target, spellid, false);
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.Dummy));
    }

    const uint SpellBrew = 16712; // Special Brew
    const uint SpellGhostly = 16713; // Ghostly
    const uint SpellHex1 = 16707; // Hex
    const uint SpellHex2 = 16708; // Hex
    const uint SpellHex3 = 16709; // Hex
    const uint SpellGrow = 16711; // Grow
    const uint SpellLaunch = 16716; // Launch (Whee!)
}

[Script] // 52514
class item_thonks_spyglass_52514 : ItemScript
{
    public item_thonks_spyglass_52514() : base("item_thonks_spyglass_52514") { }

    public override bool OnUse(Player player, Item item, SpellCastTargets targets, ObjectGuid castId)
    {
        if (player.GetQuestStatus(QuestIds.LostInTheFloods) == QuestStatus.Incomplete)
        {
            ushort Raggaran = player.GetReqKillOrCastCurrentCount(QuestIds.LostInTheFloods, (int)SpellIds.SummonTelescope01Raggaran);
            ushort Tekla = player.GetReqKillOrCastCurrentCount(QuestIds.LostInTheFloods, (int)SpellIds.SummonTelescope02Tekla);
            ushort Misha = player.GetReqKillOrCastCurrentCount(QuestIds.LostInTheFloods, (int)SpellIds.SummonTelescope03Misha);
            ushort ZenTaji = player.GetReqKillOrCastCurrentCount(QuestIds.LostInTheFloods, (int)SpellIds.SummonTelescope04Zentaji);

            if (Raggaran != 0)
                player.CastSpell(player, SpellIds.SummonTelescope01Raggaran);
            else if (Tekla != 0)
                player.CastSpell(player, SpellIds.SummonTelescope02Tekla);
            else if (Misha != 0)
                player.CastSpell(player, SpellIds.SummonTelescope03Misha);
            else if (ZenTaji != 0)
                player.CastSpell(player, SpellIds.SummonTelescope04Zentaji);
        }

        return false;
    }
}

[Script] // 39320 Raggan
class npc_durotar_watershed_telescope_39320 : CreatureAI
{
    public npc_durotar_watershed_telescope_39320(Creature creature) : base(creature) { }

    ObjectGuid m_playerGUID;
    ObjectGuid m_homeGUID;
    EventMap m_events = new();
    Position pos;

    public override void InitializeAI()
    {
        m_playerGUID = me.GetCharmerOrOwnerOrOwnGUID();
        m_events.ScheduleEvent(EventIds.TelescopeAIEventTimeout, TimeSpan.FromMilliseconds(60000));        
        me.SetUnitFlag(UnitFlags.NonAttackable);
        Player player = Global.ObjAccessor.GetPlayer(me, m_homeGUID);

        if (player != null)
        {
            GameObject gobject = player.FindNearestGameObject(ObjectIds.ThonkSpellFocus, 10.0f);

            if (gobject != null)
            {
                pos = player.GetPosition();
                m_homeGUID = gobject.GetGUID();
            }
        }
    }

    public override void PassengerBoarded(Unit passenger, sbyte seatId, bool apply)
    {
        me.SetSpeed(UnitMoveType.Run, 12.0f);
        me.GetMotionMaster().MovePath(3934601, false);
    }

    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        if (type == MovementGeneratorType.Waypoint)
        {
            if (id == 2)
                m_events.ScheduleEvent(EventIds.TelescopeAiEventEmote, TimeSpan.FromMilliseconds(4000));
            else if (id == 3)
                m_events.ScheduleEvent(EventIds.TelescopeAIEventFinale, TimeSpan.FromMilliseconds(4000));
        }
    }

    public override void UpdateAI(uint diff)
    {
        m_events.Update(diff);
        uint eventId = m_events.ExecuteEvent();

        while (eventId != 0)
        {
            switch (eventId)
            {
                case EventIds.TelescopeAIEventTimeout:
                    me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(100));
                    break;
                case EventIds.TelescopeAiEventEmote:
                    Creature npc = me.FindNearestCreature(CreatureIds.Raggaran, 30.0f);

                    if (npc != null)
                        npc.HandleEmoteCommand(Framework.Constants.Emote.OneshotRoar);
                    break;
                case EventIds.TelescopeAIEventFinale:
                    Player player = Global.ObjAccessor.GetPlayer(me, m_playerGUID);

                    if (player != null)
                    {
                        GameObject go = ObjectAccessor.GetGameObject(me, m_homeGUID);

                        if (go != null)
                        {
                            player.KilledMonsterCredit(CreatureIds.DurotarTelescopeQuestKC1);
                            player.ExitVehicle();
                            player.NearTeleportTo(go.GetPositionX(), go.GetPositionY(), go.GetPositionZ(), go.GetOrientation());
                            me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(100));
                        }
                    }
                    break;
            }
        }
    }
}

[Script] // 39345 Tekla
class npc_durotar_watershed_telescope_39345 : CreatureAI
{
    public npc_durotar_watershed_telescope_39345(Creature creature) : base(creature) { }

    ObjectGuid m_playerGUID;
    ObjectGuid m_homeGUID;
    EventMap m_events = new();
    Position pos;

    public override void InitializeAI()
    {
        m_playerGUID = me.GetCharmerOrOwnerOrOwnGUID();
        m_events.ScheduleEvent(EventIds.TelescopeAIEventTimeout, TimeSpan.FromMilliseconds(60000));
        me.SetUnitFlag(UnitFlags.NonAttackable);

        Player player = Global.ObjAccessor.GetPlayer(me, m_playerGUID);

        if (player != null)
        {
            GameObject gobject = player.FindNearestGameObject(ObjectIds.ThonkSpellFocus, 10.0f);

            if (gobject != null)
            {
                pos = player.GetPosition();
                m_homeGUID = gobject.GetGUID();
            }
        }
    }

    public override void PassengerBoarded(Unit passenger, sbyte seatId, bool apply)
    {
        me.SetSpeed(UnitMoveType.Run, 12.0f);
        me.GetMotionMaster().MovePath(3934501, false);
    }

    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        if (type == MovementGeneratorType.Waypoint)
        {
            if (id == 3)
                m_events.ScheduleEvent(EventIds.TelescopeAIEventFinale, TimeSpan.FromMilliseconds(1000));
        }
    }

    public override void UpdateAI(uint diff)
    {
        m_events.Update(diff);
        uint eventId = m_events.ExecuteEvent();

        while (eventId != 0)
        {
            switch (eventId)
            {
                case EventIds.TelescopeAIEventTimeout:
                    me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(100));
                    break;
                case EventIds.TelescopeAIEventFinale:
                    Player player = Global.ObjAccessor.GetPlayer(me, m_playerGUID);

                    if (player != null)
                    {
                        GameObject go = ObjectAccessor.GetGameObject(me, m_homeGUID);

                        if (go != null)
                        {
                            player.KilledMonsterCredit(CreatureIds.DurotarTelescopeQuestKC2);
                            player.ExitVehicle();
                            player.NearTeleportTo(go.GetPositionX(), go.GetPositionY(), go.GetPositionZ(), go.GetOrientation());
                            me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(100));
                        }
                    }
                    break;
            }
        }
    }
}

[Script] // 39346 Misha
class npc_durotar_watershed_telescope_39346 : CreatureAI
{
    public npc_durotar_watershed_telescope_39346(Creature creature) : base(creature) { }

    ObjectGuid m_playerGUID;
    ObjectGuid m_homeGUID;
    EventMap m_events = new();
    Position pos;

    public override void InitializeAI()
    {
        m_playerGUID = me.GetCharmerOrOwnerOrOwnGUID();
        m_events.ScheduleEvent(EventIds.TelescopeAIEventTimeout, TimeSpan.FromMilliseconds(60000));
        me.SetUnitFlag(UnitFlags.NonAttackable);
        Player player = Global.ObjAccessor.GetPlayer(me, m_playerGUID);

        if (player != null)
        {
            GameObject gobject = player.FindNearestGameObject(ObjectIds.ThonkSpellFocus, 10.0f);

            if (gobject != null)
            {
                pos = player.GetPosition();
                m_homeGUID = gobject.GetGUID();
            }
        }
    }

    public override void PassengerBoarded(Unit passenger, sbyte seatId, bool apply)
    {
        me.SetSpeed(UnitMoveType.Run, 12.0f);
        me.GetMotionMaster().MovePath(3934601, false);
    }

    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        if (type == MovementGeneratorType.Waypoint)
        {
            if (id == 3)
                m_events.ScheduleEvent(EventIds.TelescopeAIEventFinale, TimeSpan.FromMilliseconds(1000));
        }
    }

    public override void UpdateAI(uint diff)
    {
        m_events.Update(diff);
        uint eventId = m_events.ExecuteEvent();

        while (eventId != 0)
        {
            switch (eventId)
            {
                case EventIds.TelescopeAIEventTimeout:
                    me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(100));
                    break;
                case EventIds.TelescopeAIEventFinale:
                    Player player = Global.ObjAccessor.GetPlayer(me, m_playerGUID);

                    if (player != null)
                    {
                        GameObject go = ObjectAccessor.GetGameObject(me, m_homeGUID);

                        if (go != null)
                        {
                            player.KilledMonsterCredit(CreatureIds.DurotarTelescopeQuestKC3);
                            player.ExitVehicle();
                            player.NearTeleportTo(go.GetPositionX(), go.GetPositionY(), go.GetPositionZ(), go.GetOrientation());
                            me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(100));
                        }
                    }
                    break;
            }
        }
    }
}

[Script] // 39320 Raggan
class npc_durotar_watershed_telescope_39347 : CreatureAI
{
    public npc_durotar_watershed_telescope_39347(Creature creature) : base(creature) { }

    ObjectGuid m_playerGUID;
    ObjectGuid m_homeGUID;
    EventMap m_events = new();
    Position pos;

    public override void InitializeAI()
    {
        m_playerGUID = me.GetCharmerOrOwnerOrOwnGUID();
        m_events.ScheduleEvent(EventIds.TelescopeAIEventTimeout, TimeSpan.FromMilliseconds(60000));
        me.SetUnitFlag(UnitFlags.NonAttackable);
        Player player = Global.ObjAccessor.GetPlayer(me, m_playerGUID);

        if (player != null)
        {
            GameObject gobject = player.FindNearestGameObject(ObjectIds.ThonkSpellFocus, 10.0f);

            if (gobject != null)
            {
                pos = player.GetPosition();
                m_homeGUID = gobject.GetGUID();
            }
        }
    }

    public override void PassengerBoarded(Unit passenger, sbyte seatId, bool apply)
    {
        me.SetSpeed(UnitMoveType.Run, 12.0f);
        me.GetMotionMaster().MovePath(3934701, false);
    }

    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        if (type == MovementGeneratorType.Waypoint)
        {
            if (id == 3)
                m_events.ScheduleEvent(EventIds.TelescopeAIEventFinale, TimeSpan.FromMilliseconds(1000));
        }
    }

    public override void UpdateAI(uint diff)
    {
        m_events.Update(diff);
        uint eventId = m_events.ExecuteEvent();

        while (eventId != 0)
        {
            switch (eventId)
            {
                case EventIds.TelescopeAIEventTimeout:
                    me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(100));
                    break;
                case EventIds.TelescopeAIEventFinale:
                    Player player = Global.ObjAccessor.GetPlayer(me, m_playerGUID);

                    if (player != null)
                    {
                        GameObject go = ObjectAccessor.GetGameObject(me, m_homeGUID);

                        if (go != null)
                        {
                            player.KilledMonsterCredit(CreatureIds.DurotarTelescopeQuestKC4);
                            player.ExitVehicle();
                            player.NearTeleportTo(go.GetPositionX(), go.GetPositionY(), go.GetPositionZ(), go.GetOrientation());
                            me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(100));
                        }
                    }
                    break;
            }
        }
    }
}

[Script] //39337
class npc_wayward_plainstrider : ScriptedAI
{
    public npc_wayward_plainstrider(Creature creature) : base(creature) { }

    //probably is good fix

    public override void DamageTaken(Unit attacker, ref uint damage, DamageEffectType damageType, SpellInfo spellInfo = null)
    {
        Player player = attacker.ToPlayer();

        if (player != null && me.GetHealth() < me.CountPctFromCurHealth(10) && player.HasQuest(QuestIds.UnbiddenVisitors) && player.GetQuestStatus(QuestIds.UnbiddenVisitors) == QuestStatus.Incomplete)
        {
            player.KilledMonsterCredit(CreatureIds.ScaredWaywardPlainstriderKC);
            me.GetThreatManager().ClearAllThreat();
            me.CombatStop(true);
            me.AddUnitState(UnitState.Evade);
            me.GetMotionMaster().MovePoint(0, me.GetPositionX() + (float)(Math.Cos(me.GetOrientation()) * 30.0f), me.GetPositionY() + (float)(Math.Sin(me.GetOrientation()) * 30.0f), me.GetPositionZ());
            me.DespawnOrUnsummon(TimeSpan.FromMilliseconds(2000));
        }
    }
}