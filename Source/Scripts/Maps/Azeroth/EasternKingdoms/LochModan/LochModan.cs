// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.DataStorage;
using Game.Entities;
using Game.Scripting;
using Game.Spells;
using static Game.Scripting.SpellScript;
using System.Numerics;
using System;
using Game;

namespace Scripts.Maps.Azeroth.EasternKingdoms.LochModan;

struct QuestIds
{
    public const uint AxisOfAwful = 26868;
    public const uint ResuplyExcavation = 13639;
    public const uint ProtectShipment = 309;
    public const uint HandOfTheGoods = 13650;
    public const uint WindsLochModan = 27116;
}

struct CreatureIds
{
    public const uint MosshideCredit = 44266;
    public const uint MosshideRep = 44262;
    public const uint Huldar = 2057;
    public const uint DarkIronAmbusher = 1981;
    public const uint Sean = 1380;
    public const uint IronbandTablet = 33487;
    public const uint IronbandSandal = 33485;
    public const uint IronbandLiberty = 33486;
    public const uint Skystrider = 44572;
}

struct SpellIds
{
    public const uint MurlocPheromone = 82799;
    public const uint ShipmentCredit = 62980;
    public const uint SummonSkystrider = 83980;
    public const uint SkystriderFlames = 83984;
    public const uint RideVehcile = 46598;
}

struct ActionIds
{
    public const uint StartEvent = 1;
}

struct Strider
{
    public static Vector3[] WayPoint =
    {
            new(-5022.421f, -3608.176f, 298.2664f),
            new(-5017.780f, -3607.010f, 298.196f),
            new(-5012.820f, -3612.410f, 298.175f),
            new(-5003.470f, -3618.950f, 298.419f),
            new(-5005.520f, -3633.210f, 301.351f),
            new(-5005.900f, -3651.180f, 304.547f), // last ground position
            new(-4977.370f, -3649.060f, 305.986f),
            new(-4875.530f, -3641.770f, 312.875f),
            new(-4804.860f, -3594.520f, 309.681f),
            new(-4786.060f, -3543.530f, 311.514f),
            new(-4765.980f, -3421.280f, 317.070f),
            new(-4710.740f, -3246.950f, 325.014f),
            new(-4565.150f, -3138.970f, 312.098f),
            new(-4510.380f, -3213.140f, 307.681f),
            new(-4561.530f, -3298.220f, 297.820f),
            new(-4795.280f, -3304.000f, 305.320f),
            new(-4785.830f, -3106.710f, 323.709f),
            new(-4725.100f, -2932.050f, 340.654f),
            new(-4755.760f, -2804.530f, 336.070f),
            new(-4807.600f, -2701.550f, 332.376f),
            new(-4815.010f, -2707.960f, 334.449f)
        };
}

//scripting start

[Script]
class spell_murloc_pheromone : SpellScript
{
    SpellCastResult CheckRequirement()
    {
        if (GetCaster().FindNearestCreature(CreatureIds.MosshideRep, 25.0f, true))
            return SpellCastResult.IncorrectArea;
        return SpellCastResult.Success;
    }

    public void SelectTarget(ref WorldObject target)
    {
        target = GetCaster().FindNearestCreature(CreatureIds.MosshideRep, 25.0f, true);
    }

    void HandleDummy(uint effindex)
    {
        Unit hitUnit = GetHitUnit();
        if (!hitUnit || !GetCaster().IsPlayer())
            return;

        Creature target = hitUnit.ToCreature();
        if (target != null)
        {
            switch (target.GetEntry())
            {
                case CreatureIds.MosshideRep:
                    GetCaster().ToPlayer().RewardPlayerAndGroupAtEvent(CreatureIds.MosshideCredit, GetCaster());
                    break;
                default:
                    break;
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
class npc_mosshide_representative : ScriptedAI
{
    public npc_mosshide_representative(Creature creature) : base(creature) { }

    public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
    {
        if (spellInfo.Id != SpellIds.MurlocPheromone)
            return;

        //might not right..
        Player player = caster.ToPlayer();
        if (player != null)
            if (player.GetQuestStatus(QuestIds.AxisOfAwful) == QuestStatus.Incomplete)
                player.KilledMonsterCredit(CreatureIds.MosshideCredit);
    }
}

[Script]
class npc_huldar : ScriptedAI
{
    public npc_huldar(Creature creature) : base(creature) { }


    public override void OnQuestAccept(Player player, Quest quest)
    {
        switch (quest.Id)
        {
            case QuestIds.ProtectShipment:
                me.SummonCreature(CreatureIds.DarkIronAmbusher, -5699.78f, -3565.10f, 308.514f, 1.67945f, TempSummonType.TimedDespawnOutOfCombat, TimeSpan.FromMilliseconds(90000));
                me.SummonCreature(CreatureIds.DarkIronAmbusher, -5694.87f, -3559.27f, 307.216f, 2.15854f, TempSummonType.TimedDespawnOutOfCombat, TimeSpan.FromMilliseconds(90000));
                me.SummonCreature(CreatureIds.Sean, -5697.07f, -3562.87f, 307.883f, 1.93863f, TempSummonType.TimedDespawnOutOfCombat, TimeSpan.FromMilliseconds(90000));
                player.CastSpell(player, SpellIds.ShipmentCredit, true);
                break;
        }
    }

    public override void MoveInLineOfSight(Unit who)
    {
        Player player = who.ToPlayer();
        if (player != null)
            if (player.GetQuestStatus(QuestIds.ResuplyExcavation) == QuestStatus.Incomplete)
                player.KilledMonsterCredit(CreatureIds.Huldar);
    }
}


//fuck off @Varjgard with your creature shit script.. it's suppose to be Areatrigger...
//areat 5377
[Script]
class at_ironband_tablet : AreaTriggerScript
{
    public at_ironband_tablet() : base("at_ironband_tablet") { }

    public override bool OnTrigger(Player player, AreaTriggerRecord areaTrigger)
    {
        if (player && player.IsAlive())
        {
            if (player.GetQuestStatus(QuestIds.HandOfTheGoods) == QuestStatus.Incomplete)
                player.KilledMonsterCredit(CreatureIds.IronbandTablet);
            return true;
        }

        return false;
    }
}

//areat 5375
[Script]
class at_ironband_sandal : AreaTriggerScript
{
    public at_ironband_sandal() : base("at_ironband_sandal") { }

    public override bool OnTrigger(Player player, AreaTriggerRecord areaTrigger)
    {
        if (player && player.IsAlive())
        {
            if (player.GetQuestStatus(QuestIds.HandOfTheGoods) == QuestStatus.Incomplete)
                player.KilledMonsterCredit(CreatureIds.IronbandSandal);
            return true;
        }

        return false;
    }
}

//areat 5376
[Script]
class at_ironband_liberty : AreaTriggerScript
{
    public at_ironband_liberty() : base("at_ironband_liberty") { }

    public override bool OnTrigger(Player player, AreaTriggerRecord areaTrigger)
    {
        if (player && player.IsAlive())
        {
            if (player.GetQuestStatus(QuestIds.HandOfTheGoods) == QuestStatus.Incomplete)
                player.KilledMonsterCredit(CreatureIds.IronbandLiberty);
            return true;
        }

        return false;
    }
}

[Script]
class npc_ando_blastenheimer : ScriptedAI
{
    public npc_ando_blastenheimer(Creature creature) : base(creature) { }

    public override void OnQuestAccept(Player player, Quest quest)
    {
        switch (quest.Id)
        {
            case QuestIds.WindsLochModan:
                player.CastSpell(player, SpellIds.SummonSkystrider, true);
                Unit skystrider = me.FindNearestCreature(CreatureIds.Skystrider, 10.0f, true);
                if (skystrider != null)
                    player.CastSpell(player, SpellIds.RideVehcile, true);
                skystrider.GetMotionMaster().MoveSmoothPath(0, Strider.WayPoint, 22, false, true);
                break;
        }
    }
}

[Script]
class at_the_loch : AreaTriggerScript
{
    public at_the_loch() : base("at_the_loch") { }

    public override bool OnTrigger(Player player, AreaTriggerRecord areaTrigger)
    {
        if (player && player.IsAlive())
        {
            if (player.GetQuestStatus(QuestIds.ResuplyExcavation) == QuestStatus.Incomplete)
                player.CompleteQuest(QuestIds.ResuplyExcavation);
            return true;
        }

        return false;
    }
}