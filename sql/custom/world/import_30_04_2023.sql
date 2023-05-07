-- SQL Fixes

-- Spell scripts
DELETE FROM `spell_script_names` WHERE `spell_id` IN (20783, 60503, 79513, 80469, 85123);
INSERT INTO `spell_script_names`(`spell_id`, `ScriptName`) VALUES (20783, 'spell_destroy_karangs_banner');
INSERT INTO `spell_script_names`(`spell_id`, `ScriptName`) VALUES (60503, 'spell_murloc_pheromone');
INSERT INTO `spell_script_names`(`spell_id`, `ScriptName`) VALUES (79513, 'spell_tiki_torch');
INSERT INTO `spell_script_names`(`spell_id`, `ScriptName`) VALUES (80469, 'spell_ritual_of_shadra');
INSERT INTO `spell_script_names`(`spell_id`, `ScriptName`) VALUES (85123, 'spell_siege_cannon');




-- Creature scripts
UPDATE `creature_template` SET `ScriptName` = 'npc_ranger_lilatha' WHERE `entry` = 16295;
UPDATE `creature_template` SET `ScriptName` = 'npc_rathis_tomber' WHERE `entry` = 16224;
UPDATE `creature_template` SET `ScriptName` = 'npc_zidormi_tirisfalglades' WHERE `entry` = 141488;
UPDATE `creature_template` SET `ScriptName` = 'npc_huldar' WHERE `entry` = 2057;
UPDATE `creature_template` SET `ScriptName` = 'npc_ando_blastenheimer' WHERE `entry` = 44870;
UPDATE `creature_template` SET `ScriptName` = 'npc_oox09hl' WHERE `entry` = 7806;
UPDATE `creature_template` SET `ScriptName` = 'npc_sharpbeak' WHERE `entry` = 43161;
UPDATE `creature_template` SET `ScriptName` = 'npc_sharpbeak' WHERE `entry` = 51125;
UPDATE `creature_template` SET `ScriptName` = 'npc_trained_razorbeak' WHERE `entry` = 2657;
UPDATE `creature_template` SET `ScriptName` = 'npc_tb_spirit_guide' WHERE `entry` IN (45069, 45070, 45068, 45071, 45072, 45073, 45074, 45075, 45076, 45077, 45078, 45079);
UPDATE `creature_template` SET `ScriptName`= 'npc_gurgthock_cata' WHERE `entry` =46935;
UPDATE `creature_template` SET `ScriptName` = 'npc_hurp_derp' WHERE `entry` = 46944;
UPDATE `creature_template` SET `ScriptName` = 'npc_torg_drakeflayer' WHERE `entry` = 46945;
UPDATE `creature_template` SET `ScriptName` = 'npc_sully_kneecapper' WHERE `entry` = 46946;
UPDATE `creature_template` SET `ScriptName` = 'npc_cadaver_collage' WHERE `entry` = 46947;
UPDATE `creature_template` SET `ScriptName` = 'npc_lord_geoffery' WHERE `entry` = 46948;
UPDATE `creature_template` SET `ScriptName` = 'npc_emberscar_devourer' WHERE `entry` = 46949;
UPDATE `creature_template` SET `ScriptName` = 'npc_gloomwing' WHERE `entry` = 47476;
UPDATE `creature_template` SET `ScriptName` = 'npc_ruul_snowhoof' WHERE `entry` = 12818;
UPDATE `creature_template` SET `ScriptName` = 'npc_muglash' WHERE `entry` = 12717;
UPDATE `creature_template` SET `ScriptName` = 'npc_mikhail' WHERE `entry` = 4963;


-- Areatrigger scripts
DELETE FROM `areatrigger_scripts` WHERE `entry` IN (5377, 5375, 5376, 171);
INSERT INTO `areatrigger_scripts`(`entry`, `ScriptName`) VALUES (5377, 'at_ironband_tablet');
INSERT INTO `areatrigger_scripts`(`entry`, `ScriptName`) VALUES (5375, 'at_ironband_sandal');
INSERT INTO `areatrigger_scripts`(`entry`, `ScriptName`) VALUES (5376, 'at_ironband_liberty');
INSERT INTO `areatrigger_scripts`(`entry`, `ScriptName`) VALUES (171, 'at_the_loch');

-- Gameobject scripts
UPDATE `gameobject_template` SET `ScriptName` = 'go_naga_brazier' WHERE `entry` = 178247;