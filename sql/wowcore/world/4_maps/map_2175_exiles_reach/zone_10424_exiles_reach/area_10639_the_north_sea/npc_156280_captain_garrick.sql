##########################
# NPC Captain Garrick    #
# ENTRY 156280           #
##########################

SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql
SOURCE ../../../sql/wowcore/world/0_includes/emotes.sql

SET @ENTRY  := 156280;
SET @SCRIPT := @ENTRY * 100;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`

(`CreatureID`, `GroupID`, `ID`,                                                                                                      `Text`, `Type`, `Language`, `Probability`, `Emote`, `Duration`, `Sound`,   `SoundPlayType`, `BroadcastTextId`, `TextRange`,                   `comment`) VALUES
(      @ENTRY,         0,    0, 'Private Cole will run you through the rest of the drills. I need to discuss this storm with the helmsman.',     12,          0,           100,       0,          0,  152734,                 0,            184106,           0, 'Captain Garrick to Player'),
(      @ENTRY,         1,    0,                                                                                'Everyone below decks! Now!',     12,          0,           100,       0,          0,  152735,                 0,            177674,           0, 'Captain Garrick to Player'),
(      @ENTRY,         2,    0,                                                          '$n, step forward! We\'re approaching the island.',     12,          0,           100,       0,          0,  152731,                 0,            169340,           0, 'Captain Garrick to Player');


DELETE FROM `conversation_template` WHERE `Id` IN (10768,12818,12798);
INSERT INTO `conversation_template`
(`Id`,`FirstLineId`,`TextureKitId`,`ScriptName`,`VerifiedBuild`) VALUES
(10768,32717,0,'',45745),
(12818,31445,0,'',45745),
(12798,31382,0,'',45745);

DELETE FROM `conversation_line_template` WHERE `Id` IN (32717,31445,31382);
INSERT INTO `conversation_line_template`
(`Id`,`UiCameraID`,`ActorIdx`,`Flags`,`VerifiedBuild`) VALUES
(32717,0,0,0,45745),
(31445,0,0,0,45745),
(31382,0,0,0,45745);

DELETE FROM `conversation_actors` WHERE `ConversationId` IN (10768,12818,12798);
INSERT INTO `conversation_actors`
(`ConversationId`,`ConversationActorId`,`ConversationActorGuid`,`Idx`,`CreatureId`,`CreatureDisplayInfoId`,`NoActorObject`,`ActivePlayerObject`,`VerifiedBuild`) VALUES
(10768,73720,1050145,0,156280,92690,0,0,45745),
(12818,73720,1050145,0,156280,92690,0,0,45745),
(12798,73720,1050145,0,156280,92690,0,0,45745);

#DELETE FROM `conditions` WHERE `SourceEntry` = @ENTRY;
#INSERT INTO `conditions` VALUES ('22', '1', @ENTRY, '0', '0', '28', '0', '56775', '0', '0', '1', '0', '0', '', 'Execute SmartAI');

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY AND `source_type` = @SMART_TYPE_CREATURE;
DELETE FROM `smart_scripts` WHERE `entryorguid` IN (@SCRIPT, @SCRIPT + 1) AND `source_type` = @SMART_TYPE_TIMED_ACTIONLIST;
INSERT INTO `smart_scripts`
(`entryorguid`,                `source_type`, `id`, `link`,                `event_type`,        `event_phase_mask`, `event_chance`,             `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,                       `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`,  `target_x`, `target_y`, `target_z`, `target_o`,                                                                `comment`) VALUES
(       @ENTRY,         @SMART_TYPE_CREATURE,    0,      0,       @SMART_EVENT_DATA_SET, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              1,              1,              0,              0,              0,                   '', @SMART_ACTION_CALL_TIMED_ACTIONLIST,         @SCRIPT,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                             'Captain Garrick - On dataset - Load script'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    1,      0, @SMART_EVENT_WAYPOINT_ENDED, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,        @SCRIPT,              0,              0,              0,                   '',         @SMART_ACTION_FORCE_DESPAWN,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                              'Captain Garrick - Path complete - Despawn'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    2,      3,       @SMART_EVENT_DATA_SET, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              1,              2,              0,              0,              0,                   '',       @SMART_ACTION_REMOVE_NPC_FLAG,               2,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                  'Captain Garrick - On dataset - Remove questgiver flag'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    3,      0,           @SMART_EVENT_LINK, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,               1,     @SCRIPT + 1,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                               'Captain Garrick - On dataset - Load path'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    4,      0, @SMART_EVENT_WAYPOINT_ENDED, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,    @SCRIPT + 1,              0,              0,              0,                   '', @SMART_ACTION_CALL_TIMED_ACTIONLIST,     @SCRIPT + 1,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                          'Captain Garrick - Path complete - Load script'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    5,      0, @SMART_EVENT_WAYPOINT_ENDED, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,    @SCRIPT + 2,              0,              0,              0,                   '',         @SMART_ACTION_FORCE_DESPAWN,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                              'Captain Garrick - Path complete - Despawn'),
(       @ENTRY,         @SMART_TYPE_CREATURE,    6,      0,        @SMART_EVENT_OOC_LOS, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              1,              8,           6000,           6000,              1,                   '',                  @SMART_ACTION_TALK,               2,               0,               0,               0,               0,               0,               0,             7,               0,               0,               0,               0,           0,          0,          0,          0, 'Captain Garrick - Player Within 8 Range Out of Combat LoS - Say Line 0'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    0,      0,      @SMART_EVENT_UPDATE_IC, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           1000,           1000,              0,              0,              0,                   '',                  @SMART_ACTION_TALK,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                                       'Captain Garrick - Script - Say 0'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    1,      0,      @SMART_EVENT_UPDATE_IC, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,              0,              0,              0,                   '',               @SMART_ACTION_SET_RUN,               0,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                                 'Captain Garrick - Script - Set Run off'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    2,      0,      @SMART_EVENT_UPDATE_IC, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,              0,              0,              0,                   '',           @SMART_ACTION_MOVE_TO_POS,               0,               1,               0,               0,               0,               0,               0,             8,               0,               0,               0,               0,  -11.810547,  0.9602051,  5.5279408,          0,                            'Captain Garrick - Script - Move to position'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    3,      0,      @SMART_EVENT_UPDATE_IC, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           3000,           3000,              0,              0,              0,                   '',            @SMART_ACTION_PLAY_EMOTE, @ONESHOT_SALUTE,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                                'Captain Garrick - Script - Emote Salute'),
(      @SCRIPT, @SMART_TYPE_TIMED_ACTIONLIST,    4,      0,      @SMART_EVENT_UPDATE_IC, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           4000,           4000,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,               0,         @SCRIPT,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                                   'Captain Garrick - Script - Load path'),
(  @SCRIPT + 1, @SMART_TYPE_TIMED_ACTIONLIST,    0,      0,      @SMART_EVENT_UPDATE_IC, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           1000,           1000,              0,              0,              0,                   '',                  @SMART_ACTION_TALK,               1,               0,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                                       'Captain Garrick - Script - Say 1'),
(  @SCRIPT + 1, @SMART_TYPE_TIMED_ACTIONLIST,    1,      0,      @SMART_EVENT_UPDATE_IC, @SMART_EVENT_PHASE_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,           3000,           3000,              0,              0,              0,                   '',              @SMART_ACTION_WP_START,               1,     @SCRIPT + 2,               0,               0,               0,               0,               0,             1,               0,               0,               0,               0,           0,          0,          0,          0,                                   'Captain Garrick - Script - Load path');

DELETE FROM `waypoint_data` WHERE `id`IN (@SCRIPT, @SCRIPT + 1, @SCRIPT + 2);
INSERT INTO `waypoint_data`
(       `id`,   `point`, `position_x`, `position_y`, `position_z`, `orientation`, `delay`, `move_type`, `action`, `action_chance`, `wpguid`) VALUES
(    @SCRIPT,         1,   -0.3022461,    -1.409729,     5.774313,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,         2,     4.697754,    -2.659729,     5.524313,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,         3,     8.697754,    -4.159729,     5.524313,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,         4,    12.447754,    -5.159729,     5.524313,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,         5,    15.947754,    -5.409729,     6.274313,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,         6,    18.947754,    -5.409729,     9.274313,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,         7,    21.197754,    -4.659729,     9.774313,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,         8,    30.197754,    -3.659729,    11.024313,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,         9,    37.706055,    -3.779663,    12.020686,          NULL,       0,           0,        0,             100,        0),
(    @SCRIPT,        10,     37.83252,   -1.4055176,    12.501659,    3.31911778,       0,           0,        0,             100,        0),
(    @SCRIPT,        11,     37.83252,   -1.4055176,    12.501659,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         1,    38.556152,   -0.6357422,    13.232409,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         2,     35.56421,   -1.1977539,      12.1479,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         3,    32.572266,   -1.7597656,    11.063391,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         4,    28.660645,   -2.8950195,    10.562886,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         5,    22.253174,    -4.680664,      9.82038,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         6,    18.129395,    -4.897949,     8.297633,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         7,    13.874268,   -5.3256836,     5.080761,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         8,    10.271484,   -5.2910156,     5.001395,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 1,         9,     4.762451,   -3.1137695,    5.1861935,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 2,         1,    11.084107,    3.9018552,      5.28156,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 2,         2,    17.084106,    4.9018555,      5.53156,          NULL,       0,           0,        0,             100,        0),
(@SCRIPT + 2,         3,    26.905762,    5.4174805,     4.876927,     3.0892324,    1000,           0,        0,             100,        0),
(@SCRIPT + 2,         4,    26.905762,    5.4174805,     4.876927,          NULL,       0,           0,        0,             100,        0);