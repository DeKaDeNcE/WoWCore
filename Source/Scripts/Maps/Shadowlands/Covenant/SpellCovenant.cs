// Copyright (c) CypherCore <http://github.com/CypherCore> All rights reserved.
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
using Game.Entities;
using Game.Scripting;
using Game.Spells;

namespace Scripts.Maps.Shadowlands.Covenant;

    struct SpellIds
    {
        public const uint SulfuricEmissionCooldownAura = 347684;
        public const uint SuperiorTacticsCooldownAura = 332926;
    }

    [Script] // 323916 - Sulfuric Emission
    class spell_soulbind_sulfuric_emission : AuraScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.SulfuricEmissionCooldownAura);
        }

        bool CheckProc(AuraEffect aurEff, ProcEventInfo procInfo)
        {
            if (!procInfo.GetProcTarget().HealthBelowPct(aurEff.GetAmount()))
                return false;

            if (procInfo.GetProcTarget().HasAura(SpellIds.SulfuricEmissionCooldownAura))
                return false;

            return true;
        }

        public override void Register()
        {
            DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckProc, 0, AuraType.ProcTriggerSpell));
        }
    }

    [Script] // 332753 - Superior Tactics
    class spell_soulbind_superior_tactics : AuraScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.SuperiorTacticsCooldownAura);
        }

        bool CheckProc(AuraEffect aurEff, ProcEventInfo procInfo)
        {
            if (GetTarget().HasAura(SpellIds.SuperiorTacticsCooldownAura))
                return false;

            // only dispels from friendly targets count
            if (procInfo.GetHitMask().HasFlag(ProcFlagsHit.Dispel) && !procInfo.GetTypeMask().HasFlag(ProcFlags.DealHelpfulAbility | ProcFlags.DealHelpfulSpell | ProcFlags.DealHelpfulPeriodic))
                return false;

            return true;
        }

        public override void Register()
        {
            DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckProc, 0, AuraType.ProcTriggerSpell));
        }
    }