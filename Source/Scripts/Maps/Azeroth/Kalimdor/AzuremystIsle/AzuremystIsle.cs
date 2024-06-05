// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game;
using Game.AI;
using Game.DataStorage;
using Game.Entities;
using Game.Scripting;
using Game.Spells;
using System;
using System.Collections.Generic;

namespace Scripts.Maps.Azeroth.Kalimdor.AzuremystIsle
{
    struct SpellIDS
    {
        public const uint SPELL_IRRIDATION = 35046;
        public const uint SPELL_STUNNED = 28630;
        public const uint SPELL_FISHED_UP_RED_SNAPPER = 29867;
        public const uint SPELL_FISHED_UP_MURLOC = 29869;
    }

    struct Texts
    {
        public const uint SAY_THANK_FOR_HEAL = 0;
        public const uint SAY_ASK_FOR_HELP = 1;
    }

    struct Events
    {
        public const uint EVENT_CAN_ASK_FOR_HELP = 1;
        public const uint EVENT_THANK_PLAYER = 2;
        public const uint EVENT_RUN_AWAY = 3;
    }

    [Script]
    class npc_draenei_survivor : ScriptedAI
    {
        public npc_draenei_survivor(Creature creature) : base(creature) { }

        bool _canUpdateEvents;
        bool _tappedBySpell;
        bool _canAskForHelp;
        ObjectGuid _playerGUID;
        

        void Initialize()
        {
            _playerGUID.Clear();
            _canAskForHelp = true;
            _canUpdateEvents = false;
            _tappedBySpell = false;
        }

        public override void Reset()
        {
            Initialize();
            _events.Reset();

            DoCastSelf(SpellIDS.SPELL_IRRIDATION, new CastSpellExtraArgs(true));

            me.SetUnitFlag(UnitFlags.PlayerControlled);
            me.SetUnitFlag(UnitFlags.InCombat);
            me.SetHealth(me.CountPctFromMaxHealth(10));
            me.SetStandState(UnitStandStateType.Stand);
        }

        public override void JustEngagedWith(Unit who)
        {
            //do nothhing..
        }

        public override void MoveInLineOfSight(Unit who)
        {
            if (_canAskForHelp && who.GetTypeId() == TypeId.Player && me.IsFriendlyTo(who) && me.IsWithinDistInMap(who, 25.0f))
            {
                //Random switch between 4 texts
                Talk(Texts.SAY_ASK_FOR_HELP);

                _events.ScheduleEvent(Events.EVENT_CAN_ASK_FOR_HELP, TimeSpan.FromSeconds(16), TimeSpan.FromSeconds(20));
                _canAskForHelp = false;
                _canUpdateEvents = true;
            }
        }

        public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
        {
            if (spellInfo.SpellFamilyFlags[2].HasAnyFlag(0x80000000) && !_tappedBySpell)
            {
                _events.Reset();
                _tappedBySpell = true;
                _canAskForHelp = false;
                _canUpdateEvents = true;

                me.RemoveUnitFlag(UnitFlags.PlayerControlled);
                me.SetStandState(UnitStandStateType.Stand);

                _playerGUID = caster.GetGUID();
                Player player = caster.ToPlayer();
                if (player != null)
                    player.KilledMonsterCredit(me.GetEntry());

                me.SetFacingToObject(caster);
                DoCastSelf(SpellIDS.SPELL_STUNNED, new CastSpellExtraArgs(true));
                _events.ScheduleEvent(Events.EVENT_THANK_PLAYER, TimeSpan.FromSeconds(4));
            }
        }

        public override void UpdateAI(uint diff)
        {
            _events.Update(diff);

            _events.ExecuteEvents(eventId =>
            {
                switch (eventId)
                {
                    case Events.EVENT_CAN_ASK_FOR_HELP:
                        _canAskForHelp = true;
                        _canUpdateEvents = false;
                        break;
                    case Events.EVENT_THANK_PLAYER:
                        me.RemoveAurasDueToSpell(SpellIDS.SPELL_IRRIDATION);
                        Player player = Global.ObjAccessor.GetPlayer(me, _playerGUID);
                        if (player != null)
                            Talk(Texts.SAY_THANK_FOR_HEAL, player);
                        _events.ScheduleEvent(Events.EVENT_RUN_AWAY, TimeSpan.FromSeconds(10));
                        break;
                    case Events.EVENT_RUN_AWAY:
                        me.GetMotionMaster().Clear();
                        me.GetMotionMaster().MovePoint(0, me.GetPositionX() + (MathF.Cos(me.GetAbsoluteAngle(-4115.25f, -13754.75f)) * 28.0f), me.GetPositionY() + (MathF.Sin(me.GetAbsoluteAngle(-4115.25f, -13754.75f)) * 28.0f), me.GetPositionZ() + 1.0f);
                        me.DespawnOrUnsummon(TimeSpan.FromSeconds(4));
                        break;
                    default:
                        break;
                }
            });
        }
    }


    //29866 Casting fishing net
    [Script]
    class spell_azuremyst_isle_cast_fishing_net : SpellScript
    {
        public override bool Validate(SpellInfo spellInfo)
        {
            return ValidateSpellInfo(SpellIDS.SPELL_FISHED_UP_RED_SNAPPER, SpellIDS.SPELL_FISHED_UP_MURLOC);
        }

        void HandleDummy(uint effIndex)
        {
            GetCaster().CastSpell(GetCaster(), RandomHelper.randChance(66) ? SpellIDS.SPELL_FISHED_UP_RED_SNAPPER : SpellIDS.SPELL_FISHED_UP_MURLOC);
        }

        public override void Register()
        {
            OnEffectHit.Add(new EffectHandler(HandleDummy, 0, SpellEffectName.Dummy));
        }
    }
}