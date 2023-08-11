// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game;

namespace Scripts.Maps.Azeroth.Kalimdor.DustwallowMarsh;

struct GossipOption
{
    public const string OptionPast = "Show me Theramore before the destruction.";
    public const string OptionPresent = "Take me back to the present.";
}

struct ZoneAreaIds
{
    public const uint DustwallowMarsh = 15;
}

struct SpellIds
{
    public const uint TimeTravelling = 276824; //tirisfal glades zidormi
    public const uint FadetoBlackINSTANT2 = 129809; //visual effect spell
}

struct AchievementIds
{
    public const uint TheramoreFallA = 7523;
    public const uint TheramoreFallH = 7524;
}

[Script]
class player_start_dustwallow : PlayerScript
{
    public player_start_dustwallow() : base("player_start_dustwallow") { }

    public override void OnLogin(Player player, bool firstLogin)
    {
        if (player.GetZoneId() == ZoneAreaIds.DustwallowMarsh)
            if (!player.HasAura(SpellIds.TimeTravelling))
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.RuinsofTheramore);
            else
                PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.RuinsofTheramore);
    }

    // TODO: replace with event on change zone this is a hackfix when you exit zone and enter to zone
    public override void OnUpdate(Player player, uint diff)
    {
        if (timer <= diff)
        {
            if (player.GetZoneId() == ZoneAreaIds.DustwallowMarsh)
                if (!player.HasAura(SpellIds.TimeTravelling))
                    PhasingHandler.AddVisibleMapId(player, (int)MapIds.RuinsofTheramore);
                else
                    PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.RuinsofTheramore);

            timer = 1000;
        }
        else timer -= diff;

    }

    uint timer = 1000;
}

[Script]
class npc_zidormi_dustwallow : CreatureAI
{
    public npc_zidormi_dustwallow(Creature creature) : base(creature) { }


    public override bool OnGossipHello(Player player)
    {
        if (player.HasAchieved(player.IsTeamAlliance() ? AchievementIds.TheramoreFallA : AchievementIds.TheramoreFallH))
        {
            if (player.HasAura(SpellIds.TimeTravelling))
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
            if (player.GetZoneId() == (uint)ZoneAreaIds.DustwallowMarsh)
            {
                player.CastSpell(player, (uint)SpellIds.FadetoBlackINSTANT2, true);
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.RuinsofTheramore);

                if (player.HasAura((uint)SpellIds.TimeTravelling))
                    player.RemoveAurasDueToSpell((uint)SpellIds.TimeTravelling);
            }
        }
        else if (action == eTradeskill.GossipActionInfoDef + 1)
        {
            player.CastSpell(player, (uint)SpellIds.TimeTravelling, true);
            PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.RuinsofTheramore);
        }

        player.CloseGossipMenu();

        return true;
    }
}