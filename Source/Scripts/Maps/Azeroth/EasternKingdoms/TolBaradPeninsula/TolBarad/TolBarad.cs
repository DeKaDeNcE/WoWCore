// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game;
using System.Collections.Generic;

namespace Scripts.Maps.Azeroth.EasternKingdoms.TolBaradPeninsula.TolBarad;

struct SpellIDS
{
    public const uint SPELL_CHANNEL_SPIRIT_HEAL = 22011;
}

struct GossipOptions
{
    public const uint GOSSIP_OPTION_ID_SLAGWORKS = 0;
    public const uint GOSSIP_OPTION_ID_IRONCLAD_GARRISON = 1;
    public const uint GOSSIP_OPTION_ID_WARDENS_VIGIL = 2;
    public const uint GOSSIP_OPTION_ID_EAST_SPIRE = 3;
    public const uint GOSSIP_OPTION_ID_WEST_SPIRE = 4;
    public const uint GOSSIP_OPTION_ID_SOUTH_SPIRE = 5;
}

struct GraveyardAreaID
{
    //Tol Barad
    public const uint TB_GY_BARADIN_HOLD = 1789;
    public const uint TB_GY_IRONCLAD_GARRISON = 1783;
    public const uint TB_GY_WARDENS_VIGIL = 1785;
    public const uint TB_GY_SLAGWORKS = 1787;
    public const uint TB_GY_WEST_SPIRE = 1784;
    public const uint TB_GY_SOUTH_SPIRE = 1786;
    public const uint TB_GY_EAST_SPIRE = 1788;
    public const uint BATTLEFIELD_TB_GRAVEYARD_MAX = 7;
}

[Script]
class npc_tb_spirit_guide : ScriptedAI
{
    public npc_tb_spirit_guide(Creature creature) : base(creature) { }

    public override void UpdateAI(uint diff)
    {
        if (!me.HasUnitState(UnitState.Casting))
            DoCast(me, SpellIDS.SPELL_CHANNEL_SPIRIT_HEAL);
    }

    public override bool OnGossipSelect(Player player, uint menuId, uint gossipListId)
    {

        player.PlayerTalkClass.SendCloseGossip();

        uint areaId = 0;
        switch (gossipListId)
        {
            case GossipOptions.GOSSIP_OPTION_ID_SLAGWORKS:
                areaId = GraveyardAreaID.TB_GY_SLAGWORKS;
                break;
            case GossipOptions.GOSSIP_OPTION_ID_IRONCLAD_GARRISON:
                areaId = GraveyardAreaID.TB_GY_IRONCLAD_GARRISON;
                break;
            case GossipOptions.GOSSIP_OPTION_ID_WARDENS_VIGIL:
                areaId = GraveyardAreaID.TB_GY_WARDENS_VIGIL;
                break;
            case GossipOptions.GOSSIP_OPTION_ID_EAST_SPIRE:
                areaId = GraveyardAreaID.TB_GY_EAST_SPIRE;
                break;
            case GossipOptions.GOSSIP_OPTION_ID_WEST_SPIRE:
                areaId = GraveyardAreaID.TB_GY_WEST_SPIRE;
                break;
            case GossipOptions.GOSSIP_OPTION_ID_SOUTH_SPIRE:
                areaId = GraveyardAreaID.TB_GY_SOUTH_SPIRE;
                break;
            default:
                return true;
        }

        WorldSafeLocsEntry safeLoc = Global.ObjectMgr.GetWorldSafeLoc(areaId);
        if (safeLoc != null)
            player.TeleportTo(safeLoc.Loc);

        return true;
    }
}

[Script]
class spell_siege_cannon : SpellScript
{
    void SelectRandomTarget(List<WorldObject> targets)
    {
        if (!targets.Empty())
        {
            WorldObject target = targets.SelectRandom();
            targets.Clear();
            targets.Add(target);
        }
    }

    public override void Register()
    {
        OnObjectAreaTargetSelect.Add(new ObjectAreaTargetSelectHandler(SelectRandomTarget, 0, Targets.UnitSrcAreaEntry));
    }
}