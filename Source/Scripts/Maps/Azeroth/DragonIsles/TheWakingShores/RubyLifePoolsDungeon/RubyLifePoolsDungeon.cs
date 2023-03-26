// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

namespace Scripts.Maps.Azeroth.DragonIsles.TheWakingShores.RubyLifePoolsDungeon;

using Framework.Constants;
using Game.Maps;
using Game.Entities;
using Game.Scripting;
using Game.Spells;

struct SpellIds
{
    // Flashfrost Chillweaver
    public const uint IceShield = 372749;

    // Primal Juggernaut
    public const uint Excavate = 373497;
};

struct DataTypes
{
    // Encounters
    public const uint MelidrussaChillworn = 0;
    public const uint KokiaBlazehoof = 1;
    public const uint KyrakkaAndErkhartStormvein = 2;
}

struct CreatureIds
{
    // Bosses
    public const uint MelidrussaChillworn = 188252;
    public const uint KokiaBlazehoof = 189232;
    public const uint Kyrakka = 190484;
}

struct GameObjectIds
{
    public const uint FireWall = 377194;
}

class spell_ruby_life_pools_executed : AuraScript // 371652 - Executed
{
    void HandleEffectApply(AuraEffect aurEff, AuraEffectHandleModes mode)
    {
        Unit target = GetTarget();
        target.SetUnitFlag3(UnitFlags3.FakeDead);
        target.SetUnitFlag2(UnitFlags2.FeignDeath);
        target.SetUnitFlag(UnitFlags.PreventEmotesFromChatText);
    }

    public override void Register()
    {
        OnEffectApply.Add(new EffectApplyHandler(HandleEffectApply, 0, AuraType.ModStun, AuraEffectHandleModes.Real));
    }
}

class spell_ruby_life_pools_ice_shield : AuraScript // 384933 - Ice Shield
{
    void HandleEffectPeriodic(AuraEffect aurEff)
    {
        Aura iceShield = GetTarget()?.GetAura(SpellIds.IceShield);
        iceShield?.RefreshDuration();
    }

    public override void Register()
    {
        OnEffectPeriodic.Add(new EffectPeriodicHandler(HandleEffectPeriodic, 0, AuraType.PeriodicDummy));
    }
}

class spell_ruby_life_pools_excavate : AuraScript // 372793 - Excavate
{
    void HandleEffectPeriodic(AuraEffect aurEff)
    {
        GetCaster()?.CastSpell(GetTarget(), SpellIds.Excavate, true);
    }

    public override void Register()
    {
        OnEffectPeriodic.Add(new EffectPeriodicHandler(HandleEffectPeriodic, 0, AuraType.PeriodicDummy));
    }
}

class spell_ruby_life_pools_storm_infusion : SpellScript // 395029 - Storm Infusion
{
    void SetDest(ref SpellDestination dest)
    {
        dest.RelocateOffset(new Position(9.0f, 0.0f, 4.0f, 0.0f));
    }

    public override void Register()
    {
        OnDestinationTargetSelect.Add(new DestinationTargetSelectHandler(SetDest, 1, Targets.DestDest));
    }
}

[Script]
class instance_ruby_life_pools : InstanceMapScript
{
    public static ObjectData[] creatureData =
    {
        new ObjectData(CreatureIds.MelidrussaChillworn, DataTypes.MelidrussaChillworn),
        new ObjectData(CreatureIds.KokiaBlazehoof, DataTypes.KokiaBlazehoof),
        new ObjectData(CreatureIds.Kyrakka, DataTypes.KyrakkaAndErkhartStormvein),
    };

    public static DoorData[] doorData =
    {
        new DoorData(GameObjectIds.FireWall, DataTypes.KokiaBlazehoof, DoorType.Passage),
    };

    public static DungeonEncounterData[] encounters =
    {
        new DungeonEncounterData(DataTypes.MelidrussaChillworn, 2609 ),
        new DungeonEncounterData(DataTypes.KokiaBlazehoof, 2606 ),
        new DungeonEncounterData(DataTypes.KyrakkaAndErkhartStormvein, 2623 )
    };

    public instance_ruby_life_pools() : base(nameof(instance_ruby_life_pools), 2521) { }

    class instance_ruby_life_pools_InstanceMapScript : InstanceScript
    {
        public instance_ruby_life_pools_InstanceMapScript(InstanceMap map) : base(map)
        {
            SetHeaders("RLP");
            SetBossNumber(3);
            LoadObjectData(creatureData, null);
            LoadDoorData(doorData);
            LoadDungeonEncounterData(encounters);
        }
    }

    public override InstanceScript GetInstanceScript(InstanceMap map)
    {
        return new instance_ruby_life_pools_InstanceMapScript(map);
    }
}