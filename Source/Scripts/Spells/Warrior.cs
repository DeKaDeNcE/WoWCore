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

using System;
using System.Collections.Generic;
using System.Numerics;
using Framework.Constants;
using Game.Spells;
using Game.Entities;
using Game.Movement;
using Game.Scripting;

namespace Scripts.Spells.Warrior;

    struct SpellIds
    {
        public const uint BladestormPeriodicWhirlwind = 50622;
        public const uint BloodthirstHeal = 117313;
        public const uint Charge = 34846;
        public const uint ChargeEffect = 218104;
        public const uint ChargeEffectBlazingTrail = 198337;
        public const uint ChargePauseRageDecay = 109128;
        public const uint ChargeRootEffect = 105771;
        public const uint ChargeSlowEffect = 236027;
        public const uint ColossusSmash = 167105;
        public const uint ColossusSmashEffect = 208086;
        public const uint Execute = 20647;
        public const uint GlyphOfTheBlazingTrail = 123779;
        public const uint GlyphOfHeroicLeap = 159708;
        public const uint GlyphOfHeroicLeapBuff = 133278;
        public const uint HeroicLeapJump = 162052;
        public const uint ImpendingVictory = 202168;
        public const uint ImpendingVictoryHeal = 202166;
        public const uint ImprovedHeroicLeap = 157449;
        public const uint MortalStrike = 12294;
        public const uint MortalWounds = 213667;
        public const uint RallyingCry = 97463;
        public const uint Shockwave = 46968;
        public const uint ShockwaveStun = 132168;
        public const uint Stoicism = 70845;
        public const uint StormBoltStun = 132169;
        public const uint SweepingStrikesExtraAttack1 = 12723;
        public const uint SweepingStrikesExtraAttack2 = 26654;
        public const uint Taunt = 355;
        public const uint TraumaEffect = 215537;
        public const uint Victorious = 32216;
        public const uint VictoriousRushHeal = 118779;

        public const uint HeroicLeap = 6544;
        public const uint HeroicLeapJumpOld = 178368;
        public const uint CosmeticHeroicLeapJump = 94954;
        public const uint HeroicLeapJumpDamage = 52174;
        public const uint BoundingStride = 202163;
        public const uint BoundingStrideSpeed = 202164;
    }

    struct Misc
    {
        public const uint SpellVisualBlazingCharge = 26423;
    }

    [Script] // 23881 - Bloodthirst
    class spell_warr_bloodthirst : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.BloodthirstHeal);
        }

        void HandleDummy(uint effIndex)
        {
            GetCaster().CastSpell(GetCaster(), SpellIds.BloodthirstHeal, true);
        }

        public override void Register()
        {
            OnEffectHit.Add(new EffectHandler(HandleDummy, 3, SpellEffectName.Dummy));
        }
    }

    [Script] // 100 - Charge
    class spell_warr_charge : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.ChargeEffect, SpellIds.ChargeEffectBlazingTrail);
        }

        void HandleDummy(uint effIndex)
        {
            uint spellId = SpellIds.ChargeEffect;
            if (GetCaster().HasAura(SpellIds.GlyphOfTheBlazingTrail))
                spellId = SpellIds.ChargeEffectBlazingTrail;

            GetCaster().CastSpell(GetHitUnit(), spellId, true);
        }

        public override void Register()
        {
            OnEffectHitTarget.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.Dummy));
        }
    }

    [Script] // 126661 - Warrior Charge Drop Fire Periodic
    class spell_warr_charge_drop_fire_periodic : AuraScript
    {
        void DropFireVisual(AuraEffect aurEff)
        {
            PreventDefaultAction();
            if (GetTarget().IsSplineEnabled())
            {
                for (uint i = 0; i < 5; ++i)
                {
                    int timeOffset = (int)(6 * i * aurEff.GetPeriod() / 25);
                    Vector4 loc = GetTarget().MoveSpline.ComputePosition(timeOffset);
                    GetTarget().SendPlaySpellVisual(new Position(loc.X, loc.Y, loc.Z), Misc.SpellVisualBlazingCharge, 0, 0, 1.0f, true);
                }
            }
        }

        public override void Register()
        {
            OnEffectPeriodic.Add(new EffectPeriodicHandler(DropFireVisual, 0, AuraType.PeriodicTriggerSpell));
        }
    }

    // 198337 - Charge Effect (dropping Blazing Trail)
    [Script] // 218104 - Charge Effect
    class spell_warr_charge_effect : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.ChargePauseRageDecay, SpellIds.ChargeRootEffect, SpellIds.ChargeSlowEffect);
        }

        void HandleCharge(uint effIndex)
        {
            Unit caster = GetCaster();
            Unit target = GetHitUnit();
            caster.CastSpell(caster, SpellIds.ChargePauseRageDecay, new CastSpellExtraArgs(TriggerCastFlags.FullMask).AddSpellBP0(0));
            caster.CastSpell(target, SpellIds.ChargeRootEffect, true);
            caster.CastSpell(target, SpellIds.ChargeSlowEffect, true);
        }

        public override void Register()
        {
            OnEffectLaunchTarget.Add(new EffectHandler(HandleCharge, 0, SpellEffectName.Charge));
        }
    }

    [Script] // 167105 - Colossus Smash 7.1.5
    class spell_warr_colossus_smash_SpellScript : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.ColossusSmashEffect);
        }

        void HandleOnHit()
        {
            Unit target = GetHitUnit();
            if (target)
                GetCaster().CastSpell(target, SpellIds.ColossusSmashEffect, true);
        }

        public override void Register()
        {
            OnHit.Add(new HitHandler(HandleOnHit));
        }
    }

    [Script] // 6544 Heroic Leap
    // 162052 Heroic Leap (Jump)
    // 52174 Heroic Leap (Damage)
    class spell_warr_heroic_leap : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.HeroicLeapJump);
        }

        SpellCastResult CheckElevation()
        {
            Unit caster = GetCaster();
            WorldLocation dest = GetExplTargetDest();

            if (caster != null && dest != null)
            {
                if (caster.HasUnitMovementFlag(MovementFlag.Root))
                    return SpellCastResult.Rooted;

                if (caster.GetMap().Instanceable())
                {
                    float range = GetSpellInfo().GetMaxRange(true, caster) * 1.5f;

                    PathGenerator generatedPath = new(caster);
                    generatedPath.SetPathLengthLimit(range);

                    bool result = generatedPath.CalculatePath(dest.GetPositionX(), dest.GetPositionY(), dest.GetPositionZ(), false);

                    if (generatedPath.GetPathType().HasAnyFlag(PathType.Short))
                        return SpellCastResult.OutOfRange;
                    else if (!result || generatedPath.GetPathType().HasAnyFlag(PathType.NoPath))
                    {
                        result = generatedPath.CalculatePath(dest.GetPositionX(), dest.GetPositionY(), dest.GetPositionZ(), true);

                        if (generatedPath.GetPathType().HasAnyFlag(PathType.Short))
                            return SpellCastResult.OutOfRange;
                        else if (!result || generatedPath.GetPathType().HasAnyFlag(PathType.NoPath))
                            return SpellCastResult.NoPath;
                    }
                }
                else if (dest.GetPositionZ() > caster.GetPositionZ() + 10.0f)
                    return SpellCastResult.NoPath;

                return SpellCastResult.SpellCastOk;
            }

            return SpellCastResult.NoValidTargets;
        }

        void HandleDummy(uint effIndex)
        {
            Unit caster = GetCaster();
            WorldLocation dest = GetHitDest();

            if (caster != null && dest != null)
                caster.CastSpell(dest.GetPosition(), SpellIds.HeroicLeapJump, new CastSpellExtraArgs(true));
        }

        public override void Register()
        {
            OnCheckCast.Add(new CheckCastHandler(CheckElevation));
            OnEffectHit.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.Dummy));
        }
    }

    [Script] // 162052 Heroic Leap (Jump)
    // DEPRECATED 178368 Heroic Leap
    // DEPRECATED 94954 [DND] Cosmetic Heroic Leap (Dest)
    class spell_warr_heroic_leap_jump : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.GlyphOfHeroicLeap, SpellIds.GlyphOfHeroicLeapBuff, SpellIds.ImprovedHeroicLeap, SpellIds.Taunt);
        }

        void AfterJump(uint effIndex)
        {
            Unit caster = GetCaster();

            if (caster != null)
            {
                if (caster.HasAura(SpellIds.GlyphOfHeroicLeap))
                    caster.CastSpell(caster, SpellIds.GlyphOfHeroicLeapBuff, true);

                if (caster.HasAura(SpellIds.ImprovedHeroicLeap))
                    caster.GetSpellHistory().ResetCooldown(SpellIds.Taunt, true);
            }
        }

        public override void Register()
        {
            OnEffectHit.Add(new EffectHandler(AfterJump, 1, SpellEffectName.JumpDest));
        }
    }

    [Script] // 52174 Heroic Leap (Damage)
    class spell_warr_heroic_leap_damage : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.BoundingStride, SpellIds.BoundingStrideSpeed);
        }

        void AfterDamage(uint effIndex)
        {
            Unit caster = GetCaster();

            if (caster != null)
                if (caster.HasAura(SpellIds.BoundingStride))
                    caster.CastSpell(caster, SpellIds.BoundingStrideSpeed, true);
        }

        public override void Register()
        {
            OnEffectHit.Add(new EffectHandler(AfterDamage, 0, SpellEffectName.SchoolDamage));
        }
    }

    [Script] // 202168 - Impending Victory
    class spell_warr_impending_victory : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.ImpendingVictoryHeal);
        }

        void HandleAfterCast()
        {
            Unit caster = GetCaster();
            caster.CastSpell(caster, SpellIds.ImpendingVictoryHeal, true);
            caster.RemoveAurasDueToSpell(SpellIds.Victorious);
        }

        public override void Register()
        {
            AfterCast.Add(new CastHandler(HandleAfterCast));
        }
    }

    [Script] // 5246 - Intimidating Shout
    class spell_warr_intimidating_shout : SpellScript
    {
        void FilterTargets(List<WorldObject> unitList)
        {
            unitList.Remove(GetExplTargetWorldObject());
        }

        public override void Register()
        {
            OnObjectAreaTargetSelect.Add(new ObjectAreaTargetSelectHandler(FilterTargets, 1, Targets.UnitSrcAreaEnemy));
            OnObjectAreaTargetSelect.Add(new ObjectAreaTargetSelectHandler(FilterTargets, 2, Targets.UnitSrcAreaEnemy));
        }
    }

    [Script] // 7.1.5 70844 - Item - Warrior T10 Protection 4P Bonus
    class spell_warr_item_t10_prot_4p_bonus : AuraScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.Stoicism) && spellInfo.GetEffects().Count > 1;
        }

        void HandleProc(ProcEventInfo eventInfo)
        {
            PreventDefaultAction();

            Unit target = eventInfo.GetActionTarget();
            int bp0 = (int)MathFunctions.CalculatePct(target.GetMaxHealth(), GetEffectInfo(1).CalcValue());
            CastSpellExtraArgs args = new(TriggerCastFlags.FullMask);
            args.AddSpellBP0(bp0);
            target.CastSpell((Unit)null, SpellIds.Stoicism, args);
        }

        public override void Register()
        {
            OnProc.Add(new AuraProcHandler(HandleProc));
        }
    }

    [Script] // 12294 - Mortal Strike 7.1.5
    class spell_warr_mortal_strike : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.MortalWounds);
        }

        void HandleDummy(uint effIndex)
        {
            Unit target = GetHitUnit();
            if (target)
                GetCaster().CastSpell(target, SpellIds.MortalWounds, true);
        }

        public override void Register()
        {
            OnEffectHitTarget.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.Dummy));
        }
    }

    [Script] // 97462 - Rallying Cry
    class spell_warr_rallying_cry : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.RallyingCry);
        }

        public override bool Load()
        {
            return GetCaster().IsTypeId(TypeId.Player);
        }

        void HandleScript(uint effIndex)
        {
            CastSpellExtraArgs args = new(TriggerCastFlags.FullMask);
            args.AddSpellBP0((int)GetHitUnit().CountPctFromMaxHealth(GetEffectValue()));

            GetCaster().CastSpell(GetHitUnit(), SpellIds.RallyingCry, args);
        }

        public override void Register()
        {
            OnEffectHitTarget.Add(new EffectHandler(HandleScript, 0, SpellEffectName.Dummy));
        }
    }

    [Script] // 46968 - Shockwave
    class spell_warr_shockwave : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            if (!ValidateSpellInfo(SpellIds.Shockwave, SpellIds.ShockwaveStun))
                return false;

            return spellInfo.GetEffects().Count > 3;
        }

        public override bool Load()
        {
            return GetCaster().IsTypeId(TypeId.Player);
        }

        void HandleStun(uint effIndex)
        {
            GetCaster().CastSpell(GetHitUnit(), SpellIds.ShockwaveStun, true);
            ++_targetCount;
        }

        // Cooldown reduced by 20 sec if it strikes at least 3 targets.
        void HandleAfterCast()
        {
            if (_targetCount >= (uint)GetEffectInfo(0).CalcValue())
                GetCaster().ToPlayer().GetSpellHistory().ModifyCooldown(GetSpellInfo().Id, TimeSpan.FromSeconds(-GetEffectInfo(3).CalcValue()));
        }

        public override void Register()
        {
            OnEffectHitTarget.Add(new EffectHandler(HandleStun, 0, SpellEffectName.Dummy));
            AfterCast.Add(new CastHandler(HandleAfterCast));
        }

        uint _targetCount;
    }

    [Script] // 107570 - Storm Bolt
    class spell_warr_storm_bolt : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.StormBoltStun);
        }

        void HandleOnHit(uint effIndex)
        {
            GetCaster().CastSpell(GetHitUnit(), SpellIds.StormBoltStun, true);
        }

        public override void Register()
        {
            OnEffectHitTarget.Add(new EffectHandler(HandleOnHit, 1, SpellEffectName.Dummy));
        }
    }

    [Script] // 52437 - Sudden Death
    class spell_warr_sudden_death : AuraScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.ColossusSmash);
        }

        void HandleApply(AuraEffect aurEff, AuraEffectHandleModes mode)
        {
            // Remove cooldown on Colossus Smash
            Player player = GetTarget().ToPlayer();
            if (player)
                player.GetSpellHistory().ResetCooldown(SpellIds.ColossusSmash, true);
        }

        public override void Register()
        {
            AfterEffectApply.Add(new EffectApplyHandler(HandleApply, 0, AuraType.Dummy, AuraEffectHandleModes.Real)); // correct?
        }
    }

    [Script] // 12328, 18765, 35429 - Sweeping Strikes
    class spell_warr_sweeping_strikes : AuraScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.SweepingStrikesExtraAttack1, SpellIds.SweepingStrikesExtraAttack2);
        }

        bool CheckProc(ProcEventInfo eventInfo)
        {
            _procTarget = eventInfo.GetActor().SelectNearbyTarget(eventInfo.GetProcTarget());
            return _procTarget;
        }

        void HandleProc(AuraEffect aurEff, ProcEventInfo eventInfo)
        {
            PreventDefaultAction();
            DamageInfo damageInfo = eventInfo.GetDamageInfo();
            if (damageInfo != null)
            {
                SpellInfo spellInfo = damageInfo.GetSpellInfo();
                if (spellInfo != null && (spellInfo.Id == SpellIds.BladestormPeriodicWhirlwind || (spellInfo.Id == SpellIds.Execute && !_procTarget.HasAuraState(AuraStateType.Wounded20Percent))))
                {
                    // If triggered by Execute (while target is not under 20% hp) or Bladestorm deals normalized weapon damage
                    GetTarget().CastSpell(_procTarget, SpellIds.SweepingStrikesExtraAttack2, new CastSpellExtraArgs(aurEff));
                }
                else
                {
                    CastSpellExtraArgs args = new(aurEff);
                    args.AddSpellBP0((int)damageInfo.GetDamage());
                    GetTarget().CastSpell(_procTarget, SpellIds.SweepingStrikesExtraAttack1, args);
                }
            }
        }

        public override void Register()
        {
            DoCheckProc.Add(new CheckProcHandler(CheckProc));
            OnEffectProc.Add(new EffectProcHandler(HandleProc, 0, AuraType.Dummy));
        }

        Unit _procTarget;
    }

    [Script] // 215538 - Trauma
    class spell_warr_trauma : AuraScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.TraumaEffect);
        }

        void HandleProc(AuraEffect aurEff, ProcEventInfo eventInfo)
        {
            Unit target = eventInfo.GetActionTarget();
            //Get 25% of damage from the spell casted (Slam & Whirlwind) plus Remaining Damage from Aura
            int damage = (int)(MathFunctions.CalculatePct(eventInfo.GetDamageInfo().GetDamage(), aurEff.GetAmount()) / Global.SpellMgr.GetSpellInfo(SpellIds.TraumaEffect, GetCastDifficulty()).GetMaxTicks());
            CastSpellExtraArgs args = new(TriggerCastFlags.FullMask);
            args.AddSpellBP0(damage);
            GetCaster().CastSpell(target, SpellIds.TraumaEffect, args);
        }

        public override void Register()
        {
            OnEffectProc.Add(new EffectProcHandler(HandleProc, 0, AuraType.Dummy));
        }
    }

    [Script] // 28845 - Cheat Death
    class spell_warr_t3_prot_8p_bonus : AuraScript
    {
        bool CheckProc(ProcEventInfo eventInfo)
        {
            if (eventInfo.GetActionTarget().HealthBelowPct(20))
                return true;

            DamageInfo damageInfo = eventInfo.GetDamageInfo();
            if (damageInfo != null && damageInfo.GetDamage() != 0)
                if (GetTarget().HealthBelowPctDamaged(20, damageInfo.GetDamage()))
                    return true;

            return false;
        }

        public override void Register()
        {
            DoCheckProc.Add(new CheckProcHandler(CheckProc));
        }
    }

    [Script] // 32215 - Victorious State
    class spell_warr_victorious_state : AuraScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.ImpendingVictory);
        }

        void HandleOnProc(AuraEffect aurEff, ProcEventInfo procInfo)
        {
            if (procInfo.GetActor().GetTypeId() == TypeId.Player && procInfo.GetActor().ToPlayer().GetPrimarySpecialization() == (uint)TalentSpecialization.WarriorFury)
                PreventDefaultAction();

            procInfo.GetActor().GetSpellHistory().ResetCooldown(SpellIds.ImpendingVictory, true);
        }

        public override void Register()
        {
            OnEffectProc.Add(new EffectProcHandler(HandleOnProc, 0, AuraType.ProcTriggerSpell));
        }
    }

    [Script] // 34428 - Victory Rush
    class spell_warr_victory_rush : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIds.Victorious, SpellIds.VictoriousRushHeal);
        }

        void HandleHeal()
        {
            Unit caster = GetCaster();

            caster.CastSpell(caster, SpellIds.VictoriousRushHeal, true);
            caster.RemoveAurasDueToSpell(SpellIds.Victorious);
        }

        public override void Register()
        {
            AfterCast.Add(new CastHandler(HandleHeal));
        }
    }