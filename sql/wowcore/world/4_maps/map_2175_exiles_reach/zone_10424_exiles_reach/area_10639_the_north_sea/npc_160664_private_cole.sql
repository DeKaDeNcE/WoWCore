####################
# NPC Private Cole #
# ENTRY 160664     #
####################

SOURCE ../../../sql/wowcore/world/0_includes/waypoint_move_type.sql
SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql
SOURCE ../../../sql/wowcore/world/0_includes/languages.sql
SOURCE ../../../sql/wowcore/world/0_includes/emotes.sql
SOURCE ../../../sql/wowcore/world/0_includes/quests.sql
SOURCE ../../../sql/wowcore/world/0_includes/spells.sql
SOURCE ../../../sql/wowcore/world/0_includes/texts.sql

SET @ENTRY  = 160664;
SET @SCRIPT = @ENTRY * 100;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                                                      `Text`,                `Type`,      `Language`, `Probability`,             `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`,        `TextRange`,      `comment`) VALUES
(      @ENTRY,         0,    0,                           'Captain! We can\'t weather this storm for long!', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  152849,               0,            177675, @TEXT_RANGE_NORMAL, 'Private Cole'),
(      @ENTRY,         1,    0, 'Let\'s move into sparring positions. I\'ll let you have the first strike.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  152846,               0,            177651, @TEXT_RANGE_NORMAL, 'Private Cole');

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

DELETE FROM `smart_scripts` WHERE `entryorguid`= @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
DELETE FROM `smart_scripts` WHERE `entryorguid`= @SCRIPT AND `source_type` = @SMART_TYPE_TIMED_ACTIONLIST;
INSERT INTO `smart_scripts`
(`entryorguid`,                `source_type`, `id`, `link`,                `event_type`,             `event_phase_mask`, `event_chance`,             `event_flags`,                    `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,                       `action_type`,    `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                                                          `comment`) VALUES
(       @ENTRY,         @SMART_TYPE_CREATURE,    0,      0, @SMART_EVENT_ACCEPTED_QUEST, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG, @QUEST_ALLIANCE_STAND_YOUR_GROUND,              0,              0,              0,              0,                   '',             @SMART_ACTION_SELF_CAST, @SPELL_SUMMON_COLE,               0,               0,               0,               0,               0,               0,             7,               0,               0,               0,               0,          0,          0,          0,          0, 'Private Cole - On quest accept - Player cast Summon Cole on self'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    1,      0,  @SMART_EVENT_JUST_SUMMONED, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,                                 0,              0,              0,              0,              0,                   '', @SMART_ACTION_CALL_TIMED_ACTIONLIST,            @SCRIPT,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                           'Private Cole - On summon - Load script'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    2,      0, @SMART_EVENT_WAYPOINT_ENDED, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,                                 0,        @SCRIPT,              0,              0,              0,                   '',         @SMART_ACTION_FORCE_DESPAWN,                  0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                           'Private Cole - Path complete - Despawn'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    0,      0,                           0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,                              3000,           3000,              0,              0,              0,                   '',                  @SMART_ACTION_TALK,                  0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                                    'Private Cole - Script - Say 0'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    1,      0,                           0, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,                              6000,           6000,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,                  1,         @SCRIPT,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,          0,          0,          0,          0,                                'Private Cole - Script - Load path');

DELETE FROM `waypoint_data` WHERE `id`= @SCRIPT;
INSERT INTO `waypoint_data`
(   `id`, `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`,             `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(@SCRIPT,       1,    30.931396,    6.4296875,     4.817522,        4.1208,    1000, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0),
(@SCRIPT,       2,    30.931396,    6.4296875,     4.817522,          NULL,       0, @WAYPOINT_MOVE_TYPE_RUN,        0,             100,        0);