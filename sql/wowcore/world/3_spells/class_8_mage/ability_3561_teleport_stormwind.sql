# Mage Ability: 3561 Teleport: Stormwind
DELETE FROM `spell_target_position` WHERE `ID` = 3561;
INSERT INTO `spell_target_position`
(`ID`, `EffectIndex`, `MapID`, `PositionX`, `PositionY`, `PositionZ`, `VerifiedBuild`) VALUES
(3561,             0,       0,       -9041,      917.66,       66.69,           41488);