# Monk Mistweaver Talent: 210802 Spirit of the Crane
# Spell 100784 Blackout Kick -> Triggers Talent 210802 Spirit of the Crane -> Triggers Spell Energize 210803 Spirit of the Crane
DELETE FROM `spell_script_names` WHERE `spell_id` = 100784;
INSERT INTO `spell_script_names` VALUES (100784, 'spell_monk_blackout_kick_talent_spirit_of_the_crane');