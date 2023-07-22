// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable InvertIf

using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Constants;
using Framework.Dynamic;
using Game;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game.Spells;

namespace Scripts.Maps.Azeroth.EasternKingdoms.ElwynnForest;

    struct SpellIds
    {
        public const uint VanessaStealth        = 228928;
        public const uint VanessaTeleportBehind = 396357;
        public const uint VanessaCheapShot      = 396359;
        // WindowToThePastData
        public const uint PlayOnyxiaScene       = 402962;
    }

    struct QuestIds
    {
        public const uint AnUnlikelyInformant = 72405;
        public const uint RottenOldMemories   = 72409;
    }

    struct CreatureIds
    {
        // ChildrenOfGoldshire
        public const uint Dana              = 804;
        public const uint Cameron           = 805;
        public const uint John              = 806;
        public const uint Lisa              = 807;
        public const uint Aaron             = 810;
        public const uint Jose              = 811;
        // AnUnlikelyInformant
        public const uint MathiasShaw       = 198896;
        public const uint VanessaVanCleef   = 198883;
        // RottenOldMemories
        public const uint RottenOldMemories = 20345;
    }

    struct DisplayIds
    {
        // AnUnlikelyInformant
        public const uint VanessaInvisible = 71887;
        public const uint VanessaVisible   = 110980;
    }

    struct GossipIds
    {
        // AnUnlikelyInformant
        public const uint MenuSignalInformant   = 30224;
        public const uint OptionSignalInformant = 0;
        // WindowToThePastData
        public const uint MenuWindowToThePast   = 30224;
        public const uint OptionWindowToThePast = 2;
    }

    struct ConversationIds
    {
        // AnUnlikelyInformant
        public const uint AnUnlikelyInformantHello = 20340;
        public const uint AnUnlikelyInformant      = 20342;
        public const uint LineVanessaTeleport      = 53702;
        public const uint LineVanessaMovement      = 52465;
        public const uint LineMathiasQuestCredit   = 52466;
        public const uint ActorIdxMathias          = 0;
        public const uint ActorIdxVanessa          = 1;
        // RottenOldMemories
        public const uint RottenOldMemories        = 20345;
    }

    struct EventIds
    {
        // ChildrenOfGoldshire
        public const uint ChildrenOfGoldshire = 76;
        public const uint Goldshire           = 1;
        public const uint Woods               = 2;
        public const uint House               = 3;
        public const uint Lisa                = 4;
        public const uint PlaySounds          = 5;
        public const uint Begin               = 6;
        // AnUnlikelyInformant
        public const uint VanessaTeleport     = 1;
        public const uint VanessaMove         = 2;
        public const uint VanessaCloneLean    = 3;
        public const uint MathiasQuestCredit  = 4;
        public const uint MathiasCloneDespawn = 5;
    }

    struct PathsIds
    {
        // ChildrenOfGoldshire
        public const uint Stormwind = 644000;
        public const uint Goldshire = 644008;
        public const uint Woods     = 644016;
        public const uint House     = 644024;
        public const uint Lisa      = 645600;
    }

    struct PointIds
    {
        // ChildrenOfGoldshire
        public const uint Stormwind     = 57;
        public const uint Goldshire     = 32;
        public const uint Woods         = 22;
        public const uint House         = 35;
        public const uint Lisa          = 4;
        // AnUnlikelyInformant
        public const uint VanessaFinish = 1;
    }

    struct SoundIds
    {
        // ChildrenOfGoldshire
        public const uint BansheeDeath        = 1171;
        public const uint BansheePreAggro     = 1172;
        public const uint CThunYouWillDie     = 8585;
        public const uint CThunDeathIsClose   = 8580;
        public const uint HumanFemaleEmoteCry = 6916;
        public const uint GhostDeath          = 3416;
    }

    [Script]
    class npc_cameron : ScriptedAI
    {
        public bool _started;
        public List<ObjectGuid> _childrenGUIDs = new();

        public npc_cameron(Creature creature) : base(creature) { }

        public static uint SoundPicker() => RandomHelper.RAND(SoundIds.BansheeDeath, SoundIds.BansheePreAggro, SoundIds.CThunYouWillDie, SoundIds.CThunDeathIsClose, SoundIds.HumanFemaleEmoteCry, SoundIds.GhostDeath);

        public void MoveTheChildren()
        {
            List<Position> MovePosPositions = new() {
                new Position(-9373.521f, -67.71767f, 69.201965f, 1.117011f),
                new Position(-9374.94f, -62.51654f, 69.201965f, 5.201081f),
                new Position(-9371.013f, -71.20811f, 69.201965f, 1.937315f),
                new Position(-9368.419f, -66.47543f, 69.201965f, 3.141593f),
                new Position(-9372.376f, -65.49946f, 69.201965f, 4.206244f),
                new Position(-9377.477f, -67.8297f, 69.201965f, 0.296706f)
            };

            MovePosPositions.RandomShuffle();

            // first we break formation because children will need to move on their own now
            foreach (var guid in _childrenGUIDs)
            {
                Creature child = Global.ObjAccessor.GetCreature(me, guid);

                if (child?.GetFormation() != null)
                    child.GetFormation().RemoveMember(child);
            }

            // Move each child to an random position
            for (int i = 0; i < _childrenGUIDs.Count; ++i)
            {
                Creature children = Global.ObjAccessor.GetCreature(me, _childrenGUIDs[i]);

                if (children != null)
                {
                    children.SetWalk(true);
                    children.GetMotionMaster().MovePoint(0, MovePosPositions[i], true, MovePosPositions[i].GetOrientation());
                }
            }

            me.SetWalk(true);
            me.GetMotionMaster().MovePoint(0, MovePosPositions.Last(), true, MovePosPositions.Last().GetOrientation());
        }

        public override void WaypointReached(uint waypointId, uint pathId)
        {
            switch (pathId)
            {
                case PathsIds.Stormwind:
                {
                    if (waypointId == PointIds.Stormwind)
                    {
                        me.GetMotionMaster().MoveRandom(10.0f);
                        _events.ScheduleEvent(EventIds.Goldshire, TimeSpan.FromMinutes(11));
                    }

                    break;
                }
                case PathsIds.Goldshire:
                {
                    if (waypointId == PointIds.Goldshire)
                    {
                        me.GetMotionMaster().MoveRandom(10.0f);
                        _events.ScheduleEvent(EventIds.Woods, TimeSpan.FromMinutes(15));
                    }
                    break;
                }
                case PathsIds.Woods:
                {
                    if (waypointId == PointIds.Woods)
                    {
                        me.GetMotionMaster().MoveRandom(10.0f);
                        _events.ScheduleEvent(EventIds.House, TimeSpan.FromMinutes(6));
                        _events.ScheduleEvent(EventIds.Lisa, TimeSpan.FromSeconds(2));
                    }

                    break;
                }
                case PathsIds.House:
                {
                    if (waypointId == PointIds.House)
                    {
                        // Move children at last point
                        MoveTheChildren();

                        // After 30 seconds a random sound should play
                        _events.ScheduleEvent(EventIds.PlaySounds, TimeSpan.FromSeconds(30));
                    }
                    break;
                }
            }
        }

        public override void OnGameEvent(bool start, ushort eventId)
        {
            if (start && eventId == EventIds.ChildrenOfGoldshire)
            {
                // Start event at 7 am
                // Begin pathing
                _events.ScheduleEvent(EventIds.Begin, TimeSpan.FromSeconds(2));
                _started = true;
            }
            else if (!start && eventId == EventIds.ChildrenOfGoldshire)
            {
                // Reset event at 8 am
                _started = false;
                _events.Reset();
            }
        }

        public override void UpdateAI(uint diff)
        {
            if (!_started)
                return;

            _events.Update(diff);

            _events.ExecuteEvents(eventId =>
            {
                switch (eventId)
                {
                    case EventIds.Goldshire:
                        me.GetMotionMaster().MovePath(PathsIds.Goldshire, false);
                        break;
                    case EventIds.Woods:
                        me.GetMotionMaster().MovePath(PathsIds.Woods, false);
                        break;
                    case EventIds.House:
                        me.GetMotionMaster().MovePath(PathsIds.House, false);
                        break;
                    case EventIds.Lisa:
                    {
                        foreach (var guid in _childrenGUIDs)
                        {
                            Creature child = Global.ObjAccessor.GetCreature(me, guid);

                            if (child != null && child.GetEntry() == CreatureIds.Lisa)
                            {
                                child.GetMotionMaster().MovePath(PathsIds.Lisa, false);
                                break;
                            }
                        }
                        break;
                    }
                    case EventIds.PlaySounds:
                        me.PlayDistanceSound(SoundPicker());
                        break;
                    case EventIds.Begin:
                    {
                        _childrenGUIDs.Clear();

                        Creature dana = me.FindNearestCreature(CreatureIds.Dana, 25.0f);

                        // Get all children's guid's.
                        if (dana != null)
                            _childrenGUIDs.Add(dana.GetGUID());

                        Creature john = me.FindNearestCreature(CreatureIds.John, 25.0f);

                        if (john != null)
                            _childrenGUIDs.Add(john.GetGUID());

                        Creature lisa = me.FindNearestCreature(CreatureIds.Lisa, 25.0f);

                        if (lisa != null)
                            _childrenGUIDs.Add(lisa.GetGUID());

                        Creature aaron = me.FindNearestCreature(CreatureIds.Aaron, 25.0f);

                        if (aaron != null)
                            _childrenGUIDs.Add(aaron.GetGUID());

                        Creature jose = me.FindNearestCreature(CreatureIds.Jose, 25.0f);

                        if (jose != null)
                            _childrenGUIDs.Add(jose.GetGUID());

                        // If Formation was disbanded, remake.
                        if (!me.GetFormation().IsFormed())
                            foreach (var guid in _childrenGUIDs)
                            {
                                Creature child = Global.ObjAccessor.GetCreature(me, guid);

                                if (child != null)
                                    child.SearchFormation();
                            }

                        // Start movement
                        me.GetMotionMaster().MovePath(PathsIds.Stormwind, false);
                        break;
                    }
                }
            });
        }
    }

    [Script] // 198896 - Master Mathias Shaw
    class npc_master_mathias_shaw_human_heritage_lions_pride_inn_basement : ScriptedAI
    {
        public npc_master_mathias_shaw_human_heritage_lions_pride_inn_basement(Creature creature) : base(creature) { }

        public override bool OnGossipSelect(Player player, uint menuId, uint gossipListId)
        {
            // Quest 72408 - A Window to the Past
            if (menuId == GossipIds.MenuWindowToThePast && gossipListId == GossipIds.OptionWindowToThePast)
            {
                ScriptObject.CloseGossipMenuFor(player);

                player.CastSpell((SpellCastTargets)null, SpellIds.PlayOnyxiaScene, true);
            }

            // Quest 72405 - An Unlikely Informant
            else if (menuId == GossipIds.MenuSignalInformant && gossipListId == GossipIds.OptionSignalInformant)
            {
                ScriptObject.CloseGossipMenuFor(player);

                Conversation.CreateConversation(ConversationIds.AnUnlikelyInformant, player, player.GetPosition(), player.GetGUID());
            }

            return true;
        }
    }

    [Script] // 198883 - Vanessa VanCleef
    class npc_vanessa_vancleef_human_heritage_lions_pride_inn_basement : ScriptedAI
    {
        public npc_vanessa_vancleef_human_heritage_lions_pride_inn_basement(Creature creature) : base(creature) { }

        public override void MovementInform(MovementGeneratorType type, uint pointId)
        {
            if (pointId == PointIds.VanessaFinish)
                _events.ScheduleEvent(EventIds.VanessaCloneLean, TimeSpan.FromSeconds(1));
        }

        public override void OnQuestAccept(Player player, Quest quest)
        {
            if (quest.Id == QuestIds.RottenOldMemories)
                Conversation.CreateConversation(ConversationIds.RottenOldMemories, player, player.GetPosition(), player.GetGUID());
        }

        public override void UpdateAI(uint diff)
        {
            _events.Update(diff);

            _events.ExecuteEvents(eventId =>
            {
                switch (eventId)
                {
                    case EventIds.VanessaCloneLean:
                        me.SetVirtualItem(1, 0);
                        me.SetNpcFlag(NPCFlags.Gossip | NPCFlags.QuestGiver);
                        me.SetFacingTo(4.47226f);
                        me.HandleEmoteCommand(Emote.StateWaLean02);
                        break;
                }
            });
        }
    }

    [Script]
    class at_human_heritage_lions_pride_inn_basement_enter : AreaTriggerAI
    {
        public at_human_heritage_lions_pride_inn_basement_enter(AreaTrigger areatrigger) : base(areatrigger) { }

        public override void OnUnitEnter(Unit unit)
        {
            var player = unit.ToPlayer();

            if (player == null || player.GetQuestStatus(QuestIds.AnUnlikelyInformant) != QuestStatus.Incomplete)
                return;

            Conversation.CreateConversation(ConversationIds.AnUnlikelyInformantHello, unit, unit.GetPosition(), unit.GetGUID());
        }
    }

    [Script] // 20342 - Conversation
    class conversation_an_unlikely_informant : ConversationScript
    {
        public EventMap _events = new();

        public conversation_an_unlikely_informant() : base("conversation_an_unlikely_informant") { }

        public override void OnConversationCreate(Conversation conversation, Unit creator)
        {
            Creature mathiasObject = ScriptedAI.GetClosestCreatureWithOptions(creator, 15.0f, new FindCreatureOptions().SetIgnorePhases(true).SetCreatureId(CreatureIds.MathiasShaw));
            Creature vanessaObject = ScriptedAI.GetClosestCreatureWithOptions(creator, 15.0f, new FindCreatureOptions().SetIgnorePhases(true).SetCreatureId(CreatureIds.VanessaVanCleef));

            if (!mathiasObject || !vanessaObject)
                return;

            TempSummon mathiasClone = mathiasObject.SummonPersonalClone(mathiasObject.GetPosition(), TempSummonType.ManualDespawn, TimeSpan.Zero, 0, 0, creator.ToPlayer());
            TempSummon vanessaClone = vanessaObject.SummonPersonalClone(new Position(-9462.44f, -11.7101f, 50.161f, 2.99500f), TempSummonType.ManualDespawn, TimeSpan.Zero, 0, 0, creator.ToPlayer());
            if (!mathiasClone || !vanessaClone)
                return;

            mathiasClone.RemoveNpcFlag(NPCFlags.Gossip | NPCFlags.QuestGiver);
            vanessaClone.RemoveNpcFlag(NPCFlags.Gossip | NPCFlags.QuestGiver);
            vanessaClone.SetVirtualItem(1, vanessaClone.GetVirtualItemId(0)); // add 2nd dagger to hands

            conversation.AddActor((int)ConversationIds.AnUnlikelyInformant, ConversationIds.ActorIdxMathias, mathiasClone.GetGUID());
            conversation.AddActor((int)ConversationIds.AnUnlikelyInformant, ConversationIds.ActorIdxVanessa, vanessaClone.GetGUID());
            conversation.Start();
        }

        public override void OnConversationStart(Conversation conversation)
        {
            Locale privateOwnerLocale = conversation.GetPrivateObjectOwnerLocale();

            var teleportLineStartTime = conversation.GetLineStartTime(privateOwnerLocale, (int)ConversationIds.LineVanessaTeleport);

            if (!teleportLineStartTime.Equals(TimeSpan.Zero))
                _events.ScheduleEvent(EventIds.VanessaTeleport, teleportLineStartTime);

            var movementStartTime = conversation.GetLineStartTime(privateOwnerLocale, (int)ConversationIds.LineVanessaMovement);

            if (!movementStartTime.Equals(TimeSpan.Zero))
                _events.ScheduleEvent(EventIds.VanessaMove, movementStartTime);

            var questCreditStartTime = conversation.GetLineStartTime(privateOwnerLocale, (int)ConversationIds.LineMathiasQuestCredit);

            if (!questCreditStartTime.Equals(TimeSpan.Zero))
                _events.ScheduleEvent(EventIds.MathiasQuestCredit, questCreditStartTime);

            _events.ScheduleEvent(EventIds.MathiasCloneDespawn, conversation.GetLastLineEndTime(privateOwnerLocale));
        }

        public override void OnConversationUpdate(Conversation conversation, uint diff)
        {
            _events.Update(diff);

            switch (_events.ExecuteEvent())
            {
                case EventIds.VanessaTeleport:
                {
                    var privateObjectOwner = Global.ObjAccessor.GetUnit(conversation, conversation.GetPrivateObjectOwner());

                    if (!privateObjectOwner)
                        break;

                    Creature vanessaClone = conversation.GetActorCreature(ConversationIds.ActorIdxVanessa);

                    if (!vanessaClone)
                        break;

                    vanessaClone.CastSpell(privateObjectOwner, SpellIds.VanessaTeleportBehind, true);
                    vanessaClone.CastSpell(privateObjectOwner, SpellIds.VanessaCheapShot, true);
                    vanessaClone.RemoveAurasDueToSpell(SpellIds.VanessaStealth);
                    vanessaClone.SetEmoteState(Emote.StateReady1h);
                    break;
                }
                case EventIds.VanessaMove:
                {
                    Creature vanessaClone = conversation.GetActorCreature(ConversationIds.ActorIdxVanessa);

                    if (!vanessaClone)
                        break;

                    vanessaClone.SetWalk(true);
                    vanessaClone.SetEmoteState(Emote.StateNone);
                    vanessaClone.GetMotionMaster().MovePoint(PointIds.VanessaFinish, new Position(-9468.16f, -3.6128f, 49.876f, 4.47226f));
                    break;
                }
                case EventIds.MathiasQuestCredit:
                {
                    Unit privateObjectOwner = Global.ObjAccessor.GetUnit(conversation, conversation.GetPrivateObjectOwner());

                    if (!privateObjectOwner)
                        break;

                    Creature vanessaClone = conversation.GetActorCreature(ConversationIds.ActorIdxVanessa);

                    if (!vanessaClone)
                        break;

                    Creature mathiasClone = conversation.GetActorCreature(ConversationIds.ActorIdxMathias);

                    if (!mathiasClone)
                        break;

                    privateObjectOwner.ToPlayer().KilledMonsterCredit(CreatureIds.MathiasShaw);
                    vanessaClone.DespawnOrUnsummon();
                    mathiasClone.SetNpcFlag(NPCFlags.Gossip | NPCFlags.QuestGiver);
                    break;
                }
                case EventIds.MathiasCloneDespawn:
                {
                    Creature mathiasClone = conversation.GetActorCreature(ConversationIds.ActorIdxMathias);

                    if (!mathiasClone)
                        break;

                    mathiasClone.DespawnOrUnsummon();
                    break;
                }
            }
        }
    }