###############
# Conditions ##
###############

# Conditions Sources
SET @CONDITION_SOURCE_TYPE_NONE                          =  0;
SET @CONDITION_SOURCE_TYPE_CREATURE_LOOT_TEMPLATE        =  1;
SET @CONDITION_SOURCE_TYPE_DISENCHANT_LOOT_TEMPLATE      =  2;
SET @CONDITION_SOURCE_TYPE_FISHING_LOOT_TEMPLATE         =  3;
SET @CONDITION_SOURCE_TYPE_GAMEOBJECT_LOOT_TEMPLATE      =  4;
SET @CONDITION_SOURCE_TYPE_ITEM_LOOT_TEMPLATE            =  5;
SET @CONDITION_SOURCE_TYPE_MAIL_LOOT_TEMPLATE            =  6;
SET @CONDITION_SOURCE_TYPE_MILLING_LOOT_TEMPLATE         =  7;
SET @CONDITION_SOURCE_TYPE_PICKPOCKETING_LOOT_TEMPLATE   =  8;
SET @CONDITION_SOURCE_TYPE_PROSPECTING_LOOT_TEMPLATE     =  9;
SET @CONDITION_SOURCE_TYPE_REFERENCE_LOOT_TEMPLATE       = 10;
SET @CONDITION_SOURCE_TYPE_SKINNING_LOOT_TEMPLATE        = 11;
SET @CONDITION_SOURCE_TYPE_SPELL_LOOT_TEMPLATE           = 12;
SET @CONDITION_SOURCE_TYPE_SPELL_IMPLICIT_TARGET         = 13;
SET @CONDITION_SOURCE_TYPE_GOSSIP_MENU                   = 14;
SET @CONDITION_SOURCE_TYPE_GOSSIP_MENU_OPTION            = 15;
SET @CONDITION_SOURCE_TYPE_CREATURE_TEMPLATE_VEHICLE     = 16;
SET @CONDITION_SOURCE_TYPE_SPELL                         = 17;
SET @CONDITION_SOURCE_TYPE_SPELL_CLICK_EVENT             = 18;
SET @CONDITION_SOURCE_TYPE_QUEST_AVAILABLE               = 19;
#Condition source type 20 unused
SET @CONDITION_SOURCE_TYPE_VEHICLE_SPELL                 = 21;
SET @CONDITION_SOURCE_TYPE_SMART_EVENT                   = 22;
SET @CONDITION_SOURCE_TYPE_NPC_VENDOR                    = 23;
SET @CONDITION_SOURCE_TYPE_SPELL_PROC                    = 24;
SET @CONDITION_SOURCE_TYPE_TERRAIN_SWAP                  = 25;
SET @CONDITION_SOURCE_TYPE_PHASE                         = 26;
SET @CONDITION_SOURCE_TYPE_GRAVEYARD                     = 27;
SET @CONDITION_SOURCE_TYPE_AREATRIGGER                   = 28;
SET @CONDITION_SOURCE_TYPE_CONVERSATION_LINE             = 29;
SET @CONDITION_SOURCE_TYPE_AREATRIGGER_CLIENT_TRIGGERED  = 30;
SET @CONDITION_SOURCE_TYPE_TRAINER_SPELL                 = 31;
SET @CONDITION_SOURCE_TYPE_OBJECT_ID_VISIBILITY          = 32;
SET @CONDITION_SOURCE_TYPE_SPAWN_GROUP                   = 33;

# Conditions Targets
SET @CONDITION_TARGET_TYPE_SPELL_CLICK_EVENT_CLICKER     =  0;
SET @CONDITION_TARGET_TYPE_SPELL_CLICK_EVENT_CLICKEE     =  1;

# Condition Types                                              value1                   value2         value3
SET @CONDITION_NONE                                      =  0; # 0                      0              0                  always true
SET @CONDITION_AURA                                      =  1; # spell_id               effindex       0                  true if target has aura of spell_id with effect effindex
SET @CONDITION_ITEM                                      =  2; # item_id                count          bank               true if has #count of item_ids (if 'bank' is set it searches in bank slots too)
SET @CONDITION_ITEM_EQUIPPED                             =  3; # item_id                0              0                  true if has item_id equipped
SET @CONDITION_ZONEID                                    =  4; # zone_id                0              0                  true if in zone_id
SET @CONDITION_REPUTATION_RANK                           =  5; # faction_id             rankMask       0                  true if has min_rank for faction_id
SET @CONDITION_TEAM                                      =  6; # player_team            0,             0                  469 - Alliance, 67 - Horde)
SET @CONDITION_SKILL                                     =  7; # skill_id               skill_value    0                  true if has skill_value for skill_id
SET @CONDITION_QUESTREWARDED                             =  8; # quest_id               0              0                  true if quest_id was rewarded before
SET @CONDITION_QUESTTAKEN                                =  9; # quest_id               0,             0                  true while quest active
SET @CONDITION_DRUNKENSTATE                              = 10; # DrunkenState           0,             0                  true if player is drunk enough
SET @CONDITION_WORLD_STATE                               = 11; # index                  value          0                  true if world has the value for the index
SET @CONDITION_ACTIVE_EVENT                              = 12; # event_id               0              0                  true if event is active
SET @CONDITION_INSTANCE_INFO                             = 13; # entry                  data           type               true if the instance info defined by type (enum InstanceInfo) equals data.
SET @CONDITION_QUEST_NONE                                = 14; # quest_id               0              0                  true if doesn't have quest saved
SET @CONDITION_CLASS                                     = 15; # class                  0              0                  true if player's class is equal to class
SET @CONDITION_RACE                                      = 16; # race                   0              0                  true if player's race is equal to race
SET @CONDITION_ACHIEVEMENT                               = 17; # achievement_id         0              0                  true if achievement is complete
SET @CONDITION_TITLE                                     = 18; # title id               0              0                  true if player has title
SET @CONDITION_SPAWNMASK                                 = 19; # DEPRECATED
SET @CONDITION_GENDER                                    = 20; # gender                 0              0                  true if player's gender is equal to gender
SET @CONDITION_UNIT_STATE                                = 21; # unitState              0              0                  true if unit has unitState
SET @CONDITION_MAPID                                     = 22; # map_id                 0              0                  true if in map_id
SET @CONDITION_AREAID                                    = 23; # area_id                0              0                  true if in area_id
SET @CONDITION_CREATURE_TYPE                             = 24; # cinfo.type             0              0                  true if creature_template.type = value1
SET @CONDITION_SPELL                                     = 25; # spell_id               0              0                  true if player has learned spell
SET @CONDITION_PHASEMASK                                 = 26; # phaseid                0              0                  true if object is in phaseid
SET @CONDITION_LEVEL                                     = 27; # level                  ComparisonType 0                  true if unit's level is equal to param1 (param2 can modify the statement)
SET @CONDITION_QUEST_COMPLETE                            = 28; # quest_id               0              0                  true if player has quest_id with all objectives complete, but not yet rewarded
SET @CONDITION_NEAR_CREATURE                             = 29; # creature entry         distance       dead (0/1)         true if there is a creature of entry in range
SET @CONDITION_NEAR_GAMEOBJECT                           = 30; # gameobject entry       distance       0                  true if there is a gameobject of entry in range
SET @CONDITION_OBJECT_ENTRY_GUID                         = 31; # LEGACY_TypeID          entry          guid               true if object is type TypeID and the entry is 0 or matches entry of the object or matches guid of the object
SET @CONDITION_TYPE_MASK                                 = 32; # LEGACY_TypeMask        0              0                  true if object is type object's TypeMask matches provided TypeMask
SET @CONDITION_RELATION_TO                               = 33; # ConditionTarget        RelationType   0                  true if object is in given relation with object specified by ConditionTarget
SET @CONDITION_REACTION_TO                               = 34; # ConditionTarget        rankMask       0                  true if object's reaction matches rankMask object specified by ConditionTarget
SET @CONDITION_DISTANCE_TO                               = 35; # ConditionTarget        distance       ComparisonType     true if object and ConditionTarget are within distance given by parameters
SET @CONDITION_ALIVE                                     = 36; # 0                      0              0                  true if unit is alive
SET @CONDITION_HP_VAL                                    = 37; # hpVal                  ComparisonType 0                  true if unit's hp matches given value
SET @CONDITION_HP_PCT                                    = 38; # hpPct                  ComparisonType 0                  true if unit's hp matches given pct
SET @CONDITION_REALM_ACHIEVEMENT                         = 39; # achievement_id         0              0                  true if realm achievement is complete
SET @CONDITION_IN_WATER                                  = 40; # 0                      0              0                  true if unit in water
SET @CONDITION_TERRAIN_SWAP                              = 41; # terrainSwap            0              0                  true if object is in terrainswap
SET @CONDITION_STAND_STATE                               = 42; # stateType              state          0                  true if unit matches specified sitstate (0,x: has exactly state x; 1,0: any standing state; 1,1: any sitting state;)
SET @CONDITION_DAILY_QUEST_DONE                          = 43; # quest id               0              0                  true if daily quest has been completed for the day
SET @CONDITION_CHARMED                                   = 44; # 0                      0              0                  true if unit is currently charmed
SET @CONDITION_PET_TYPE                                  = 45; # mask                   0              0                  true if player has a pet of given type(s)
SET @CONDITION_TAXI                                      = 46; # 0                      0              0                  true if player is on taxi
SET @CONDITION_QUESTSTATE                                = 47; # quest_id               state_mask     0                  true if player is in any of the provided quest states for the quest (1 = not taken, 2 = completed, 8 = in progress, 32 = failed, 64 = rewarded)
SET @CONDITION_QUEST_OBJECTIVE_COMPLETE                  = 48; # ID                     0              progressValue      true if player has ID objective progress equal to ConditionValue3 (and quest is in quest log)
SET @CONDITION_DIFFICULTY_ID                             = 49; # Difficulty             0              0                  true is map has difficulty id
SET @CONDITION_GAMEMASTER                                = 50; # canBeGM                0              0                  true if player is gamemaster (or can be gamemaster)
SET @CONDITION_OBJECT_ENTRY_GUID                         = 51; # TypeID                 entry          guid               true if object is type TypeID and the entry is 0 or matches entry of the object or matches guid of the object
SET @CONDITION_TYPE_MASK                                 = 52; # TypeMask               0              0                  true if object is type object's TypeMask matches provided TypeMask
SET @CONDITION_BATTLE_PET_COUNT                          = 53; # SpecieId               count          ComparisonType     true if player has `count` of battle pet species
SET @CONDITION_SCENARIO_STEP                             = 54; # ScenarioStepId         0              0                  true if player is at scenario with current step equal to ScenarioStepID
SET @CONDITION_SCENE_IN_PROGRESS                         = 55; # SceneScriptPackageId   0              0                  true if player is playing a scene with ScriptPackageId equal to given value
SET @CONDITION_PLAYER_CONDITION                          = 56; # PlayerConditionId      0              0                  true if player satisfies PlayerCondition