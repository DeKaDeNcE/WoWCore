#############################
# NPC Warlord Breka Grimaxe #
# ENTRY 166573              #
#############################

SET @ENTRY  = 166573;

DELETE FROM `creature_text` WHERE `CreatureID` = @ENTRY;
INSERT INTO `creature_text`
(`CreatureID`, `GroupID`, `ID`,                                                                       `Text`, `Type`, `Language`, `Probability`, `Emote`, `Duration`, `Sound`, `SoundPlayType`, `BroadcastTextId`, `TextRange`,               `comment`) VALUES
(      @ENTRY,         0,    0,                               'Look sharp, $n! We\'re almost to the island.',     12,          0,           100,       0,          0,  156949,               0,            194677,           0, 'Warlord Breka Grimaxe'),
(      @ENTRY,         1,    0,            'We need to be ready for anything. Show me your skill in combat.',     12,          0,           100,       0,          0,  156950,               0,            195834,           0, 'Warlord Breka Grimaxe'),
(      @ENTRY,         2,    0,     'Fine work. Next, we... rain? Our shaman said the skies would be clear.',     12,          0,           100,       0,          0,  156951,               0,            195844,           0, 'Warlord Breka Grimaxe'),
(      @ENTRY,         3,    0, 'Throg will spar with you for now. I must speak to the crew about the rain.',     12,          0,           100,       0,          0,  156952,               0,            199040,           0, 'Warlord Breka Grimaxe'),
(      @ENTRY,         4,    0,                                                'Soldiers, brace yourselves!',     12,          0,           100,       0,          0,  156953,               0,            195893,           0, 'Warlord Breka Grimaxe');