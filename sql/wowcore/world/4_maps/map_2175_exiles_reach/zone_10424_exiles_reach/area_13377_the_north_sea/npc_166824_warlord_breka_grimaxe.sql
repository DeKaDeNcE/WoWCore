#############################
# NPC Warlord Breka Grimaxe #
# ENTRY 166824              #
#############################

SOURCE ../../../sql/wowcore/world/0_includes/waypoint_move_type.sql
SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql
SOURCE ../../../sql/wowcore/world/0_includes/languages.sql
SOURCE ../../../sql/wowcore/world/0_includes/emotes.sql
SOURCE ../../../sql/wowcore/world/0_includes/texts.sql

SET @ENTRY  = 166824;
SET @SCRIPT = @ENTRY * 100;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                                                       `Text`,                `Type`,      `Language`, `Probability`,             `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`,        `TextRange`,               `comment`) VALUES
(      @ENTRY,         0,    0,                               'Look sharp, $n! We\'re almost to the island.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156949,               0,            194677, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe'),
(      @ENTRY,         1,    0,            'We need to be ready for anything. Show me your skill in combat.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156950,               0,            195834, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe'),
(      @ENTRY,         2,    0,     'Fine work. Next, we... rain? Our shaman said the skies would be clear.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156951,               0,            195844, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe'),
(      @ENTRY,         3,    0, 'Throg will spar with you for now. I must speak to the crew about the rain.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156952,               0,            199040, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe'),
(      @ENTRY,         4,    0,                                                'Soldiers, brace yourselves!', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156953,               0,            195893, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe');

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
DELETE FROM `smart_scripts` WHERE `entryorguid` = @SCRIPT AND `source_type` = @SMART_TYPE_TIMED_ACTIONLIST;
INSERT INTO `smart_scripts`
(`entryorguid`,                `source_type`, `id`, `link`,               `event_type`,             `event_phase_mask`, `event_chance`,             `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,                       `action_type`,       `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                                             `comment`) VALUES
(       @ENTRY,         @SMART_TYPE_CREATURE,    0,      0, @SMART_EVENT_JUST_SUMMONED, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,              0,              0,              0,                   '', @SMART_ACTION_CALL_TIMED_ACTIONLIST,               @SCRIPT,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0, 'Warlord Breka Grimaxe - Just Summoned - Load script'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    0,      0,                          0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           1000,           1000,              0,              0,              0,                   '',                  @SMART_ACTION_TALK,                     3,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,              'Warlord Breka Grimaxe - Script - Say 3'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    1,      0,                          0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,              0,              0,              0,                   '',               @SMART_ACTION_SET_RUN,                     0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,        'Warlord Breka Grimaxe - Script - Set Run off'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    2,      0,                          0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,              0,              0,              0,                   '',           @SMART_ACTION_MOVE_TO_POS,                     0,               1,               0,               0,               0,               0,               0,             8,               0,               0,               0,               0, -10.602051,   8.918945,   8.779607,          0,   'Warlord Breka Grimaxe - Script - Move to position'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    3,      0,                          0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           3000,           3000,              0,              0,              0,                   '',            @SMART_ACTION_PLAY_EMOTE, @EMOTE_ONESHOT_SALUTE,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,       'Warlord Breka Grimaxe - Script - Emote Salute'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    4,      0,                          0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           3000,           3000,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,                     1,         @SCRIPT,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,          'Warlord Breka Grimaxe - Script - Load path');

DELETE FROM `waypoint_data` WHERE `id` = @SCRIPT;
INSERT INTO `waypoint_data`
(   `id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`,             `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(@SCRIPT,       1,     2.480713,     9.498291,     9.528435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       2,     3.480713,     9.748291,     9.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       3,     3.730713,     9.998291,    10.028435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       4,     5.230713,    12.248291,    10.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       5,     6.730713,    11.748291,    12.278435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       6,     9.230713,    10.748291,    14.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       7,    12.480713,     9.498291,    17.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       8,    13.730713,     9.498291,    18.528435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       9,    14.480713,     9.248291,    18.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      10,    15.480713,     8.998291,    18.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      11,    15.480713,     6.748291,    18.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      12,    15.730713,     5.748291,    18.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      13,    15.230713,     5.498291,    18.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      14,    15.230713,     5.248291,    18.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      15,    15.980713,     4.248291,    19.528435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      16,    18.480713,     3.248291,    22.028435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      17,    23.480713,     2.248291,    26.278435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      18,    24.480713,   0.24829102,    26.528435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      19,    25.730713,    -1.751709,    27.278435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      20,    28.730713,    -6.001709,    27.278435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      21,    29.730713,    -5.751709,    27.528435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      22,    32.730713,    -7.501709,    30.278435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      23,    35.480713,    -6.751709,    33.528435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      24,    35.980713,    -6.501709,    33.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      25,    38.730713,    -5.501709,    34.528435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      26,    39.980713,    -5.001709,    35.028435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      27,    40.980713,    -4.751709,    36.028435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      28,    44.480713,    -4.001709,    39.778435,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      29,    45.563477,   -3.4223633,    39.777264,        3.1642,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,      30,    45.563477,   -3.4223633,    39.777264,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0);