// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable InconsistentNaming

using System;

namespace Framework.Constants;

    public enum SmartScriptType
    {
        Creature                             = 0,
        GameObject                           = 1,
        AreaTrigger                          = 2,
        Event                                = 3,
        Gossip                               = 4,
        Quest                                = 5,
        Spell                                = 6,
        Transport                            = 7,
        Instance                             = 8,
        TimedActionlist                      = 9,
        Scene                                = 10,
        AreaTriggerEntity                    = 11,
        AreaTriggerEntityServerside          = 12,
        Max                                  = 11
    }

    public struct SmartScriptTypeMaskId
    {
        public const uint None               = 0;
        public const uint Creature           = 1;
        public const uint GameObject         = 2;
        public const uint AreaTrigger        = 4;
        public const uint Event              = 8;
        public const uint Gossip             = 16;
        public const uint Quest              = 32;
        public const uint Spell              = 64;
        public const uint Transport          = 128;
        public const uint Instance           = 256;
        public const uint TimedActionlist    = 512;
        public const uint Scene              = 1024;
        public const uint AreaTriggerEntity  = 2048;
    }

    public enum SmartPhase
    {
        Always                               = 0,
        Phase1                               = 1,
        Phase2                               = 2,
        Phase3                               = 3,
        Phase4                               = 4,
        Phase5                               = 5,
        Phase6                               = 6,
        Phase7                               = 7,
        Phase8                               = 8,
        Phase9                               = 9,
        Phase10                              = 10,
        Phase11                              = 11,
        Phase12                              = 12,
        Count                                = 12,
        Max                                  = 13
    }

    public enum SmartEventPhaseBits
    {
        PhaseAlwaysBit                       = 0,
        Phase1Bit                            = 1,
        Phase2Bit                            = 2,
        Phase3Bit                            = 4,
        Phase4Bit                            = 8,
        Phase5Bit                            = 16,
        Phase6Bit                            = 32,
        Phase7Bit                            = 64,
        Phase8Bit                            = 128,
        Phase9Bit                            = 256,
        Phase10Bit                           = 512,
        Phase11Bit                           = 1024,
        Phase12Bit                           = 2048,
        All                                  = Phase1Bit + Phase2Bit + Phase3Bit + Phase4Bit + Phase5Bit + Phase6Bit + Phase7Bit + Phase8Bit + Phase9Bit + Phase10Bit + Phase11Bit + Phase12Bit
    }

    [Flags]
    public enum SmartEventFlags
    {
        NotRepeatable                        = 0x01,       //Event can not repeat
        Difficulty0                          = 0x02,       //Event only occurs in instance difficulty 0
        Difficulty1                          = 0x04,       //Event only occurs in instance difficulty 1
        Difficulty2                          = 0x08,       //Event only occurs in instance difficulty 2
        Difficulty3                          = 0x10,       //Event only occurs in instance difficulty 3
        Reserved5                            = 0x20,
        Reserved6                            = 0x40,
        DebugOnly                            = 0x80,       //Event only occurs in debug build
        DontReset                            = 0x100,      //Event will not reset in SmartScript.OnReset()
        WhileCharmed                         = 0x200,      //Event occurs even if AI owner is charmed

        DifficultyAll                        = Difficulty0 | Difficulty1 | Difficulty2 | Difficulty3,
        All                                  = NotRepeatable | DifficultyAll | Reserved5 | Reserved6 | DebugOnly | DontReset | WhileCharmed,

        // Temp flags, used only at runtime, never stored in DB
        TempIgnoreChanceRoll                 = 0x40000000  //Event occurs no matter what roll_chance_i(e.event.event_chance) returns.
    }

    public enum SmartRespawnCondition
    {
        None                                 = 0,
        Map                                  = 1,
        Area                                 = 2,
        End                                  = 3
    }

    [Flags]
    public enum SmartAiSpawnFlags
    {
        None                                 = 0x00,
        IgnoreRespawn                        = 0x01,
        ForceSpawn                           = 0x02,
        NosaveRespawn                        = 0x04,
    }

    [Flags]
    public enum SmartCastFlags
    {
        InterruptPrevious                    = 0x01,       // Interrupt any spell casting
        Triggered                            = 0x02,       // Triggered (this makes spell cost zero mana and have no cast time)
        //CAST_FORCE_CAST                    = 0x04,       // Forces cast even if creature is out of mana or out of range
        //CAST_NO_MELEE_IF_OOM               = 0x08,       // Prevents creature from entering melee if out of mana or out of range
        //CAST_FORCE_TARGET_SELF             = 0x10,       // Forces the target to cast this spell on itself
        AuraNotPresent                       = 0x20,       // Only casts the spell if the target does not have an aura from the spell
        CombatMove                           = 0x40        // Prevents combat movement if cast successful. Allows movement on range, OOM, LOS
    }

    public enum SmartActionSummonCreatureFlags
    {
        None                                 = 0,
        PersonalSpawn                        = 1,
        PreferUnit                           = 2,
        All                                  = PersonalSpawn | PreferUnit,
    }

    public enum SmartEvents
    {
        UpdateIc                             = 0,          // InitialMin, InitialMax, RepeatMin, RepeatMax
        UpdateOoc                            = 1,          // InitialMin, InitialMax, RepeatMin, RepeatMax
        HealthPct                            = 2,          // HpMin%, HpMax%,  RepeatMin, RepeatMax
        ManaPct                              = 3,          // ManaMin%, ManaMax%, RepeatMin, RepeatMax
        Aggro                                = 4,          // None
        Kill                                 = 5,          // CooldownMin0, CooldownMax1, PlayerOnly2, Else Creature Entry3
        Death                                = 6,          // None
        Evade                                = 7,          // None
        SpellHit                             = 8,          // Spellid, School, CooldownMin, CooldownMax
        Range                                = 9,          // MinDist, Maxdist, RepeatMin, RepeatMax
        OocLos                               = 10,         // HostilityMode, MaxRange, CooldownMin, CooldownMax
        Respawn                              = 11,         // Type, Mapid, Zoneid
        TargetHealthPct                      = 12,         // UNUSED, DO NOT REUSE
        VictimCasting                        = 13,         // RepeatMin, RepeatMax
        FriendlyHealth                       = 14,         // UNUSED, DO NOT REUSE
        FriendlyIsCc                         = 15,         // Radius, RepeatMin, RepeatMax
        FriendlyMissingBuff                  = 16,         // Spellid, Radius, RepeatMin, RepeatMax
        SummonedUnit                         = 17,         // CreatureId(0 All), CooldownMin, CooldownMax
        TargetManaPct                        = 18,         // UNUSED, DO NOT REUSE
        AcceptedQuest                        = 19,         // QuestID (0 = any), CooldownMin, CooldownMax
        RewardQuest                          = 20,         // QuestID (0 = any), CooldownMin, CooldownMax
        ReachedHome                          = 21,         // None
        ReceiveEmote                         = 22,         // EmoteId, CooldownMin, CooldownMax, Condition, Val1, Val2, Val3
        HasAura                              = 23,         // Param1 = Spellid, Param2 = Stack Amount, Param3/4 RepeatMin, RepeatMax
        TargetBuffed                         = 24,         // Param1 = Spellid, Param2 = Stack Amount, Param3/4 RepeatMin, RepeatMax
        Reset                                = 25,         // Called After Combat, When The Creature Respawn And Spawn.
        IcLos                                = 26,         // HostilityMode, MaxRange, CooldownMin, CooldownMax
        PassengerBoarded                     = 27,         // CooldownMin, CooldownMax
        PassengerRemoved                     = 28,         // CooldownMin, CooldownMax
        Charmed                              = 29,         // onRemove (0 - on apply, 1 - on remove)
        CharmedTarget                        = 30,         // UNUSED, DO NOT REUSE
        SpellHitTarget                       = 31,         // Spellid, School, CooldownMin, CooldownMax
        Damaged                              = 32,         // MinDmg, MaxDmg, CooldownMin, CooldownMax
        DamagedTarget                        = 33,         // MinDmg, MaxDmg, CooldownMin, CooldownMax
        Movementinform                       = 34,         // MovementType(Any), Pointid
        SummonDespawned                      = 35,         // Entry, CooldownMin, CooldownMax
        CorpseRemoved                        = 36,         // None
        AIInit                               = 37,         // None
        DataSet                              = 38,         // Id, Value, CooldownMin, CooldownMax
        WaypointStart                        = 39,         // UNUSED, DO NOT REUSE
        WaypointReached                      = 40,         // Pointid(0any), PathId(0any)
        TransportAddplayer                   = 41,         // None
        TransportAddcreature                 = 42,         // Entry (0 Any)
        TransportRemovePlayer                = 43,         // None
        TransportRelocate                    = 44,         // Pointid
        InstancePlayerEnter                  = 45,         // Team (0 Any), CooldownMin, CooldownMax
        AreatriggerOntrigger                 = 46,         // Triggerid(0 Any)
        QuestAccepted                        = 47,         // None
        QuestObjCompletion                   = 48,         // None
        QuestCompletion                      = 49,         // None
        QuestRewarded                        = 50,         // None
        QuestFail                            = 51,         // None
        TextOver                             = 52,         // Groupid From CreatureText,  Creature Entry Who Talks (0 Any)
        ReceiveHeal                          = 53,         // MinHeal, MaxHeal, CooldownMin, CooldownMax
        JustSummoned                         = 54,         // None
        WaypointPaused                       = 55,         // Pointid(0any), PathId(0any)
        WaypointResumed                      = 56,         // Pointid(0any), PathId(0any)
        WaypointStopped                      = 57,         // Pointid(0any), PathId(0any)
        WaypointEnded                        = 58,         // Pointid(0any), PathId(0any)
        TimedEventTriggered                  = 59,         // Id
        Update                               = 60,         // InitialMin, InitialMax, RepeatMin, RepeatMax
        Link                                 = 61,         // Internal Usage, No Params, Used To Link Together Multiple Events, Does Not Use Any Extra Resources To Iterate Event Lists Needlessly
        GossipSelect                         = 62,         // Menuid, ActionId
        JustCreated                          = 63,         // None
        GossipHello                          = 64,         // noReportUse (for GOs)
        FollowCompleted                      = 65,         // None
        PhaseChange                          = 66,         // UNUSED, DO NOT REUSE
        IsBehindTarget                       = 67,         // UNUSED, DO NOT REUSE
        GameEventStart                       = 68,         // GameEvent.Entry
        GameEventEnd                         = 69,         // GameEvent.Entry
        GoLootStateChanged                   = 70,         // Go State
        GoEventInform                        = 71,         // EventId
        ActionDone                           = 72,         // EventId (SharedDefines.EventId)
        OnSpellclick                         = 73,         // Clicker (Unit)
        FriendlyHealthPCT                    = 74,         // minHpPct, maxHpPct, repeatMin, repeatMax
        DistanceCreature                     = 75,         // guid, entry, distance, repeat
        DistanceGameobject                   = 76,         // guid, entry, distance, repeat
        CounterSet                           = 77,         // id, value, cooldownMin, cooldownMax
        SceneStart                           = 78,         // none
        SceneTrigger                         = 79,         // param_string : triggerName
        SceneCancel                          = 80,         // none
        SceneComplete                        = 81,         // none
        SummonedUnitDies                     = 82,         // CreatureId(0 all), CooldownMin, CooldownMax
        OnSpellCast                          = 83,         // SpellID, CooldownMin, CooldownMax
        OnSpellFailed                        = 84,         // SpellID, CooldownMin, CooldownMax
        OnSpellStart                         = 85,         // SpellID, CooldownMin, CooldownMax
        OnDespawn                            = 86,         // NONE

        End
    }

    public enum SmartActions
    {
        None                                 = 0,          // No Action
        Talk                                 = 1,          // Groupid From CreatureText, Duration To Wait Before TextOver Event Is Triggered, useTalkTarget (0/1) - use target as talk target
        SetFaction                           = 2,          // FactionId (Or 0 For Default)
        MorphToEntryOrModel                  = 3,          // CreatureTemplate Entry(Param1) Or Modelid (Param2) (Or 0 For Both To Demorph)
        Sound                                = 4,          // SoundId, TextRange
        PlayEmote                            = 5,          // EmoteId
        FailQuest                            = 6,          // QuestId
        OfferQuest                           = 7,          // QuestId, DirectAdd
        SetReactState                        = 8,          // State
        ActivateGobject                      = 9,          //
        RandomEmote                          = 10,         // EmoteId1, EmoteId2, EmoteId3...
        Cast                                 = 11,         // Spellid, CastFlags, TriggeredFlags
        SummonCreature                       = 12,         // CreatureId, SummonType, Duration In Ms, StorageId, AttackInvoker, flags(SmartActionSummonCreatureFlags)
        ThreatSinglePct                      = 13,         // Threat%
        ThreatAllPct                         = 14,         // Threat%
        CallAreaexploredoreventhappens       = 15,         // Questid
        SetIngamePhaseGroup                  = 16,         // phaseGroupId, apply
        SetEmoteState                        = 17,         // EmoteId
        SetUnitFlag                          = 18,         // UNUSED, DO NOT REUSE
        RemoveUnitFlag                       = 19,         // UNUSED, DO NOT REUSE
        AutoAttack                           = 20,         // AllowAttackState (0 = Stop Attack, Anything Else Means Continue Attacking)
        AllowCombatMovement                  = 21,         // AllowCombatMovement (0 = Stop Combat Based Movement, Anything Else Continue Attacking)
        SetEventPhase                        = 22,         // Phase
        IncEventPhase                        = 23,         // Value (May Be Negative To Decrement Phase, Should Not Be 0)
        Evade                                = 24,         // No Params
        FleeForAssist                        = 25,         // With Emote
        CallGroupeventhappens                = 26,         // Questid
        CombatStop                           = 27,         //
        RemoveAurasFromSpell                 = 28,         // Spellid, 0 Removes All Auras
        Follow                               = 29,         // Distance (0 = Default), Angle (0 = Default), EndCreatureEntry, Credit, CreditType (0MonsterKill, 1event)
        RandomPhase                          = 30,         // PhaseId1, PhaseId2, PhaseId3...
        RandomPhaseRange                     = 31,         // PhaseMin, PhaseMax
        ResetGobject                         = 32,         //
        CallKilledmonster                    = 33,         // CreatureId,
        SetInstData                          = 34,         // Field, Data, Type (0 = SetData, 1 = SetBossState)
        SetInstData64                        = 35,         // Field,
        UpdateTemplate                       = 36,         // Entry
        Die                                  = 37,         // No Params
        SetInCombatWithZone                  = 38,         // No Params
        CallForHelp                          = 39,         // Radius, With Emote
        SetSheath                            = 40,         // Sheath (0-Unarmed, 1-Melee, 2-Ranged)
        ForceDespawn                         = 41,         // Timer
        SetInvincibilityHpLevel              = 42,         // MinHPValue(+Pct, -Flat)
        MountToEntryOrModel                  = 43,         // CreatureTemplate Entry(Param1) Or Modelid (Param2) (Or 0 For Both To Dismount)
        SetIngamePhaseId                     = 44,         // Id
        SetData                              = 45,         // Field, Data (Only Creature Todo)
        AttackStop                           = 46,
        SetVisibility                        = 47,         // On/Off
        SetActive                            = 48,         // No Params
        AttackStart                          = 49,         //
        SummonGo                             = 50,         // GameObjectId, Despawntime In Ms,
        KillUnit                             = 51,         //
        ActivateTaxi                         = 52,         // TaxiId
        WpStart                              = 53,         // Run/Walk, PathId, CanRepeat, Quest, Despawntime
        WpPause                              = 54,         // Time
        WpStop                               = 55,         // Despawntime, Quest, Fail?
        AddItem                              = 56,         // ItemId, Count
        RemoveItem                           = 57,         // ItemId, Count
        InstallAITemplate                    = 58,         // UNUSED, DO NOT REUSE
        SetRun                               = 59,         // 0/1
        SetDisableGravity                    = 60,         // 0/1
        SetSwim                              = 61,         // UNUSED, DO NOT REUSE
        Teleport                             = 62,         // Mapid,
        SetCounter                           = 63,         // id, value, reset (0/1)
        StoreTargetList                      = 64,         // VarId,
        WpResume                             = 65,         // None
        SetOrientation                       = 66,         //
        CreateTimedEvent                     = 67,         // Id, InitialMin, InitialMax, RepeatMin(Only If It Repeats), RepeatMax(Only If It Repeats), Chance
        Playmovie                            = 68,         // Entry
        MoveToPos                            = 69,         // PointId, transport, disablePathfinding, ContactDistance
        EnableTempGobj                       = 70,         //
        Equip                                = 71,         // Entry, Slotmask Slot1, Slot2, Slot3   , Only Slots With Mask Set Will Be Sent To Client, Bits Are 1, 2, 4, Leaving Mask 0 Is Defaulted To Mask 7 (Send All), Slots1-3 Are Only Used If No Entry Is Set
        CloseGossip                          = 72,         // None
        TriggerTimedEvent                    = 73,         // Id(>1)
        RemoveTimedEvent                     = 74,         // Id(>1)
        AddAura                              = 75,         // UNUSED, DO NOT REUSE
        OverrideScriptBaseObject             = 76,         // UNUSED, DO NOT REUSE
        ResetScriptBaseObject                = 77,         // UNUSED, DO NOT REUSE
        CallScriptReset                      = 78,         // None
        SetRangedMovement                    = 79,         // Distance, Angle
        CallTimedActionlist                  = 80,         // Id (Overwrites Already Running Actionlist), Stop After Combat?(0/1), Timer Update Type(0-Ooc, 1-Ic, 2-Always)
        SetNpcFlag                           = 81,         // Flags
        AddNpcFlag                           = 82,         // Flags
        RemoveNpcFlag                        = 83,         // Flags
        SimpleTalk                           = 84,         // Groupid, Can Be Used To Make Players Say Groupid, TextOver Event Is Not Triggered, Whisper Can Not Be Used (Target Units Will Say The Text)
        SelfCast                             = 85,         // Spellid, CastFlags
        CrossCast                            = 86,         // Spellid, CastFlags, CasterTargetType, CasterTarget Param1, CasterTarget Param2, CasterTarget Param3, ( + The Original Target Fields As Destination Target),   CasterTargets Will Cast Spellid On All Targets (Use With Caution If Targeting Multiple * Multiple Units)
        CallRandomTimedActionlist            = 87,         // Script9 Ids 1-9
        CallRandomRangeTimedActionlist       = 88,         // Script9 Id Min, Max
        RandomMove                           = 89,         // Maxdist
        SetUnitFieldBytes1                   = 90,         // Bytes, Target
        RemoveUnitFieldBytes1                = 91,         // Bytes, Target
        InterruptSpell                       = 92,
        SendGoCustomAnim                     = 93,         // UNUSED, DO NOT REUSE
        SetDynamicFlag                       = 94,         // UNUSED, DO NOT REUSE
        AddDynamicFlag                       = 95,         // UNUSED, DO NOT REUSE
        RemoveDynamicFlag                    = 96,         // UNUSED, DO NOT REUSE
        JumpToPos                            = 97,         // SpeedXY, SpeedZ, Gravity, UseDefaultGravity, PointId, ContactDistance
        SendGossipMenu                       = 98,         // Menuid, optionIndex
        GoSetLootState                       = 99,         // State
        SendTargetToTarget                   = 100,        // Id
        SetHomePos                           = 101,        // None
        SetHealthRegen                       = 102,        // 0/1
        SetRoot                              = 103,        // Off/On
        SetGoFlag                            = 104,        // UNUSED, DO NOT REUSE
        AddGoFlag                            = 105,        // UNUSED, DO NOT REUSE
        RemoveGoFlag                         = 106,        // UNUSED, DO NOT REUSE
        SummonCreatureGroup                  = 107,        // Group, AttackInvoker
        SetPower                             = 108,        // PowerType, newPower
        AddPower                             = 109,        // PowerType, newPower
        RemovePower                          = 110,        // PowerType, newPower
        GameEventStop                        = 111,        // GameEventId
        GameEventStart                       = 112,        // GameEventId
        StartClosestWaypoint                 = 113,        // wp1, wp2, wp3, wp4, wp5, wp6, wp7
        MoveOffset                           = 114,        // PointId
        RandomSound                          = 115,        // SoundId1, SoundId2, SoundId3, SoundId4, SoundId5, onlySelf
        SetCorpseDelay                       = 116,        // timer
        DisableEvade                         = 117,        // 0/1 (1 = disabled, 0 = enabled)
        GoSetGoState                         = 118,
        SetCanFly                            = 119,        // UNUSED, DO NOT REUSE
        RemoveAurasByType                    = 120,        // UNUSED, DO NOT REUSE
        SetSightDist                         = 121,        // UNUSED, DO NOT REUSE
        Flee                                 = 122,        // UNUSED, DO NOT REUSE
        AddThreat                            = 123,        // +threat, -threat
        LoadEquipment                        = 124,        // id
        TriggerRandomTimedEvent              = 125,        // id min range, id max range
        RemoveAllGameobjects                 = 126,        // UNUSED, DO NOT REUSE
        PauseMovement                        = 127,        // MovementSlot (default = 0, active = 1, controlled = 2), PauseTime (ms), Force
        PlayAnimkit                          = 128,
        ScenePlay                            = 129,        // sceneId
        SceneCancel                          = 130,        // sceneId
        SpawnSpawngroup                      = 131,        // Group ID, min secs, max secs, spawnflags
        DespawnSpawngroup                    = 132,        // Group ID, min secs, max secs, spawnflags
        RespawnBySpawnId                     = 133,        // spawnType, spawnId
        InvokerCast                          = 134,        // spellID, castFlags
        PlayCinematic                        = 135,        // entry, cinematic
        SetMovementSpeed                     = 136,        // movementType, speedInteger, speedFraction
        PlaySpellVisualKit                   = 137,        // spellVisualKitId, kitType (unknown values, copypaste from packet dumps), duration
        OverrideLight                        = 138,        // zoneId, overrideLightID, transitionMilliseconds
        OverrideWeather                      = 139,        // zoneId, weatherId, intensity
        SetAIAnimKit                         = 140,        // DEPRECATED, DO REUSE (it was never used in any branch, treat as free action id)
        SetHover                             = 141,        // 0/1
        SetHealthPct                         = 142,        // percent
        CreateConversation                   = 143,        // conversation_template.id
        SetImmunePC                          = 144,        // 0/1
        SetImmuneNPC                         = 145,        // 0/1
        SetUninteractible                    = 146,        // 0/1
        ActivateGameobject                   = 147,        // GameObjectActions
        AddToStoredTargetList                = 148,        // varID
        BecomePersonalCloneForPlayer         = 149,        // summonType 1-8, duration in ms
        TriggerGameEvent                     = 150,        // eventId, useSaiTargetAsGameEventSource
        DoAction                             = 151,
        End
    }

    public enum SmartTargets
    {
        None                                 = 0,          // None
        Self                                 = 1,          // Self Cast
        Victim                               = 2,          // Our Current Target (Ie: Highest Aggro)
        HostileSecondAggro                   = 3,          // Second highest aggro, maxdist, playerOnly, powerType + 1
        HostileLastAggro                     = 4,          // Dead last on aggro, maxdist, playerOnly, powerType + 1
        HostileRandom                        = 5,          // Just any random target on our threat list, maxdist, playerOnly, powerType + 1
        HostileRandomNotTop                  = 6,          // Any random target except top threat, maxdist, playerOnly, powerType + 1
        ActionInvoker                        = 7,          // Unit Who Caused This Event To Occur
        Position                             = 8,          // Use XYZ From Event Params
        CreatureRange                        = 9,          // CreatureEntry(0any), MinDist, MaxDist
        CreatureGuid                         = 10,         // Guid, Entry
        CreatureDistance                     = 11,         // CreatureEntry(0any), Maxdist
        Stored                               = 12,         // Id, Uses Pre-Stored Target(List)
        GameobjectRange                      = 13,         // Entry(0any), Min, Max
        GameobjectGuid                       = 14,         // Guid, Entry
        GameobjectDistance                   = 15,         // Entry(0any), Maxdist
        InvokerParty                         = 16,         // Invoker'S Party Members
        PlayerRange                          = 17,         // Min, Max
        PlayerDistance                       = 18,         // Maxdist
        ClosestCreature                      = 19,         // CreatureEntry(0any), MaxDist, Dead?
        ClosestGameobject                    = 20,         // Entry(0any), MaxDist
        ClosestPlayer                        = 21,         // Maxdist
        ActionInvokerVehicle                 = 22,         // Unit's vehicle who caused this event to occur
        OwnerOrSummoner                      = 23,         // Unit's owner or summoner, Use Owner/Charmer of this unit
        ThreatList                           = 24,         // All units on creature's threat list, maxdist
        ClosestEnemy                         = 25,         // maxDist, playerOnly
        ClosestFriendly                      = 26,         // maxDist, playerOnly
        LootRecipients                       = 27,         // all players that have tagged this creature (for kill credit)
        Farthest                             = 28,         // maxDist, playerOnly, isInLos
        VehiclePassenger                     = 29,         // seatMask (0 - all seats)
        ClosestUnspawnedGameobject           = 30,

        End                                  = 31
    }

    public enum LOSHostilityMode
    {
        Hostile                              = 0,
        NotHostile                           = 1,
        Any                                  = 2,
        End
    }