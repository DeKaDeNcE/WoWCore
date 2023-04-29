################
# NPC Kee-la   #
# ENTRY 157043 #
################

SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql
SOURCE ../../../sql/wowcore/world/0_includes/waypoint_move_type.sql

SET @ENTRY  = 157043;
SET @SCRIPT = @ENTRY * 100;

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
DELETE FROM `smart_scripts` WHERE `entryorguid` = @SCRIPT AND `source_type` = @SMART_TYPE_TIMED_ACTIONLIST;
INSERT INTO `smart_scripts`
(`entryorguid`,                `source_type`, `id`, `link`,                `event_type`,             `event_phase_mask`, `event_chance`,             `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,                       `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                          `comment`) VALUES
(       @ENTRY,         @SMART_TYPE_CREATURE,    0,      0,  @SMART_EVENT_JUST_SUMMONED, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,              0,              0,              0,                   '', @SMART_ACTION_CALL_TIMED_ACTIONLIST,         @SCRIPT,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0, 'Kee-la - On summon - Load script'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    1,      0, @SMART_EVENT_WAYPOINT_ENDED, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,        @SCRIPT,              0,              0,              0,                   '',         @SMART_ACTION_FORCE_DESPAWN,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0, 'Kee-la - Path complete - Despawn'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    0,      0,                           0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           8000,           8000,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,               1,         @SCRIPT,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,      'Kee-la - Script - Load path');

DELETE FROM `waypoint_data` WHERE `id` = @SCRIPT;
INSERT INTO `waypoint_data`
(   `id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`,             `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(@SCRIPT,       1,    -9.640718,     3.819191,    5.6868877,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       2,    -3.890718,     3.319191,    5.4368877,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       3,     1.609282,     3.819191,    5.4368877,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       4,    10.359282,     4.819191,    5.1868877,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       5,    16.609282,     5.569191,    5.1868877,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       6,    23.859282,     5.069191,    5.1868877,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       7,    27.859282,     4.069191,    5.1868877,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       8,    29.109282,  -0.43080902,    5.1868877,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       9,    27.582764,  -0.94091797,     4.871175,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0);