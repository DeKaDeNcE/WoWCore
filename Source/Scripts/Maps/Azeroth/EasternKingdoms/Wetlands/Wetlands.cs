// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

using Framework.Constants;
using Game;
using Game.AI;
using Game.Entities;
using Game.Scripting;
using Game.Spells;

namespace Scripts.Maps.Azeroth.EasternKingdoms.Wetlands;

struct QuestIDs
{
    public const uint QUEST_MISSING_DIPLO_PT11 = 1249;
}

struct SpellIDs
{
    public const uint SPELL_STEALTH = 1785;
    public const uint SPELL_CALL_FRIENDS = 16457;
}

struct CreatureIDs
{
    public const uint NPC_SLIMS_FRIEND = 4971;
    public const uint NPC_TAPOKE_SLIM_JAHN = 4962;
}

[Script]
class npc_tapoke_slim_jahn : EscortAI
{
    public npc_tapoke_slim_jahn(Creature creature) : base(creature) { }

    bool IsFriendSummoned;

    public override void Reset()
    {
        if (!HasEscortState(EscortState.Escorting))
            IsFriendSummoned = false;
    }

    public override void WaypointReached(uint waypointId, uint pathId)
    {
        switch (waypointId)
        {
            case 2:
                if (me.HasStealthAura())
                    me.RemoveAurasByType(AuraType.ModStealth);
                me.SetSpeed(UnitMoveType.Run, 1.14f);
                me.SetFaction((uint)FactionTemplates.Enemy);
                break;
        }
    }

    public override void JustEnteredCombat(Unit who)
    {
        if (HasEscortState(EscortState.Escorting) && !IsFriendSummoned && GetPlayerForEscort())
        {
            for (byte i = 0; i < 3; ++i)
                DoCast(me, SpellIDs.SPELL_CALL_FRIENDS, new CastSpellExtraArgs(true));

            IsFriendSummoned = true;
        }
    }

    public override void JustSummoned(Creature summon)
    {
        Player player = GetPlayerForEscort();
        if (player != null)
            summon.GetAI().AttackStart(player);
    }

    public override void OwnerAttackedBy(Unit attacker)
    {
        if (me.GetVictim())
            return;

        if (me.IsFriendlyTo(attacker))
            return;

        AttackStart(attacker);
    }

    public override void DamageTaken(Unit attacker, ref uint damage, DamageEffectType damageType, SpellInfo spellInfo = null)
    {
        if (HealthBelowPct(20))
        {
            Player player = GetPlayerForEscort();
            if (player != null)
            {
                if (player.IsPlayer())
                    player.GroupEventHappens(QuestIDs.QUEST_MISSING_DIPLO_PT11, me);

                damage = 0;

                me.RestoreFaction();
                me.RemoveAllAuras();
                me.CombatStop(true);

                me.SetSpeed(UnitMoveType.Walk, 1.0f);
            }
        }
    }
}

[Script]
class npc_mikhail : EscortAI
{
    public npc_mikhail(Creature creature) : base(creature) { }



    public bool OnQuestAccept(Player player, Quest quest)
    {
        if (quest.Id == QuestIDs.QUEST_MISSING_DIPLO_PT11)
        {
            Creature pSlim = me.FindNearestCreature(CreatureIDs.NPC_TAPOKE_SLIM_JAHN, 25.0f);
            if (!pSlim)
                return false;

            if (!pSlim.HasStealthAura())
                pSlim.CastSpell(pSlim, SpellIDs.SPELL_STEALTH, true);

            npc_tapoke_slim_jahn pEscortAI = pSlim.GetAI<npc_tapoke_slim_jahn>();
            if (pEscortAI != null)
                Start(false, player.GetGUID(), quest);
        }
        return false;
    }
}