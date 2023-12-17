# Fixes AreaTriggers around Stormwind Wizard's Sanctum Mage Portal ID 311875
DELETE FROM `areatrigger_template` WHERE `Id` IN (1, 2, 4733, 4734);
INSERT INTO `areatrigger_template`
(`Id`, `IsServerSide`, `Type`, `Flags`, `Data0`, `Data1`, `Data2`, `Data3`, `Data4`, `Data5`, `Data6`, `Data7`, `VerifiedBuild`) VALUES
(4733,              1,      1,       0,       3,       1,       3,       0,       0,       0,       0,       0,               0),
(4734,              1,      1,       0,     3.5,     1.5,       3,       0,       0,       0,       0,       0,               0);

DELETE FROM `areatrigger_template_actions` WHERE `AreaTriggerId` IN (1, 2, 4733, 4734);
INSERT INTO `areatrigger_template_actions`
(`AreaTriggerId`, `IsServerSide`, `ActionType`, `ActionParam`, `TargetType`) VALUES
(           4733,              1,            2,          3631,            5),
(           4734,              1,            2,          3630,            5);

DELETE FROM `areatrigger` WHERE `AreaTriggerId` IN (1, 2, 4733, 4734);
INSERT INTO `areatrigger`
(`SpawnId`, `AreaTriggerId`, `IsServerSide`, `MapId`,   `PosX`,  `PosY`,  `PosZ`, `Orientation`, `PhaseUseFlags`, `PhaseId`, `PhaseGroup`, `Shape`, `ShapeData0`, `ShapeData1`, `ShapeData2`, `ShapeData3`, `ShapeData4`, `ShapeData5`, `ShapeData6`, `ShapeData7`, `ScriptName`,                        `Comment`) VALUES
(     4733,            4733,              1,       0, -9016.11, 876.142, 148.617,        0.7259,               1,         0,            0,       1,            3,            1,            3,            0,            0,            0,            0,            0,           '', 'Stormwind Mage Portal Entrance'),
(     4734,            4734,              1,       0, -8999.87,  864.14, 65.8898,        0.6539,               1,         0,            0,       1,          3.5,          1.5,            3,            0,            0,            0,            0,            0,           '',     'Stormwind Mage Portal Exit');