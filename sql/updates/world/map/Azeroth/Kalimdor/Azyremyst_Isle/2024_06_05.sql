-- Quest POIS

DELETE FROM `quest_poi` WHERE `QuestID` = 9305;
INSERT INTO `quest_poi`(`QuestID`, `BlobIndex`, `Idx1`, `ObjectiveIndex`, `QuestObjectiveID`, `QuestObjectID`, `MapID`, `UiMapID`, `Priority`, `Flags`, `WorldEffectID`, `PlayerConditionID`, `NavigationPlayerConditionID`, `SpawnTrackingID`, `AlwaysAllowMergingBlobs`, `VerifiedBuild`) VALUES 
(9305, 0, 0, -1, 0, 0, 530, 468, 0, 0, 0, 0, 0, 0, 0, 54630),
(9305, 0, 1, 0, 260704, 22978, 530, 468, 0, 0, 0, 0, 0, 0, 0, 54630),
(9305, 0, 2, 32, 0, 0, 530, 468, 0, 2, 0, 0, 0, 0, 0, 54630),
(9305, 1, 3, 32, 0, 0, 530, 97, 0, 2, 0, 0, 0, 0, 0, 54630);

DELETE FROM `quest_poi_points` WHERE `QuestID` = 9305;
INSERT INTO `quest_poi_points`(`QuestID`, `Idx1`, `Idx2`, `X`, `Y`, `Z`, `VerifiedBuild`) VALUES 
(9305, 0, 0, -4185, -13733, 74, 54630),
(9305, 1, 0, -4482, -14122, 152, 54630),
(9305, 1, 1, -4461, -14114, 161, 54630),
(9305, 1, 2, -4358, -13833, 162, 54630),
(9305, 1, 3, -4362, -13826, 167, 54630),
(9305, 1, 4, -4424, -13799, 88, 54630),
(9305, 1, 5, -4437, -13797, 85, 54630),
(9305, 1, 6, -4669, -13970, 85, 54630),
(9305, 2, 0, -4184, -13733, 74, 54630),
(9305, 3, 0, -4184, -13733, 74, 54630);


DELETE FROM `quest_poi` WHERE `QuestID` = 9283;
INSERT INTO `quest_poi`(`QuestID`, `BlobIndex`, `Idx1`, `ObjectiveIndex`, `QuestObjectiveID`, `QuestObjectID`, `MapID`, `UiMapID`, `Priority`, `Flags`, `WorldEffectID`, `PlayerConditionID`, `NavigationPlayerConditionID`, `SpawnTrackingID`, `AlwaysAllowMergingBlobs`, `VerifiedBuild`) VALUES 
(9283, 0, 0, -1, 0, 0, 530, 468, 0, 0, 0, 0, 0, 0, 0, 54630),
(9283, 0, 1, 0, 261114, 16483, 530, 468, 0, 0, 0, 0, 0, 0, 0, 54630),
(9283, 0, 2, 32, 0, 0, 530, 468, 0, 0, 0, 0, 0, 136398, 0, 54630);

DELETE FROM `quest_poi_points` WHERE `QuestID` = 9283;
INSERT INTO `quest_poi_points`(`QuestID`, `Idx1`, `Idx2`, `X`, `Y`, `Z`, `VerifiedBuild`) VALUES 
(9283, 0, 0, -4118, -13763, 74, 54630),
(9283, 1, 0, -3885, -13845, 0, 54630),
(9283, 1, 1, -3858, -13748, 0, 54630),
(9283, 1, 2, -3887, -13533, 0, 54630),
(9283, 1, 3, -3925, -13476, 0, 54630),
(9283, 1, 4, -4319, -13327, 0, 54630),
(9283, 1, 5, -4396, -13349, 0, 54630),
(9283, 1, 6, -4381, -13482, 0, 54630),
(9283, 1, 7, -4319, -13549, 0, 54630),
(9283, 1, 8, -3966, -13833, 0, 54630),
(9283, 2, 0, -4118, -13763, 74, 54630);
