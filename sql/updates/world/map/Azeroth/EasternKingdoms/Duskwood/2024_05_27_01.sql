-- Duskwood
-- NPC UPDATE SCRIPTS
UPDATE `creature_template` SET `ScriptName` = 'npc_ebenlocke' WHERE `entry` = 263;
UPDATE `creature_template` SET `ScriptName` = 'npc_stalvan' WHERE `entry` = 315;
UPDATE `creature_template` SET `ScriptName` = 'boss_twilight_corrupter' WHERE `entry` = 15625;
UPDATE `creature_template` SET `ScriptName` = 'npc_oliver_harris' WHERE `entry` = 43730;
UPDATE `creature_template` SET `ScriptName` = 'npc_soothing_incense_cloud' WHERE `entry` = 43925;

UPDATE `creature_template` SET `ScriptName` = 'npc_blaze_darkshire' WHERE `entry` = 43918;
UPDATE `creature_template` SET `ScriptName` = 'npc_stitches' WHERE `entry` = 43862;
UPDATE `creature_template` SET `ScriptName` = 'npc_nightwatch_guard' WHERE `entry` = 43903;

-- Area trigger update scripts
DELETE FROM `areatrigger_scripts` WHERE `entry` = 4017;
INSERT INTO `areatrigger_scripts`(`entry`, `ScriptName`) VALUES (4017, 'at_twilight_grove');

DELETE FROM `areatrigger_scripts` WHERE `entry` = 6112;
INSERT INTO `areatrigger_scripts`(`entry`, `ScriptName`) VALUES (6112, 'at_addele_stead');

-- Spell scripts
DELETE FROM `spell_script_names` WHERE `spell_id` IN (82029, 82130);
INSERT INTO `spell_script_names`(`spell_id`, `ScriptName`) VALUES 
(82029, 'spell_summon_stalvan'),
(82130, 'spell_sacred_cleansing');

DELETE FROM `creature_queststarter` WHERE `quest` = 325; -- not in game anymore

DELETE FROM `quest_poi` WHERE `QuestID` = 26685;
INSERT INTO `quest_poi`(`QuestID`, `BlobIndex`, `Idx1`, `ObjectiveIndex`, `QuestObjectiveID`, `QuestObjectID`, `MapID`, `UiMapID`, `Priority`, `Flags`, `WorldEffectID`, `PlayerConditionID`, `NavigationPlayerConditionID`, `SpawnTrackingID`, `AlwaysAllowMergingBlobs`, `VerifiedBuild`) VALUES 
(26685, 0, 0, -1, 0, 0, 0, 47, 0, 0, 0, 0, 0, 11845, 0, 46366),
(26685, 0, 1, 0, 266026, 1968, 0, 47, 0, 0, 0, 0, 0, 31458, 0, 46366),
(26685, 0, 2, 32, 0, 0, 0, 47, 0, 0, 0, 0, 0, 11845, 0, 46366);

DELETE FROM `quest_poi_points` WHERE `QuestID` = 26685;
INSERT INTO `quest_poi_points`(`QuestID`, `Idx1`, `Idx2`, `X`, `Y`, `Z`, `VerifiedBuild`) VALUES 
(26685, 0, 0, -10571, -1314, 49, 46366),
(26685, 1, 0, -11235, -188, 5, 46366),
(26685, 2, 0, -10571, -1314, 49, 46366);

-- creature
UPDATE `creature` SET `spawnDifficulties` = '0', `zoneId` = 10, `areaId` = 94 , `PhaseId` = 0 WHERE `id` IN (43730, 43731);
UPDATE `creature_template_addon` SET `auras` = '' WHERE `entry` IN (43730, 288);

DELETE FROM `smart_scripts` WHERE `entryorguid` = 43814 AND `source_type` = 0;

UPDATE `creature_template` SET `ScriptName` = 'npc_lurking_worgen_curse' WHERE `entry` = 43814
