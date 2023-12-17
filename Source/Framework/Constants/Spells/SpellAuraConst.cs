﻿// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using System;

namespace Framework.Constants
{
    public enum AuraType
    {
        Any = -1,

        None = 0,
        BindSight = 1,
        ModPossess = 2,
        PeriodicDamage = 3,
        Dummy = 4,
        ModConfuse = 5,
        ModCharm = 6,
        ModFear = 7,
        PeriodicHeal = 8,
        ModAttackspeed = 9,
        ModThreat = 10,
        ModTaunt = 11,
        ModStun = 12,
        ModDamageDone = 13,
        ModDamageTaken = 14,
        DamageShield = 15,
        ModStealth = 16,
        ModStealthDetect = 17,
        ModInvisibility = 18,
        ModInvisibilityDetect = 19,
        ObsModHealth = 20,   // 20, 21 Unofficial
        ObsModPower = 21,
        ModResistance = 22,
        PeriodicTriggerSpell = 23,
        PeriodicEnergize = 24,
        ModPacify = 25,
        ModRoot = 26,
        ModSilence = 27,
        ReflectSpells = 28,
        ModStat = 29,
        ModSkill = 30,
        ModIncreaseSpeed = 31,
        ModIncreaseMountedSpeed = 32,
        ModDecreaseSpeed = 33,
        ModIncreaseHealth = 34,
        ModIncreaseEnergy = 35,
        ModShapeshift = 36,
        EffectImmunity = 37,
        StateImmunity = 38,
        SchoolImmunity = 39,
        DamageImmunity = 40,
        DispelImmunity = 41,
        ProcTriggerSpell = 42,
        ProcTriggerDamage = 43,
        TrackCreatures = 44,
        TrackResources = 45,
        Unk46 = 46,   // Ignore All Gear Test Spells
        ModParryPercent = 47,
        PeriodicTriggerSpellFromClient = 48,   // One Periodic Spell
        ModDodgePercent = 49,
        ModCriticalHealingAmount = 50,
        ModBlockPercent = 51,
        ModWeaponCritPercent = 52,
        PeriodicLeech = 53,
        ModHitChance = 54,
        ModSpellHitChance = 55,
        Transform = 56,
        ModSpellCritChance = 57,
        ModIncreaseSwimSpeed = 58,
        ModDamageDoneCreature = 59,
        ModPacifySilence = 60,
        ModScale = 61,
        PeriodicHealthFunnel = 62,
        ModAdditionalPowerCost = 63,   // NYI
        PeriodicManaLeech = 64,
        ModCastingSpeedNotStack = 65,
        FeignDeath = 66,
        ModDisarm = 67,
        ModStalked = 68,
        SchoolAbsorb = 69,
        PeriodicWeaponPercentDamage = 70,
        StoreTeleportReturnPoint = 71,
        ModPowerCostSchoolPct = 72,
        ModPowerCostSchool = 73,
        ReflectSpellsSchool = 74,
        ModLanguage = 75,
        FarSight = 76,
        MechanicImmunity = 77,
        Mounted = 78,
        ModDamagePercentDone = 79,
        ModPercentStat = 80,
        SplitDamagePct = 81,
        WaterBreathing = 82,
        ModBaseResistance = 83,
        ModRegen = 84,
        ModPowerRegen = 85,
        ChannelDeathItem = 86,
        ModDamagePercentTaken = 87,
        ModHealthRegenPercent = 88,
        PeriodicDamagePercent = 89,
        Unk90 = 90,   // Old ModResistChance
        ModDetectRange = 91,
        PreventsFleeing = 92,
        ModUnattackable = 93,
        InterruptRegen = 94,
        Ghost = 95,
        SpellMagnet = 96,
        ManaShield = 97,
        ModSkillTalent = 98,
        ModAttackPower = 99,
        AurasVisible = 100,
        ModResistancePct = 101,
        ModMeleeAttackPowerVersus = 102,
        ModTotalThreat = 103,
        WaterWalk = 104,
        FeatherFall = 105,
        Hover = 106,
        AddFlatModifier = 107,
        AddPctModifier = 108,
        AddTargetTrigger = 109,
        ModPowerRegenPercent = 110,
        InterceptMeleeRangedAttacks = 111,
        OverrideClassScripts = 112,
        ModRangedDamageTaken = 113,
        ModRangedDamageTakenPct = 114,
        ModHealing = 115,
        ModRegenDuringCombat = 116,
        ModMechanicResistance = 117,
        ModHealingPct = 118,
        PvpTalents = 119,
        Untrackable = 120,
        Empathy = 121,
        ModOffhandDamagePct = 122,
        ModTargetResistance = 123,
        ModRangedAttackPower = 124,
        ModMeleeDamageTaken = 125,
        ModMeleeDamageTakenPct = 126,
        RangedAttackPowerAttackerBonus = 127,
        ModFixate = 128,
        ModSpeedAlways = 129,
        ModMountedSpeedAlways = 130,
        ModRangedAttackPowerVersus = 131,
        ModIncreaseEnergyPercent = 132,
        ModIncreaseHealthPercent = 133,
        ModManaRegenInterrupt = 134,
        ModHealingDone = 135,
        ModHealingDonePercent = 136,
        ModTotalStatPercentage = 137,
        ModMeleeHaste = 138,
        ForceReaction = 139,
        ModRangedHaste = 140,
        Unk141 = 141,  // Old ModRangedAmmoHaste, Unused Now
        ModBaseResistancePct = 142,
        ModRecoveryRateBySpellLabel = 143, // NYI
        SafeFall = 144,
        ModIncreaseHealthPercent2 = 145,
        AllowTamePetType = 146,
        MechanicImmunityMask = 147,
        ModChargeRecoveryRate = 148, // NYI
        ReducePushback = 149,  //    Reduce Pushback
        ModShieldBlockvaluePct = 150,
        TrackStealthed = 151,  //    Track Stealthed
        ModDetectedRange = 152,  //    Mod Detected Range
        ModAutoAttackRange = 153,
        ModStealthLevel = 154,  //    Stealth Level Modifier
        ModWaterBreathing = 155,  //    Mod Water Breathing
        ModReputationGain = 156,  //    Mod Reputation Gain
        PetDamageMulti = 157,  //    Mod Pet Damage
        AllowTalentSwapping = 158,
        NoPvpCredit = 159,
        Unk160 = 160,  // Old ModAoeAvoidance. Unused 4.3.4
        ModHealthRegenInCombat = 161,
        PowerBurn = 162,
        ModCritDamageBonus = 163,
        ForceBeathBar = 164,
        MeleeAttackPowerAttackerBonus = 165,
        ModAttackPowerPct = 166,
        ModRangedAttackPowerPct = 167,
        ModDamageDoneVersus = 168,
        SetFFAPvp = 169,
        DetectAmore = 170,
        ModSpeedNotStack = 171,
        ModMountedSpeedNotStack = 172,
        ModRecoveryRate2 = 173,  // NYI
        ModSpellDamageOfStatPercent = 174,  // By Defeult Intelect, Dependent From ModSpellHealingOfStatPercent
        ModSpellHealingOfStatPercent = 175,
        SpiritOfRedemption = 176,
        AoeCharm = 177,
        ModMaxPowerPct = 178,
        ModPowerDisplay = 179,
        ModFlatSpellDamageVersus = 180,
        ModSpellCurrencyReagentsCountPct = 181,  // NYI
        SuppressItemPassiveEffectBySpellLabel = 182,  //NYI
        ModCritChanceVersusTargetHealth = 183,
        ModAttackerMeleeHitChance = 184,
        ModAttackerRangedHitChance = 185,
        ModAttackerSpellHitChance = 186,
        ModAttackerMeleeCritChance = 187,
        ModUIHealingRange = 188, // handled clientside - affects UnitInRange lua function only
        ModRating = 189,
        ModFactionReputationGain = 190,
        UseNormalMovementSpeed = 191,
        ModMeleeRangedHaste = 192,
        MeleeSlow = 193,
        ModTargetAbsorbSchool = 194,
        LearnSpell = 195,
        ModCooldown = 196,  // Only 24818 Noxious Breath
        ModAttackerSpellAndWeaponCritChance = 197,
        ModCombatRatingFromCombatRating = 198,
        Unk199 = 199,  // Old ModIncreasesSpellPctToHit. Unused 4.3.4
        ModXpPct = 200,
        Fly = 201,
        IgnoreCombatResult = 202,
        PreventInterrupt = 203, // NYI
        PreventCorpseRelease = 204, // NYI
        ModChargeCooldown = 205, // NYI
        ModIncreaseVehicleFlightSpeed = 206,
        ModIncreaseMountedFlightSpeed = 207,
        ModIncreaseFlightSpeed = 208,
        ModMountedFlightSpeedAlways = 209,
        ModVehicleSpeedAlways = 210,
        ModFlightSpeedNotStack = 211,
        ModHonorGainPct = 212,
        ModRageFromDamageDealt = 213,
        Unk214 = 214,
        ArenaPreparation = 215,
        HasteSpells = 216,
        ModMeleeHaste2 = 217,
        AddPctModifierBySpellLabel = 218,
        AddFlatModifierBySpellLabel = 219,
        ModAbilitySchoolMask = 220, //NYI
        ModDetaunt = 221,
        RemoveTransmogCost = 222,
        RemoveBarberShopCost = 223,
        LearnTalent = 224, // NYI
        ModVisibilityRange = 225,
        PeriodicDummy = 226,
        PeriodicTriggerSpellWithValue = 227,
        DetectStealth = 228,
        ModAoeDamageAvoidance = 229,
        ModMaxHealth = 230,
        ProcTriggerSpellWithValue = 231,
        MechanicDurationMod = 232,
        ChangeModelForAllHumanoids = 233,  // Client-Side Only
        MechanicDurationModNotStack = 234,
        HoverNoHeightOffset = 235,
        ControlVehicle = 236,
        Unk237 = 237,
        Unk238 = 238,
        ModScale2 = 239,
        ModExpertise = 240,
        ForceMoveForward = 241,
        ModSpellDamageFromHealing = 242,
        ModFaction = 243,
        ComprehendLanguage = 244,
        ModAuraDurationByDispel = 245,
        ModAuraDurationByDispelNotStack = 246,
        CloneCaster = 247,
        ModCombatResultChance = 248,
        ConvertRune = 249,
        ModIncreaseHealth2 = 250,
        ModEnemyDodge = 251,
        ModSpeedSlowAll = 252,
        ModBlockCritChance = 253,
        ModDisarmOffhand = 254,
        ModMechanicDamageTakenPercent = 255,
        NoReagentUse = 256,
        ModTargetResistBySpellClass = 257,
        OverrideSummonedObject = 258,
        ModHotPct = 259,
        ScreenEffect = 260,
        Phase = 261,
        AbilityIgnoreAurastate = 262,
        DisableCastingExceptAbilities = 263,
        DisableAttackingExceptAbilities = 264,
        Unk265 = 265,
        SetVignette = 266, //NYI
        ModImmuneAuraApplySchool = 267,
        ModArmorPctFromStat = 268,
        ModIgnoreTargetResist = 269,
        ModSchoolMaskDamageFromCaster = 270,
        ModSpellDamageFromCaster = 271,
        ModBlockValuePct = 272, //NYI
        XRay = 273,
        ModBlockValueFlat = 274,  //NYI
        ModIgnoreShapeshift = 275,
        ModDamageDoneForMechanic = 276,
        Unk277 = 277,  // Old ModMaxAffectedTargets. Unused 4.3.4
        ModDisarmRanged = 278,
        InitializeImages = 279,
        ProvideSpellFocus = 280,
        ModGuildReputationGainPct = 281, // NYI
        ModBaseHealthPct = 282,
        ModHealingReceived = 283,  // Possibly Only For Some Spell Family Class Spells
        Linked = 284,
        Linked2 = 285,
        ModRecoveryRate = 286,
        DeflectSpells = 287,
        IgnoreHitDirection = 288,
        PreventDurabilityLoss = 289,
        ModCritPct = 290,
        ModXpQuestPct = 291,
        OpenStable = 292,
        OverrideSpells = 293,
        PreventRegeneratePower = 294,
        ModPeriodicDamageTaken = 295,
        SetVehicleId = 296,
        ModRootDisableGravity = 297, //NYI
        ModStunDisableGravity = 298, //NYI
        Unk299 = 299,
        ShareDamagePct = 300,
        SchoolHealAbsorb = 301,
        Unk302 = 302,
        ModDamageDoneVersusAurastate = 303,
        ModFakeInebriate = 304,
        ModMinimumSpeed = 305,
        ModCritChanceForCaster = 306,
        CastWhileWalkingBySpellLabel = 307, //NYI
        ModCritChanceForCasterWithAbilities = 308,
        ModResilience = 309,  // NYI
        ModCreatureAoeDamageAvoidance = 310,
        IgnoreCombat = 311, //NYI
        AnimReplacementSet = 312,
        MountAnimReplacementSet = 313,
        PreventResurrection = 314,
        UnderwaterWalking = 315,
        SchoolAbsorbOverkill = 316, // NYI - absorbs overkill damage
        ModSpellPowerPct = 317,
        Mastery = 318,
        ModMeleeHaste3 = 319,
        Unk320 = 320,
        ModNoActions = 321,
        InterfereTargetting = 322,
        Unk323 = 323,  // Not Used In 4.3.4
        OverrideUnlockedAzeriteEssenceRank = 324,  // testing aura
        LearnPvpTalent = 325,  // NYI
        PhaseGroup = 326,  // Phase Related
        PhaseAlwaysVisible = 327,  // Sets PhaseShiftFlags::AlwaysVisible
        TriggerSpellOnPowerPct = 328,
        ModPowerGainPct = 329,
        CastWhileWalking = 330,
        ForceWeather = 331,
        OverrideActionbarSpells = 332,
        OverrideActionbarSpellsTriggered = 333, // Spells cast with this override have no cast time or power cost
        ModAutoAttackCritChance = 334,
        Unk335 = 335,
        MountRestrictions = 336,
        ModVendorItemsPrices = 337,
        ModDurabilityLoss = 338,
        ModCritChanceForCasterPet = 339,
        ModResurrectedHealthByGuildMember = 340,  // Increases Health Gained When Resurrected By A Guild Member By X
        ModSpellCategoryCooldown = 341,
        ModMeleeRangedHaste2 = 342,
        ModMeleeDamageFromCaster = 343,
        ModAutoAttackDamage = 344,
        BypassArmorForCaster = 345,
        EnableAltPower = 346,  // Nyi
        ModSpellCooldownByHaste = 347,
        ModMoneyGain = 348,  // Modifies gold gains from source: [Misc = 0, Quests][Misc = 1, Loot]
        ModCurrencyGain = 349,
        Unk350 = 350,
        ModCurrencyCategoryGainPct = 351,
        Unk352 = 352,
        ModCamouflage = 353,  // Nyi
        ModHealingDonePctVersusTargetHealth = 354,  // Restoration Shaman Mastery - Mod Healing Based On Target'S Health (Less = More Healing)
        ModCastingSpeed = 355,  // NYI
        ProvideTotemCategory = 356,
        EnableBoss1UnitFrame = 357,
        WorgenAlteredForm = 358,
        ModHealingDoneVersusAurastate = 359,
        ProcTriggerSpellCopy = 360,  // Procs The Same Spell That Caused This Proc (Dragonwrath, Tarecgosa'S Rest)
        OverrideAutoattackWithMeleeSpell = 361, // NYI
        Unk362 = 362,  // Not Used In 4.3.4
        ModNextSpell = 363,  // Used By 101601 Throw Totem - Causes The Client To Initialize Spell Cast With Specified Spell
        Unk364 = 364,  // Not Used In 4.3.4
        MaxFarClipPlane = 365,  // Overrides Client'S View Distance Setting To Max("Fair", CurrentSetting) And Turns Off Terrain Display
        OverrideSpellPowerByApPct = 366,  // Sets Spellpower Equal To % Of Attack Power, Discarding All Other Bonuses (From Gear And Buffs)
        OverrideAutoattackWithRangedSpell = 367, // NYI
        Unk368 = 368,  // Not Used In 4.3.4
        EnablePowerBarTimer = 369,
        SpellOverrideNameGroup = 370, // picks a random SpellOverrideName id from a group (group id in miscValue)
        Unk371 = 371,
        OverrideMountFromSet = 372, // NYI
        ModSpeedNoControl = 373, // NYI
        ModifyFallDamagePct = 374,
        ModPossessPet = 375,
        ModCurrencyGainFromSource = 376, // NYI
        CastWhileWalkingAll = 377, // Enables casting all spells while moving
        Unk378 = 378,
        ModManaRegenPct = 379,
        Unk380 = 380,
        ModDamageTakenFromCasterPet = 381, // NYI
        ModPetStatPct = 382, // NYI
        IgnoreSpellCooldown = 383,
        Unk384 = 384,
        Unk385 = 385,
        Unk386 = 386,
        Unk387 = 387,
        ModTaxiFlightSpeed = 388,
        Unk389 = 389,
        Unk390 = 390,
        Unk391 = 391,
        Unk392 = 392,
        BlockSpellsInFront = 393, //NYI
        ShowConfirmationPrompt = 394,
        AreaTrigger = 395, // NYI
        TriggerSpellOnPowerAmount = 396,
        BattleGroundPlayerPositionFactional = 397,
        BattleGroundPlayerPosition = 398,
        ModTimeRate = 399,
        ModSkill2 = 400,
        Unk401 = 401,
        ModOverridePowerDisplay = 402,
        OverrideSpellVisual = 403,
        OverrideAttackPowerBySpPct = 404,
        ModRatingPct = 405,
        KeyboundOverride = 406,
        ModFear2 = 407,
        SetActionButtonSpellCount = 408,
        CanTurnWhileFalling = 409,
        Unk410 = 410,
        ModMaxCharges = 411,
        Unk412 = 412,
        ModRangedAttackDeflectChance = 413, //NYI
        ModRangedAttackBlockChanceInFront = 414, //NYI
        Unk415 = 415,
        ModCooldownByHasteRegen = 416,
        ModGlobalCooldownByHasteRegen = 417,
        ModMaxPower = 418, // NYI
        ModBaseManaPct = 419,
        ModBattlePetXpPct = 420,
        ModAbsorbEffectsDonePct = 421, // NYI
        ModAbsorbEffectsTakenPct = 422, //NYI
        ModManaCostPct = 423,
        CasterIgnoreLos = 424, //NYI
        Unk425 = 425,
        Unk426 = 426,
        ScalePlayerLevel = 427, // NYI
        LinkedSummon = 428,
        ModSummonDamage = 429, // NYI - increases damage done by all summons, not just controlled pets
        PlayScene = 430,
        ModOverrideZonePvpType = 431, // NYI
        Unk432 = 432,
        Unk433 = 433,
        Unk434 = 434,
        Unk435 = 435,
        ModEnvironmentalDamageTaken = 436,
        ModMinimumSpeedRate = 437,
        PreloadPhase = 438, // NYI
        Unk439 = 439,
        ModMultistrikeDamage = 440, // NYI
        ModMultistrikeChance = 441, // NYI
        ModReadiness = 442, // NYI
        ModLeech = 443, // NYI
        Unk444 = 444,
        Unk445 = 445,
        Unk446 = 446,
        ModXpFromCreatureType = 447,
        Unk448 = 448,
        Unk449 = 449,
        Unk450 = 450,
        OverridePetSpecs = 451,
        Unk452 = 452,
        ChargeRecoveryMod = 453,
        ChargeRecoveryMultiplier = 454,
        ModRoot2 = 455,
        ChargeRecoveryAffectedByHaste = 456,
        ChargeRecoveryAffectedByHasteRegen = 457,
        IgnoreDualWieldHitPenalty = 458,
        IgnoreMovementForces = 459,
        ResetCooldownsOnDuelStart = 460,
        Unk461 = 461,
        ModHealingAndAbsorbFromCaster = 462, // NYI
        ConvertCritRatingPctToParryRating = 463, // NYI
        ModAttackPowerOfBonusArmor = 464, // NYI
        ModBonusArmor = 465,
        ModBonusArmorPct = 466,
        ModStatBonusPct = 467,
        TriggerSpellOnHealthPct = 468,
        ShowConfirmationPromptWithDifficulty = 469,
        ModAuraTimeRateBySpellLabel = 470, // NYI
        ModVersatility = 471,
        Unk472 = 472,
        PreventDurabilityLossFromCombat = 473,
        ReplaceItemBonusTree = 474, // NYI
        AllowUsingGameobjectsWhileMounted = 475,
        ModCurrencyGainLooted = 476,
        Unk477 = 477,
        Unk478 = 478,
        Unk479 = 479,
        ModArtifactItemLevel = 480,
        ConvertConsumedRune = 481,
        Unk482 = 482,
        SuppressTransforms = 483, // NYI
        AllowInterruptSpell = 484, // NYI
        ModMovementForceMagnitude = 485,
        Unk486 = 486,
        CosmeticMounted = 487,
        Unk488 = 488,
        ModAlternativeDefaultLanguage = 489, // NYI
        Unk490 = 490,
        Unk491 = 491,
        Unk492 = 492,
        Unk493 = 493,
        SetPowerPointCharge = 494, // NYI
        TriggerSpellOnExpire = 495,
        AllowChangingEquipmentInTorghast = 496, // NYI
        ModAnimaGain = 497, // NYI
        CurrencyLossPctOnDeath = 498, // NYI
        ModRestedXpConsumption = 499,
        IgnoreSpellChargeCooldown = 500, // NYI
        ModCriticalDamageTakenFromCaster = 501,
        ModVersatilityDamageDoneBenefit = 502, // NYI
        ModVersatilityHealingDoneBenefit = 503, // NYI
        ModHealingTakenFromCaster = 504,
        ModPlayerChoiceRerolls = 505, // NYI
        DisableInertia = 506, // NYI
        ModDamageTakenFromCasterByLabel = 507,
        Unk508 = 508,
        Unk509 = 509,
        ModifiedRaidInstance = 510, // NYI; Related to "Fated" raid affixes
        ApplyProfessionEffect = 511, // Nyi; Miscvalue[0] = Professioneffectid
        Unk512 = 512,
        Unk513 = 513,
        Unk514 = 514,
        Unk515 = 515,
        Unk516 = 516,
        Unk517 = 517,
        Unk518 = 518,
        ModCooldownRecoveryRateAll = 519, // Nyi; Applies To All Spells, Not Filtered By Familyflags Or Label
        Unk520 = 520,
        Unk521 = 521,
        Unk522 = 522,
        Unk523 = 523,
        Unk524 = 524,
        DisplayProfessionEquipment = 525, // Nyi; Miscvalue[0] = Profession (Enum, Not Id)
        Unk526 = 526,
        Unk527 = 527,
        AllowBlockingSpells = 528, // Nyi
        ModSpellBlockChance = 529, // Nyi
        Unk530 = 530,
        Unk531 = 531,
        Unk532 = 532,
        DisableNavigation = 533, // Disables Map Pins
        Unk534 = 534,
        Unk535 = 535,
        Total
    }

    [Flags]
    public enum AuraEffectHandleModes
    {
        Default = 0x0,
        Real = 0x01, // Handler Applies/Removes Effect From Unit
        SendForClient = 0x02, // Handler Sends Apply/Remove Packet To Unit
        ChangeAmount = 0x04, // Handler Updates Effect On Target After Effect Amount Change
        Reapply = 0x08, // Handler Updates Effect On Target After Aura Is Reapplied On Target
        Stat = 0x10, // Handler Updates Effect On Target When Stat Removal/Apply Is Needed For Calculations By Core
        Skill = 0x20, // Handler Updates Effect On Target When Skill Removal/Apply Is Needed For Calculations By Core
        SendForClientMask = SendForClient | Real, // Any Case Handler Need To Send Packet
        ChangeAmountMask = ChangeAmount | Real, // Any Case Handler Applies Effect Depending On Amount
        ChangeAmountSendForClientMask = ChangeAmountMask | SendForClientMask,
        RealOrReapplyMask = Reapply | Real
    }

    public enum AuraTriggerOnPowerChangeDirection
    {
        Gain = 0,
        Loss = 1
    }

    public enum AuraTriggerOnHealthChangeDirection
    {
        Above = 0,
        Below = 1,
    }

    // Diminishing Returns Types
    public enum DiminishingReturnsType
    {
        None = 0,                                // this spell is not diminished, but may have its duration limited
        Player = 1,                              // this spell is diminished only when applied on players
        All = 2                                  // this spell is diminished in every case
    }

    // Diminishing Return Groups
    public enum DiminishingGroup
    {
        None = 0,
        Root = 1,
        Stun = 2,
        Incapacitate = 3,
        Disorient = 4,
        Silence = 5,
        AOEKnockback = 6,
        Taunt = 7,
        LimitOnly = 8,

        Max
    }

    public enum DiminishingLevels
    {
        Level1 = 0,
        Level2 = 1,
        Level3 = 2,
        Immune = 3,
        Level4 = 3,
        TauntImmune = 4
    }
}
