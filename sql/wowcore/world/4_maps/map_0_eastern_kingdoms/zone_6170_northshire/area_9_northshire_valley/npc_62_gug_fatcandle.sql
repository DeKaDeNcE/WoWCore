###############################
# NPC Gug Fatcandle           #
# ENTRY 62                    #
# GUID  279748                #
#                             #
# Spells:                     #
# 12544 Frost Armor           #
# 20793 Fireball              #
###############################

SOURCE ../../../sql/wowcore/world/0_includes/smart_scripts.sql
SOURCE ../../../sql/wowcore/world/0_includes/spells.sql

SET @ENTRY             = 62;
SET @GUID              = 279748;

# Enable SmartAI
UPDATE `creature_template` SET `AIName` = 'SmartAI' WHERE `entry` = @ENTRY;

DELETE FROM `smart_scripts` WHERE `entryorguid` = @ENTRY;
INSERT INTO `smart_scripts`
(`entryorguid`,        `source_type`, `id`, `link`,            `event_type`,             `event_phase_mask`, `event_chance`,             `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `event_param5`, `event_param_string`,           `action_type`,    `action_param1`,              `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `action_param7`,        `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_param4`, `target_x`, `target_y`, `target_z`, `target_o`,                                              `comment`) VALUES
(       @ENTRY, @SMART_TYPE_CREATURE,    0,      0, @SMART_EVENT_UPDATE_OOC, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,          10000,          10000,              0,                   '', @SMART_ACTION_SELF_CAST, @SPELL_FROST_ARMOR, @SMART_CAST_AURA_NOT_PRESENT,               0,               0,               0,               0,               0,   @SMART_TARGET_SELF,               0,               0,               0,               0,          0,          0,          0,          0, 'Gug Fatcandle - OOC - Self Cast Aura \'Frost Armor\''),
(       @ENTRY, @SMART_TYPE_CREATURE,    1,      0,  @SMART_EVENT_UPDATE_IC, @SMART_EVENT_PHASE_MASK_ALWAYS,            100, @SMART_EVENT_FLAG_NO_FLAG,              0,              0,           3600,           4000,              0,                   '',      @SMART_ACTION_CAST,    @SPELL_FIREBALL,      @SMART_CAST_COMBAT_MOVE,               0,               0,               0,               0,               0, @SMART_TARGET_VICTIM,               0,               0,               0,               0,          0,          0,          0,          0,               'Gug Fatcandle - IC - Cast \'Fireball\'');