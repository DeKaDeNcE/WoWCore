SET @CREATURE_ID_ALAR_PHOENIX_GOD = 10000000;
SET @CGUID = @CREATURE_ID_ALAR_PHOENIX_GOD;

DELETE FROM `creature_template` WHERE `entry` = @CREATURE_ID_ALAR_PHOENIX_GOD;
INSERT INTO `creature_template`
(                      `entry`, `difficulty_entry_1`, `difficulty_entry_2`, `difficulty_entry_3`, `KillCredit1`, `KillCredit2`,   `name`, `femaleName`,     `subname`, `TitleAlt`, `IconName`, `HealthScalingExpansion`, `RequiredExpansion`, `VignetteID`, `faction`, `npcflag`, `speed_walk`, `speed_run`, `scale`, `rank`, `dmgschool`, `BaseAttackTime`, `RangeAttackTime`, `BaseVariance`, `RangeVariance`, `unit_class`, `unit_flags`, `unit_flags2`, `unit_flags3`, `dynamicflags`, `family`, `trainer_class`, `type`, `type_flags`, `type_flags2`, `lootid`, `pickpocketloot`, `skinloot`, `VehicleId`, `mingold`, `maxgold`, `AIName`, `MovementType`, `HealthModifier`, `HealthModifierExtra`, `ManaModifier`, `ManaModifierExtra`, `ArmorModifier`, `DamageModifier`, `ExperienceModifier`, `RacialLeader`, `movementId`, `CreatureDifficultyID`, `WidgetSetID`, `WidgetSetUnitConditionID`, `RegenHealth`, `mechanic_immune_mask`, `spell_school_immune_mask`, `flags_extra`, `ScriptName`, `StringId`, `VerifiedBuild`) VALUES
(@CREATURE_ID_ALAR_PHOENIX_GOD,                    0,                    0,                    0,             0,             0, 'Al\'ar',           '', 'Phoenix God',       NULL,       NULL,                        1,                   0,            0,        35,        0,             1,     2.14286,     0.5,     1,            0,             2000,              2000,              1,               1,            1,        32832,          2048,             0,              0,        0,               0,      4,          108,           128,        0,                0,          0,           0,         0,         0,       '',              0,              280,                     1,              1,                   1,               1,               35,                    1,              0,          188,                  15363,             0,                          0,             1,              617299803,                          0,             1,           '',       NULL,           47936);


DELETE FROM `creature_template_addon` WHERE `entry` = @CREATURE_ID_ALAR_PHOENIX_GOD;
INSERT INTO `creature_template_addon`
(                      `entry`, `path_id`, `mount`, `MountCreatureID`, `StandState`, `AnimTier`, `VisFlags`, `SheathState`, `PvPFlags`, `emote`, `aiAnimKit`, `movementAnimKit`, `meleeAnimKit`, `visibilityDistanceType`, `auras`) VALUES
(@CREATURE_ID_ALAR_PHOENIX_GOD,         0,       0,                 0,            0,          0,          0,             1,          0,       0,           0,                 0,              0,                        5,    NULL);

DELETE FROM `creature_template_locale` WHERE `entry` = @CREATURE_ID_ALAR_PHOENIX_GOD;
INSERT INTO `creature_template_locale`
(                      `entry`, `locale`,   `Name`, `NameAlt`,            `Title`, `TitleAlt`, `VerifiedBuild`) VALUES
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'deDE', 'Al\'ar',        '',       'Phönixgott',       NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'esES', 'Al\'ar',        '',       'Dios Fénix',       NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'esMX', 'Al\'ar',        '',       'Dios Fénix',       NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'frFR', 'Al\'ar',        '',      'Dieu phénix',       NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'itIT', 'Al\'ar',      NULL, 'Dio delle Fenici',       NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'koKR',  '알라르',        '',         '불사조 신',      NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'ptBR', 'Al\'ar',      NULL,       'Deus Fênix',       NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'ruRU', 'Ал\'ар',        '',       'Феникс-бог',       NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'zhCN',     '奥',        '',          '凤凰之神',       NULL,           47936),
(@CREATURE_ID_ALAR_PHOENIX_GOD,   'zhTW',   '歐爾',        '',            '鳳凰神',       NULL,           47936);

DELETE FROM `creature_template_model` WHERE `CreatureID` = @CREATURE_ID_ALAR_PHOENIX_GOD;
INSERT INTO `creature_template_model`
(                 `CreatureID`, `Idx`, `CreatureDisplayID`, `DisplayScale`, `Probability`, `VerifiedBuild`) VALUES
(@CREATURE_ID_ALAR_PHOENIX_GOD,     0,               18945,              1,             0,           47936);

DELETE FROM `creature_template_movement` WHERE `CreatureId` = @CREATURE_ID_ALAR_PHOENIX_GOD;
INSERT INTO `creature_template_movement`
(                 `CreatureId`, `Ground`, `Swim`, `Flight`, `Rooted`, `Chase`, `Random`, `InteractionPauseTimer`) VALUES
(@CREATURE_ID_ALAR_PHOENIX_GOD,        0,      1,        1,        0,       0,        0,                    NULL);

DELETE FROM `creature` WHERE `id` = @CREATURE_ID_ALAR_PHOENIX_GOD;
INSERT INTO `creature`
(      `guid`,                          `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `phaseUseFlags`, `PhaseId`, `PhaseGroup`, `terrainSwapMap`, `modelid`, `equipment_id`, `position_x`, `position_y`, `position_z`, `orientation`, `spawntimesecs`, `wander_distance`, `currentwaypoint`, `curhealth`, `curmana`, `MovementType`, `npcflag`, `unit_flags`, `unit_flags2`, `unit_flags3`, `dynamicflags`, `ScriptName`, `StringId`, `VerifiedBuild`) VALUES
(@CGUID +  0, @CREATURE_ID_ALAR_PHOENIX_GOD,     0,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -8912.44,      -140.41,      103.135,       2.01732,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  1, @CREATURE_ID_ALAR_PHOENIX_GOD,     1,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      10383.8,      756.459,      1357.31,       2.43692,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  2, @CREATURE_ID_ALAR_PHOENIX_GOD,     0,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -6167.01,          384,      425.812,       3.31831,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  3, @CREATURE_ID_ALAR_PHOENIX_GOD,     1,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -603.635,     -4200.81,      55.3998,       4.74696,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  4, @CREATURE_ID_ALAR_PHOENIX_GOD,   654,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -1431.07,      1408.72,       58.863,       3.13772,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  5, @CREATURE_ID_ALAR_PHOENIX_GOD,   530,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -4064.31,     -13768.1,      95.6248,       5.87083,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  6, @CREATURE_ID_ALAR_PHOENIX_GOD,     0,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -4962.32,       865.51,      284.016,       3.03644,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  7, @CREATURE_ID_ALAR_PHOENIX_GOD,     0,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      1667.77,      1662.08,      155.794,      0.564776,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  8, @CREATURE_ID_ALAR_PHOENIX_GOD,     1,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -2897.41,     -227.136,      81.8345,       4.72496,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID +  9, @CREATURE_ID_ALAR_PHOENIX_GOD,     1,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -1108.06,     -5337.41,      59.6695,       3.50002,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 10, @CREATURE_ID_ALAR_PHOENIX_GOD,   530,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,        10401,      -6344.3,      52.8721,       3.26929,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 11, @CREATURE_ID_ALAR_PHOENIX_GOD,   648,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -8424.25,       1340.9,      115.592,       5.31547,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 12, @CREATURE_ID_ALAR_PHOENIX_GOD,   860,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      1448.11,      3393.36,      186.514,       1.38736,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 13, @CREATURE_ID_ALAR_PHOENIX_GOD,  1865,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      2166.26,      3319.67,      63.5798,       3.18958,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 14, @CREATURE_ID_ALAR_PHOENIX_GOD,  1220,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,       256.64,      3383.65,      145.474,       2.42615,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 15, @CREATURE_ID_ALAR_PHOENIX_GOD,  1220,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,       4072.9,      4358.77,      700.764,       1.26475,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 16, @CREATURE_ID_ALAR_PHOENIX_GOD,  1860,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      431.663,      1437.05,      776.985,      0.454656,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 17, @CREATURE_ID_ALAR_PHOENIX_GOD,     1,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      1561.47,     -4182.36,      71.7517,       5.49797,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 18, @CREATURE_ID_ALAR_PHOENIX_GOD,  2081,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      1658.17,      499.981,      224.966,       2.32486,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 19, @CREATURE_ID_ALAR_PHOENIX_GOD,  1643,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      1069.19,      -527.68,      39.4336,       2.14486,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 20, @CREATURE_ID_ALAR_PHOENIX_GOD,  1642,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,     -1115.28,      805.067,      526.878,     0.0296266,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0),
(@CGUID + 21, @CREATURE_ID_ALAR_PHOENIX_GOD,  2268,        0,        0,                 '0',               0,         0,            0,               -1,         0,              0,      736.256,       574.26,     -217.913,       2.18807,             300,                 0,                 0,     2122400,         0,              0,         0,            0,             0,             0,              0,           '',       NULL,               0);