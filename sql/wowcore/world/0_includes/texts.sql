#####################
# Chat Message Type #
#####################

SET @CHAT_MSG_ADDON                            = -1;
SET @CHAT_MSG_SYSTEM                           =  0; # 0x00
SET @CHAT_MSG_SAY                              =  1; # 0x01
SET @CHAT_MSG_PARTY                            =  2; # 0x02
SET @CHAT_MSG_RAID                             =  3; # 0x03
SET @CHAT_MSG_GUILD                            =  4; # 0x04
SET @CHAT_MSG_OFFICER                          =  5; # 0x05
SET @CHAT_MSG_YELL                             =  6; # 0x06
SET @CHAT_MSG_WHISPER                          =  7; # 0x07
SET @CHAT_MSG_WHISPER_FOREIGN                  =  8; # 0x08
SET @CHAT_MSG_WHISPER_INFORM                   =  9; # 0x09
SET @CHAT_MSG_EMOTE                            = 10; # 0x0A
SET @CHAT_MSG_TEXT_EMOTE                       = 11; # 0x0B
SET @CHAT_MSG_MONSTER_SAY                      = 12; # 0x0C
SET @CHAT_MSG_MONSTER_PARTY                    = 13; # 0x0D
SET @CHAT_MSG_MONSTER_YELL                     = 14; # 0x0E
SET @CHAT_MSG_MONSTER_WHISPER                  = 15; # 0x0F
SET @CHAT_MSG_MONSTER_EMOTE                    = 16; # 0x10
SET @CHAT_MSG_CHANNEL                          = 17; # 0x11
SET @CHAT_MSG_CHANNEL_JOIN                     = 18; # 0x12
SET @CHAT_MSG_CHANNEL_LEAVE                    = 19; # 0x13
SET @CHAT_MSG_CHANNEL_LIST                     = 20; # 0x14
SET @CHAT_MSG_CHANNEL_NOTICE                   = 21; # 0x15
SET @CHAT_MSG_CHANNEL_NOTICE_USER              = 22; # 0x16
SET @CHAT_MSG_AFK                              = 23; # 0x17
SET @CHAT_MSG_DND                              = 24; # 0x18
SET @CHAT_MSG_IGNORED                          = 25; # 0x19
SET @CHAT_MSG_SKILL                            = 26; # 0x1A
SET @CHAT_MSG_LOOT                             = 27; # 0x1B
SET @CHAT_MSG_MONEY                            = 28; # 0x1C
SET @CHAT_MSG_OPENING                          = 29; # 0x1D
SET @CHAT_MSG_TRADESKILLS                      = 30; # 0x1E
SET @CHAT_MSG_PET_INFO                         = 31; # 0x1F
SET @CHAT_MSG_COMBAT_MISC_INFO                 = 32; # 0x20
SET @CHAT_MSG_COMBAT_XP_GAIN                   = 33; # 0x21
SET @CHAT_MSG_COMBAT_HONOR_GAIN                = 34; # 0x22
SET @CHAT_MSG_COMBAT_FACTION_CHANGE            = 35; # 0x23
SET @CHAT_MSG_BG_SYSTEM_NEUTRAL                = 36; # 0x24
SET @CHAT_MSG_BG_SYSTEM_ALLIANCE               = 37; # 0x25
SET @CHAT_MSG_BG_SYSTEM_HORDE                  = 38; # 0x26
SET @CHAT_MSG_RAID_LEADER                      = 39; # 0x27
SET @CHAT_MSG_RAID_WARNING                     = 40; # 0x28
SET @CHAT_MSG_RAID_BOSS_EMOTE                  = 41; # 0x29
SET @CHAT_MSG_RAID_BOSS_WHISPER                = 42; # 0x2A
SET @CHAT_MSG_FILTERED                         = 43; # 0x2B
SET @CHAT_MSG_RESTRICTED                       = 44; # 0x2C
SET @CHAT_MSG_BATTLENET                        = 45; # 0x2D
SET @CHAT_MSG_ACHIEVEMENT                      = 46; # 0x2E
SET @CHAT_MSG_GUILD_ACHIEVEMENT                = 47; # 0x2F
SET @CHAT_MSG_ARENA_POINTS                     = 48; # 0x30
SET @CHAT_MSG_PARTY_LEADER                     = 49; # 0x31
SET @CHAT_MSG_TARGETICONS                      = 50; # 0x32
SET @CHAT_MSG_BN_WHISPER                       = 51; # 0x33
SET @CHAT_MSG_BN_WHISPER_INFORM                = 52; # 0x34
SET @CHAT_MSG_BN_INLINE_TOAST_ALERT            = 53; # 0x35
SET @CHAT_MSG_BN_INLINE_TOAST_BROADCAST        = 54; # 0x36
SET @CHAT_MSG_BN_INLINE_TOAST_BROADCAST_INFORM = 55; # 0x37
SET @CHAT_MSG_BN_INLINE_TOAST_CONVERSATION     = 56; # 0x38
SET @CHAT_MSG_BN_WHISPER_PLAYER_OFFLINE        = 57; # 0x39
SET @CHAT_MSG_CURRENCY                         = 58; # 0x3A
SET @CHAT_MSG_QUEST_BOSS_EMOTE                 = 59; # 0x3B
SET @CHAT_MSG_PET_BATTLE_COMBAT_LOG            = 60; # 0x3C
SET @CHAT_MSG_PET_BATTLE_INFO                  = 61; # 0x3D
SET @CHAT_MSG_INSTANCE_CHAT                    = 62; # 0x3E
SET @CHAT_MSG_INSTANCE_CHAT_LEADER             = 63; # 0x3F
SET @CHAT_MSG_GUILD_ITEM_LOOTED                = 64; # 0x40
SET @CHAT_MSG_COMMUNITIES_CHANNEL              = 65; # 0x41
SET @CHAT_MSG_VOICE_TEXT                       = 66; # 0x42

##############
# Text Range #
##############

SET @TEXT_RANGE_NORMAL                         = 0;
SET @TEXT_RANGE_AREA                           = 1;
SET @TEXT_RANGE_ZONE                           = 2;
SET @TEXT_RANGE_MAP                            = 3;
SET @TEXT_RANGE_WORLD                          = 4;
SET @TEXT_RANGE_PERSONAL                       = 5;