// Copyright (c) CypherCore <http://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable InvertIf

using System.Collections.Generic;
using Framework.Constants;
using Game.Entities;
using Game.Scripting;
using Game.Spells;

using static Global;

namespace Scripts.Maps.Shadowlands.Torghast;

    struct SpellIds
    {
        public const uint DoorOfShadows = 300728;
        public const uint MageBlink = 1953;
        public const uint MageShimmer = 212653;
    }

    [Script] // 300771 - Blade of the Lifetaker
    class spell_torghast_blade_of_the_lifetaker : AuraScript
    {
        void HandleProc(AuraEffect aurEff, ProcEventInfo procInfo)
        {
            PreventDefaultAction();

            procInfo.GetActor().CastSpell(procInfo.GetProcTarget(), aurEff.GetSpellEffectInfo().TriggerSpell, new CastSpellExtraArgs(aurEff).AddSpellBP0((int)GetTarget().CountPctFromMaxHealth(aurEff.GetAmount())).SetTriggeringSpell(procInfo.GetProcSpell()));
        }

        public override void Register()
        {
            OnEffectProc.Add(new EffectProcHandler(HandleProc, 0, AuraType.ProcTriggerSpell));
        }
    }

    [Script] // 300796 - Touch of the Unseen
    class spell_torghast_touch_of_the_unseen : AuraScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.DoorOfShadows);
        }

        bool CheckProc(ProcEventInfo procInfo)
        {
            return procInfo.GetSpellInfo() != null && procInfo.GetSpellInfo().Id == SpellIds.DoorOfShadows;
        }

        void HandleProc(AuraEffect aurEff, ProcEventInfo procInfo)
        {
            PreventDefaultAction();

            procInfo.GetActor().CastSpell(procInfo.GetProcTarget(), aurEff.GetSpellEffectInfo().TriggerSpell, new CastSpellExtraArgs(aurEff).AddSpellBP0((int)GetTarget().CountPctFromMaxHealth(aurEff.GetAmount())).SetTriggeringSpell(procInfo.GetProcSpell()));
        }

        public override void Register()
        {
            DoCheckProc.Add(new CheckProcHandler(CheckProc));
            OnEffectProc.Add(new EffectProcHandler(HandleProc, 0, AuraType.ProcTriggerSpell));
        }
    }

    [Script] // 305060 - Yel'Shir's Powerglove
    class spell_torghast_yelshirs_powerglove : SpellScript
    {
        void HandleEffect(uint effIndex)
        {
            SpellInfo triggeringSpell = GetTriggeringSpell();

            if (triggeringSpell != null)
            {
                Aura triggerAura = GetCaster().GetAura(triggeringSpell.Id);

                if (triggerAura != null)
                    SetEffectValue(GetEffectValue() * triggerAura.GetStackAmount());
            }
        }

        public override void Register()
        {
            OnEffectLaunchTarget.Add(new EffectHandler(HandleEffect, 0, SpellEffectName.SchoolDamage));
        }
    }

    [Script] // 321706 - Dimensional Blade
    class spell_torghast_dimensional_blade : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.MageBlink, SpellIds.MageShimmer);
        }

        void FilterTargets(List<WorldObject> targets)
        {
            if (!targets.Empty())
            {
                GetCaster().GetSpellHistory().RestoreCharge(SpellMgr.GetSpellInfo(SpellIds.MageBlink, Difficulty.None).ChargeCategoryId);
                GetCaster().GetSpellHistory().RestoreCharge(SpellMgr.GetSpellInfo(SpellIds.MageShimmer, Difficulty.None).ChargeCategoryId);
            }

            // filter targets by entry here and not with conditions table because we need to know if any enemy was hit for charge restoration, not just Mawrats
            targets.RemoveAll(target => {
                switch (target.GetEntry())
                {
                    case 151353: // Mawrat
                    case 179458: // Protective Mawrat
                    case 154030: // Oddly Large Mawrat
                    case 169871: // Hungry Mawrat
                        return false;
                }

                return true;
            });
        }

        public override void Register()
        {
            OnObjectAreaTargetSelect.Add(new ObjectAreaTargetSelectHandler(FilterTargets, 1, Targets.UnitDestAreaEnemy));
        }
    }