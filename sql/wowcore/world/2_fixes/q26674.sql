-- WoWCore Fix
-- World: Azeroth
-- Map: Eastern Kingodm
-- Zone: Duskwood
-- Quest 26674 (Mistmantle's Revenge)

DELETE FROM `quest_poi` WHERE `QuestID` = 26674;
INSERT INTO `quest_poi` (`QuestID`, `BlobIndex`, `Idx1`, `ObjectiveIndex`, `QuestObjectiveID`, `QuestObjectID`, `MapID`, `UiMapID`, `Priority`, `Flags`, `WorldEffectID`, `PlayerConditionID`, `NavigationPlayerConditionID`, `SpawnTrackingID`, `AlwaysAllowMergingBlobs`, `VerifiedBuild`) VALUES (26674, 0, 0, -1, 0, 0, 0, 47, 0, 1, 0, 0, 0, 0, 0, 46366);
INSERT INTO `quest_poi` (`QuestID`, `BlobIndex`, `Idx1`, `ObjectiveIndex`, `QuestObjectiveID`, `QuestObjectID`, `MapID`, `UiMapID`, `Priority`, `Flags`, `WorldEffectID`, `PlayerConditionID`, `NavigationPlayerConditionID`, `SpawnTrackingID`, `AlwaysAllowMergingBlobs`, `VerifiedBuild`) VALUES (26674, 0, 1, 0, 253786, 315, 0, 47, 0, 1, 0, 0, 0, 0, 0, 46366);
INSERT INTO `quest_poi` (`QuestID`, `BlobIndex`, `Idx1`, `ObjectiveIndex`, `QuestObjectiveID`, `QuestObjectID`, `MapID`, `UiMapID`, `Priority`, `Flags`, `WorldEffectID`, `PlayerConditionID`, `NavigationPlayerConditionID`, `SpawnTrackingID`, `AlwaysAllowMergingBlobs`, `VerifiedBuild`) VALUES (26674, 0, 2, 32, 0, 0, 0, 47, 0, 0, 0, 0, 0, 11843, 0, 46366);

DELETE FROM `quest_poi_points` WHERE `QuestID` = 26674;
INSERT INTO `quest_poi_points` (`QuestID`, `Idx1`, `Idx2`, `X`, `Y`, `Z`, `VerifiedBuild`) VALUES (26674, 2, 0, -10532, -1213, 28, 46366);
INSERT INTO `quest_poi_points` (`QuestID`, `Idx1`, `Idx2`, `X`, `Y`, `Z`, `VerifiedBuild`) VALUES (26674, 1, 0, -10372, -1252, 36, 46366);
INSERT INTO `quest_poi_points` (`QuestID`, `Idx1`, `Idx2`, `X`, `Y`, `Z`, `VerifiedBuild`) VALUES (26674, 0, 0, -10512, -1301, 35, 46366);

DELETE FROM `gameobject_template` WHERE `entry` = 301070;
INSERT INTO `gameobject_template` (`entry`, `type`, `displayId`, `name`, `IconName`, `castBarCaption`, `unk1`, `size`, `Data0`, `Data1`, `Data2`, `Data3`, `Data4`, `Data5`, `Data6`, `Data7`, `Data8`, `Data9`, `Data10`, `Data11`, `Data12`, `Data13`, `Data14`, `Data15`, `Data16`, `Data17`, `Data18`, `Data19`, `Data20`, `Data21`, `Data22`, `Data23`, `Data24`, `Data25`, `Data26`, `Data27`, `Data28`, `Data29`, `Data30`, `Data31`, `Data32`, `Data33`, `Data34`, `ContentTuningId`, `AIName`, `ScriptName`, `VerifiedBuild`) VALUES (301070, 8, 299, 'Manor Mistmantle', '', '', '', 1, 1677, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', '', 1);

DELETE FROM `gameobject` WHERE `id` = 301070 AND `guid` = 18;
INSERT INTO `gameobject` (`guid`, `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `phaseUseFlags`, `PhaseId`, `PhaseGroup`, `terrainSwapMap`, `position_x`, `position_y`, `position_z`, `orientation`, `rotation0`, `rotation1`, `rotation2`, `rotation3`, `spawntimesecs`, `animprogress`, `state`, `ScriptName`, `VerifiedBuild`) VALUES (18, 301070, 0, 10, 1098, '0', 0, 0, 0, -1, -10368.3, -1256.38, 35.9093, 0, 0, 0, 0, 1, 180, 0, 1, '', 0);


UPDATE `creature_template` SET `ScriptName` = 'npc_ebenlocke' WHERE `entry` = 263;
UPDATE `creature_template` SET `ScriptName` = 'npc_stalvan' WHERE `entry` = 315;
UPDATE `creature_template` SET `ScriptName` = 'boss_twilight_corrupter' WHERE `entry` = 15625;
UPDATE `creature_template` SET `ScriptName` = 'npc_oliver_harris' WHERE `entry` = 43730;

-- Area trigger update scripts
DELETE FROM `areatrigger_scripts` WHERE `entry` = 4017;
INSERT INTO `areatrigger_scripts`(`entry`, `ScriptName`) VALUES (4017, 'at_twilight_grove');

-- Spell scripts
DELETE FROM `spell_script_names` WHERE `spell_id` IN (82029, 82130);
INSERT INTO `spell_script_names`(`spell_id`, `ScriptName`) VALUES 
(82029, 'spell_summon_stalvan'),
(82130, 'spell_sacred_cleansing');