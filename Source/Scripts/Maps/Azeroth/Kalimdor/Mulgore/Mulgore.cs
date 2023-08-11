// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game.Spells;
using System;

namespace Scripts.Maps.Azeroth.Kalimdor.Mulgore;

struct SpellIds
{
    public const uint SootheEarthSpirit = 69453;
    public const uint RockBarrage = 81305;
    public const uint LunchFirKyle = 42222;
    public const uint EjectAllPassengers = 50630;
    public const uint SpiritFrom = 69324;
}

struct CreatureIds
{
    public const uint EarthSpiritCreditBunny = 36872;
    public const uint KyleTheFrenzied = 23616;
    public const uint KyleTheFriendly = 23622;
}

// 36845
[Script]
class npc_agitated_earth_spirit : ScriptedAI
{
    public npc_agitated_earth_spirit(Creature creature) : base(creature) { }

    ObjectGuid _playerGUID;

    public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
    {
        if (spellInfo.Id == SpellIds.SootheEarthSpirit)
        {
            Position pos = me.GetRandomNearPosition(10);
            caster.GetNearPoint(null, out float x, out float y, out float z, 2.0f, caster.GetOrientation()); ;
            me.GetMotionMaster().MovePoint(1, pos);
            _playerGUID = caster.GetGUID();
        }
    }


    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        if (type == MovementGeneratorType.Point && id == 1)
        {
            switch (RandomHelper.URand(0, 1))
            {
                case 0:
                    me.SetFaction(35);
                    Player player = Global.ObjAccessor.GetPlayer(me, _playerGUID);
                    if (player != null)
                        player.KilledMonsterCredit(CreatureIds.EarthSpiritCreditBunny);


                    _scheduler.Schedule(TimeSpan.FromSeconds(1), task =>
                    {
                        me.DisappearAndDie();
                    });
                    break;
                case 1:
                    me.SetFaction(14);
                    Player victim = Global.ObjAccessor.GetPlayer(me, _playerGUID);
                    if (victim != null)
                        me.Attack(victim, true);
                    break;

            }
        }
    }

    public override void JustEnteredCombat(Unit who)
    {
        _scheduler.Schedule(TimeSpan.FromSeconds(5), task =>
        {
            Unit target = SelectTarget(SelectTargetMethod.Random);

            if (target != null)
            {
                me.CastSpell(target, SpellIds.RockBarrage, false);
                task.Repeat(TimeSpan.FromSeconds(18), TimeSpan.FromSeconds(21));
            }
        });
    }
}

// 23616
[Script]
class npc_kyle_the_frenzied : ScriptedAI
{
    public npc_kyle_the_frenzied(Creature creature) : base(creature) { }

    ObjectGuid _playerGUID;

    public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
    {
        if (spellInfo.Id == SpellIds.LunchFirKyle)
        {
            Position pos = me.GetRandomNearPosition(10);
            caster.GetNearPoint(null, out float x, out float y, out float z, 0.5f, caster.GetOrientation()); ;
            me.GetMotionMaster().MovePoint(1, pos);
            _playerGUID = caster.GetGUID();
        }
    }

    public override void MovementInform(MovementGeneratorType type, uint id)
    {
        if (type == MovementGeneratorType.Point && id == 1)
        {
            _scheduler.Schedule(TimeSpan.FromSeconds(15), task =>
            {
                //me.GetMotionMaster().Top().Resume(); //was..
                me.SetWalk(true);
            });

            Talk(0);

            _scheduler.Schedule(TimeSpan.FromSeconds(4), task =>
            {
                Talk(1);
            });

            _scheduler.Schedule(TimeSpan.FromSeconds(9), task =>
            {
                Talk(2);
                me.UpdateEntry(CreatureIds.KyleTheFriendly);
                me.HandleEmoteCommand(Framework.Constants.Emote.StateDance);
                Player player = Global.ObjAccessor.GetPlayer(me, _playerGUID);
                if (player != null)
                    player.KilledMonsterCredit(CreatureIds.KyleTheFrenzied);
            });
            _scheduler.Schedule(TimeSpan.FromSeconds(15), task =>
            {
                me.UpdateEntry(CreatureIds.KyleTheFrenzied);
                me.HandleEmoteCommand(Framework.Constants.Emote.StateNone);
            });
        }
    }
}