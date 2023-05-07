// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game;

namespace Scripts.Maps.Azeroth.EasternKingdoms.TirisfalGlades;

    struct GossipOption
{
    public const string OptionPast = "Show me Tirisfal Glades before the destruction.";
    public const string OptionPresent = "Take me back to the present.";
}

struct PhaseIds
{
    //phase ids acording to wowhead.. but still we will need sniffs
    public const uint PhaseBforeBattle = 0;
    public const uint PhaseAfterBattle = 11519;
}

struct QuestIds
{
    public const uint TheBattleforLordaeronA = 51795;
    public const uint TheBattleforLordaeronH = 51796;
}

struct ZoneIDs
{
    public const uint TirisfalGlades = 85;
}

struct SpellIDs
{
    public const uint TimeTravelling = 276824; //tirisfal glades zidormi
    public const uint FadetoBlackINSTANT2 = 129809; //visual effect spell
}

[Script]
class player_start_tirisfalglades : PlayerScript
{
    public player_start_tirisfalglades() : base("player_start_dustwallow") { }


    public override void OnLogin(Player player, bool firstLogin)
    {
        if (player.GetZoneId() == ZoneIDs.TirisfalGlades)
            if (!player.HasAura(SpellIDs.TimeTravelling))
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.LordaeronBlight);
            else
                PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.LordaeronBlight);
    }


    public override void OnUpdateZone(Player player, uint newZone, uint newArea)
    {
        if (player.GetZoneId() == ZoneIDs.TirisfalGlades)
            if (!player.HasAura(SpellIDs.TimeTravelling))
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.LordaeronBlight);
            else
                PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.LordaeronBlight);
    }

}

[Script]
class npc_zidormi_tirisfalglades : CreatureAI
{
    public npc_zidormi_tirisfalglades(Creature creature) : base(creature) { }

    public override bool OnGossipHello(Player player)
    {
        if (player.GetLevel() > 49)
        {
            if (player.HasAura(SpellIDs.TimeTravelling))
                player.AddGossipItem(GossipOptionNpc.None, GossipOption.OptionPresent, eTradeskill.GossipSenderMain, eTradeskill.GossipActionInfoDef + 0);
            else
                player.AddGossipItem(GossipOptionNpc.None, GossipOption.OptionPast, eTradeskill.GossipSenderMain, eTradeskill.GossipActionInfoDef + 1);

            player.SendGossipMenu(player.GetGossipTextId(me), me.GetGUID());

            return true;
        }

        return false;
    }

    public override bool OnGossipSelect(Player player, uint menuId, uint gossipListId)
    {
        uint action = player.PlayerTalkClass.GetGossipOptionAction(gossipListId);
        player.PlayerTalkClass.ClearMenus();

        if (action == eTradeskill.GossipActionInfoDef + 0)
        {
            if (player.GetZoneId() == ZoneIDs.TirisfalGlades)
            {
                player.CastSpell(player, SpellIDs.FadetoBlackINSTANT2, true);
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.LordaeronBlight);

                if (player.HasAura(SpellIDs.TimeTravelling))
                    player.RemoveAurasDueToSpell(SpellIDs.TimeTravelling);
            }
        }
        else if (action == eTradeskill.GossipActionInfoDef + 1)
        {
            player.CastSpell(player, SpellIDs.FadetoBlackINSTANT2, true);
            player.CastSpell(player, SpellIDs.TimeTravelling, true);
            PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.LordaeronBlight);
        }

        player.CloseGossipMenu();

        return true;
    }

    //to do..  also change map phase and map when pressing "M" might be needed a sniff....
}