################
# NPC Tiger    #
# ENTRY 167347 #
################

SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql
SOURCE ../../../sql/wowcore/world/0_includes/waypoint_move_type.sql

SET @ENTRY  = 167347;
SET @SCRIPT = @ENTRY * 100;

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
DELETE FROM `smart_scripts` WHERE `entryorguid` = @SCRIPT + 1 AND `source_type` = @SMART_TYPE_TIMED_ACTIONLIST;
INSERT INTO `smart_scripts`
(`entryorguid`,                `source_type`, `id`, `link`,                `event_type`,             `event_phase_mask`, `event_chance`,             `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,                       `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                          `comment`) VALUES
(       @ENTRY,         @SMART_TYPE_CREATURE,    0,      0,       @SMART_EVENT_DATA_SET, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              1,              1,              0,              0,              0,                   '', @SMART_ACTION_CALL_TIMED_ACTIONLIST,     @SCRIPT + 1,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0, 'Tiger - On dataset - Load script'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    1,      0, @SMART_EVENT_WAYPOINT_ENDED, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,        @SCRIPT,              0,              0,              0,                   '',         @SMART_ACTION_FORCE_DESPAWN,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,  'Tiger - Path complete - Despawn'),
(  @SCRIPT + 1, @SMART_TYPE_TIMED_ACTIONLIST,    0,      0,                           0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           8000,           8000,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,               1,         @SCRIPT,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,       'Tiger - Script - Load path');

DELETE FROM `waypoint_data` WHERE `id` = @SCRIPT;
INSERT INTO `waypoint_data`
(   `id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`,             `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(@SCRIPT,       1,       31.472,     -2.22219,       4.9285,       2.19466,    1000, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       2,       31.472,     -2.22219,       4.9285,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0);