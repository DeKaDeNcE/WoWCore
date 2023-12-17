#######################
# NPC Blackrock Worg  #
# ENTRY 49871         #
# Emote:              #
# 36 ONESHOT_ATTACK1H #
#######################

SOURCE ../../../sql/wowcore/world/0_includes/emotes.sql

SET @ENTRY = 49871;

# Sparring
DELETE FROM `creature_template_sparring` WHERE `Entry` = @ENTRY;
INSERT INTO `creature_template_sparring`
(`Entry`, `NoNPCDamageBelowHealthPct`) VALUES
( @ENTRY, 85);

# Sparring Hackfix: Set MovementType 0 Prevent creature from moving
UPDATE `creature` SET `MovementType` = 0 WHERE `id` = @ENTRY AND `guid` IN (279887, 279889, 279891, 279905, 279929, 279938, 279947, 279916, 279998, 279999, 280000, 280004, 280006, 280013, 280016);

# Sparring Hackfix: Fix position and orientation
UPDATE `creature` SET `position_x` = -8959.87, `position_y` = -228.029, `position_z` = 77.3554, `orientation` = 0.993575 WHERE `id` = @ENTRY AND `guid` = 279887 LIMIT 1;
UPDATE `creature` SET `orientation` = 3.7338407 WHERE `id` = @ENTRY AND `guid` = 279889 LIMIT 1;
UPDATE `creature` SET `position_x` = -8820.068, `position_y` = -141.05054, `position_z` = 81.09247, `orientation` = 4.2228303 WHERE `id` = @ENTRY AND `guid` = 279905 LIMIT 1;
UPDATE `creature` SET `position_x` = -8855.611, `position_y` = -126.54584, `position_z` = 80.979294, `orientation` = 4.3932557 WHERE `id` = @ENTRY AND `guid` = 279929 LIMIT 1;
UPDATE `creature` SET `position_x` = -8871.355, `position_y` = -119.39932, `position_z` = 81.00436, `orientation` = 3.9236 WHERE `id` = @ENTRY AND `guid` = 279938 LIMIT 1;
UPDATE `creature` SET `orientation` = 3.780153 WHERE `id` = @ENTRY AND `guid` = 279998 LIMIT 1;
UPDATE `creature` SET `orientation` = 4.5655518 WHERE `id` = @ENTRY AND `guid` = 279999 LIMIT 1;
UPDATE `creature` SET `orientation` = 1.4962301 WHERE `id` = @ENTRY AND `guid` = 280000 LIMIT 1;
UPDATE `creature` SET `orientation` = 0.5537524 WHERE `id` = @ENTRY AND `guid` = 280004 LIMIT 1;
UPDATE `creature` SET `orientation` = 0.6621338 WHERE `id` = @ENTRY AND `guid` = 280006 LIMIT 1;
UPDATE `creature` SET `position_x` = -8979.26, `position_y` = -67.3655, `position_z` = 90.03, `orientation` = 3.43017 WHERE `id` = @ENTRY AND `guid` = 280013 LIMIT 1;
UPDATE `creature` SET `orientation` = 3.2932088 WHERE `id` = @ENTRY AND `guid` = 280016 LIMIT 1;

# Sparring Hackfix: Add Emote 36 ONESHOT_ATTACK1H
DELETE FROM `creature_addon` WHERE `guid` IN (279887, 279889, 279891, 279905, 279929, 279938, 279947, 279916, 279998, 279999, 280000, 280004, 280006, 280013, 280016);
INSERT INTO `creature_addon`
(`guid`, `path_id`, `mount`, `MountCreatureID`, `StandState`, `AnimTier`, `VisFlags`, `SheathState`, `PvPFlags`,           `emote`, `aiAnimKit`, `movementAnimKit`, `meleeAnimKit`, `visibilityDistanceType`, `auras`) VALUES
(279887,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279889,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279891,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279905,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279929,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279938,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279947,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279916,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279998,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(279999,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(280000,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(280004,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(280006,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(280013,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL),
(280016,         0,       0,                 0,            0,          0,          0,             0,          0, @ONESHOT_ATTACK1H,           0,                 0,              0,                        0,    NULL);