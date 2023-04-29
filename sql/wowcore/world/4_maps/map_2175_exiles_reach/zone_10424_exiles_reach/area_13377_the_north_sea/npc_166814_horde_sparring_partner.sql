##############################
# NPC Horde Sparring Partner #
# ENTRY 166814               #
##############################

SOURCE ../../../sql/wowcore/world/0_includes/emotes.sql
SOURCE ../../../sql/wowcore/world/0_includes/waypoint_move_type.sql

SET @ENTRY = 166814;

UPDATE `creature_template` SET `ScriptName` = 'npc_sparring_partner' WHERE `entry` = @ENTRY;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                                   `Text`, `Type`, `Language`, `Probability`,            `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`, `TextRange`,               `comment`) VALUES
(      @ENTRY,         0,    0, 'I\'ll take my position. Strike first, and strike hard!',     12,          0,           100,                  0,          0,  156973,               0,            195852,           0, 'Horde Sparing Partner'),
(      @ENTRY,         1,    0,             'Never run from your foe! Victory or death!',     12,          0,           100,                  0,          0,  156974,               0,            195853,           0, 'Horde Sparing Partner'),
(      @ENTRY,         2,    0,                                'Always face your enemy!',     12,          0,           100,                  0,          0,  156975,               0,            195854,           0, 'Horde Sparing Partner'),
(      @ENTRY,         3,    0, 'I concede! Your strength will see our mission through.',     12,          0,           100, @ONESHOT_CHEER_DNR,          0,  156976,               0,            195857,           0, 'Horde Sparing Partner');

DELETE FROM `creature_summoned_data` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_summoned_data`
(`CreatureID`, `CreatureIDVisibleToSummoner`, `GroundMountDisplayID`, `FlyingMountDisplayID`) VALUES
(      @ENTRY,                        166815,                   NULL,                   NULL);

DELETE FROM `waypoint_data` WHERE `id` = 10501870;
INSERT INTO `waypoint_data`
(    `id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`,              `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(10501870,       1,   -10.846191,    11.937012,    8.9623165,          NULL,       0, @WAYPOINT_MOVE_TYPE_WALK,        0,             100,        0),
(10501870,       2,   -10.846191,    11.937012,    8.9623165,      4.677482,    1000, @WAYPOINT_MOVE_TYPE_WALK,        0,             100,        0),
(10501870,       3,   -10.846191,    11.937012,    8.9623165,          NULL,       0, @WAYPOINT_MOVE_TYPE_WALK,        0,             100,        0);