#############################
# NPC Warlord Breka Grimaxe #
# ENTRY 166827              #
#############################

SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql
SOURCE ../../../sql/wowcore/world/0_includes/waypoint_move_type.sql

SET @ENTRY  = 166827;
SET @SCRIPT = @ENTRY * 100;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                                                       `Text`, `Type`, `Language`, `Probability`, `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`, `TextRange`,               `comment`) VALUES
(      @ENTRY,         0,    0,                               'Look sharp, $n! We\'re almost to the island.',     12,          0,           100,       0,          0,  156949,               0,            194677,           0, 'Warlord Breka Grimaxe'),
(      @ENTRY,         1,    0,            'We need to be ready for anything. Show me your skill in combat.',     12,          0,           100,       0,          0,  156950,               0,            195834,           0, 'Warlord Breka Grimaxe'),
(      @ENTRY,         2,    0,     'Fine work. Next, we... rain? Our shaman said the skies would be clear.',     12,          0,           100,       0,          0,  156951,               0,            195844,           0, 'Warlord Breka Grimaxe'),
(      @ENTRY,         3,    0, 'Throg will spar with you for now. I must speak to the crew about the rain.',     12,          0,           100,       0,          0,  156952,               0,            199040,           0, 'Warlord Breka Grimaxe'),
(      @ENTRY,         4,    0,                                                'Soldiers, brace yourselves!',     12,          0,           100,       0,          0,  156953,               0,            195893,           0, 'Warlord Breka Grimaxe');

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
DELETE FROM `smart_scripts` WHERE `entryorguid` = @SCRIPT AND `source_type` = @SMART_TYPE_TIMED_ACTIONLIST;
INSERT INTO `smart_scripts`
(`entryorguid`,                `source_type`, `id`, `link`,                `event_type`, `event_phase_mask`, `event_chance`, `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,                       `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                                                    `comment`) VALUES
(       @ENTRY,         @SMART_TYPE_CREATURE,    0,      1,  @SMART_EVENT_JUST_SUMMONED,                  0,            100,             0,              0,              0,              0,              0,              0,                   '',       @SMART_ACTION_REMOVE_NPC_FLAG,               2,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0, 'Warlord Breka Grimaxe - On summon - Remove questgiver flag'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    1,      0,           @SMART_EVENT_LINK,                  0,            100,             0,              0,              0,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,               1,         @SCRIPT,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,              'Warlord Breka Grimaxe - On summon - Load path'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    2,      0, @SMART_EVENT_WAYPOINT_ENDED,                  0,            100,             0,              0,        @SCRIPT,              0,              0,              0,                   '', @SMART_ACTION_CALL_TIMED_ACTIONLIST,         @SCRIPT,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,        'Warlord Breka Grimaxe - Path complete - Load script'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    3,      0, @SMART_EVENT_WAYPOINT_ENDED,                  0,            100,             0,              0,    @SCRIPT + 1,              0,              0,              0,                   '',         @SMART_ACTION_FORCE_DESPAWN,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,            'Warlord Breka Grimaxe - Path complete - Despawn'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    0,      0,                           0,                  0,            100,             0,           1000,           1000,              0,              0,              0,                   '',                  @SMART_ACTION_TALK,               4,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                     'Warlord Breka Grimaxe - Script - Say 4'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    1,      0,                           0,                  0,            100,             0,           3000,           3000,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,               1,     @SCRIPT + 1,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                 'Warlord Breka Grimaxe - Script - Load path');

DELETE FROM `waypoint_data` WHERE `id` IN (@SCRIPT, @SCRIPT + 1);
INSERT INTO `waypoint_data`
(       `id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`,              `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(    @SCRIPT,       1,    23.386414,   -1.0205078,    26.200092,          NULL,       0,  @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(    @SCRIPT,       2,    14.803528,    1.5292969,    18.348095,          NULL,       0,  @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(    @SCRIPT,       3,    15.010681,    5.6401367,    18.400091,          NULL,       0,  @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(    @SCRIPT,       4,     13.93335,    10.104649,    18.484093,          NULL,       0,  @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(    @SCRIPT,       5,    1.4016113,     10.96875,     9.103544,          NULL,       0,  @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT + 1,       1,   -5.3898315,  -0.84814453,     9.203571,      3.146518,    1000,  @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT + 1,       2,   -5.3898315,  -0.84814453,     9.203571,             0,       0,  @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0);