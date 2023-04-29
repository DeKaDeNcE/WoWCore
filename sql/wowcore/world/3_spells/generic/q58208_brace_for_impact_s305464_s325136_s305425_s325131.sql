DELETE FROM `spell_script_names` WHERE `spell_id` IN (305464, 325136, 305425, 325131);
INSERT INTO `spell_script_names`
(`spell_id`,                               `ScriptName`) VALUES
(    305464,              'spell_crash_landed_alliance'),
(    325136,                 'spell_crash_landed_horde'),
(    305425, 'spell_alliance_spell_ship_crash_teleport'),
(    325131,    'spell_horde_spell_ship_crash_teleport');