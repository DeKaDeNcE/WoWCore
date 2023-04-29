# Warrior Protection Talent: 12975 Last Stand

# Spell 12975 Last Stand -> Triggers 280001 Bolster (Aura) -> Triggers 132404 Shield Block (Aura)
DELETE FROM `spell_script_names` WHERE `spell_id` = 12975;
INSERT INTO `spell_script_names` VALUES (12975, 'spell_warr_last_stand');