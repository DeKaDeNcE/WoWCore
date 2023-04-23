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

// Death Knight Starting Zone

using System;
using System.Numerics;
using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Movement;
using Game.Scripting;
using Game.Spells;

namespace Scripts.Maps.Azeroth.EasternKingdoms.EasternPlaguelands.TheScarletEnclave;

struct CreatureIds
{
    // Val'kyr Battle-maiden
    public const uint ValkyrBattlemaiden = 28534;

    // Unworthy Initiate
    public const uint UnworthyInitiateAnchor = 29521;

    // EyeOfAcherus
    public const uint EyeOfAcherus = 28511;
    public const uint ScarletHold = 28542;
    public const uint NewAvalonTownHall = 28543;
    public const uint ChapelOfTheCrimsonFlame = 28544;
    public const uint NewAvalonForge = 28525;

    // SalanarTheHorseman
    public const uint DarkRiderOfAcherus = 28654;
    public const uint SalanarInRealmOfShadows = 28788;

    // dkc1_gothik
    public const uint Ghouls = 28845;
    public const uint Ghosts = 28846;

    // ScarletMinerCart
    public const uint Miner = 28841;
}

struct QuestIds
{
    public const uint TheEmblazonedRuneblade = 12619;
    public const uint RuneforgingPreparationForBattle = 12842;
}

struct SpellIds
{
    // Val'kyr Battle-maiden
    public const uint Revive = 51918;
    // Emblazon
    public const uint EmblazonRuneblade = 51770;
    // Runeforge
    public const uint RuneforgingCredit = 54586;
    // Unworthy Initiate
    public const uint SoulPrisonChainSelf = 54612;
    public const uint SoulPrisonChain = 54613;
    public const uint DeathKnightInitiateVisual = 51519;
    public const uint IcyTouch = 52372;
    public const uint PlagueStrike = 52373;
    public const uint BloodStrike = 52374;
    public const uint BloodPlague = 55078;
    // Eye Of Acherus
    public const uint SummonEyeOfAcherus = 51888;
    public const uint TheEyeOfAcherus = 51852;
    public const uint EyeOfAcherusVisual = 51892;
    public const uint EyeOfAcherusFlightBoost = 51923;
    public const uint EyeOfAcherusFlight = 51890;
    public const uint RootSelf = 51860;
    // Quest 12641 Death Comes From On High
    public const uint SiphonOfAcherus = 51858;
    public const uint ForgeCredit = 51974;
    public const uint TownHallCredit = 51977;
    public const uint ScarletHoldCredit = 51980;
    public const uint ChapelCredit = 51982;
}

struct EventIds
{
    // Unworthy Initiate
    public const uint IcyTouch = 1;
    public const uint PlagueStrike = 2;
    public const uint BloodStrike = 3;
    public const uint BloodPlague = 4;

    // Eye Of Acherus
    public const uint LaunchToDestination = 1;
    public const uint UnRoot = 2;
    public const uint LaunchTowardsDestination = 3;
    public const uint GrantControl = 4;
}

struct TextIds
{
    // Val'kyr Battle-maiden
    public const uint SayRevive = 0;

    // Unworthy Initiate
    public const uint SayEventStart = 0;
    public const uint SayEventAttack = 1;

    // EyeOfAcherus
    public const uint SayLaunchToDestination = 0;
    public const uint SayEyeUnderControl = 1;

    // DeathKnightInitiate
    public const uint SayDuel = 0;

    // DarkRiderOfAcherus
    public const uint SayDarkRider = 0;

    // SalanarTheHorseman
    public const uint SaySalanar = 0;

    // ScarletMiner
    public const uint SayScarletMiner0 = 0;
    public const uint SayScarletMiner1 = 1;
}

struct MiscConst
{
    // Unworthy Initiate
    public static uint[] acherus_soul_prison = { 191577, 191580, 191581, 191582, 191583, 191584, 191585, 191586, 191587, 191588, 191589, 191590 };
    public static uint[] acherus_unworthy_initiate = { 29519, 29520, 29565, 29566, 29567 };

    // EyeOfAcherus
    public static Vector3[] EyeOfAcherusPath =
    {
        new(2361.21f, -5660.45f, 496.74396f),
        new(2341.5713f, -5672.797f, 538.3942f),
        new(1957.3962f, -5844.1055f, 273.86673f),
        new(1758.007f, -5876.7847f, 166.86671f)
    };

    // DeathKnightInitiate
    public static uint QuestDeathChallenge = 12733;
    public static uint FactionHostile = 2068;

    // SalanarTheHorseman
    public static uint GossipSalanarMenu = 9739;
    public static uint GossipSalanarOption = 0;
    public static uint QuestIntoRealmOfShadows = 12687;
}

[Script] // NPC 28534 Val'kyr Battle-maiden
// Spell 51915 Undying Resolve
// Spell 51918 Revive
// TODO: fix Whisper twice
class npc_valkyr_battle_maiden : PassiveAI
{
    uint FlyBackTimer;
    float x;
    float y;
    float z;
    uint phase;
    Player player;

    public npc_valkyr_battle_maiden(Creature creature) : base(creature)
    {
        FlyBackTimer = 500;
        phase = 0;
        x = 0.0f;
        y = 0.0f;
        z = 0.0f;
    }

    public override void Reset()
    {
        me.SetActive(true);
        me.SetFarVisible(true);
        me.SetVisible(false);
        me.SetCanFly(true);

        me.GetPosition(out x, out y, out z);
        z += 4.0f;
        x -= 3.5f;
        y -= 5.0f;
        me.GetMotionMaster().Clear();
        me.UpdatePosition(x, y, z, 0.0f);
    }

    public override void UpdateAI(uint diff)
    {
        if (FlyBackTimer <= diff)
        {
            if (me.IsSummon())
                player = me.ToTempSummon().GetSummoner().ToPlayer();

            if (player == null)
                phase = 3;

            switch (phase)
            {
                case 0:
                    me.SetWalk(false);
                    me.HandleEmoteCommand(Emote.StateFlygrabclosed);
                    FlyBackTimer = 500;
                    break;
                case 1:
                    if (player != null)
                    {
                        player.GetClosePoint(out x, out y, out z, me.GetCombatReach());
                        z += 2.5f;
                        x -= 2.0f;
                        y -= 1.5f;
                        me.GetMotionMaster().MovePoint(0, x, y, z);
                        me.SetTarget(player.GetGUID());
                        me.SetVisible(true);
                        FlyBackTimer = 4500;
                    }
                    break;
                case 2:
                    if (player != null && !player.IsResurrectRequested())
                    {
                        me.HandleEmoteCommand(Emote.OneshotCustomSpell01);
                        DoCast(player, SpellIds.Revive, new CastSpellExtraArgs(true));
                        Talk(TextIds.SayRevive, player);
                    }
                    FlyBackTimer = 5000;
                    break;
                case 3:
                    me.SetVisible(false);
                    FlyBackTimer = 3000;
                    break;
                case 4:
                    me.DisappearAndDie();
                    break;
            }

            ++phase;
        }
        else FlyBackTimer -= diff;
    }
}

[Script] // 51769 - Emblazon Runeblade
// Quest 12619 The Emblazoned Runeblade
// Spell 51769 Emblazon Runeblade
// NPC 28481 Runeforge (SE)
// Spell 51770 Emblazon Runeblade
// TODO: rotate sword inside Runeforge while channeling spell
class spell_q12619_emblazon_runeblade : AuraScript
{
    void HandleEffectPeriodic(AuraEffect aurEff)
    {
        PreventDefaultAction();
        Unit caster = GetCaster();

        // Triggers Spell 51770 Emblazon Runeblade
        caster?.CastSpell(caster, aurEff.GetSpellEffectInfo().TriggerSpell, new CastSpellExtraArgs(aurEff));
    }

    public override void Register()
    {
        OnEffectPeriodic.Add(new EffectPeriodicHandler(HandleEffectPeriodic, 0, AuraType.PeriodicTriggerSpell));
    }
}

[Script] // Spell 51770 Emblazon Runeblade
// Quest 12619 The Emblazoned Runeblade
// Spell 51771 Emblazon Runeblade
// Item 38607 Battle-Worn Sword
// NPC 28476 Runebladed Sword
class spell_q12619_emblazon_runeblade_effect : SpellScript
{
    void HandleScript(uint effIndex)
    {
        Unit caster = GetCaster();

        // Triggers Spell 51771 Emblazon Runeblade
        caster?.CastSpell(caster, (uint)GetEffectValue());
    }

    public override void Register()
    {
        OnEffectHit.Add(new EffectHandler(HandleScript, 0, SpellEffectName.ScriptEffect));
    }
}

[Script] // Spell 54586 Runeforging Credit
// Quest 12842 Runeforging: Preparation For Battle
// GameObject 190557 Runeforge
class spell_chapter1_runeforging_credit : SpellScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.RuneforgingCredit) && Global.ObjectMgr.GetQuestTemplate(QuestIds.RuneforgingPreparationForBattle) != null;
    }

    void HandleDummy(uint effIndex)
    {
        Unit caster = GetCaster();

        if (caster != null)
        {
            Player player = caster.ToPlayer();

            if (player != null)
            {
                if (player.GetQuestStatus(QuestIds.RuneforgingPreparationForBattle) == QuestStatus.Incomplete)
                    player.CastSpell(player, SpellIds.RuneforgingCredit, true);
            }
        }
    }

    public override void Register()
    {
        OnEffectHit.Add(new EffectHandler(HandleDummy, 1, SpellEffectName.Dummy));
    }
}

[Script] // NPC Unworthy Initiate
// Quest 12848 The Endless Hunger
// Spell 54612 Chained Peasant (Chest)
// Spell 54613 Chained Peasant (Breath)
class npc_unworthy_initiate : ScriptedAI
{
    ObjectGuid playerGUID;
    UnworthyInitiatePhase phase;
    uint wait_timer;
    float anchorX, anchorY;
    ObjectGuid anchorGUID;

    enum UnworthyInitiatePhase
    {
        Chained,
        ToEquip,
        Equipping,
        ToAttack,
        Attacking
    }

    public npc_unworthy_initiate(Creature creature) : base(creature)
    {
        Initialize();

        me.SetReactState(ReactStates.Passive);

        if (me.GetCurrentEquipmentId() == 0)
            me.SetCurrentEquipmentId((byte)me.GetOriginalEquipmentId());

        wait_timer = 0;
        anchorX = 0.0f;
        anchorY = 0.0f;
    }

    void Initialize()
    {
        anchorGUID.Clear();
        phase = UnworthyInitiatePhase.Chained;
    }

    public override void Reset()
    {
        _scheduler.CancelAll();

        _scheduler.Schedule(TimeSpan.FromSeconds(2), context =>
        {
            Initialize();
            _events.Reset();
            me.SetFaction(7);
            me.SetImmuneToPC(true);
            me.SetStandState(UnitStandStateType.Kneel);
            me.LoadEquipment(0);
            me.CastSpell(me ,SpellIds.SoulPrisonChainSelf);
        });
    }

    public override void JustEngagedWith(Unit who)
    {
        _events.ScheduleEvent(EventIds.IcyTouch, TimeSpan.FromSeconds(1), 1);
        _events.ScheduleEvent(EventIds.PlagueStrike, TimeSpan.FromSeconds(3), 1);
        _events.ScheduleEvent(EventIds.BloodStrike, TimeSpan.FromSeconds(2), 1);
        _events.ScheduleEvent(EventIds.BloodPlague, TimeSpan.FromSeconds(5), 1);
    }

    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        if (type != MovementGeneratorType.Point)
            return;

        if (id == 1)
        {
            wait_timer = 5000;
            me.LoadEquipment();
            me.CastSpell(me, SpellIds.DeathKnightInitiateVisual, true);

            Player starter = Global.ObjAccessor.GetPlayer(me, playerGUID);

            if (starter != null)
                Talk(TextIds.SayEventAttack, starter);

            phase = UnworthyInitiatePhase.ToAttack;
        }
    }

    public void EventStart(Creature anchor, Player target)
    {
        wait_timer = 5000;
        phase = UnworthyInitiatePhase.ToEquip;

        me.SetStandState(UnitStandStateType.Stand);
        me.RemoveAurasDueToSpell(SpellIds.SoulPrisonChainSelf);
        me.RemoveAurasDueToSpell(SpellIds.SoulPrisonChain);

        anchor.GetContactPoint(me, out anchorX, out anchorY, out _, 1.0f);

        playerGUID = target.GetGUID();
        Talk(TextIds.SayEventStart, target);
    }

    public override void UpdateAI(uint diff)
    {
        _scheduler.Update(diff);

        switch (phase)
        {
            case UnworthyInitiatePhase.Chained:
                if (anchorGUID.IsEmpty())
                {
                    Creature anchor = me.FindNearestCreature(CreatureIds.UnworthyInitiateAnchor, 30);

                    if (anchor != null)
                    {
                        anchor.CastSpell(me, SpellIds.SoulPrisonChain);
                        anchorGUID = me.GetGUID();
                        ((npc_unworthy_initiate_anchor)anchor.GetAI()).SetGUID(anchorGUID);
                    }
                    else Log.outError(LogFilter.Scripts, "npc_unworthy_initiateAI: unable to find anchor!");

                    foreach (var soul_prison in MiscConst.acherus_soul_prison)
                    {
                        GameObject prison = me.FindNearestGameObject(soul_prison, 30);

                        if (prison != null)
                            prison.ResetDoorOrButton();
                        else
                            Log.outError(LogFilter.Scripts, "npc_unworthy_initiateAI: unable to find prison!");
                    }
                }
                break;
            case UnworthyInitiatePhase.ToEquip:
                if (wait_timer != 0)
                {
                    if (wait_timer > diff)
                        wait_timer -= diff;
                    else
                    {
                        me.GetMotionMaster().MovePoint(1, anchorX, anchorY, me.GetPositionZ());
                        phase = UnworthyInitiatePhase.Equipping;
                        wait_timer = 0;
                    }
                }
                break;
            case UnworthyInitiatePhase.ToAttack:
                if (wait_timer != 0)
                {
                    if (wait_timer > diff)
                        wait_timer -= diff;
                    else
                    {
                        me.SetFaction(14);
                        me.SetImmuneToPC(false);
                        me.SetReactState(ReactStates.Aggressive);
                        phase = UnworthyInitiatePhase.Attacking;

                        Player target = Global.ObjAccessor.GetPlayer(me, playerGUID);

                        if (target != null)
                            AttackStart(target);

                        wait_timer = 0;
                    }
                }
                break;
            case UnworthyInitiatePhase.Attacking:
                if (!UpdateVictim())
                    return;

                _events.Update(diff);
                _events.ExecuteEvents(eventId =>
                {
                    switch (eventId)
                    {
                        case EventIds.IcyTouch:
                            DoCastVictim(SpellIds.IcyTouch, new CastSpellExtraArgs(true));
                            _events.DelayEvents(TimeSpan.FromSeconds(1), 1);
                            _events.ScheduleEvent(EventIds.IcyTouch, TimeSpan.FromSeconds(5), 1);
                            break;
                        case EventIds.PlagueStrike:
                            DoCastVictim(SpellIds.PlagueStrike, new CastSpellExtraArgs(true));
                            _events.DelayEvents(TimeSpan.FromSeconds(1), 1);
                            _events.ScheduleEvent(EventIds.PlagueStrike, TimeSpan.FromSeconds(5), 1);
                            break;
                        case EventIds.BloodStrike:
                            DoCastVictim(SpellIds.BloodStrike, new CastSpellExtraArgs(true));
                            _events.DelayEvents(TimeSpan.FromSeconds(1), 1);
                            _events.ScheduleEvent(EventIds.BloodStrike, TimeSpan.FromSeconds(5), 1);
                            break;
                        case EventIds.BloodPlague:
                            DoCastVictim(SpellIds.BloodPlague, new CastSpellExtraArgs(true));
                            _events.DelayEvents(TimeSpan.FromSeconds(1), 1);
                            _events.ScheduleEvent(EventIds.BloodPlague, TimeSpan.FromSeconds(5), 1);
                            break;
                    }
                });

                DoMeleeAttackIfReady();
                break;
        }
    }
}

[Script] // NPC 29521 Unworthy Initiate Anchor
// Quest 12848 The Endless Hunger
class npc_unworthy_initiate_anchor : PassiveAI
{
    ObjectGuid prisonerGUID;
    public npc_unworthy_initiate_anchor(Creature creature) : base(creature) { }

    public override void SetGUID(ObjectGuid guid, int id = 0)
    {
        if (prisonerGUID.IsEmpty())
            prisonerGUID = guid;
    }

    public override ObjectGuid GetGUID(int id = 0)
    {
        return prisonerGUID;
    }
}

[Script] // GameObject 191584 Acherus Soul Prison
// Quest 12848 The Endless Hunger
// Item 40732 Acherus Shackle Key
// Spell 54669 Unlocking Soul Prison
class go_acherus_soul_prison : GameObjectAI
{
    public go_acherus_soul_prison(GameObject go) : base(go) { }

    // TODO: only start after progress bar finished
    public override bool OnGossipHello(Player player)
    {
        Creature anchor = me.FindNearestCreature(CreatureIds.UnworthyInitiateAnchor, 15);

        if (anchor != null)
        {
            ObjectGuid prisonerGUID = ((npc_unworthy_initiate_anchor)anchor.GetAI()).GetGUID();

            if (!prisonerGUID.IsEmpty())
            {
                Creature prisoner = ObjectAccessor.GetCreature(player, prisonerGUID);

                if (prisoner != null && prisoner.IsAlive())
                    ((npc_unworthy_initiate)prisoner.GetAI()).EventStart(anchor, player);
                else
                {
                    foreach (var unworthy_initiate in MiscConst.acherus_unworthy_initiate)
                    {
                        prisoner = me.FindNearestCreature(unworthy_initiate, 30);

                        if (prisoner != null && prisoner.IsAlive())
                        {
                            ((npc_unworthy_initiate)prisoner.GetAI()).EventStart(anchor, player);
                            break;
                        }
                    }
                }
            }
        }

        return false;
    }
}

[Script] // Spell 51519 Death Knight Initiate Visual
// Quest 12848 The Endless Hunger
// NPC 29567 Unworthy Initiate
class spell_death_knight_initiate_visual : SpellScript
{
    void HandleScriptEffect(uint effIndex)
    {
        Creature target = GetHitCreature();

        if (target != null)
        {
            uint spellId;

            switch (target.GetDisplayId())
            {
                case 25369: spellId = 51552; break; // bloodelf female
                case 25373: spellId = 51551; break; // bloodelf male
                case 25363: spellId = 51542; break; // draenei female
                case 25357: spellId = 51541; break; // draenei male
                case 25361: spellId = 51537; break; // dwarf female
                case 25356: spellId = 51538; break; // dwarf male
                case 25372: spellId = 51550; break; // forsaken female
                case 25367: spellId = 51549; break; // forsaken male
                case 25362: spellId = 51540; break; // gnome female
                case 25359: spellId = 51539; break; // gnome male
                case 25355: spellId = 51534; break; // human female
                case 25354: spellId = 51520; break; // human male
                case 25360: spellId = 51536; break; // nightelf female
                case 25358: spellId = 51535; break; // nightelf male
                case 25368: spellId = 51544; break; // orc female
                case 25364: spellId = 51543; break; // orc male
                case 25371: spellId = 51548; break; // tauren female
                case 25366: spellId = 51547; break; // tauren male
                case 25370: spellId = 51545; break; // troll female
                case 25365: spellId = 51546; break; // troll male
                default: return;
            }

            target.CastSpell(target, spellId, GetSpell());
            target.LoadEquipment();
        }
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleScriptEffect, 0, SpellEffectName.ScriptEffect));
    }
}

[Script] // NPC 28511 Eye of Acherus
// Quest 12641 Death Comes From On High
// Spell 51852 The Eye of Acherus
// Spell 52694 Recall Eye of Acherus
// Spell 52006 Shroud
// Spell 51904 Summon Ghouls On Scarlet Crusade
// Spell 51859 Siphon of Acherus
// Phase 170
class npc_eye_of_acherus : ScriptedAI
{
    public npc_eye_of_acherus(Creature creature) : base(creature)
    {
        me.SetDisplayFromModel(0);
        me.SetReactState(ReactStates.Passive);

        CharmInfo info = me.GetCharmInfo();

        if (info != null)
            info.InitPossessCreateSpells();
    }

    public override void InitializeAI()
    {
        DoCastSelf(SpellIds.RootSelf);
        DoCastSelf(SpellIds.EyeOfAcherusVisual);
        DoCastSelf(SpellIds.EyeOfAcherusFlight);
        _events.ScheduleEvent(EventIds.LaunchToDestination, TimeSpan.FromSeconds(7));
    }

    public override void OnCharmed(bool apply)
    {
        if (!apply)
        {
            me.GetCharmerOrOwner().RemoveAurasDueToSpell(SpellIds.TheEyeOfAcherus);
            me.GetCharmerOrOwner().RemoveAurasDueToSpell(SpellIds.EyeOfAcherusFlightBoost);
        }
    }

    public override void UpdateAI(uint diff)
    {
        _events.Update(diff);

        _events.ExecuteEvents(eventId =>
        {
            switch (eventId)
            {
                case EventIds.LaunchToDestination:
                {
                    Unit owner = me.GetCharmerOrOwner();

                    if (owner != null)
                        Talk(TextIds.SayLaunchToDestination, owner);

                    _events.ScheduleEvent(EventIds.UnRoot, TimeSpan.FromMilliseconds(1200));
                }
                break;
                case EventIds.UnRoot:
                {
                    me.RemoveAurasDueToSpell(SpellIds.RootSelf);
                    DoCastSelf(SpellIds.EyeOfAcherusFlightBoost);
                    _events.ScheduleEvent(EventIds.LaunchTowardsDestination, TimeSpan.FromMilliseconds(1200));
                }
                break;
                case EventIds.LaunchTowardsDestination:
                {
                    var initializer = (MoveSplineInit init) =>
                    {
                        init.MovebyPath(MiscConst.EyeOfAcherusPath);
                        init.SetUncompressed();
                        init.SetSmooth();
                        init.SetFly();

                        Unit owner = me.GetCharmerOrOwner();

                        if (owner != null)
                            init.SetVelocity(owner.GetSpeed(UnitMoveType.Run));
                    };
                    me.GetMotionMaster().LaunchMoveSpline(initializer, 1, MovementGeneratorPriority.Normal, MovementGeneratorType.Point);
                }
                break;
                case EventIds.GrantControl:
                {
                    me.RemoveAurasDueToSpell(SpellIds.RootSelf);
                    me.RemoveAurasDueToSpell(SpellIds.EyeOfAcherusFlightBoost);
                    me.SetCanFly(true);

                    Unit owner = me.GetCharmerOrOwner();

                    if (owner != null)
                        Talk(TextIds.SayEyeUnderControl, owner);
                }
                break;
            }
        });
    }

    public override void MovementInform(MovementGeneratorType movementType, uint pointId)
    {
        if (movementType != MovementGeneratorType.Point)
            return;

        switch (pointId)
        {
            case 1:
                DoCastSelf(SpellIds.RootSelf);
                _events.ScheduleEvent(EventIds.GrantControl, TimeSpan.FromMilliseconds(2500));
                break;
        }
    }
}

[Script] // Spell 51858 Siphon of Acherus
// Quest 12641 Death Comes From On High
// Spell 51859 Siphon of Acherus
// 51974 Forge Credit
// 51977 Town Hall Credit
// 51980 Scarlet Hold Credit
// 51982 Chapel Credit
// Phase 170
class spell_q12641_death_comes_from_on_high : SpellScript
{
    public override bool Validate(SpellInfo spellInfo)
    {
        return ValidateSpellInfo(SpellIds.SiphonOfAcherus, SpellIds.ForgeCredit, SpellIds.TownHallCredit, SpellIds.ScarletHoldCredit, SpellIds.ChapelCredit);
    }

    void HandleDummy(uint effIndex)
    {
        uint spellId;

        switch (GetHitCreature().GetEntry())
        {
            case CreatureIds.NewAvalonForge:
                spellId = SpellIds.ForgeCredit;
                break;
            case CreatureIds.NewAvalonTownHall:
                spellId = SpellIds.TownHallCredit;
                break;
            case CreatureIds.ScarletHold:
                spellId = SpellIds.ScarletHoldCredit;
                break;
            case CreatureIds.ChapelOfTheCrimsonFlame:
                spellId = SpellIds.ChapelCredit;
                break;
            default:
                return;
        }

        GetCaster().CastSpell((SpellCastTargets)null, spellId, true);
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.Dummy));
    }
}

[Script] // Spell 52694 Recall Eye of Acherus
// Quest 12641 Death Comes From On High
class spell_q12641_recall_eye_of_acherus : SpellScript
{
    void HandleDummy(uint effIndex)
    {
        Unit caster = GetCaster();

        if (caster != null)
        {
            if (caster.IsPlayer())
            {
                Player player = caster.ToPlayer();

                if (player != null)
                {
                    player.StopCastingCharm();
                    player.StopCastingBindSight();
                    player.RemoveAura(SpellIds.TheEyeOfAcherus);
                }
            }
            else
            {
                Unit owner = caster.GetCharmerOrOwner();

                if (owner != null)
                {
                    Player player = owner.ToPlayer();

                    if (player != null)
                    {
                        player.StopCastingCharm();
                        player.StopCastingBindSight();
                        player.RemoveAura(SpellIds.TheEyeOfAcherus);
                    }
                }
            }
        }
    }

    public override void Register()
    {
        OnEffectHitTarget.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.ScriptEffect));
    }
}