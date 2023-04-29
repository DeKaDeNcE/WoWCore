#############################
# NPC Warlord Breka Grimaxe #
# ENTRY 166573              #
#############################

SOURCE ../../../sql/wowcore/world/0_includes/languages.sql
SOURCE ../../../sql/wowcore/world/0_includes/emotes.sql
SOURCE ../../../sql/wowcore/world/0_includes/texts.sql

SET @ENTRY  = 166573;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                                                       `Text`,                `Type`,      `Language`, `Probability`,             `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`,        `TextRange`,               `comment`) VALUES
(      @ENTRY,         0,    0,                               'Look sharp, $n! We\'re almost to the island.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156949,               0,            194677, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe'),
(      @ENTRY,         1,    0,            'We need to be ready for anything. Show me your skill in combat.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156950,               0,            195834, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe'),
(      @ENTRY,         2,    0,     'Fine work. Next, we... rain? Our shaman said the skies would be clear.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156951,               0,            195844, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe'),
(      @ENTRY,         3,    0, 'Throg will spar with you for now. I must speak to the crew about the rain.', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156952,               0,            199040, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe'),
(      @ENTRY,         4,    0,                                                'Soldiers, brace yourselves!', @CHAT_MSG_MONSTER_SAY, @LANG_UNIVERSAL,           100, @EMOTE_ONESHOT_NONE,          0,  156953,               0,            195893, @TEXT_RANGE_NORMAL, 'Warlord Breka Grimaxe');