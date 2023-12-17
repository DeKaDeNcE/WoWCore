###################
# NPC Grunt Throg #
# ENTRY 166583    #
###################

SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql
SOURCE ../../../sql/wowcore/world/0_includes/waypoint_move_type.sql

SET @ENTRY  = 166583;
SET @SCRIPT = @ENTRY * 100;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                                   `Text`, `Type`, `Language`, `Probability`, `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`, `TextRange`,     `comment`) VALUES
(      @ENTRY,         0,    0,            'Warlord! This storm will soon overwhelm us!',     12,          0,           100,       0,          0,  156977,               0,            195892,           0, 'Grunt Throg'),
(      @ENTRY,         1,    0, 'I\'ll take my position. Strike first, and strike hard!',     12,          0,           100,       0,          0,  156973,               0,            195852,           0, 'Grunt Throg');

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
DELETE FROM `smart_scripts` WHERE `entryorguid` = @SCRIPT AND `source_type` = @SMART_TYPE_TIMED_ACTIONLIST;
INSERT INTO `smart_scripts`
(`entryorguid`,                `source_type`, `id`, `link`,                `event_type`, `event_phase_mask`, `event_chance`, `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,                       `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                                                          `comment`) VALUES
(       @ENTRY,         @SMART_TYPE_CREATURE,    0,      0, @SMART_EVENT_ACCEPTED_QUEST,                  0,            100,             0,          59927,              0,              0,              0,              0,                   '',             @SMART_ACTION_SELF_CAST,          325107,               0,               0,               0,               0,               0,               0,             7,               0,               0,               0,               0,          0,          0,          0,          0, 'Grunt Throg - On quest accept - Player cast Summon Throg on self'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    1,      0,  @SMART_EVENT_JUST_SUMMONED,                  0,            100,             0,              0,              0,              0,              0,              0,                   '', @SMART_ACTION_CALL_TIMED_ACTIONLIST,         @SCRIPT,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                            'Grunt Throg - On summon - Load script'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    2,      0, @SMART_EVENT_WAYPOINT_ENDED,                  0,            100,             0,              0,        @SCRIPT,              0,              0,              0,                   '',         @SMART_ACTION_FORCE_DESPAWN,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                            'Grunt Throg - Path complete - Despawn'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    0,      0,                           0,                  0,            100,             0,           3000,           3000,              0,              0,              0,                   '',                  @SMART_ACTION_TALK,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                                     'Grunt Throg - Script - Say 0'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    1,      0,                           0,                  0,            100,             0,           6000,           6000,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,               1,         @SCRIPT,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                                 'Grunt Throg - Script - Load path');

DELETE FROM `waypoint_data` WHERE `id` = @SCRIPT;
INSERT INTO `waypoint_data`
(   `id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`,             `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(@SCRIPT,       1,   -3.9334717,     1.109375,      9.06326,      3.353885,    1000, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       2,   -3.9334717,     1.109375,      9.06326,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0);