#######################
# NPC Goblin Assassin #
# ENTRY 50039         #
#######################

SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql

SET @ENTRY         = 50039;
SET @TALK_GROUP_ID = 0;

# Add missing texts
DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY AND `GroupID` = @TALK_GROUP_ID;
INSERT INTO `creature_text`
(`CreatureID`,      `GroupID`, `ID`,                                          `Text`, `Type`, `Language`, `Probability`, `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`, `TextRange`,         `comment`) VALUES
(      @ENTRY, @TALK_GROUP_ID,    0,      'We\'ll kill anybody for the right price!',     12,          0,           100,       0,          0,       0,               0,             49837,           0, 'Goblin Assassin'),
(      @ENTRY, @TALK_GROUP_ID,    1, 'Time to join your friends, kissin\' the dirt!',     12,          0,           100,       0,          0,       0,               0,             49838,           0, 'Goblin Assassin'),
(      @ENTRY, @TALK_GROUP_ID,    2,                                        'DIE!!!',     12,          0,           100,       0,          0,       0,               0,             49839,           0, 'Goblin Assassin'),
(      @ENTRY, @TALK_GROUP_ID,    3,   'We\'re gonna burn this place to the ground!',     12,          0,           100,       0,          0,       0,               0,             49840,           0, 'Goblin Assassin');

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

SET @SMART_ACTION_TALK_PARAM1_GROUPID            = @TALK_GROUP_ID;
SET @SMART_ACTION_TALK_PARAM2_DURATION_TEXT_OVER = 0;
SET @SMART_ACTION_TALK_PARAM3_USE_TALK_TARGET    = 1;

# Say text on Aggro
DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
INSERT INTO `smart_scripts`
(`entryorguid`,        `source_type`, `id`, `link`,       `event_type`,        `event_phase_mask`, `event_chance`,                    `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,      `action_type`,                   `action_param1`,                              `action_param2`,                           `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`,                `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                                             `comment`) VALUES
(       @ENTRY, @SMART_TYPE_CREATURE,    0,      0, @SMART_EVENT_AGGRO, @SMART_EVENT_PHASE_ALWAYS,             20, @SMART_EVENT_FLAG_NOT_REPEATABLE,              0,              0,              0,              0,              0,                   '', @SMART_ACTION_TALK, @SMART_ACTION_TALK_PARAM1_GROUPID, @SMART_ACTION_TALK_PARAM2_DURATION_TEXT_OVER, @SMART_ACTION_TALK_PARAM3_USE_TALK_TARGET,               0,               0,               0,               0, @SMART_TARGET_ACTION_INVOKER,               0,               0,               0,               0,          0,          0,          0,          0, 'Goblin Assassin - On Aggro - Say Line 0 (No Repeat)');