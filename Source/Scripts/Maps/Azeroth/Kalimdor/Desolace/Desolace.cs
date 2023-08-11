// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

namespace Scripts.Maps.Azeroth.Kalimdor.Desolace;

using Framework.Constants;
using Game.AI;
using Game.Entities;
using Game.Maps;
using Game.Scripting;
using Game.Spells;
using System.Collections.Generic;
using System;
using Game;

struct QuestIDs
{
    public const uint QUEST_KODO = 5561;
}

struct SpellIDs
{
    public const uint SPELL_KODO_KOMBO_ITEM = 18153;
    public const uint SPELL_KODO_KOMBO_PLAYER_BUFF = 18172;
    public const uint SPELL_KODO_KOMBO_DESPAWN_BUFF = 18377;
    public const uint SPELL_KODO_KOMBO_GOSSIP = 18362;
}

struct CreatureIDs
{
    public const uint NPC_SMEED = 11596;
    public const uint NPC_AGED_KODO = 4700;
    public const uint NPC_DYING_KODO = 4701;
    public const uint NPC_ANCIENT_KODO = 4702;
    public const uint NPC_TAMED_KODO = 11627;
}

struct Texts
{
    public const uint SAY_SMEED_HOME = 0;
}


[Script]
class npc_aged_dying_ancient_kodo : ScriptedAI
{
    public npc_aged_dying_ancient_kodo(Creature creature) : base(creature) { }

    public override void MoveInLineOfSight(Unit who)
    {
        if (who.GetEntry() == CreatureIDs.NPC_SMEED && me.IsWithinDistInMap(who, 10.0f) && !me.HasAura(SpellIDs.SPELL_KODO_KOMBO_GOSSIP))
        {
            me.GetMotionMaster().Clear();
            DoCast(me, SpellIDs.SPELL_KODO_KOMBO_GOSSIP, new CastSpellExtraArgs(true));
            Creature smeed = who.ToCreature();
            if (smeed != null)
                smeed.GetAI().Talk(Texts.SAY_SMEED_HOME);
        }
    }

    public override void SpellHit(WorldObject caster, SpellInfo spellInfo)
    {
        Unit unitCaster = caster.ToUnit();
        if (!unitCaster)
            return;

        if (spellInfo.Id == SpellIDs.SPELL_KODO_KOMBO_ITEM)
        {
            if (!(unitCaster.HasAura(SpellIDs.SPELL_KODO_KOMBO_PLAYER_BUFF) || me.HasAura(SpellIDs.SPELL_KODO_KOMBO_DESPAWN_BUFF))
                && (me.GetEntry() == CreatureIDs.NPC_AGED_KODO || me.GetEntry() == CreatureIDs.NPC_DYING_KODO || me.GetEntry() == CreatureIDs.NPC_ANCIENT_KODO))
            {
                unitCaster.CastSpell(unitCaster, SpellIDs.SPELL_KODO_KOMBO_PLAYER_BUFF, true);
                DoCast(me, SpellIDs.SPELL_KODO_KOMBO_DESPAWN_BUFF, new CastSpellExtraArgs(true));

                me.UpdateEntry(CreatureIDs.NPC_TAMED_KODO);
                me.CombatStop();
                me.SetFaction((uint)FactionTemplates.Friendly);
                me.SetSpeedRate(UnitMoveType.Run, 0.6f);

                EngagementOver();

                me.GetMotionMaster().MoveFollow(unitCaster, 1.0f, me.GetFollowAngle());
                me.SetActive(true);
                me.RemoveNpcFlag(NPCFlags.Gossip);
            }
        }
        else if (spellInfo.Id == SpellIDs.SPELL_KODO_KOMBO_GOSSIP)
        {
            me.SetNpcFlag(NPCFlags.Gossip);
            me.SetHomePosition(me.GetPositionX(), me.GetPositionY(), me.GetPositionZ(), me.GetOrientation());
            me.GetMotionMaster().Clear();
            me.GetMotionMaster().MoveIdle();
            me.SetActive(false);
            me.DespawnOrUnsummon(TimeSpan.FromSeconds(60));
        }
    }

    public override bool OnGossipHello(Player player)
    {
        if (player.HasAura(SpellIDs.SPELL_KODO_KOMBO_PLAYER_BUFF) && me.HasAura(SpellIDs.SPELL_KODO_KOMBO_DESPAWN_BUFF))
        {
            player.TalkedToCreature(me.GetEntry(), ObjectGuid.Empty);
            player.RemoveAurasDueToSpell(SpellIDs.SPELL_KODO_KOMBO_PLAYER_BUFF);
        }

        player.SendGossipMenu(player.GetGossipTextId(me), me.GetGUID());
        return true;
    }
}