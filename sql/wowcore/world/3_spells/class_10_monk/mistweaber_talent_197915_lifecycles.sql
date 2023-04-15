# Monk Mistweaver Talent: 197915 Lifecycles

# Spell 116670 Vivify -> Triggers 197919 Lifecycles (Enveloping Mist)
DELETE FROM `spell_script_names` WHERE `spell_id` = 116670;
INSERT INTO `spell_script_names` VALUES (116670, 'spell_monk_vivify_talent_lifecycles');

# Spell 124682 Enveloping Mist -> Triggers 197916 Lifecycles (Vivify)
DELETE FROM `spell_script_names` WHERE `spell_id` = 124682;
INSERT INTO `spell_script_names` VALUES (124682, 'spell_monk_enveloping_mist_talent_lifecycles');

# Talent 197915 Lifecycles (Does it really needs an entry in `spell_proc`? Values might not be correct, needs more checking)
DELETE FROM `spell_proc` WHERE `SpellId` IN (197915);
INSERT INTO `spell_proc`
(`SpellId`,`SchoolMask`,`SpellFamilyName`,`SpellFamilyMask0`,`SpellFamilyMask1`,`SpellFamilyMask2`,`SpellFamilyMask3`,`ProcFlags`,`ProcFlags2`,`SpellTypeMask`,`SpellPhaseMask`,`HitMask`,`AttributesMask`,`DisableEffectsMask`,`ProcsPerMinute`,`Chance`,`Cooldown`,`Charges`) VALUES
(   197915,        0x00,               53,        0x00000000,        0x00000000,        0x00000000,        0x00000000, 0x00004000,         0x0,            0x0,             0x0,      0x0,             0x0,                 0x0,                0,      0,         0,        0);