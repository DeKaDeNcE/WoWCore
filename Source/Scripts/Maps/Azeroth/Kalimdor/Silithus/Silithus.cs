// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game;

namespace Scripts.Maps.Azeroth.Kalimdor.Silithus;

struct ZoneAreaIds
{
    public const uint Silithus = 1377;
    public const uint SilithusTheWound = 9310;    
}

struct SpellIds
{
    public const uint TimeTravelling6 = 264606;
    public const uint FadetoBlackINSTANT2 = 129809;
}

struct QuestIds
{
    public const uint ADyingWorld1 = 53028;
}

struct GossipOption
{
    public const string OptionPast = "Can you show me what Silithus was like before the Wound in the World?";
    public const string OptionPresent = "Can you return me back to the present time?";
}

[Script]
class player_start_silithus : PlayerScript
{
    public player_start_silithus() : base("player_start_silithus") { }

    public override void OnLogin(Player player, bool firstLogin)
    {
        if (player.GetZoneId() == (uint)ZoneAreaIds.Silithus || player.GetZoneId() == (uint)ZoneAreaIds.SilithusTheWound)
            if (!player.HasAura((uint)SpellIds.TimeTravelling6))
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.SilithusTheWound);
            else
                PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.SilithusTheWound);
    }

    // TODO: replace with event on change zone this is a hackfix when you exit zone and enter to zone
    public override void OnUpdate(Player player, uint diff)
    {
        if (timer <= diff)
        {
            if (player.GetZoneId() == (uint)ZoneAreaIds.Silithus || player.GetZoneId() == (uint)ZoneAreaIds.SilithusTheWound)
                if (!player.HasAura((uint)SpellIds.TimeTravelling6))
                    PhasingHandler.AddVisibleMapId(player, (int)MapIds.SilithusTheWound);
                else
                    PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.SilithusTheWound);

            timer = 1000;
        }
        else timer -= diff;

    }

    uint timer = 1000;
}

[Script]
class npc_zidormi_silithus : CreatureAI
{
    public npc_zidormi_silithus(Creature creature) : base(creature) { }

    public override bool OnGossipHello(Player player)
    {
        if (player.GetQuestStatus(QuestIds.ADyingWorld1) == QuestStatus.None || player.GetQuestStatus(QuestIds.ADyingWorld1) == QuestStatus.Incomplete)
        {
            if (player.HasAura(SpellIds.TimeTravelling6))
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
            if (player.GetZoneId() == ZoneAreaIds.Silithus || player.GetZoneId() == ZoneAreaIds.SilithusTheWound)
            {
                player.CastSpell(player, SpellIds.FadetoBlackINSTANT2, true);
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.SilithusTheWound);

                if (player.HasAura((uint)SpellIds.TimeTravelling6))
                    player.RemoveAurasDueToSpell(SpellIds.TimeTravelling6);
            }
        }
        else if (action == eTradeskill.GossipActionInfoDef + 1)
        {
            player.CastSpell(player, SpellIds.FadetoBlackINSTANT2, true);
            player.CastSpell(player, SpellIds.TimeTravelling6, true);
            PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.SilithusTheWound);
        }

        player.CloseGossipMenu();

        return true;
    }
}