# Monk Mistweaver Talent: 116680 Thunder Focus Tea

# Spell 124682 Enveloping Mist -> Removes 16680 Thunder Focus Tea
DELETE FROM `spell_script_names` WHERE `spell_id` = 124682;
INSERT INTO `spell_script_names` VALUES (124682, 'spell_monk_talent_thunder_focus_tea');

# Spell 115151 Renewing Mist -> Removes 16680 Thunder Focus Tea
DELETE FROM `spell_script_names` WHERE `spell_id` = 115151;
INSERT INTO `spell_script_names` VALUES (115151, 'spell_monk_talent_thunder_focus_tea');

# Spell 116670 Vivify -> Removes 16680 Thunder Focus Tea
DELETE FROM `spell_script_names` WHERE `spell_id` = 116670;
INSERT INTO `spell_script_names` VALUES (116670, 'spell_monk_talent_thunder_focus_tea');

# Spell 107428 Rising Sun Kick -> Removes 16680 Thunder Focus Tea
DELETE FROM `spell_script_names` WHERE `spell_id` = 107428;
INSERT INTO `spell_script_names` VALUES (107428, 'spell_monk_talent_thunder_focus_tea');

# Spell 191837 Essence Font -> Removes 16680 Thunder Focus Tea
DELETE FROM `spell_script_names` WHERE `spell_id` = 191837;
INSERT INTO `spell_script_names` VALUES (191837, 'spell_monk_talent_thunder_focus_tea');

# Talent 116680 Thunder Focus Tea (Does it really needs an entry in `spell_proc`? Values might not be correct, needs more checking)
DELETE FROM `spell_proc` WHERE `SpellId` = 116680;
INSERT INTO `spell_proc`
(`SpellId`,`SchoolMask`,`SpellFamilyName`,`SpellFamilyMask0`,`SpellFamilyMask1`,`SpellFamilyMask2`,`SpellFamilyMask3`,`ProcFlags`,`ProcFlags2`,`SpellTypeMask`,`SpellPhaseMask`,`HitMask`,`AttributesMask`,`DisableEffectsMask`,`ProcsPerMinute`,`Chance`,`Cooldown`,`Charges`) VALUES
(   116680,        0x08,               53,        0x04800000,        0x00000080,        0x00000000,        0x00000800, 0x00005010,         0x0,            0x0,             0x0,      0x0,             0x0,                 0x0,               0,       0,         0,        0);