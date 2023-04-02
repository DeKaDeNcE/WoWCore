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

using Framework.Constants;
using Game.Scripting;
using Game.Spells;

namespace Scripts.Maps.Azeroth.TheMaelstrom.Deepholm;

struct SpellIds
{
    // Spells
    public const uint SpeakToFallenSpiritMale   = 79658;
    public const uint SpeakToFallenSpiritFemale = 94879;
}

struct CreatureIds
{
    public const uint SlainCrewMemberMale       = 42681;
}

//https://www.wowhead.com/quest=26248/all-our-friends-are-dead

[Script]
class spell_spirit_totem : SpellScript // 79614 - Spirit Totem
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.SpeakToFallenSpiritMale, SpellIds.SpeakToFallenSpiritFemale);
    }

    void HandleHit(uint effIndex)
    {
        uint spellId = SpellIds.SpeakToFallenSpiritFemale;

        if (GetHitUnit().GetEntry() == CreatureIds.SlainCrewMemberMale)
            spellId = SpellIds.SpeakToFallenSpiritFemale;

        GetHitUnit().CastSpell(GetCaster(), spellId, true);
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleHit, 0, SpellEffectName.Dummy));
    }
}