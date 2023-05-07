// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game.Spells;
using System.Numerics;
using System;
using Game;

namespace Scripts.Maps.Azeroth.EasternKingdoms.TheHinterlands;

struct QuestIDS
{
    public const uint QUEST_RESQUE_OOX_09 = 836;
    public const uint QUEST_RAZORBEAKFRIENDS = 26546;
}

struct SpellIDS
{
    public const uint SPELL_EJECT_ALL_PASSENGERS = 50630;
    public const uint SPELL_FEED_RAZORBEAK = 80782;
}

struct Texts
{
    public const uint SAY_OOX_START = 0;
    public const uint SAY_OOX_AGGRO = 1;
    public const uint SAY_OOX_AMBUSH = 2;
    public const uint SAY_OOX_AMBUSH_REPLY = 3;
    public const uint SAY_OOX_END = 4;
}

struct CreatureIDS
{
    public const uint NPC_MARAUDING_OWL = 7808;
    public const uint NPC_VILE_AMBUSHER = 7809;
    public const uint NPC_SHARPBEAK_CAMP = 43161;
    public const uint NPC_SHARPBEAK_JINTHAALOR = 51125;
    public const uint NPC_RAZORBEAK_CREDIT = 43236;
    public const uint NPC_MASK_BURNT_CREDIT = 42704;
    public const uint NPC_SHADRA_NW_ALTAR_BUNNY = 43067;
    public const uint NPC_SHADRA_SW_ALTAR_BUNNY = 43068;
    public const uint NPC_SHADRA_E_ALTAR_BUNNY = 43069;
}

struct Misc
{
    public const uint FACTION_ESCORTEE_A = 774;
    public const uint FACTION_ESCORTEE_H = 775;
}

struct Positions
{
    public static Position[] src1 =
    {
            new Position (147.927444f, -3851.513428f, 130.893f)
        };

    public static Position[] src2 =
    {
                new Position (147.927444f, -3851.513428f, 130.893f, 0)
        };

    public static Vector3[] campPath =
    {
            new(-75.40077f, -4037.111f, 114.6418f),
            new(-68.80193f, -4034.235f, 123.6844f),
            new(-62.20310f, -4031.360f, 132.7270f),
            new(-48.58510f, -4008.040f, 156.9770f),
            new(-26.26910f, -3987.880f, 176.7550f),
            new(11.50870f, -3960.860f, 203.5610f),
            new(45.00870f, -3922.580f, 236.6720f),
            new(75.44270f, -3856.910f, 255.6720f),
            new(74.83510f, -3768.840f, 279.8390f),
            new(-53.01040f, -3582.620f, 287.7550f),
            new(-169.1230f, -3582.080f, 282.8660f),
            new(-241.8403f, -3625.010f, 247.4203f)
        };

    public static Vector3[] jinthaalorPath =
    {
            new(-249.4681f, -3632.487f, 232.6947f),
            new(-241.6060f, -3627.713f, 236.6187f),
            new(-235.6163f, -3624.076f, 239.6081f),
            new(-226.8698f, -3623.929f, 244.8882f),
            new(-193.6406f, -3618.776f, 244.8882f),
            new(-149.7292f, -3613.349f, 244.8882f),
            new(-103.8976f, -3623.828f, 238.0368f),
            new(-41.33681f, -3710.568f, 232.4109f),
            new(6.201389f, -3739.243f, 214.2869f),
            new(37.44097f, -3773.431f, 189.4650f),
            new(44.21875f, -3884.991f, 177.7446f),
            new(39.81424f, -3934.679f, 168.1627f),
            new(32.17535f, -3983.781f, 166.1228f),
            new(21.34896f, -4005.293f, 162.9598f),
            new(-5.734375f, -4028.695f, 149.0161f),
            new(-23.23611f, -4040.689f, 140.1189f),
            new(-35.45139f, -4047.543f, 133.2071f),
            new(-59.21181f, -4051.257f, 128.0297f),
            new(-76.90625f, -4040.207f, 126.0433f),
            new(-77.51563f, -4022.026f, 123.2135f)
        };
}

[Script]
class npc_oox09hl : EscortAI
{
    public npc_oox09hl(Creature creature) : base(creature) { }

    public override void Reset()
    {
        //empty
    }

    public override void JustEnteredCombat(Unit who)
    {
        if (who.GetEntry() == CreatureIDS.NPC_MARAUDING_OWL || who.GetEntry() == CreatureIDS.NPC_VILE_AMBUSHER)
            return;

        Talk(Texts.SAY_OOX_AGGRO);
    }

    public override void JustSummoned(Creature summon)
    {
        summon.GetMotionMaster().MovePoint(0, me.GetPositionX(), me.GetPositionY(), me.GetPositionZ());
    }


    public override void OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIDS.QUEST_RESQUE_OOX_09)
        {
            me.SetStandState(UnitStandStateType.Stand);
            me.SetFaction(player.GetTeam() == Team.Alliance ? Misc.FACTION_ESCORTEE_A : Misc.FACTION_ESCORTEE_H);
            Talk(Texts.SAY_OOX_START, player);
            Start(false, player.GetGUID(), quest);
        }
    }



    public override void WaypointReached(uint waypointId, uint pathId)
    {
        switch (waypointId)
        {
            case 26:
                Talk(Texts.SAY_OOX_AMBUSH);
                break;
            case 43:
                Talk(Texts.SAY_OOX_AMBUSH);
                break;
            case 64:
                Talk(Texts.SAY_OOX_END);
                Player player = GetPlayerForEscort();
                if (player != null)
                    player.GroupEventHappens(QuestIDS.QUEST_RESQUE_OOX_09, me);
                break;
        }
    }

    public override void WaypointStarted(uint pointId, uint pathId)
    {
        switch (pointId)
        {
            case 27:
                for (byte i = 0; i < 3; ++i)
                {
                    Position dst = me.GetRandomPoint(Positions.src1[0], 7.0f);
                    DoSummon(CreatureIDS.NPC_MARAUDING_OWL, dst, TimeSpan.FromMilliseconds(25000), TempSummonType.CorpseTimedDespawn);
                }
                break;
            case 44:
                for (byte i = 0; i < 3; ++i)
                {
                    Position dst = me.GetRandomPoint(Positions.src2[0], 7.0f);
                    me.SummonCreature(CreatureIDS.NPC_VILE_AMBUSHER, dst, TempSummonType.CorpseTimedDespawn, TimeSpan.FromMilliseconds(25000));
                }
                break;
        }
    }
}

[Script]
class npc_sharpbeak : ScriptedAI
{
    public npc_sharpbeak(Creature creature) : base(creature)
    {
        Initialize();
    }

    int endPoint;

    void Initialize()
    {
        endPoint = 0;
    }

    public override void PassengerBoarded(Unit passenger, sbyte seatId, bool apply)
    {
        if (!apply)
            return;

        switch (me.GetEntry())
        {
            case CreatureIDS.NPC_SHARPBEAK_CAMP:
                me.GetMotionMaster().MoveSmoothPath((uint)Positions.campPath.Length, Positions.campPath, Positions.campPath.Length, false);
                endPoint = Positions.campPath.Length;
                break;
            case CreatureIDS.NPC_SHARPBEAK_JINTHAALOR:
                me.GetMotionMaster().MoveSmoothPath((uint)Positions.jinthaalorPath.Length, Positions.jinthaalorPath, Positions.jinthaalorPath.Length, false, true);
                endPoint = Positions.jinthaalorPath.Length;
                break;
        }
    }

    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        if (type == MovementGeneratorType.Effect && id == endPoint)
            DoCast(SpellIDS.SPELL_EJECT_ALL_PASSENGERS);
    }
}

[Script]
class npc_trained_razorbeak : ScriptedAI
{
    public npc_trained_razorbeak(Creature creature) : base(creature) { }

    public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
    {
        if (spellInfo.Id != SpellIDS.SPELL_FEED_RAZORBEAK)
            return;

        Player player = caster.ToPlayer();
        if (player && player.GetQuestStatus(QuestIDS.QUEST_RAZORBEAKFRIENDS) == QuestStatus.Incomplete)
            player.KilledMonsterCredit(CreatureIDS.NPC_RAZORBEAK_CREDIT, ObjectGuid.Empty);
    }
}

[Script]
class spell_tiki_torch : SpellScript
{
    void HandleScript(uint effIndex)
    {
        Player player = GetCaster().ToPlayer();
        if (player != null)
        {
            Creature target = GetHitCreature();
            if (target != null)
            {
                player.RewardPlayerAndGroupAtEvent(CreatureIDS.NPC_MASK_BURNT_CREDIT, GetCaster());
                target.DisappearAndDie();
            }
        }
    }

    void SelectTarget(ref WorldObject target)
    {
        target = GetCaster().FindNearestCreature(CreatureIDS.NPC_MASK_BURNT_CREDIT, 15.0f, true);
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleScript, 0, SpellEffectName.Dummy));
        OnObjectTargetSelect.Add(new ObjectTargetSelectHandler(SelectTarget, 0, Targets.UnitNearbyEntry));
    }
}

[Script]
class spell_ritual_of_shadra : SpellScript
{
    void SelectTarget(ref WorldObject target)
    {
        target = GetCaster().FindNearestCreature(CreatureIDS.NPC_SHADRA_NW_ALTAR_BUNNY, 15.0f, true);
        if (!target)
            target = GetCaster().FindNearestCreature(CreatureIDS.NPC_SHADRA_SW_ALTAR_BUNNY, 15.0f, true);
        if (!target)
            target = GetCaster().FindNearestCreature(CreatureIDS.NPC_SHADRA_E_ALTAR_BUNNY, 15.0f, true);
    }

    SpellCastResult CheckRequirement()
    {
        Creature northwest = GetCaster().FindNearestCreature(CreatureIDS.NPC_SHADRA_NW_ALTAR_BUNNY, 15.0f, true);
        Creature southwest = GetCaster().FindNearestCreature(CreatureIDS.NPC_SHADRA_SW_ALTAR_BUNNY, 15.0f, true);
        Creature east = GetCaster().FindNearestCreature(CreatureIDS.NPC_SHADRA_E_ALTAR_BUNNY, 15.0f, true);

        if (!northwest && !southwest && !east)
            return SpellCastResult.IncorrectArea;
        return SpellCastResult.Success;
    }

    void HandleDummy(uint effIndex)
    {
        Creature hitCreature = GetHitCreature();
        Player player = GetCaster().ToPlayer();
        if (!hitCreature || !player)
            return;

        switch (hitCreature.GetEntry())
        {
            case CreatureIDS.NPC_SHADRA_NW_ALTAR_BUNNY:
                player.RewardPlayerAndGroupAtEvent(hitCreature.GetEntry(), GetCaster());
                break;
            case CreatureIDS.NPC_SHADRA_SW_ALTAR_BUNNY:
                player.RewardPlayerAndGroupAtEvent(hitCreature.GetEntry(), GetCaster());
                break;
            case CreatureIDS.NPC_SHADRA_E_ALTAR_BUNNY:
                player.RewardPlayerAndGroupAtEvent(hitCreature.GetEntry(), GetCaster());
                break;
            default:
                break;
        }
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.Dummy));
        OnObjectTargetSelect.Add(new ObjectTargetSelectHandler(SelectTarget, 0, Targets.UnitNearbyEntry));
        OnCheckCast.Add(new CheckCastHandler(CheckRequirement));
    }
}