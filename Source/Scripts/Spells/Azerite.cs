﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable InvertIf

using System.Collections.Generic;
using Framework.Constants;
using Game.Entities;
using Game.Networking.Packets;
using Game.Scripting;
using Game.Spells;

namespace Scripts.Spells.Azerite;

struct SpellIds
{
    // StrengthInNumbers
    public const uint StrengthInNumbersTrait       = 271546;
    public const uint StrengthInNumbersBuff        = 271550;
    // BlessedPortents
    public const uint BlessedPortentsTrait         = 267889;
    public const uint BlessedPortentsHeal          = 280052;
    // ConcentratedMending
    public const uint ConcentratedMendingTrait     = 267882;
    // BracingChill
    public const uint BracingChillTrait            = 267884;
    public const uint BracingChill                 = 272276;
    public const uint BracingChillHeal             = 272428;
    public const uint BracingChillSearchJumpTarget = 272436;
    // OrbitalPrecision
    public const uint MageFrozenOrb                = 84714;
    // BlurOfTalons
    public const uint HunterCoordinatedAssault     = 266779;
    // Tradewinds
    public const uint TradewindsAllyBuff           = 281844;
    //  BastionOfMight
    public const uint IgnorePain                   = 190456;
    // Echoing Blades
    public const uint EchoingBladesTrait           = 287649;
    // Hour of Reaping
    public const uint SoulBarrier                  = 263648;
}

[Script]
class spell_azerite_gen_aura_calc_from_2nd_effect_triggered_spell : AuraScript
{

    public override bool Validate(SpellInfo spellInfo)
    {
        return spellInfo.GetEffects().Count > 1 && ValidateSpellInfo(spellInfo.GetEffect(1).TriggerSpell);
    }

    void CalculateAmount(AuraEffect aurEff, ref int amount, ref bool canBeRecalculated)
    {
        Unit caster = GetCaster();

        if (caster != null)
        {
            AuraEffect trait = caster.GetAuraEffect(GetEffectInfo(1).TriggerSpell, 0);

            if (trait != null)
            {
                amount = trait.GetAmount();
                canBeRecalculated = false;
            }
        }
    }

    public override void Register()
    {
        DoEffectCalcAmount.Add(new EffectCalcAmountHandler(CalculateAmount, 0, AuraType.ModRating));
    }
}

[Script] // 270658 - Azerite Fortification
class spell_item_azerite_fortification : AuraScript
{
    bool CheckProc(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        Spell procSpell = eventInfo.GetProcSpell();

        if (!procSpell)
            return false;

        return procSpell.GetSpellInfo().HasAura(AuraType.ModStun) || procSpell.GetSpellInfo().HasAura(AuraType.ModRoot) || procSpell.GetSpellInfo().HasAura(AuraType.ModRoot2) || procSpell.GetSpellInfo().HasEffect(SpellEffectName.KnockBack);
    }

    public override void Register()
    {
        DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckProc, 0, AuraType.ProcTriggerSpell));
    }
}

[Script] // 271548 - Strength in Numbers
class spell_item_strength_in_numbers : SpellScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.StrengthInNumbersTrait, SpellIds.StrengthInNumbersBuff);
    }

    void TriggerHealthBuff()
    {
        AuraEffect trait = GetCaster().GetAuraEffect(SpellIds.StrengthInNumbersTrait, 0, GetCaster().GetGUID());

        if (trait != null)
        {
            long enemies = GetUnitTargetCountForEffect(0);

            if (enemies != 0)
            {
                CastSpellExtraArgs args = new(TriggerCastFlags.FullMask);
                args.AddSpellBP0(trait.GetAmount());
                args.AddSpellMod(SpellValueMod.AuraStack, (int)enemies);

                GetCaster().CastSpell(GetCaster(), SpellIds.StrengthInNumbersBuff, args);
            }
        }
    }

    public override void Register()
    {
        AfterHit.Add(new HitHandler(TriggerHealthBuff));
    }
}

[Script] // 271843 - Blessed Portents
class spell_item_blessed_portents : AuraScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.BlessedPortentsTrait, SpellIds.BlessedPortentsHeal);
    }

    void CheckProc(AuraEffect aurEff, DamageInfo dmgInfo, ref uint absorbAmount)
    {
        Unit target = GetTarget();
        Unit caster = GetCaster();

        if (target != null && caster != null)
        {
            if (target.HealthBelowPctDamaged(50, dmgInfo.GetDamage()))
            {
                AuraEffect trait = caster.GetAuraEffect(SpellIds.BlessedPortentsTrait, 0, caster.GetGUID());

                if (trait != null)
                {
                    CastSpellExtraArgs args = new(TriggerCastFlags.FullMask);
                    args.AddSpellBP0(trait.GetAmount());

                    caster.CastSpell(target, SpellIds.BlessedPortentsHeal, args);
                }
            }
            else PreventDefaultAction();
        }
        else PreventDefaultAction();
    }

    public override void Register()
    {
        OnEffectAbsorb.Add(new EffectAbsorbHandler(CheckProc, 0));
    }
}

[Script] // 272260 - Concentrated Mending
class spell_item_concentrated_mending : AuraScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.ConcentratedMendingTrait);
    }

    void RecalculateHealAmount(AuraEffect aurEff)
    {
        Unit caster = GetCaster();

        if (caster != null)
        {
            AuraEffect trait = caster.GetAuraEffect(SpellIds.ConcentratedMendingTrait, 0, caster.GetGUID());

            if (trait != null)
                aurEff.ChangeAmount(trait.GetAmount() * (int)aurEff.GetTickNumber());
        }
    }

    public override void Register()
    {
        OnEffectUpdatePeriodic.Add(new EffectUpdatePeriodicHandler(RecalculateHealAmount, 0, AuraType.PeriodicHeal));
    }
}

[Script] // 272276 - Bracing Chill
class spell_item_bracing_chill_proc : AuraScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.BracingChillTrait, SpellIds.BracingChillHeal, SpellIds.BracingChillSearchJumpTarget);
    }

    bool CheckHealCaster(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        return GetCasterGUID() == eventInfo.GetActor().GetGUID();
    }

    void HandleProc(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        Unit caster = eventInfo.GetActor();

        if (!caster)
            return;

        AuraEffect trait = caster.GetAuraEffect(SpellIds.BracingChillTrait, 0, caster.GetGUID());

        if (trait != null)
            caster.CastSpell(eventInfo.GetProcTarget(), SpellIds.BracingChillHeal, new CastSpellExtraArgs(TriggerCastFlags.FullMask).AddSpellBP0(trait.GetAmount()));

        if (GetStackAmount() > 1)
        {
            CastSpellExtraArgs args = new(TriggerCastFlags.FullMask);
            args.AddSpellMod(SpellValueMod.AuraStack, GetStackAmount() - 1);

            caster.CastSpell((CastSpellTargetArg)null, SpellIds.BracingChillSearchJumpTarget, args);
        }

        Remove();
    }

    public override void Register()
    {
        DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckHealCaster, 0, AuraType.Dummy));
        AfterEffectProc.Add(new EffectProcHandler(HandleProc, 0, AuraType.Dummy));
    }
}

[Script] // 272436 - Bracing Chill
class spell_item_bracing_chill_search_jump_target : SpellScript
{
    void FilterTargets(List<WorldObject> targets)
    {
        if (targets.Empty())
            return;

        List<WorldObject> copy = new (targets);
        copy.RandomResize(target => target.IsUnit() && !target.ToUnit().HasAura(SpellIds.BracingChill, GetCaster().GetGUID()), 1);

        if (!copy.Empty())
        {
            // found a preferred target, use that
            targets = new (copy);
            return;
        }

        WorldObject target = targets.SelectRandom();
        targets.Clear();
        targets.Add(target);
    }

    void MoveAura(uint effIndex)
    {
        Unit caster = GetCaster();

        if (caster != null)
            caster.CastSpell(GetHitUnit(), SpellIds.BracingChill, new CastSpellExtraArgs(TriggerCastFlags.FullMask).AddSpellMod(SpellValueMod.AuraStack, GetSpellValue().AuraStackAmount));
    }

    public override void Register()
    {
        OnObjectAreaTargetSelect.Add(new ObjectAreaTargetSelectHandler(FilterTargets, 0, Targets.UnitDestAreaAlly));
        OnEffectHitTarget.Add(new EffectHandler(MoveAura, 0, SpellEffectName.Dummy));
    }
}

[Script] // 272837 - Trample the Weak
class spell_item_trample_the_weak : AuraScript
{
    bool CheckHealthPct(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        return eventInfo.GetActor().GetHealthPct() > eventInfo.GetActionTarget().GetHealthPct();
    }

    public override void Register()
    {
        DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckHealthPct, 0, AuraType.ProcTriggerSpell));
    }
}

[Script] // 275514 - Orbital Precision
class spell_item_orbital_precision : AuraScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.MageFrozenOrb);
    }

    bool CheckFrozenOrbActive(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        return eventInfo.GetActor().GetAreaTrigger(SpellIds.MageFrozenOrb) != null;
    }

    public override void Register()
    {
        DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckFrozenOrbActive, 0, AuraType.ProcTriggerSpell));
    }
}

[Script] // 277966 - Blur of Talons
class spell_item_blur_of_talons : AuraScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.HunterCoordinatedAssault);
    }

    bool CheckCoordinatedAssaultActive(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        return eventInfo.GetActor().HasAura(SpellIds.HunterCoordinatedAssault, eventInfo.GetActor().GetGUID());
    }

    public override void Register()
    {
        DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckCoordinatedAssaultActive, 0, AuraType.ProcTriggerSpell));
    }
}

[Script] // 278519 - Divine Right
class spell_item_divine_right : AuraScript
{
    bool CheckHealthPct(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        return eventInfo.GetProcTarget().HasAuraState(AuraStateType.Wounded20Percent, eventInfo.GetSpellInfo(), eventInfo.GetActor());
    }

    public override void Register()
    {
        DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckHealthPct, 0, AuraType.ProcTriggerSpell));
    }
}

[Script] // 280409 - Blood Rite
class spell_item_blood_rite : AuraScript
{
    void HandleProc(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        RefreshDuration();
    }

    public override void Register()
    {
        AfterEffectProc.Add(new EffectProcHandler(HandleProc, 1, AuraType.Dummy));
    }
}

[Script] // 281843 - Tradewinds
class spell_item_tradewinds : AuraScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.TradewindsAllyBuff);
    }

    void HandleRemove(AuraEffect aurEff, AuraEffectHandleModes mode)
    {
        Unit target = GetTarget();

        if (target != null)
        {
            AuraEffect trait = target.GetAuraEffect(GetEffectInfo(1).TriggerSpell, 1);

            if (trait != null)
                target.CastSpell((SpellCastTargets)null, SpellIds.TradewindsAllyBuff, new CastSpellExtraArgs(aurEff).AddSpellBP0(trait.GetAmount()));
        }
    }

    public override void Register()
    {
        AfterEffectRemove.Add(new EffectApplyHandler(HandleRemove, 0, AuraType.ModRating, AuraEffectHandleModes.Real));
    }
}

[Script] // 287379 - Bastion of Might
class spell_item_bastion_of_might : SpellScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.IgnorePain);
    }

    void TriggerIgnorePain()
    {
        Unit caster = GetCaster();

        if (caster != null)
            caster.CastSpell(caster, SpellIds.IgnorePain, GetSpell());
    }

    public override void Register()
    {
        AfterHit.Add(new HitHandler(TriggerIgnorePain));
    }
}

[Script] // 287650 - Echoing Blades
class spell_item_echoing_blades : AuraScript
{
    ObjectGuid _lastFanOfKnives;

    void PrepareProc(ProcEventInfo eventInfo)
    {
        Spell procSpell = eventInfo.GetProcSpell();

        if (procSpell != null)
        {
            if (procSpell.m_castId != _lastFanOfKnives)
                GetEffect(0).RecalculateAmount();

            _lastFanOfKnives = procSpell.m_castId;
        }
    }

    bool CheckFanOfKnivesCounter(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        return aurEff.GetAmount() > 0;
    }

    void ReduceCounter(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        aurEff.SetAmount(aurEff.GetAmount() - 1);
    }

    public override void Register()
    {
        DoPrepareProc.Add(new AuraProcHandler(PrepareProc));
        DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckFanOfKnivesCounter, 0, AuraType.ProcTriggerSpell));
        AfterEffectProc.Add(new EffectProcHandler(ReduceCounter, 0, AuraType.ProcTriggerSpell));
    }
}

[Script] // 287653 - Echoing Blades
class spell_item_echoing_blades_damage : SpellScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.EchoingBladesTrait) && Global.SpellMgr.GetSpellInfo(SpellIds.EchoingBladesTrait, Difficulty.None).GetEffects().Count > 2;
    }

    void CalculateDamage(uint effIndex)
    {
        AuraEffect trait = GetCaster().GetAuraEffect(SpellIds.EchoingBladesTrait, 2);

        if (trait != null)
            SetHitDamage(trait.GetAmount() * 2);
    }

    void ForceCritical(Unit victim, ref float critChance)
    {
        critChance = 100.0f;
    }

    public override void Register()
    {
        OnEffectLaunchTarget.Add(new EffectHandler(CalculateDamage, 0, SpellEffectName.SchoolDamage));
        OnCalcCritChance.Add(new OnCalcCritChanceHandler(ForceCritical));
    }
}

[Script] // 288882 - Hour of Reaping
class spell_item_hour_of_reaping : AuraScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.SoulBarrier);
    }

    bool CheckProc(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        return GetStackAmount() == GetAura().CalcMaxStackAmount();
    }

    void TriggerSoulBarrier(AuraEffect aurEff, ProcEventInfo eventInfo)
    {
        GetTarget().CastSpell(GetTarget(), SpellIds.SoulBarrier, new CastSpellExtraArgs(aurEff));
    }

    public override void Register()
    {
        DoCheckEffectProc.Add(new CheckEffectProcHandler(CheckProc, 0, AuraType.Dummy));
        AfterEffectProc.Add(new EffectProcHandler(TriggerSoulBarrier, 0, AuraType.Dummy));
    }
}

[Script] // 277253 - Heart of Azeroth
class spell_item_heart_of_azeroth : AuraScript
{
    void SetEquippedFlag(AuraEffect aurEff, AuraEffectHandleModes mode)
    {
        SetState(true);
    }

    void ClearEquippedFlag(AuraEffect aurEff, AuraEffectHandleModes mode)
    {
        SetState(false);
    }

    void SetState(bool equipped)
    {
        Unit target = GetTarget();

        if (target != null)
        {
            Player player = target.ToPlayer();

            if (player != null)
            {
                player.ApplyAllAzeriteEmpoweredItemMods(equipped);

                PlayerAzeriteItemEquippedStatusChanged statusChanged = new();
                statusChanged.IsHeartEquipped = equipped;
                player.SendPacket(statusChanged);
            }
        }
    }

    public override void Register()
    {
        OnEffectApply.Add(new EffectApplyHandler(SetEquippedFlag, 0, AuraType.Dummy, AuraEffectHandleModes.Real));
        OnEffectRemove.Add(new EffectApplyHandler(ClearEquippedFlag, 1, AuraType.Dummy, AuraEffectHandleModes.Real));
    }
}