# Remove duplicate spawns for NPC 28481 Runeforge (SE)
DELETE FROM `creature` WHERE `guid` IN (129838, 130430, 129836, 130428, 129837, 130429) AND `id` = 28481;