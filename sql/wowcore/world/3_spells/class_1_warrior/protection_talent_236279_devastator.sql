# Warrior Protection Talent: 236279 Devastator

# Spell 236279 Devastator -> Triggers 23922 Shield Slam (Reset Cooldown)
DELETE FROM `spell_script_names` WHERE `spell_id` = 236279;
INSERT INTO `spell_script_names` VALUES (236279, 'spell_warr_devastator');