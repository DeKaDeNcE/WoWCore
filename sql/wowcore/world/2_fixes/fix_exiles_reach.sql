-- Phase updates
UPDATE `creature` SET `PhaseId`=0 WHERE `id`=171872; -- Apprentice Kutz
UPDATE `creature` SET `PhaseId`=0 WHERE `id`=172033; -- Helmsman Da'vees
UPDATE `creature` SET `PhaseId`=15286 WHERE `id`=166573; -- Warlord Breka Grimaxe
UPDATE `creature` SET `PhaseId`=15286 WHERE `id`=166824; -- Warlord Breka Grimaxe
UPDATE `creature` SET `PhaseId`=15287 WHERE `id`=166827; -- Warlord Breka Grimaxe
UPDATE `creature` SET `PhaseId`=14353 WHERE `guid` IN (1050176,1050177,1050178,1050179,1050180,1050181);
-- Misc creature and template fixes
UPDATE `creature` SET `equipment_id`=1 WHERE `guid`=1050189;
UPDATE `creature` SET `spawntimesecs`=120 WHERE `map`=2369 AND `spawntimesecs`=7200;
UPDATE `creature` SET `curhealth`=1 WHERE `map`=2369;
UPDATE `creature_template` SET `npcflag`=2 WHERE `entry`=166827;
UPDATE `creature_template` SET `flags_extra`=128 WHERE `entry` IN (174971,168039);
UPDATE `creature_template` SET `faction`=35,`unit_flags`=33554432,`unit_flags2`=2048,`unit_flags3`=524320 WHERE `entry`=166814;

-- Spawn Conditions for Warlord Breka Grimaxe spawns Phase 1
DELETE FROM `conditions` WHERE `SourceTypeOrReferenceId`=32 AND `SourceGroup`=5 AND `SourceEntry` IN (166573,166824) AND `SourceId`=0;
INSERT INTO `conditions` (`SourceTypeOrReferenceId`, `SourceGroup`, `SourceEntry`, `SourceId`, `ElseGroup`, `ConditionTypeOrReference`, `ConditionTarget`, `ConditionValue1`, `ConditionValue2`, `ConditionValue3`, `NegativeCondition`, `ErrorType`, `ErrorTextId`, `ScriptName`, `Comment`) VALUES
(32,5,166573,0,0,47,0,59926,11,0,0,0,0,'','Spawn of creature with entry 166573 requires Quest 59926 not rewarded'),
(32,5,166824,0,0,47,0,59926,64,0,0,0,0,'','Spawn of creature with entry 166824 requires Quest 59926 rewarded');

-- Phase conditions
DELETE FROM `conditions` WHERE `SourceTypeOrReferenceId`=26 AND `SourceGroup` IN (15284,15286,15287,15514,15516) AND `SourceEntry`=13377;
INSERT INTO `conditions` (`SourceTypeOrReferenceId`, `SourceGroup`, `SourceEntry`, `SourceId`, `ElseGroup`, `ConditionTypeOrReference`, `ConditionTarget`, `ConditionValue1`, `ConditionValue2`, `ConditionValue3`, `NegativeCondition`, `ErrorType`, `ErrorTextId`, `ScriptName`, `Comment`) VALUES
(26,15284,13377,0,0,47,0,59927,8,0,1,0,0,'','Allow Phase 15284 if Quest 59927 is not inprogress'),
(26,15284,13377,0,0,47,0,59928,2,0,1,0,0,'','Allow Phase 15284 if Quest 59928 is not complete'),
(26,15286,13377,0,0,47,0,59928,2,0,1,0,0,'','Allow Phase 15286 if Quest 59928 is not complete'),
(26,15287,13377,0,0,47,0,59928,66,0,0,0,0,'','Allow Phase 15287 if Quest 59928 is complete or rewarded'),
(26,15514,13377,0,0,47,0,59928,2,0,1,0,0,'','Allow Phase 15514 if Quest 59928 is not Complete'),
(26,15516,13377,0,0,47,0,59928,2,0,0,0,0,'','Allow Phase 15516 if Quest 59928 is Complete');

-- Fix condition showing Alliance quest phase on horde on ship
DELETE FROM `conditions` WHERE `SourceTypeOrReferenceId`=26 AND `SourceGroup` IN (13861,14350,14353) AND `SourceEntry`=10639;
INSERT INTO `conditions` (`SourceTypeOrReferenceId`, `SourceGroup`, `SourceEntry`, `SourceId`, `ElseGroup`, `ConditionTypeOrReference`, `ConditionTarget`, `ConditionValue1`, `ConditionValue2`, `ConditionValue3`, `NegativeCondition`, `ErrorType`, `ErrorTextId`, `ScriptName`, `Comment`) VALUES
(26,13861,10639,0,0,47,0,54933,2,0,0,0,0,'','Apply Phase 13861 if Quest 54933 is complete'),
(26,14350,10639,0,0,47,0,56775,64,0,0,0,0,'','Apply Phase 14350 if Quest 56775 is rewarded'),
(26,14350,10639,0,0,47,0,58208,2,0,1,0,0,'','Apply Phase 14350 if Quest 58208 is not complete'),
(26,14353,10639,0,0,47,0,58208,2,0,0,0,0,'','Allow Phase 14353 if Quest 58208 is complete');

UPDATE `conditions` SET `ConditionValue1`=58208, `Comment`='Apply Phase 13861 if Quest 58208 is complete' WHERE `SourceTypeOrReferenceId`=26 AND `SourceGroup`=13861 AND `SourceEntry`=10639;

DELETE FROM `conditions` WHERE `SourceTypeOrReferenceId`=29 AND `SourceEntry` IN (32717,35650,31445,36093,31382,36096,36099,36100,36101,36102,36103,36104);
INSERT INTO `conditions` (`SourceTypeOrReferenceId`, `SourceGroup`, `SourceEntry`, `SourceId`, `ElseGroup`, `ConditionTypeOrReference`, `ConditionTarget`, `ConditionValue1`, `ConditionValue2`, `ConditionValue3`, `NegativeCondition`, `ErrorType`, `ErrorTextId`, `ScriptName`, `Comment`) VALUES
(29,0,32717,0,0,6,0,469,0,0,0,0,0,'','Allow conversation line 32717 if team is Alliance'),
(29,0,35650,0,0,6,0,67,0,0,0,0,0,'','Allow conversation line 35650 if team is horde'),
(29,0,31445,0,0,6,0,469,0,0,0,0,0,'','Allow conversation line 31445 if team is Alliance'),
(29,0,36093,0,0,6,0,67,0,0,0,0,0,'','Allow conversation line 36093 if team is horde'),
(29,0,31382,0,0,6,0,469,0,0,0,0,0,'','Allow conversation line 31382 if team is Alliance'),
(29,0,36096,0,0,6,0,67,0,0,0,0,0,'','Allow conversation line 36096 if team is horde'),
(29,0,36099,0,0,6,0,469,0,0,0,0,0,'','Allow conversation line 36099 if team is Alliance'),
(29,0,36100,0,0,6,0,67,0,0,0,0,0,'','Allow conversation line 36100 if team is horde'),
(29,0,36101,0,0,6,0,469,0,0,0,0,0,'','Allow conversation line 36101 if team is Alliance'),
(29,0,36102,0,0,6,0,67,0,0,0,0,0,'','Allow conversation line 36102 if team is horde'),
(29,0,36103,0,0,6,0,469,0,0,0,0,0,'','Allow conversation line 36103 if team is Alliance'),
(29,0,36104,0,0,6,0,67,0,0,0,0,0,'','Allow conversation line 36104 if team is horde');

-- Phasing

DELETE FROM `phase_area` WHERE `AreaId`=13377 AND `PhaseId` IN (13753,14349,14350,14355,15284,15286,15287,15514,15516);
INSERT INTO `phase_area` (`AreaId`,`PhaseId`,`Comment`) VALUES
(13377,13753, 'NPE both ships all stages Unknown'),
(13377,14349, 'NPE Alliance Ship - Captain Garrick Stage 1'),
(13377,14350, 'NPE Alliance Ship - Captain Garrick Stage 1'),
(13377,14355, 'NPE Alliance Ship - Decoration NPC´s Stage 1'),
(13377,15284, 'NPE Horde Ship - Grunt Throg Stage 1'),
(13377,15286, 'NPE Horde Ship - Warlord Breka Grimaxe both Stage 1 (Quest 59927 incomplete'),
(13377,15287, 'NPE Horde Ship - Grunt Throg & Warlord Breka Grimaxe Stage 2 (Quest 59928 complete'),
(13377,15514, 'NPE Horde Ship - Decoration NPC´s Stage 1 (Quest 59927 incomplete'),
(13377,15516, 'NPE Horde Ship - Decoration NPC´s Stage 2 (Quest 59928 complete');

DELETE FROM `phase_area` WHERE `AreaId`=10639 AND `PhaseId` IN (13753,13861,14349,14350,14353,14355);
INSERT INTO `phase_area` (`AreaId`,`PhaseId`,`Comment`) VALUES
(10639,13753, 'NPE both ships all stages Unknown'),
(10639,13861, 'NPE Alliance Ship - Lightning Stage 2'),
(10639,14349, 'NPE Alliance Ship - Captain Garrick Stage 1'),
(10639,14350, 'NPE Alliance Ship - Captain Garrick Stage 1'),
(10639,14353, 'NPE Alliance Ship - Decoration NPC´s Stage 2'),
(10639,14355, 'NPE Alliance Ship - Decoration NPC´s Stage 1');

DELETE FROM `phase_name` WHERE `ID` IN (13753,13861,14349,14350,14353,14355,15284,15286,15287,15514,15516);
INSERT INTO `phase_name` (`ID`,`Name`) VALUES
(13753,'Cosmetic - NPE both ships all stages Unknown'),
(13861,'Cosmetic - NPE Alliance Ship - Lightning Stage 2'),
(14349,'Cosmetic - NPE Alliance Ship - Captain Garrick before quest 56775 complete'),
(14350,'Cosmetic - NPE Alliance Ship - Captain Garrick after quest 56775 complete'),
(14353,'Cosmetic - NPE Alliance Ship - Decoration NPC´s Stage 2'),
(14355,'Cosmetic - NPE Alliance Ship - Decoration NPC´s Stage 1'),
(15284,'Cosmetic - NPE Horde Ship - Grunt Throg Stage 1'),
(15286,'Cosmetic - NPE Horde Ship - Warlord Breka Grimaxe both Stage 1'),
(15287,'Cosmetic - NPE Horde Ship - Grunt Throg & Warlord Breka Grimaxe Stage 2'),
(15514,'Cosmetic - NPE Horde Ship - Decoration NPC´s Stage 1'),
(15516,'Cosmetic - NPE Horde Ship - Decoration NPC´s Stage 2');

-- Crash movie spell cast on player for "Brace for Impact" quest rewarded Alliance and Horde
DELETE FROM `spell_area` WHERE `spell` IN (346797,346799);
INSERT INTO `spell_area` (`spell`,`area`,`quest_start`,`quest_end`,`aura_spell`,`racemask`,`gender`,`flags`,`quest_start_status`,`quest_end_status`) VALUES
(346797,10453,58208,0,305425,0,2,1,64,0),
(346799,10453,59928,0,325131,0,2,1,64,0);

-- Wrong storm spell in db
UPDATE `spell_area` SET `spell`=305421 WHERE `spell`=305422;

-- Add scene scripts
UPDATE `scene_template` SET `ScriptName`='scene_alliance_and_horde_ship' WHERE `SceneId` IN (2236,2486);
DELETE FROM `scene_template` WHERE `SceneId` IN (2334,2487);
INSERT INTO `scene_template` (`SceneId`,`Flags`,`ScriptPackageID`,`Encrypted`,`ScriptName`) VALUES
(2334,16,2708,0,'scene_alliance_and_horde_crash'), -- Alliance
(2487,16,2708,0,'scene_alliance_and_horde_crash'); -- Horde