// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.﻿

using System;

namespace Framework.Constants
{
    public struct BattlegroundConst
    {
        //Time Intervals
        public const uint CheckPlayerPositionInverval = 1000;   // Ms
        public const uint ResurrectionInterval = 30000;         // Ms
        //RemindInterval                 = 10000,               // Ms
        public const uint InvitationRemindTime = 20000;         // Ms
        public const uint InviteAcceptWaitTime = 90000;         // Ms
        public const uint AutocloseBattleground = 120000;       // Ms
        public const uint MaxOfflineTime = 300;                 // Secs
        public const uint RespawnOneDay = 86400;                // Secs
        public const uint RespawnImmediately = 0;               // Secs
        public const uint BuffRespawnTime = 180;                // Secs
        public const uint BattlegroundCountdownMax = 120;       // Secs
        public const uint ArenaCountdownMax = 60;               // Secs
        public const uint PlayerPositionUpdateInterval = 5000;  // Ms

        //EventIds
        public const int EventIdFirst = 0;
        public const int EventIdSecond = 1;
        public const int EventIdThird = 2;
        public const int EventIdFourth = 3;
        public const int EventIdCount = 4;

        //Quests
        public const uint WsQuestReward = 43483;
        public const uint AbQuestReward = 43484;
        public const uint AvQuestReward = 43475;
        public const uint AvQuestKilledBoss = 23658;
        public const uint EyQuestReward = 43477;
        public const uint SaQuestReward = 61213;
        public const uint AbQuestReward4Bases = 24061;
        public const uint AbQuestReward5Bases = 24064;

        //BuffObjects
        public const uint SpeedBuff = 179871;
        public const uint RegenBuff = 179904;
        public const uint BerserkerBuff = 179905;

        //QueueGroupTypes
        public const uint BgQueuePremadeAlliance = 0;
        public const uint BgQueuePremadeHorde = 1;
        public const uint BgQueueNormalAlliance = 2;
        public const uint BgQueueNormalHorde = 3;
        public const int BgQueueTypesCount = 4;

        //PlayerPosition
        public const sbyte PlayerPositionIconNone = 0;
        public const sbyte PlayerPositionIconHordeFlag = 1;
        public const sbyte PlayerPositionIconAllianceFlag = 2;

        public const sbyte PlayerPositionArenaSlotNone = 1;
        public const sbyte PlayerPositionArenaSlot1 = 2;
        public const sbyte PlayerPositionArenaSlot2 = 3;
        public const sbyte PlayerPositionArenaSlot3 = 4;
        public const sbyte PlayerPositionArenaSlot4 = 5;
        public const sbyte PlayerPositionArenaSlot5 = 6;

        //Spells
        public const uint SpellSpiritHealChannelAoE = 22011;                // used for AoE resurrections
        public const uint SpellSpiritHealPlayerAura = 156758;               // individual player timers for resurrection
        public const uint SpellSpiritHealChannelSelf = 305122;               // channel visual for individual area spirit healers
        public const uint SpellWaitingForResurrect = 2584;                 // Waiting To Resurrect
        public const uint SpellSpiritHealChannelVisual = 3060;
        public const uint SpellSpiritHeal = 22012;                // Spirit Heal
        public const uint SpellResurrectionVisual = 24171;                // Resurrection Impact Visual
        public const uint SpellArenaPreparation = 32727;                // Use This One, 32728 Not Correct
        public const uint SpellPreparation = 44521;                // Preparation
        public const uint SpellSpiritHealMana = 44535;                // Spirit Heal
        public const uint SpellRecentlyDroppedFlag = 42792;                // Recently Dropped Flag
        public const uint SpellAuraPlayerInactive = 43681;                // Inactive
        public const uint SpellHonorableDefender25y = 68652;                // +50% Honor When Standing At A Capture Point That You Control, 25yards Radius (Added In 3.2)
        public const uint SpellHonorableDefender60y = 66157;              // +50% Honor When Standing At A Capture Point That You Control, 60yards Radius (Added In 3.2), Probably For 40+ Player Battlegrounds
        public const uint SpellMercenaryContractHorde = 193472;
        public const uint SpellMercenaryContractAlliance = 193475;
        public const uint SpellMercenaryHorde1 = 193864;
        public const uint SpellMercenaryHordeReactions = 195838;
        public const uint SpellMercenaryAlliance1 = 193863;
        public const uint SpellMercenaryAllianceReactions = 195843;
        public const uint SpellMercenaryShapeshift = 193970;
        public const uint SpellPetSummoned = 6962; // used after resurrection
    }

    [Flags]
    public enum BattlegroundEventFlags
    {
        None = 0x00,
        Event1 = 0x01,
        Event2 = 0x02,
        Event3 = 0x04,
        Event4 = 0x08
    }

    // BattlemasterList.db2 (10.0.5.48317)
    public enum BattlegroundTypeId
    {
        None = 0,   // None
        AlteracValley = 1, // Epic Battleground
        ClassicWarsongGulch = 2, // Capture the Flag
        ClassicArathiBasin = 3, // Domination
        NagrandArenaold = 4,
        zzOldBladesEdgeArena = 5,
        AllArenas = 6,
        EyeoftheStorm = 7, // Domination
        RuinsofLordaeron = 8,
        StrandoftheAncients = 9, // Siege
        DalaranSewers = 10,
        TheRingofValor = 11,
        IsleofConquest = 30, // Epic Battleground
        RandomBattleground = 32,
        RatedBattleground = 100,
        RatedBattleground2 = 101,
        RatedBattleground3 = 102,
        TwinPeaks = 108, // Capture the Flag
        TheBattleforGilneas = 120, // Domination
        RatedEyeoftheStorm = 656,
        TempleofKotmogu = 699, // Power Orbs
        CTF3 = 706,
        SilvershardMines = 708, // Payload
        TolVironArena = 719,
        DeepwindGorgeLegacy = 754, // Domination
        TheTigersPeak = 757,
        SouthshorevsTarrenMill = 789, // Brawl
        SmallBattlegroundD = 803, // Various
        BlackRookHoldArena = 808,
        NagrandArena = 809,
        AshamanesFall = 816,
        BladesEdgeArena = 844,
        TheBattleforGilneasOldCityMap = 846, // Brawl
        ArathiBasinWinter = 847, // Brawl
        AITestArathiBasin = 848,
        DeepwindDunk = 849, // Brawl
        ShadoPanShowdown = 853, // Brawl
        TEMPRaceTrackBG = 856, // Domination
        BattleforBlackrockMountain = 857,
        TempleofHotmogu = 858, // Brawl
        GravityLapse = 859, // Brawl
        DeepwindDunk2 = 860, // Brawl
        WarsongScramble = 861, // Brawl
        EyeoftheHorn = 862, // Brawl
        AllArenas2 = 866, // Brawl
        RuinsofLordaeron2 = 868,
        DalaranSewers2 = 869,
        TolVironArena2 = 870,
        TheTigersPeak2 = 871,
        BlackRookHoldArena2 = 872,
        NagrandArena2 = 873,
        AshamanesFall2 = 874,
        BladesEdgeArena2 = 875,
        AITestWarsongGulch = 878,
        DeepSix = 879, // Brawl
        ArathiBasin = 880, // Brawl
        DeepwindGorge = 881, // Brawl
        EyeoftheStorm2 = 882, // Brawl
        SilvershardMines2 = 883, // Brawl
        TempleofKotmogu2 = 884, // Brawl
        TheBattleforGilneas2 = 885, // Brawl
        WarsongGulch = 886, // Brawl
        CookingImpossible = 887,
        SeethingStrand = 890, // Domination
        BattleForAzeroth8_0BGTemp = 893, // Domination
        SeethingShore = 894, // Resource Race
        HookPoint = 897,
        RandomEpicBattleground = 901,
        Mugambala = 903,
        BrawlAllArenas = 904,
        WarsongGulch2 = 1014, // Capture the Flag
        BattleforWintergrasp = 1017, // Epic Battleground
        ArathiBasin2 = 1018, // Domination
        ArathiBasinCompStomp = 1019,
        Ashran = 1020, // Epic Battleground
        ClassicAshran = 1021, // Endless Epic Battleground
        ArathiBasin3 = 1022, // Brawl
        TheRobodrome = 1025,
        RandomBattleground2 = 1029,
        BattleforWintergrasp2 = 1030, // Epic Battleground
        ProgrammerMapBattlefield = 1031,
        KorraksRevenge = 1033, // Limited Time Event
        EpicBattlegroundWarfrontArathiPvP = 1036,
        DeepwindGorge2 = 1037, // Domination
        DeepwindGorge3 = 1039, // Domination
        EmpyreanDomain = 1041,
        Mugambala2 = 1047,
        TheRobodrome2 = 1048,
        HookPoint2 = 1049,
        EmpyreanDomain2 = 1050,
        Mugambala3 = 1053,
        AshamanesFall3 = 1054,
        TolVironArena3 = 1055,
        TheRobodrome3 = 1056,
        TheTigersPeak3 = 1057,
        RuinsofLordaeron3 = 1058,
        NagrandArena3 = 1059,
        HookPoint3 = 1060,
        EmpyreanDomain3 = 1061,
        DalaranSewers3 = 1062,
        BladesEdgeArena3 = 1063,
        BlackRookHoldArena3 = 1064,
        AllArenas4 = 1065,
        MaldraxxusColiseum = 1066,
        EnigmaArena = 1068,
        RuinsofLordaeron4 = 1070,
        MaldraxxusColiseum2 = 1071,
        DeepwindDetonation = 1075, // Brawl
        EnigmaCrucible = 1076,
        EnigmaCrucible2 = 1077,
        NokhudonProvingGrounds = 1088,
        NokhudonProvingGrounds2 = 1092,
        AllArenas5 = 1094,
        AllArenas6 = 1095,
        AllArenas7 = 1096,
        Max = 1097
    }

    public enum BattlegroundQueueIdType
    {
        Battleground = 0,
        Arena = 1,
        Wargame = 2,
        Cheat = 3,
        ArenaSkirmish = 4
    }

    public enum BattlegroundPointCaptureStatus
    {
        AllianceControlled,
        AllianceCapturing,
        Neutral,
        HordeCapturing,
        HordeControlled
    }

    public enum BattlegroundQueueInvitationType
    {
        NoBalance = 0, // no balance: N+M vs N players
        Balanced = 1, // teams balanced: N+1 vs N players
        Even = 2  // teams even: N vs N players
    }

    public struct BattlegroundBroadcastTexts
    {
        public const uint AllianceWins = 10633;
        public const uint HordeWins = 10634;

        public const uint StartTwoMinutes = 18193;
        public const uint StartOneMinute = 18194;
        public const uint StartHalfMinute = 18195;
        public const uint HasBegun = 18196;
    }

    public enum BattlegroundSounds
    {
        HordeWins = 8454,
        AllianceWins = 8455,
        BgStart = 3439,
        BgStartL70etc = 11803
    }

    public enum PvPTeamId
    {
        Horde = 0, // Battleground: Horde,    Arena: Green
        Alliance = 1, // Battleground: Alliance, Arena: Gold
        Neutral = 2  // Battleground: Neutral,  Arena: None
    }

    public enum BattlegroundMarksCount
    {
        WinnterCount = 3,
        LoserCount = 1
    }

    public enum BattlegroundCreatures
    {
        A_SpiritGuide = 13116,           // alliance
        H_SpiritGuide = 13117            // horde
    }

    public enum BattlegroundStartTimeIntervals
    {
        Delay2m = 120000,               // Ms (2 Minutes)
        Delay1m = 60000,                // Ms (1 Minute)
        Delay30s = 30000,                // Ms (30 Seconds)
        Delay15s = 15000,                // Ms (15 Seconds) Used Only In Arena
        None = 0                     // Ms
    }

    public enum BattlegroundStatus
    {
        None = 0,                                // first status, should mean bg is not instance
        WaitQueue = 1,                                // means bg is empty and waiting for queue
        WaitJoin = 2,                                // this means, that BG has already started and it is waiting for more players
        InProgress = 3,                                // means bg is running
        WaitLeave = 4                                 // means some faction has won BG and it is ending
    }

    public enum BGHonorMode
    {
        Normal = 0,
        Holiday,
        HonorModeNum
    }

    public enum GroupJoinBattlegroundResult
    {
        None = 0,
        Deserters = 2,        // You Cannot Join The BattlegroundYet Because You Or One Of Your Party Members Is Flagged As A Deserter.
        ArenaTeamPartySize = 3,        // Incorrect Party Size For This Arena.
        TooManyQueues = 4,        // You Can Only Be Queued For 2 Battles At Once
        CannotQueueForRated = 5,        // You Cannot Queue For A Rated Match While Queued For Other Battles
        BattledgroundQueuedForRated = 6,        // You Cannot Queue For Another Battle While Queued For A Rated Arena Match
        TeamLeftQueue = 7,        // Your Team Has Left The Arena Queue
        NotInBattleground= 8,        // You Can'T Do That In A Battleground.
        JoinXpGain = 9,        // Cannot join as a group unless all the members of your party have the same XP gain setting.
        JoinRangeIndex = 10,       // Cannot Join The Queue Unless All Members Of Your Party Are In The Same BattlegroundLevel Range.
        JoinTimedOut = 11,       // %S Was Unavailable To Join The Queue. (Uint64 Guid Exist In Client Cache)
                                 //JoinTimedOut               = 12,       // Same As 11
                                 //TeamLeftQueue              = 13,       // Same As 7
        LfgCantUseBattleground= 14,       // You Cannot Queue For A BattlegroundOr Arena While Using The Dungeon System.
        InRandomBg = 15,       // Can'T Do That While In A Random BattlegroundQueue.
        InNonRandomBg = 16,       // Can'T Queue For Random BattlegroundWhile In Another BattlegroundQueue.
        BgDeveloperOnly = 17,       // This Battleground Is Only Available For Developer Testing At This Time.
        BattlegroundInvitationDeclined = 18,       // Your War Game Invitation Has Been Declined
        MeetingStoneNotFound = 19,       // Player Not Found.
        WargameRequestFailure = 20,       // War Game Request Failed
        BattlefieldTeamPartySize = 22,       // Incorrect Party Size For This Battlefield.
        NotOnTournamentRealm = 23,       // Not Available On A Tournament Realm.
        BattlegroundPlayersFromDifferentRealms = 24,       // You Cannot Queue For A Battleground While Players From Different Realms Are In Your Party.
        BattlegroundJoinLevelup = 33,       // You Have Been Removed From A Pvp Queue Because You Have Gained A Level.
        RemoveFromPvpQueueFactionChange = 34,       // You Have Been Removed From A Pvp Queue Because You Changed Your Faction.
        BattlegroundJoinFailed = 35,       // Join As A Group Failed
        BattlegroundDupeQueue = 43,       // Someone In Your Group Is Already Queued For That.
        BattlegroundJoinNoValidSpecForRole = 44,       // Role Check Failed Because One Of Your Party Members Selected An Invalid Role.
        BattlegroundJoinRespec = 45,       // You Have Been Removed From A Pvp Queue Because Your Specialization Changed.
        AlreadyUsingLfgList = 46,       // You Can'T Do That While Using Premade Groups.
        BattlegroundJoinMustCompleteQuest = 47,       // You Have Been Removed From A Pvp Queue Because Someone Is Missing Required Quest Completion.
        BattlergoundRestrictedAccount = 48,       // Free Trial Accounts Cannot Perform That Action
        BattlegroundJoinMercenary = 49,       // Cannot Join As A Group Unless All The Members Of Your Party Are Flagged As A Mercenary.
        BattlegroundJoinTooManyHealers = 51,       // You Can Not Enter This Bracket Of Arena With More Than One Healer. / You Can Not Enter A Rated Battleground With More Than Three Healers.
        BattlegroundJoinTooManyTanks = 52,       // You Can Not Enter This Bracket Of Arena With More Than One Tank.
        BattlegroundJoinTooManyDamage = 53,       // You Can Not Enter This Bracket Of Arena With More Than Two Damage Dealers.
        GroupJoinBattlegroundDead = 57,       // You Cannot Join The Battleground Because You Or One Of Your Party Members Is Dead.
        BattlegroundJoinRequiresLevel = 58,       // Tournament Rules Requires All Participants To Be Max Level.
        BattlegroundJoinDisqualified = 59,       // %S Has Been Disqualified From Ranked Play In This Bracket.
        ArenaExpiredCais = 60,       // You May Not Queue While One Or More Of Your Team Members Is Under The Effect Of Restricted Play.
        SoloShuffleWargameGroupSize = 64,       // Exactly 6 Non-Spectator Players Must Be Present To Begin A Solo Shuffle Wargame.
        SoloShuffleWargameGroupComp = 65,       // Exactly 4 Dps, And Either 2 Tanks Or 2 Healers, Must Be Present To Begin A Solo Shuffle Wargame.
    }

    public enum ScoreType
    {
        KillingBlows = 1,
        Deaths = 2,
        HonorableKills = 3,
        BonusHonor = 4,
        DamageDone = 5,
        HealingDone = 6,

        // Ws And Ey
        FlagCaptures = 7,
        FlagReturns = 8,

        // Ab And Ic
        BasesAssaulted = 9,
        BasesDefended = 10,

        // Av
        GraveyardsAssaulted = 11,
        GraveyardsDefended = 12,
        TowersAssaulted = 13,
        TowersDefended = 14,
        MinesCaptured = 15,

        // Sota
        DestroyedDemolisher = 16,
        DestroyedWall = 17
    }

    //Arenas
    public struct ArenaBroadcastTexts
    {
        public const uint OneMinute = 15740;
        public const uint ThirtySeconds = 15741;
        public const uint FifteenSeconds = 15739;
        public const uint HasBegun = 15742;
    }

    public struct ArenaSpellIds
    {
        public const uint AllianceGoldFlag = 32724;
        public const uint AllianceGreenFlag = 32725;
        public const uint HordeGoldFlag = 35774;
        public const uint HordeGreenFlag = 35775;
        public const uint LastManStanding = 26549;            // Arena Achievement Related
    }

    [Flags]
    public enum ArenaTeamCommandTypes
    {
        Create_S = 0x00,
        Invite_SS = 0x01,
        Quit_S = 0x03,
        Founder_S = 0x0e
    }

    [Flags]
    public enum ArenaTeamCommandErrors
    {
        ArenaTeamCreated = 0x00,
        ArenaTeamInternal = 0x01,
        AlreadyInArenaTeam = 0x02,
        AlreadyInArenaTeamS = 0x03,
        InvitedToArenaTeam = 0x04,
        AlreadyInvitedToArenaTeamS = 0x05,
        ArenaTeamNameInvalid = 0x06,
        ArenaTeamNameExistsS = 0x07,
        ArenaTeamLeaderLeaveS = 0x08,
        ArenaTeamPermissions = 0x08,
        ArenaTeamPlayerNotInTeam = 0x09,
        ArenaTeamPlayerNotInTeamSs = 0x0a,
        ArenaTeamPlayerNotFoundS = 0x0b,
        ArenaTeamNotAllied = 0x0c,
        ArenaTeamIgnoringYouS = 0x13,
        ArenaTeamTargetTooLowS = 0x15,
        ArenaTeamTargetTooHighS = 0x16,
        ArenaTeamTooManyMembersS = 0x17,
        ArenaTeamNotFound = 0x1b,
        ArenaTeamsLocked = 0x1e,
        ArenaTeamTooManyCreate = 0x21,
    }

    public enum ArenaTeamEvents
    {
        JoinSs = 3,            // Player Name + Arena Team Name
        LeaveSs = 4,            // Player Name + Arena Team Name
        RemoveSss = 5,            // Player Name + Arena Team Name + Captain Name
        LeaderIsSs = 6,            // Player Name + Arena Team Name
        LeaderChangedSss = 7,            // Old Captain + New Captain + Arena Team Name
        DisbandedS = 8             // Captain Name + Arena Team Name
    }

    public enum ArenaTypes
    {
        BG = 0,
        Team2v2 = 2,
        Team3v3 = 3,
        Team5v5 = 5
    }

    public enum ArenaErrorType
    {
        NoTeam = 0,
        ExpiredCAIS = 1,
        CantUseBattleground = 2
    }

    public enum ArenaTeamInfoType
    {
        Id = 0,
        Type = 1,                       // new in 3.2 - team type?
        Member = 2,                       // 0 - captain, 1 - member
        GamesWeek = 3,
        GamesSeason = 4,
        WinsSeason = 5,
        PersonalRating = 6,
        End = 7
    }

    public enum BattlegroundCapturePointState
    {
        Neutral = 1,
        ContestedHorde = 2,
        ContestedAlliance = 3,
        HordeCaptured = 4,
        AllianceCaptured = 5
    }
}