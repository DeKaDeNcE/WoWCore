// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable InvertIf

using System;
using System.Collections.Generic;
using Framework.Constants;
using Game;
using Game.AI;
using Game.Spells;
using Game.Entities;
using Game.Scripting;

namespace Scripts.Maps.Shadowlands.ExilesReach;

// TODO: Implement Basic Tutorial
// TODO: Fix remaining Phasing issues
// TODO: Add missing waypoints for Gryphons
// TODO: Add missing waypoints for Alliance Sailor and update model
// TODO: Fix Spell Jump Behind and remove Hackfix in Quest Stand Your Ground
// TODO: Fix Core BUG when Transport hits certain position while looping route nothing works anymore for 10 seconds (combat stops working and moving NPCs are falling through boat)
// TODO: Fix Performance by removing any code in PlayerScript.OnUpdate()

struct QuestIds
{
    public const uint WarmingUpAlliance                     = 56775;
    public const uint WarmingUpHorde                        = 59926;
    public const uint StandYourGroundAlliance               = 58209;
    public const uint StandYourGroundHorde                  = 59927;
    public const uint BraceForImpactAlliance                = 58208;
    public const uint BraceForImpactHorde                   = 59928;
}

struct CreatureIds
{
    public const uint WarlordBrekagrimaxe1                  = 166573;
    public const uint WarlordBrekagrimaxe2                  = 166824;
    public const uint WarlordBrekagrimaxe3                  = 166827;
    public const uint GruntThrog                            = 166583;
    public const uint MithdranDawntracker                   = 166590;
    public const uint LanaJordan                            = 166794;
    public const uint Bo                                    = 166585;
    public const uint ProvisonerJinHake                     = 166799;
    public const uint CaptainGarrick                        = 156280;
    public const uint QuartermasterRichter                  = 157042;
    public const uint KeeLa                                 = 157043;
    public const uint BjornStouthands                       = 157044;
    public const uint AustinHuxworth                        = 157046;
    public const uint PrivateCole                           = 160664;
    public const uint MechanicalBunnyPet                    = 167337; // Gnome Hunter Pet
    public const uint MothPet                               = 167342; // Draenei Hunter Pet
    public const uint DragonhawkPet                         = 167343; // Blood Elf Hunter Pet
    public const uint ScorpionPet                           = 167344; // Goblin Hunter Pet
    public const uint GreyWolfPet                           = 167345; // Human Hunter Pet
    public const uint RedWolfPet                            = 167346; // Orc Hunter Pet
    public const uint TigerPet                              = 167347; // Night Elf Hunter Pet
    public const uint TurtlePet                             = 167348; // Pandaren Hunter Pet
    public const uint PlainstriderPet                       = 167349; // Tauren Hunter Pet
    public const uint RaptorPet                             = 167350; // Troll Hunter Pet
    public const uint BatPet                                = 167351; // Undead Hunter Pet
    public const uint DogPet                                = 167352; // Worgen Hunter Pet
    public const uint BearPet                               = 167375; // Dwarf Hunter Pet

    // SparringPartner
    public const uint AllianceSparringPartner               = 157051;
    public const uint HordeSparringPartner                  = 166814;
    public const uint SparPointAdvertisement                = 174971;
    public const uint KillCredit                            = 155607;

    // SpellCrashLanded
    public const uint CaptainGarrick2                       = 156626;
    public const uint WarlordBrekaGrimaxe4                  = 166782;

}

struct SpellIds
{
    // SparringPartner
    public const uint JumpBehind1                           = 312755;
    public const uint JumpBehind2                           = 312757;
    public const uint CombatTraining                        = 323071;
    public const uint UpdatePhaseShift                      = 82238;
    public const uint SummonCole                            = 303064;
    public const uint SummonThrog                           = 325107;

    // PlayerScriptHordeShipCrash
    public const uint AllianceShipCrash                     = 305446;
    public const uint HordeShipCrash                        = 325133;

    // AllianceHordeShipSceneSpells
    public const uint BeginTutorial                         = 295600;
    public const uint KnockedDown                           = 305445;
    public const uint CrashedLandedAlliance                 = 305464;
    public const uint CrashedLandedHorde                    = 325136;
}

struct EventIds
{
    // SparringPartner
    public const uint MoveToAPosition                       = 1;
    public const uint PreFightConversation                  = 2;
    public const uint WalkBack                              = 3;
}

struct PathIds
{
    public const uint AllianceSparringPartner               = 10501460;
    public const uint HordeSparringPartner                  = 10501870;
}

struct PointIds
{
    public const uint SparPointAdvertisement                = 1;
    public const uint SparPointReady                        = 2;
}

struct ItemIds
{
    public const int Sword                                  = 108493;
    public const int Axe                                    = 175161;
}

struct MovieIds
{
    public const uint AllianceShipCrash                     = 895;
    public const uint HordeShipCrash                        = 931;
}

//Storylines
//The Missing Expedition

// Minimap hidden
// XP bar hidden

// Tutorial
// 1. Position your hands and click Okay
// 2. Look around
// 3. Walk around
// 4. Talk with Captain Garrick
// 5. Attack a Combat Dummy
// 6. Press 1 to use Slam
// 7. [Rage generation]
// 8. Quest complete! Return to the questgiver.
// 9. You've gained experience! You'll level up when this bar is filled
// 10. Open your Spellbook and find Charge
// 11. Drag Charge to your Action Bar

// On Move
// Sound + Captain Garrick says: $n, step forward! We're approaching the island.
// Broadcast ID 169340 Sound ID 152731 Temp Sound ID 152836 Temp Sound ID 161221

// Warming Up 56775
// 55 copper (scaling with player level)
// 200 exp (scaling with player level)
// On Accept Quest
// Captain Garrick says: I need everyone in top form if we're to complete this mission. Show me what you can do!
// On Complete Quest Objective
// Captain Garrick says: Good! Next... hm, that's odd. We weren't expecting rain...
// On Finish Quest
// Waypoint move
// Captain Garrick says: Private Cole will run you through the rest of the drills. I need to discuss this storm with the helmsman.

// Stand your ground 58209
// 55 copper (scaling with player level)
// 200 exp (scaling with player level)
// 1 silver 20 copper (level 2)
// 340 exp (level 2)
// On Accept Quest
// Waypoint move
// Sound
// NPC: Private Cole 157051
// NPC: Alliance Sparring Partner 157051
// Health: 100%
// Private Cole says: Let's move into sparring positions. I'll let you have the first strike.
// Health: 90%
// Private Cole says: Never run from your opponent. Stand your ground and fight until the end!
// Health: 50%
// Private Cole says: Remember to always face your enemy!
// Teleports behind player
// Health: 5%
// Private Cole says: I yield! Well, I'd say you're more than ready for whatever we find on that island.
// Waypoint move
// On Finish Quest
// Sound Keep up the good fight

// Brace for impact 58208
// 30 copper
// 40 exp
// On Accept Quest
// Storm effect Phased?
// Waypoint move Phased?
// Sound + Private Cole says: Captain! We can't weather this storm for long!
// Sound + Captain Garrick says: Everyone below decks! Now!
// On Finish Quest
// Teleport
// Movie
// In-game cinematic
// Captain Garrick says: $n! Thank the Light! Are you injured?
// .debug play movie 895
// 895 - alliance
// 931 - horde
// .scene playpackage 2708
// 2708 - alliance

[Script]
class player_start_exiles_reach : PlayerScript
{
    private uint timer = 1000;
    private bool isActive;

    public player_start_exiles_reach() : base("player_start_exiles_reach") { }

    public override void OnUpdate(Player player, uint diff)
    {
        // TODO: Fix Performance by removing any code in PlayerScript.OnUpdate()
        if (player.GetZoneId() == (uint)AreaIds.ExilesReach && player.GetAreaId() == (uint)AreaIds.TheNorthSea3)
        {
            if (player.GetQuestStatus(QuestIds.WarmingUpAlliance) == QuestStatus.None)
            {
                var garrick = player.FindNearestCreature(CreatureIds.CaptainGarrick, 75.0f);

                if (garrick != null && player.IsWithinDistInMap(garrick, 11.0f) && !isActive)
                {
                    Global.CreatureTextMgr.SendChat(garrick, 0, player);
                    timer = 12000;
                    isActive = true;
                }

                if (timer <= diff)
                {
                    if (garrick != null && player.IsWithinDistInMap(garrick, 11.0f))
                        Global.CreatureTextMgr.SendChat(garrick, 0, player);

                    timer = 12000;
                    isActive = false;
                }
                else timer -= diff;
            }

            if (player.GetQuestStatus(QuestIds.StandYourGroundAlliance) == QuestStatus.Incomplete)
            {
                if (player.HealthBelowPct(50))
                    player.SetFullHealth();
            }
        }

        if (player.GetZoneId() == (uint)AreaIds.ExilesReach && player.GetAreaId() == (uint)AreaIds.TheNorthSea4)
        {
            if (player.GetQuestStatus(QuestIds.WarmingUpHorde) == QuestStatus.None)
            {
                var breka = player.FindNearestCreature(CreatureIds.WarlordBrekagrimaxe1, 75.0f);

                if (breka != null && player.IsWithinDistInMap(breka, 6.0f) && !isActive)
                {
                    Global.CreatureTextMgr.SendChat(breka, 0, player);
                    timer = 12000;
                    isActive = true;
                }

                if (timer <= diff)
                {
                    if (breka != null && player.IsWithinDistInMap(breka, 6.0f))
                        Global.CreatureTextMgr.SendChat(breka, 0, player);

                    timer = 12000;
                    isActive = false;
                }
                else timer -= diff;
            }

            if (player.GetQuestStatus(QuestIds.StandYourGroundHorde) == QuestStatus.Incomplete)
            {
                if (player.HealthBelowPct(50))
                    player.SetFullHealth();
            }
        }
    }

    public override void OnMovieComplete(Player player, uint movieId)
    {
        switch (movieId)
        {
            case MovieIds.AllianceShipCrash:
                player.CastSpell(player,SpellIds.AllianceShipCrash, true);
                break;
            case MovieIds.HordeShipCrash:
                player.CastSpell(player, SpellIds.HordeShipCrash, true);
                break;
        }
    }
}

[Script]
class npc_sparring_partner : ScriptedAI
{
    // private EventMap _events;
    private bool _jumped;
    private byte _actorID;
    private uint _path;
    private uint _summonSpell;
    private ObjectGuid _playerGUID;

    public npc_sparring_partner(Creature creature) : base(creature) { }

    public override void JustAppeared()
    {
        if (me.GetEntry() == CreatureIds.AllianceSparringPartner)
        {
            SetEquipmentSlots(false, ItemIds.Sword);
            _summonSpell = SpellIds.SummonCole;
            _path = PathIds.AllianceSparringPartner;
            _actorID = 0;
        }
        else if (me.GetEntry() == CreatureIds.HordeSparringPartner)
        {
            SetEquipmentSlots(false, ItemIds.Axe);
            _summonSpell = SpellIds.SummonThrog;
            _path = PathIds.HordeSparringPartner;
            _actorID = 1;
        }
    }

    public override void IsSummonedBy(WorldObject summoner)
    {
        _jumped = false;

        var unit = summoner.ToUnit();

        if (unit != null)
        {
            if (unit.IsPlayer())
            {
                _playerGUID = unit.GetGUID();
                _events.ScheduleEvent(EventIds.MoveToAPosition, TimeSpan.FromSeconds(2));
            }
        }
    }

    public override void EnterEvadeMode(EvadeReason why)
    {
        if (!me.IsAlive())
            return;

        me.CombatStop(true);
        EngagementOver();
        me.ResetPlayerDamageReq();
        _events.ScheduleEvent(EventIds.WalkBack, TimeSpan.FromSeconds(1));
    }

    public override void MovementInform(MovementGeneratorType type, uint pointId)
    {
        if (type != MovementGeneratorType.Point)
            return;

        switch (pointId)
        {
            case PointIds.SparPointAdvertisement:
                me.SetWalk(true);
                me.GetMotionMaster().MovePoint(PointIds.SparPointReady, me.GetFirstCollisionPosition(2.0f, RandomHelper.NextSingle() * (2 * MathF.PI)));
                break;
            case PointIds.SparPointReady:
                var player = Global.ObjAccessor.GetPlayer(me, _playerGUID);

                if (player != null)
                    me.SetFacingToObject(player);

                me.RemoveUnitFlag(UnitFlags.ImmuneToPc | UnitFlags.Uninteractible);
                me.SetFaction(32); // TODO: HACK to be removed after issue with entering combat with faction 35 fixed
                break;
        }
    }

    public override void WaypointPathEnded(uint nodeId, uint pathId)
    {
        var player = Global.ObjAccessor.GetPlayer(me, _playerGUID);

        if (player != null)
        {
            player.KilledMonsterCredit(CreatureIds.KillCredit); // TODO: Remove MINOR HACK should be done when fight ends but phase change is tied to quest conditions.
            player.CastSpell(player, SpellIds.UpdatePhaseShift);
            player.RemoveAura(_summonSpell);
        }
    }

    public override void DamageTaken(Unit attacker, ref uint damage, DamageEffectType damageType, SpellInfo spellInfo = null)
    {
        if (me.GetHealth() <= damage)
        {
            damage = 0;
            me.SetHealth(1);
            DoStopAttack();
            me.SetUnitFlag(UnitFlags.ImmuneToPc | UnitFlags.Uninteractible);

            me.SetFacingToObject(attacker);
            Talk(3, attacker);
            attacker.CastSpell(attacker, SpellIds.CombatTraining);
        }

        if (me.HealthBelowPct(65) && !_jumped)
        {
            _jumped = true;
            // TODO: Remove hackfix and fix spell
            // DoCastSelf(SpellIds.JumpBehind1, new CastSpellExtraArgs(true));
            me.SetWalk(false);
            me.GetMotionMaster().MovePoint(0, me.GetPositionX() + (float)(Math.Cos(me.GetOrientation()) * 5.0f), me.GetPositionY() + (float)(Math.Sin(me.GetOrientation()) * 5.0f), me.GetPositionZ());
            Talk(2, attacker);
        }
    }

    public override void JustEngagedWith(Unit who)
    {
        var player = who.ToPlayer();

        if (player != null)
            Talk(1, player);
    }

    public override void UpdateAI(uint diff)
    {
        _events.Update(diff);

        _events.ExecuteEvents(eventId =>
        {
            switch (eventId)
            {
                case EventIds.MoveToAPosition:
                    {
                        var sparringPoints = me.GetCreatureListWithEntryInGrid(CreatureIds.SparPointAdvertisement, 25.0f);
                        sparringPoints.RandomResize(1);

                        foreach (var creature in sparringPoints)
                        {
                            me.GetMotionMaster().MovePoint(PointIds.SparPointAdvertisement, creature.GetPosition());
                            me.SetUnitFlag(UnitFlags.ImmuneToPc);
                        }

                        var player = Global.ObjAccessor.GetPlayer(me, _playerGUID);

                        if (player != null)
                            Talk(0, player);
                    }
                    break;
                case EventIds.WalkBack:
                    me.GetMotionMaster().Clear();
                    me.SetWalk(true);
                    me.GetMotionMaster().MovePath(_path, false);
                    break;
            }
        });

        if (!UpdateVictim())
            return;

        DoMeleeAttackIfReady();
    }
}

[Script] // 305464 Crash Landed Alliance
class spell_crash_landed_alliance : SpellScript
{
    void HandleEffect(uint effIndex)
    {
        Unit caster = GetCaster();

        if (caster != null)
        {
            Player player = caster.ToPlayer();

            if (player != null)
            {
                Creature garrick = player.FindNearestCreature(CreatureIds.CaptainGarrick2, 40.0f);

                if (garrick != null)
                {
                    Creature garrick2 = garrick.SummonPersonalClone(garrick.GetPosition(), TempSummonType.TimedDespawn, TimeSpan.FromSeconds(10), 0, 0, player);

                    if (garrick2.IsAIEnabled())
                        garrick2.GetAI().SetData(1, 1);
                }
            }
        }
    }

    public override void Register()
    {
        OnEffectHit.Add(new EffectHandler(HandleEffect, 0, SpellEffectName.SendEvent));
    }
}

[Script] // 325136 Crash Landed Horde
class spell_crash_landed_horde : SpellScript
{
    void HandleEffect(uint effIndex)
    {
        Unit caster = GetCaster();

        if (caster != null)
        {
            Player player = caster.ToPlayer();

            if (player != null)
            {
                Creature breka = player.FindNearestCreature(CreatureIds.WarlordBrekaGrimaxe4, 40.0f);

                if (breka != null)
                {
                    Creature breka2 = breka.SummonPersonalClone(breka.GetPosition(), TempSummonType.TimedDespawn, TimeSpan.FromSeconds(10), 0, 0, player);

                    if (breka2.IsAIEnabled())
                        breka2.GetAI().SetData(1, 1);
                }
            }
        }
    }

    public override void Register()
    {
        OnEffectHit.Add(new EffectHandler(HandleEffect, 0, SpellEffectName.SendEvent));
    }
}

[Script] // 305425 Ship Crash (DNT) Alliance Need Spell.cpp updated to handle this
class spell_alliance_spell_ship_crash_teleport : SpellScript
{
    void RelocateTransportOffset(uint effIndex)
    {
        Unit target = GetHitUnit();

        if (target != null)
        {
            var transport = target.GetTransport();

            if (transport != null)
                transport.RemovePassenger(target);
        }
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(RelocateTransportOffset, 4, SpellEffectName.TeleportUnits));
    }
}

[Script] // 325131 Ship Crash (DNT) Horde Need Spell.cpp updated to handle this
class spell_horde_spell_ship_crash_teleport : SpellScript
{
    void RelocateTransportOffset(uint effIndex)
    {
        Unit target = GetHitUnit();

        if (target != null)
        {
            var transport = target.GetTransport();

            if (transport != null)
                transport.RemovePassenger(target);
        }
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(RelocateTransportOffset, 3, SpellEffectName.TeleportUnits));
    }
}

[Script]
class quest_warming_up : QuestScript
{
    public quest_warming_up() : base("quest_warming_up") { }

    public override void OnQuestStatusChange(Player player, Quest quest, QuestStatus oldStatus, QuestStatus newStatus)
    {
        var garrick = player.FindNearestCreature(CreatureIds.CaptainGarrick, 75.0f);
        var breka = player.FindNearestCreature(CreatureIds.WarlordBrekagrimaxe1, 75.0f);

        switch (newStatus)
        {
            case QuestStatus.Incomplete:
                if (garrick != null)
                    Global.CreatureTextMgr.SendChat(garrick, 1, player);

                if (breka != null)
                    Global.CreatureTextMgr.SendChat(breka, 1, player);
                break;
            case QuestStatus.Complete:
                if (garrick != null)
                    Global.CreatureTextMgr.SendChat(garrick, 2, player);

                if (breka != null)
                    Global.CreatureTextMgr.SendChat(breka, 2, player);
                break;
            case QuestStatus.Rewarded:
                if (quest.Id == QuestIds.WarmingUpAlliance)
                {
                    var garrickpos = new Position(-3.0797f, -0.546193f, 5.29752f, 3.3191178f); // transport offset
                    var transport = player.GetDirectTransport();

                    if (transport != null)
                    {
                        if (garrick != null)
                        {
                            garrickpos.GetPosition(out var x, out var y, out var z, out var o);
                            transport.CalculatePassengerPosition(ref x, ref y, ref z, ref o);
                            garrickpos.Relocate(x, y, z, o);

                            var garrick2 = garrick.SummonPersonalClone(garrickpos, TempSummonType.TimedDespawn, TimeSpan.FromSeconds(60), 0, 0, player);

                            if (garrick2 != null)
                            {
                                garrick2.RemoveNpcFlag(NPCFlags.QuestGiver);

                                if (garrick2.IsAIEnabled())
                                    garrick2.GetAI().SetData(1, 1);
                            }
                        }
                    }
                }
                else if (quest.Id == QuestIds.WarmingUpHorde)
                {
                    var breka1 = player.FindNearestCreature(CreatureIds.WarlordBrekagrimaxe1, 10.0f);

                    if (breka1 != null)
                    {
                        var breka2 = player.FindNearestCreature(CreatureIds.WarlordBrekagrimaxe2, 75.0f);

                        if (breka2 != null)
                            breka2.SummonPersonalClone(breka1.GetPosition(), TempSummonType.TimedDespawn, TimeSpan.FromSeconds(18), 0, 0, player);
                    }
                }
                break;
        }
    }
}

[Script]
class quest_stand_your_ground : QuestScript
{
    public quest_stand_your_ground() : base("quest_stand_your_ground") { }

    public override void OnQuestStatusChange(Player player, Quest quest, QuestStatus oldStatus, QuestStatus newStatus)
    {
        if (newStatus == QuestStatus.None)
        {
            if (player.IsTeamAlliance())
                player.RemoveAura(SpellIds.SummonCole);
            else
                player.RemoveAura(SpellIds.SummonThrog);
        }
    }
}

[Script]
class quest_brace_for_impact : QuestScript
{
    public quest_brace_for_impact() : base("quest_brace_for_impact") { }

    public override void OnQuestStatusChange(Player player, Quest quest, QuestStatus oldStatus, QuestStatus newStatus)
    {
        if (newStatus == QuestStatus.Complete)
        {
            if (quest.Id == QuestIds.BraceForImpactAlliance)
            {
                var garrickpos = new Position(35.5643f, -1.19837f, 12.1479f, 3.3272014f);
                var richterpos = new Position(-1.84858f, -8.38776f, 5.10018f, 1.5184366f);
                var keelapos = new Position(-15.3642f, 6.5793f, 5.5026f, 3.1415925f);
                var bjornpos = new Position(12.8406f, -8.49553f, 4.98031f, 4.8520155f);
                var austinpos = new Position(-4.48607f, 9.89729f, 5.07851f, 1.5184366f);
                var colepos = new Position(-13.3396f, 0.702157f, 5.57996f, 0.087266445f);
                var petpos = new Position(-1.4492f, 8.06887f,  5.10348f, 2.6005409f);

                SpawnActor(player, CreatureIds.CaptainGarrick, garrickpos);
                SpawnActor(player, CreatureIds.QuartermasterRichter, richterpos);
                SpawnActor(player, CreatureIds.KeeLa, keelapos);
                SpawnActor(player, CreatureIds.BjornStouthands, bjornpos);
                SpawnActor(player, CreatureIds.AustinHuxworth, austinpos);
                SpawnActor(player, CreatureIds.PrivateCole, colepos);

                // Spawn pet
                if (player.GetClass() == Class.Hunter)
                {
                    switch (player.GetRace())
                    {
                        case Race.Human:
                            SpawnActor(player, CreatureIds.GreyWolfPet, petpos);
                            break;
                        case Race.Dwarf:
                            SpawnActor(player, CreatureIds.BearPet, petpos);
                            break;
                        case Race.NightElf:
                            SpawnActor(player, CreatureIds.TigerPet, petpos);
                            break;
                        case Race.Gnome:
                            SpawnActor(player, CreatureIds.MechanicalBunnyPet, petpos);
                            break;
                        case Race.Draenei:
                            SpawnActor(player, CreatureIds.MothPet, petpos);
                            break;
                        case Race.Worgen:
                            SpawnActor(player, CreatureIds.DogPet, petpos);
                            break;
                        case Race.PandarenAlliance:
                            SpawnActor(player, CreatureIds.TurtlePet, petpos);
                            break;
                    }
                }
            }
            else if (quest.Id == QuestIds.BraceForImpactHorde)
            {
                var brekapos = new Position(25.5237f, 0.283005f, 26.5455f, 3.3526998f);
                var throgpos = new Position(-10.8399f, 11.9039f, 8.88028f, 6.2308254f);
                var mithpos = new Position(-24.4763f, -4.48273f, 9.13471f, 0.62831855f);
                var lanapos = new Position(-5.1971f, -15.0268f, 8.992f, 4.712389f);
                var bopos = new Position(-22.1559f, 5.58041f, 9.09176f, 6.143559f);
                var jinpos = new Position(-31.9464f, 7.5772f, 10.6408f, 6.0737457f);
                var petpos = new Position(-22.8374f, -3.08287f, 9.12613f, 3.857178f);

                SpawnActor(player, CreatureIds.WarlordBrekagrimaxe3, brekapos);
                SpawnActor(player, CreatureIds.GruntThrog, throgpos);
                SpawnActor(player, CreatureIds.MithdranDawntracker, mithpos);
                SpawnActor(player, CreatureIds.LanaJordan, lanapos);
                SpawnActor(player, CreatureIds.Bo, bopos);
                SpawnActor(player, CreatureIds.ProvisonerJinHake, jinpos);

                // Spawn pet
                if (player.GetClass() == Class.Hunter)
                {
                    switch (player.GetRace())
                    {
                        case Race.Orc:
                            SpawnActor(player, CreatureIds.RedWolfPet, petpos);
                            break;
                        case Race.Undead:
                            SpawnActor(player, CreatureIds.BatPet, petpos);
                            break;
                        case Race.Tauren:
                            SpawnActor(player, CreatureIds.PlainstriderPet, petpos);
                            break;
                        case Race.Troll:
                            SpawnActor(player, CreatureIds.RaptorPet, petpos);
                            break;
                        case Race.Goblin:
                            SpawnActor(player, CreatureIds.ScorpionPet, petpos);
                            break;
                        case Race.BloodElf:
                            SpawnActor(player, CreatureIds.DragonhawkPet, petpos);
                            break;
                        case Race.PandarenHorde:
                            SpawnActor(player, CreatureIds.TurtlePet, petpos);
                            break;
                    }
                }
            }
        }
    }

    void SpawnActor(Player player, uint entry, Position position)
    {
        var creature = player.FindNearestCreature(entry, 75.0f);

        if (creature != null)
        {
            var transport = player.GetDirectTransport();

            if (transport != null)
            {
                position.GetPosition(out var x, out var y, out var z, out var o);
                transport.CalculatePassengerPosition(ref x, ref y, ref z, ref o);
                position.Relocate(x, y, z, o);
                Creature actor = creature.SummonPersonalClone(position, TempSummonType.ManualDespawn, TimeSpan.Zero, 0, 0, player);

                // Needed for pathing
                switch (entry)
                {
                    case CreatureIds.CaptainGarrick:
                        if (actor.IsAIEnabled())
                            actor.GetAI().SetData(1, 2);
                        break;
                    case CreatureIds.GreyWolfPet:
                    case CreatureIds.BearPet:
                    case CreatureIds.TigerPet:
                    case CreatureIds.MechanicalBunnyPet:
                    case CreatureIds.MothPet:
                    case CreatureIds.DogPet:
                    case CreatureIds.RedWolfPet:
                    case CreatureIds.BatPet:
                    case CreatureIds.PlainstriderPet:
                    case CreatureIds.RaptorPet:
                    case CreatureIds.ScorpionPet:
                    case CreatureIds.DragonhawkPet:
                    case CreatureIds.TurtlePet:
                        if (actor.IsAIEnabled())
                        {
                            if (player.IsTeamAlliance())
                                actor.GetAI().SetData(1, 1);
                            else
                                actor.GetAI().SetData(1, 2);
                        }
                        break;
                }
            }
        }
    }
}

[Script]
class scene_alliance_and_horde_ship : SceneScript
{
    public scene_alliance_and_horde_ship() : base("scene_alliance_and_horde_ship") { }

    public override void OnSceneComplete(Player player, uint sceneInstanceID, SceneTemplate sceneTemplate)
    {
        player.CastSpell(player, SpellIds.BeginTutorial, true);
    }

    public override void OnSceneCancel(Player player, uint sceneInstanceID, SceneTemplate sceneTemplate)
    {
        player.CastSpell(player, SpellIds.BeginTutorial, true);
    }
}

[Script]
class scene_alliance_and_horde_crash : SceneScript
{
    public scene_alliance_and_horde_crash() : base("scene_alliance_and_horde_crash") { }

    public override void OnSceneTriggerEvent(Player player, uint sceneInstanceID, SceneTemplate sceneTemplate, string triggerName)
    {
        if (triggerName == "Begin Knockdown Aura")
            player.CastSpell(player, SpellIds.KnockedDown, true);
    }

    public override void OnSceneComplete(Player player, uint sceneInstanceID, SceneTemplate sceneTemplate)
    {
        if (player.IsTeamAlliance())
            player.CastSpell(player, SpellIds.CrashedLandedAlliance, true);
        else
            player.CastSpell(player, SpellIds.CrashedLandedHorde, true);
    }
}