// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable InvertIf

using System;
using System.Numerics;
using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Scripting;

namespace Scripts.Maps.Azeroth.EasternKingdoms.DunMorogh.ColdridgeValley;

struct CreatureIds
{
    public const uint MiloGeartwinge = 37518;
}

struct SpellIds
{
    public const uint RideVehicleHardcoded = 46598;
    public const uint EjectAllPassengers = 50630;
}

struct EventIds
{
    public const uint StartPath = 1;
    public const uint MiloSay0 = 2;
    public const uint MiloSay1 = 3;
    public const uint MiloSay2 = 4;
    public const uint MiloSay3 = 5;
    public const uint MiloSay4 = 6;
    public const uint MiloSay5 = 7;
    public const uint MiloSay6 = 8;
    public const uint MiloDespawn = 9;
}
struct MiscConst
{
    public static Vector3[] kharanosPath =
    {
        new(-6247.328f, 299.5365f, 390.266f),
        new(-6247.328f, 299.5365f, 390.266f),
        new(-6250.934f, 283.5417f, 393.46f),
        new(-6253.335f, 252.7066f, 403.0702f),
        new(-6257.292f, 217.4167f, 424.3807f),
        new(-6224.2f, 159.9861f, 447.0882f),
        new(-6133.597f, 164.3177f, 491.0316f),
        new(-6084.236f, 183.375f, 508.5401f),
        new(-6020.382f, 179.5052f, 521.5396f),
        new(-5973.592f, 161.7396f, 521.5396f),
        new(-5953.665f, 151.6111f, 514.5687f),
        new(-5911.031f, 146.4462f, 482.1806f),
        new(-5886.389f, 124.125f, 445.6252f),
        new(-5852.08f,  55.80903f, 406.7922f),
        new(-5880.707f, 12.59028f, 406.7922f),
        new(-5927.887f, -74.02257f, 406.7922f),
        new(-5988.436f, -152.0174f, 425.6251f),
        new(-6015.274f, -279.467f, 449.528f),
        new(-5936.465f, -454.1875f, 449.528f),
        new(-5862.575f, -468.0504f, 444.3899f),
        new(-5783.58f,  -458.6042f, 432.5026f),
        new(-5652.707f, -463.4427f, 415.0308f),
        new(-5603.897f, -466.3438f, 409.8931f),
        new(-5566.957f, -472.5642f, 399.0056f)
    };
}

// https://www.wowhead.com/quest=24491/follow-that-gyro-copter
// Quest 24491 Follow that Gyro-Copter!
// Spell 70047 Follow That Gyro-Copter - Quest Start
// Spell 70042 See Coldridge Tunnel Rocks - See Quest Invis 1
// Spell 70045 Milo Geartwinge Invisibility - Quest Invis 2
// Spell 46598 Ride Vehicle Hardcoded
// Spell 50630 Eject All Passengers
// NPC 37113 Milo Geartwinge
// NPC 37518 Milo Geartwinge Phased
// NPC 37169 Milo's Gyro
// NPC 37198 Milo's Gyro Phased

[Script]
class spell_follow_that_gyrocopter_quest_start : SpellScript
{
    void HandleForceCast(uint effIndex)
    {
        PreventHitDefaultEffect(effIndex);
        GetHitUnit().CastSpell(GetHitUnit(), GetEffectInfo().TriggerSpell, true);
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleForceCast, 1, SpellEffectName.ForceCast));
    }
}

[Script]
class npc_milos_gyro : VehicleAI
{
    private ObjectGuid _miloGUID;
    private bool _waitBeforePath;

    public npc_milos_gyro(Creature creature) : base(creature)
    {
        _waitBeforePath = true;
    }

    public override void Reset()
    {
        _events.Reset();
        _miloGUID.Clear();
        _waitBeforePath = true;
    }

    public override void PassengerBoarded(Unit passenger, sbyte seatId, bool apply)
    {
        if (apply && passenger.IsPlayer())
        {
            Creature milo = passenger.SummonCreature(CreatureIds.MiloGeartwinge, me.GetPosition(), TempSummonType.CorpseDespawn, TimeSpan.Zero);

            if (milo != null)
            {
                _waitBeforePath = false;
                _miloGUID = milo.GetGUID();
                milo.CastSpell(me, SpellIds.RideVehicleHardcoded);
                _events.ScheduleEvent(EventIds.StartPath, TimeSpan.FromSeconds(1));
            }
        }
    }

    public override void MovementInform(MovementGeneratorType movementType, uint pointId)
    {
        if (movementType == MovementGeneratorType.Effect && pointId == (uint)MiscConst.kharanosPath.Length)
            _events.ScheduleEvent(EventIds.MiloDespawn, TimeSpan.FromSeconds(1));
    }

    public override void UpdateAI(uint diff)
    {
        if (_waitBeforePath)
            return;

        _events.Update(diff);

        if (me.HasUnitState(UnitState.Casting))
            return;

        _events.ExecuteEvents(eventId =>
        {
            Creature milo = ObjectAccessor.GetCreature(me, _miloGUID);

            switch (eventId)
            {
                case EventIds.StartPath:
                    me.GetMotionMaster().MoveSmoothPath((uint)MiscConst.kharanosPath.Length, MiscConst.kharanosPath, MiscConst.kharanosPath.Length, false, true);
                    _events.ScheduleEvent(EventIds.MiloSay0, TimeSpan.FromSeconds(5));
                    break;
                case EventIds.MiloSay0:
                    if (milo != null)
                        milo.GetAI().Talk(0, me);

                    _events.ScheduleEvent(EventIds.MiloSay1, TimeSpan.FromSeconds(6));
                    break;
                case EventIds.MiloSay1:
                    if (milo != null)
                        milo.GetAI().Talk(1, me);

                    _events.ScheduleEvent(EventIds.MiloSay2, TimeSpan.FromSeconds(11));
                    break;
                case EventIds.MiloSay2:
                    if (milo != null)
                        milo.GetAI().Talk(2, me);

                    _events.ScheduleEvent(EventIds.MiloSay3, TimeSpan.FromSeconds(11));
                    break;
                case EventIds.MiloSay3:
                    if (milo != null)
                        milo.GetAI().Talk(3, me);

                    _events.ScheduleEvent(EventIds.MiloSay4, TimeSpan.FromSeconds(18));
                    break;
                case EventIds.MiloSay4:
                    if (milo != null)
                        milo.GetAI().Talk(4, me);

                    _events.ScheduleEvent(EventIds.MiloSay5, TimeSpan.FromSeconds(11));
                    break;
                case EventIds.MiloSay5:
                    if (milo != null)
                        milo.GetAI().Talk(5, me);

                    _events.ScheduleEvent(EventIds.MiloSay6, TimeSpan.FromSeconds(14));
                    break;
                case EventIds.MiloSay6:
                    if (milo != null)
                        milo.GetAI().Talk(6, me);
                    break;
                case EventIds.MiloDespawn:
                    me.RemoveAllAuras();
                    if (milo != null)
                        milo.DespawnOrUnsummon();
                    _waitBeforePath = true;
                    break;
            }
        });
    }
}