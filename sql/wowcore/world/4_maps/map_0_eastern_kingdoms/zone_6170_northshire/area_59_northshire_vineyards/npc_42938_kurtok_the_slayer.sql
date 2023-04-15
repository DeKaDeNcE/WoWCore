##########################
# NPC Kurtok the Slayer  #
# ENTRY 42938            #
# BUGS:                  #
# Missing emote on idle  #
# Missing emote on aggro #
# Missing buff on weapon #
##########################

SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql

SET @ENTRY               = 42938;
SET @TALK_GROUP_ID_AGGRO = 0;
SET @TALK_GROUP_ID_DEATH = 1;

# Add missing texts
DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY  AND `GroupID` IN (@TALK_GROUP_ID_AGGRO, @TALK_GROUP_ID_DEATH);
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                     `Text`, `Type`, `Language`, `Probability`, `Emote`, `Duration`, `Sound`, `BroadcastTextId`, `TextRange`,                     `comment`) VALUES
(      @ENTRY,         0,    0, 'Alliance weakling, your lands will burn!',     12,          0,           100,       0,          0,       0,                 0,           0, 'Kurtok the Slayer Aggro Say'),
(      @ENTRY,         1,    0,       'The Blackrock Clan will end you...',     12,          0,           100,       0,          0,       0,                 0,           0, 'Kurtok the Slayer Death Say');

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

SET @SMART_ACTION_TALK_PARAM1_GROUPID_AGGRO      = @TALK_GROUP_ID_AGGRO;
SET @SMART_ACTION_TALK_PARAM1_GROUPID_DEATH      = @TALK_GROUP_ID_DEATH;
SET @SMART_ACTION_TALK_PARAM2_DURATION_TEXT_OVER = 0;
SET @SMART_ACTION_TALK_PARAM3_USE_TALK_TARGET    = 1;

# Say text on Aggro and Death
DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
INSERT INTO `smart_scripts`
(`entryorguid`,        `source_type`, `id`, `link`,       `event_type`,        `event_phase_mask`, `event_chance`,                    `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,      `action_type`,                         `action_param1`,                              `action_param2`,                           `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`,                `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                                               `comment`) VALUES
(       @ENTRY, @SMART_TYPE_CREATURE,    0,      0, @SMART_EVENT_AGGRO, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NOT_REPEATABLE,              0,              0,              0,              0,              0,                   '', @SMART_ACTION_TALK, @SMART_ACTION_TALK_PARAM1_GROUPID_AGGRO, @SMART_ACTION_TALK_PARAM2_DURATION_TEXT_OVER, @SMART_ACTION_TALK_PARAM3_USE_TALK_TARGET,               0,               0,               0,               0, @SMART_TARGET_ACTION_INVOKER,               0,               0,               0,               0,          0,          0,          0,          0, 'Kurtok the Slayer - On Aggro - Say Line 0 (No Repeat)'),
(       @ENTRY, @SMART_TYPE_CREATURE,    1,      0, @SMART_EVENT_DEATH, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NOT_REPEATABLE,              0,              0,              0,              0,              0,                   '', @SMART_ACTION_TALK, @SMART_ACTION_TALK_PARAM1_GROUPID_DEATH, @SMART_ACTION_TALK_PARAM2_DURATION_TEXT_OVER, @SMART_ACTION_TALK_PARAM3_USE_TALK_TARGET,               0,               0,               0,               0, @SMART_TARGET_ACTION_INVOKER,               0,               0,               0,               0,          0,          0,          0,          0, 'Kurtok the Slayer - On Death - Say Line 0 (No Repeat)');