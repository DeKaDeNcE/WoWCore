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

// Demon Hunter Starting Zone

using Framework.Constants;
using Game.Entities;
using Game.Scripting;

namespace Scripts.Maps.Azeroth.BrokenIsles.Mardum;

struct QuestIds
{
    public const uint TheInvasionBegins          = 40077;
}

struct SpellIds
{
    public const uint PhaseQuestZoneSpecific01   = 193525;
    public const uint StartDemonHuntersPlayScene = 193525;
}

[Script]
class player_start_demon_hunters : PlayerScript
{
    public player_start_demon_hunters() : base("player_start_demon_hunters") { }

    public override void OnLogin(Player player, bool firstLogin)
    {
        if (player.GetClass() == Class.DemonHunter && player.GetZoneId() == (uint)AreaIds.MardumtheShatteredAbyss && firstLogin)
            player.RemoveAurasDueToSpell(SpellIds.PhaseQuestZoneSpecific01);
    }

    public override void OnUpdate(Player player, uint diff)
    {
        if (timer <= diff)
        {
            if (player.GetClass() == Class.DemonHunter && player.GetZoneId() == (uint)AreaIds.MardumtheShatteredAbyss && player.GetPositionY() < 3281.0f)
                if (!player.HasAura(SpellIds.StartDemonHuntersPlayScene) && !player.HasAura(SpellIds.PhaseQuestZoneSpecific01))
                    if (player.GetQuestStatus(QuestIds.TheInvasionBegins) == QuestStatus.None)
                    {
                        player.CastSpell(player, SpellIds.StartDemonHuntersPlayScene, true);
                        Conversation.CreateConversation(705, player, player.GetPosition(), player.GetGUID());
                    }

            timer = 1000;
        }
        else timer -= diff;
    }

    uint timer = 1000;
}