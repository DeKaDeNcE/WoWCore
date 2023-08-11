// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.DataStorage;
using Game.Entities;
using Game.Groups;
using Game.Scripting;
using Game;

namespace Scripts.Maps.Azeroth.EasternKingdoms.BlastedLands;

struct GossipOption
{
    public const string OptionPast = "Show me Blasted Lands before the invasion.";
    public const string OptionPresent = "Take me back to the present.";
}

struct SpellIds
{
    public const uint TimeTravelBlastedLands = 176111;
    public const uint FadetoBlackINSTANT2 = 129809;

    public const uint SPELL_TIME_SHIFT = 176111;

    public const uint SPELL_ASHRAN_TELE = 176809;
    public const uint SPELL_ASHRAN_HORDE = 173143;  //5352.16 Y: -3944.96 Z: 32.7331 O: 3.921144
    public const uint SPELL_ASHRAN_ALLIANCE = 167220;
    public const uint SPELL_TELE_OUT_ALLIANCE = 167221; //Teleport Out: Alliance / 2308.57 Y: 447.469 Z: 5.11977 O: 2.199202
    public const uint SPELL_TELE_OUT_HORDE = 167220; //Teleport Out: Alliance

    public const uint SPELL_TELE_INTRO = 167771;

    public const uint SPELL_TELEPORT_SINGLE = 12885;
    public const uint SPELL_TELEPORT_SINGLE_IN_GROUP = 13142;
    public const uint SPELL_TELEPORT_GROUP = 27686;
}

struct QuestIDS
{
    public const uint QuestStartDraenor = 34398;
    public const uint QuestStartDraenorII = 36881;
    public const uint QuestDarkPortal = 34398;
    public const uint QuestAzerothsLastStand = 35933;
    public const uint QuestOnslaughtEnd = 34392;
    public const uint QuestThePortalPower = 34393;
    public const uint QuestBledDryAlly = 35240;
    public const uint QuestBledDryHorde = 34421;
    public const uint QuestCostOfWar = 34420;
    public const uint QuestBlazeOfGlory = 34422;
    public const uint QuestAltarAltercation = 34423;
    public const uint QuestAPotentialAlly = 34478;
    public const uint QuestAPotentialAllyHorde = 34427;
    public const uint QuestKargatharProvingGrounds = 34425;
    public const uint QuestKillYourHundred = 34429;
    public const uint QuestMastersOfShadowAlly = 34431;
    public const uint QuestMastersOfShadowHorde = 34737;
    public const uint QuestKeliDanTheBreakerHorde = 34741;
    public const uint QuestKeliDanTheBreakerAlly = 34436;
    public const uint QuestTheGunpowderPlot = 34987;
    public const uint QuestATasteOfIron = 34445;
    public const uint QuestTheHomeStretchHorde = 34446;
    public const uint QuestTheHomeStretchAlly = 35884;
    public const uint QuestYrelHorde = 34740;
    public const uint QuestYrelTanaan = 34434;
    public const uint QuestABattleToPrepareAlly = 35019;
    public const uint QuestABattleToPrepareHorde = 35005;
    public const uint QuestTheBattleOfTheForge = 34439;
    public const uint QuestTakingATripToTheTopOfTheTank = 35747;
    public const uint QuestGaNarOfTheFrostwolf = 34442;
    public const uint QuestTheProdigalFrostwolf = 34437;

    public const uint QUEST__A = 35884;
    public const uint QUEST__H = 34446;
}


struct ZoneAreaIds
{
    public const uint BlastedLands = 4;
}

[Script]
class player_zone_blasted_land : PlayerScript
{
    public player_zone_blasted_land() : base("player_zone_blasted_land") { }

    public override void OnLogin(Player player, bool firstLogin)
    {
        if (player.GetZoneId() == ZoneAreaIds.BlastedLands)
            if (!player.HasAura(SpellIds.TimeTravelBlastedLands))
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.BlastedLandsPhase);
            else
                PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.BlastedLandsPhase);
    }

    public override void OnUpdate(Player player, uint diff)
    {
        if (timer <= diff)
        {
            if (player.GetZoneId() == ZoneAreaIds.BlastedLands)
                if (!player.HasAura(SpellIds.TimeTravelBlastedLands))
                    PhasingHandler.AddVisibleMapId(player, (int)MapIds.BlastedLandsPhase);
                else
                    PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.BlastedLandsPhase);

            timer = 1000;
        }
        else timer -= diff;
    }

    uint timer = 1000;

}

[Script]
class npc_zidormi_blastedlands : CreatureAI
{
    public npc_zidormi_blastedlands(Creature creature) : base(creature) { }

    public override bool OnGossipHello(Player player)
    {
        if (player.GetLevel() > 49)
        {
            if (player.HasAura(SpellIds.TimeTravelBlastedLands))
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
            if (player.GetZoneId() == (uint)ZoneAreaIds.BlastedLands)
            {
                player.CastSpell(player, SpellIds.FadetoBlackINSTANT2, true);
                PhasingHandler.AddVisibleMapId(player, (int)MapIds.BlastedLandsPhase);

                if (player.HasAura(SpellIds.TimeTravelBlastedLands))
                    player.RemoveAurasDueToSpell(SpellIds.TimeTravelBlastedLands);
            }
        }
        else if (action == eTradeskill.GossipActionInfoDef + 1)
        {
            player.CastSpell(player, SpellIds.FadetoBlackINSTANT2, true);
            player.CastSpell(player, SpellIds.TimeTravelBlastedLands, true);
            PhasingHandler.RemoveVisibleMapId(player, (int)MapIds.BlastedLandsPhase);
        }

        player.CloseGossipMenu();

        return true;
    }
}

[Script]
class npc_archmage_khadgar_gossip : CreatureAI
{
    public npc_archmage_khadgar_gossip(Creature creature) : base(creature) { }

    public override bool OnGossipSelect(Player player, uint menuId, uint gossipListId)
    {
        if (player.GetQuestStatus(QuestIDS.QuestStartDraenor) == QuestStatus.None || player.GetQuestStatus(QuestIDS.QuestStartDraenorII) == QuestStatus.None)
        {
            return true;
        }

        if (player.GetQuestStatus(QuestIDS.QuestTheHomeStretchHorde) == QuestStatus.Rewarded)
        {
            player.TeleportTo(1116, 5538.213379f, 5015.2690f, 13.0f, player.GetOrientation());
            return true;
        }
        else if (player.GetQuestStatus(QuestIDS.QuestTheHomeStretchAlly) == QuestStatus.Rewarded)
        {
            player.TeleportTo(1116, 2308.9621f, 454.9409f, 6.0f, player.GetOrientation());
            return true;
        }

        if (player.GetQuestStatus(QuestIDS.QuestStartDraenor) == QuestStatus.Incomplete || player.GetQuestStatus(QuestIDS.QuestStartDraenorII) == QuestStatus.Incomplete)
        {
            player.SendMovieStart(199);
            player.TeleportTo(1265, 4066.7370f, -2381.9917f, 94.858f, 2.90f);

            if (player.HasQuest(QuestIDS.QuestStartDraenor))
            {
                player.CompleteQuest(QuestIDS.QuestStartDraenor);
            }
            else if (player.HasQuest(QuestIDS.QuestStartDraenorII))
            {
                player.CompleteQuest(QuestIDS.QuestStartDraenorII);
            }
            return true;
        }

        if ((player.GetQuestStatus(QuestIDS.QuestStartDraenor) == QuestStatus.Incomplete ||
            player.GetQuestStatus(QuestIDS.QuestStartDraenor) == QuestStatus.Rewarded) &&
            player.GetQuestStatus(QuestIDS.QuestThePortalPower) != QuestStatus.Rewarded)
        {

            player.TeleportTo(1265, 4066.7370f, -2381.9917f, 94.858f, 2.90f);
        }
        else if (player.GetQuestStatus(QuestIDS.QuestThePortalPower) == QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestBlazeOfGlory) != QuestStatus.Rewarded)
        {
            player.TeleportTo(1265, 3949.97f, -2515.91f, 69.756538f, 2.454291f);
        }
        else if (player.GetQuestStatus(QuestIDS.QuestBlazeOfGlory) == QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestAltarAltercation) != QuestStatus.Rewarded)
        {
            player.TeleportTo(1265, 3841.56f, -2775.08f, 93.84f, 5.34f);
        }
        else if (player.GetQuestStatus(QuestIDS.QuestAltarAltercation) == QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestKargatharProvingGrounds) != QuestStatus.Rewarded)
        {
            player.TeleportTo(1265, 4227.83f, -2810.83f, 17.19f, 6.15f);
        }
        else if (player.GetQuestStatus(QuestIDS.QuestKargatharProvingGrounds) == QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestKeliDanTheBreakerHorde) != QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestKeliDanTheBreakerAlly) != QuestStatus.Rewarded)
        {
            player.TeleportTo(1265, 4509.39f, -2632.79f, 1.84f, 0.0f);
        }
        else if (player.GetQuestStatus(QuestIDS.QuestKeliDanTheBreakerHorde) == QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestKeliDanTheBreakerAlly) == QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestTheBattleOfTheForge) != QuestStatus.Rewarded)
        {
            player.TeleportTo(1265, 4616.05f, -2245.06f, 14.75f, 1.89f);
        }
        else if (player.GetQuestStatus(QuestIDS.QuestTheBattleOfTheForge) == QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestTheHomeStretchHorde) != QuestStatus.Rewarded &&
                 player.GetQuestStatus(QuestIDS.QuestTheHomeStretchAlly) != QuestStatus.Rewarded)
        {
            player.TeleportTo(1265, 4055.08f, -2018.38f, 73.20f, 3.14f);
        }

        return true;
    }
}

[Script]
class at_wod_dark_portal : AreaTriggerScript
{
    public at_wod_dark_portal() : base("at_wod_dark_portal") { }

    public override bool OnTrigger(Player player, AreaTriggerRecord trigger)
    {
        if (player.GetLevel() < 20 || player.HasAura(SpellIds.SPELL_TIME_SHIFT)) //i guess 20 since at level 20 you unlock chromie..
            return false;       //tele to outlend

        if (player.GetQuestStatus(QuestIDS.QUEST__A) == QuestStatus.Rewarded ||
            player.GetQuestStatus(QuestIDS.QUEST__H) == QuestStatus.Rewarded)
        {
            player.CastSpell(player, player.GetTeam() == Team.Horde ? SpellIds.SPELL_TELE_OUT_HORDE : SpellIds.SPELL_TELE_OUT_ALLIANCE);
        }
        else
            player.CastSpell(player, SpellIds.SPELL_TELE_INTRO);

        return true;
    }
}

[Script]
class spell_razelikh_teleport_group : SpellScript
{
    void HandleScriptEffect(uint effIndex)
    {
        Player player = GetHitPlayer();
        if (player != null)
        {
            Group group = player.GetGroup();
            if (group != null)
            {
                for (GroupReference groupRef = group.GetFirstMember(); groupRef != null; groupRef = groupRef.Next())
                {
                    Player member = groupRef.GetSource();
                    if (member)
                        if (member.IsWithinDistInMap(player, 20.0f) && !member.IsDead())
                            member.CastSpell(member, SpellIds.SPELL_TELEPORT_SINGLE_IN_GROUP, true);
                }
            }
            else
                player.CastSpell(player, SpellIds.SPELL_TELEPORT_SINGLE, true);
        }
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleScriptEffect, 1, SpellEffectName.ScriptEffect));
    }
}