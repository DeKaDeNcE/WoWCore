# Warrior Talent: 6544 Heroic Leap

# Spell 6544 Heroic Leap
DELETE FROM `spell_script_names` WHERE `spell_id` = 6544;
INSERT INTO `spell_script_names` VALUES (6544, 'spell_warr_heroic_leap');

# Spell 6544 Heroic Leap -> Triggers 162052 Heroic Leap (Jump) -> Triggers 52174 Heroic Leap (Damage)
DELETE FROM `spell_script_names` WHERE `spell_id` = 162052;
INSERT INTO `spell_script_names` VALUES (162052, 'spell_warr_heroic_leap_jump');

# Spell 6544 Heroic Leap -> Triggers 162052 Heroic Leap (Jump) -> Triggers 52174 Heroic Leap (Damage)
DELETE FROM `spell_script_names` WHERE `spell_id` = 52174;
INSERT INTO `spell_script_names` VALUES (52174, 'spell_warr_heroic_leap_damage');