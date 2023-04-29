#################################
# NPC Alliance Sparring Partner #
# ENTRY 157051                  #
#################################

SOURCE ../../../sql/wowcore/world/0_includes/emotes.sql

SET @ENTRY := 157051;

UPDATE `creature_template` SET `ScriptName`='npc_sparring_partner' WHERE `entry` = @ENTRY;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                                                                 `Text`, `Type`, `Language`, `Probability`,            `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`, `TextRange`,                  `comment`) VALUES
(      @ENTRY,         0,    0, 'I yield! Well, I\'d say you\'re more than ready for whatever we find on that island.',     12,          0,           100, @ONESHOT_CHEER_DNR,          0,  152848,               0,            177677,           0, 'Alliance Sparing Partner'),
(      @ENTRY,         0,    1,             'Never run from your opponent. Stand your ground and fight until the end!',     12,          0,           100,                  0,          0,  152847,               0,            184223,           0, 'Alliance Sparing Partner'),
(      @ENTRY,         0,    2,                                                  'Remember to always face your enemy!',     12,          0,           100,                  0,          0,  152897,               0,            177657,           0, 'Alliance Sparing Partner');

DELETE FROM `creature_summoned_data` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_summoned_data`
(`CreatureID`,`CreatureIDVisibleToSummoner`,`GroundMountDisplayID`,`FlyingMountDisplayID`) VALUES
(      @ENTRY,                       155607,                  NULL,                  NULL);

DELETE FROM `waypoint_data` WHERE `id` = 10501460;
INSERT INTO `waypoint_data`
(    `id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`, `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(10501460,       1,   -13.461914,   0.69628906,     5.677742,          NULL,       0,           0,        0,             100,        0),
(10501460,       2,   -13.461914,   0.69628906,     5.677742,      6.143559,    1000,           0,        0,             100,        0),
(10501460,       3,   -13.461914,   0.69628906,     5.677742,          NULL,       0,           0,        0,             100,        0);