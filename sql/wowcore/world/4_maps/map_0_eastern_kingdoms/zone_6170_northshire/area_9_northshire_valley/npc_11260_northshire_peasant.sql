###########################
# NPC Northshire Peasant  #
# ENTRY 11260             #
# Emote:                  #
# 234 STATE_WORK_CHOPWOOD #
###########################

SOURCE ../../../sql/wowcore/world/0_includes/emotes.sql

SET @ENTRY = 11260;

# Fix for Northshire Peasant Model
UPDATE `creature` SET `modelid` = 85277 WHERE `MovementType` = 0 AND `id` = @ENTRY AND `guid` IN (279815, 279818, 279826);

# Add Emote 234 Chopping Wood
DELETE FROM `creature_addon` WHERE `guid` IN (279815, 279818, 279826);
INSERT INTO `creature_addon`
(`guid`, `path_id`, `mount`, `MountCreatureID`, `StandState`, `AnimTier`, `VisFlags`, `SheathState`, `PvPFlags`,                    `emote`, `aiAnimKit`, `movementAnimKit`, `meleeAnimKit`, `visibilityDistanceType`, `auras`) VALUES
(279815,         0,       0,                 0,            0,          0,          0,             0,          0, @EMOTE_STATE_WORK_CHOPWOOD,           0,                 0,              0,                        0,    NULL),
(279818,         0,       0,                 0,            0,          0,          0,             0,          0, @EMOTE_STATE_WORK_CHOPWOOD,           0,                 0,              0,                        0,    NULL),
(279826,         0,       0,                 0,            0,          0,          0,             0,          0, @EMOTE_STATE_WORK_CHOPWOOD,           0,                 0,              0,                        0,    NULL);