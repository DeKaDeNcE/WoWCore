DELETE FROM `spell_proc` WHERE `SpellId` IN (57954,105788,113043,115946,144585,144613,144625,144864,144899,144962,144998,145179,145251,145380);
INSERT INTO `spell_proc` (`SpellId`,`SchoolMask`,`SpellFamilyName`,`SpellFamilyMask0`,`SpellFamilyMask1`,`SpellFamilyMask2`,`SpellFamilyMask3`,`ProcFlags`,`ProcFlags2`,`SpellTypeMask`,`SpellPhaseMask`,`HitMask`,`AttributesMask`,`DisableEffectsMask`,`ProcsPerMinute`,`Chance`,`Cooldown`,`Charges`) VALUES
(57954,0x00,10,0x00800000,0x00000000,0x00000000,0x00000000,0x0,0x0,0x1,0x2,0x2,0x0,0x0,0,0,0,0), -- Glyph of Fire From the Heavens
(105788,0x00,3,0x20400021,0x00001000,0x00000000,0x00000000,0x0,0x0,0x1,0x2,0x403,0x0,0x0,0,0,0,0), -- Item - Mage T13 2P Bonus (Haste)
(113043,0x00,7,0x00000000,0x00001000,0x00000000,0x00000000,0x0,0x0,0x2,0x2,0x3,0x0,0x0,0,0,0,0), -- Omen of Clarity
(115946,0x00,4,0x00000000,0x04000000,0x00000000,0x00000000,0x0,0x0,0x4,0x2,0x0,0x0,0x0,0,0,0,0), -- Glyph of Burning Anger (Fury, Protection)
(144585,0x00,0,0x00000000,0x00000000,0x00000000,0x00000000,0x0,0x0,0x4,0x2,0x1000,0x0,0x0,0,0,0,0), -- Ancestral Fury
(144613,0x00,10,0x00000000,0x00000100,0x00000000,0x00000000,0x0,0x0,0x4,0x2,0x0,0x0,0x0,0,0,0,0), -- Item - Paladin T16 Holy 4P Bonus
(144625,0x00,10,0x00000000,0x00000000,0x00000000,0x00100000,0x0,0x0,0x4,0x2,0x0,0x0,0x0,0,0,0,0), -- Item - Paladin T16 Holy 2P Bonus
(144864,0x00,7,0x00000000,0x00000000,0x00000000,0x00040000,0x0,0x0,0x4,0x2,0x0,0x0,0x0,0,0,0,0), -- Item - Druid T16 Feral 2P Bonus
(144899,0x00,15,0x00000000,0x00000000,0x00020000,0x00000010,0x0,0x0,0x4,0x2,0x0,0x0,0x0,0,0,0,0), -- Item - Death Knight T16 DPS 2P Bonus
(144962,0x00,11,0x00000000,0x00000000,0x00000400,0x00000000,0x0,0x0,0x4,0x4,0x0,0x0,0x0,0,0,0,0), -- Item - Shaman T16 Enhancement 2P Bonus
(144998,0x00,0,0x00000000,0x00000000,0x00000000,0x00000000,0x0,0x0,0x0,0x0,0x0,0x0,0x1,0,0,0,0), -- Item - Shaman T16 Elemental 2P Bonus
(145179,0x00,0,0x00000000,0x00000000,0x00000000,0x00000000,0x0,0x0,0x0,0x0,0x0,0x0,0x1,0,0,0,0), -- Item - Priest T16 Shadow 4P Bonus
(145251,0x00,3,0x00000800,0x00000000,0x00000000,0x00000000,0x0,0x0,0x4,0x1,0x403,0x0,0x0,0,0,0,0), -- Item - Mage T16 2P Bonus
(145380,0x00,11,0x00000000,0x00000000,0x20000000,0x00000000,0x0,0x0,0x4,0x2,0x0,0x0,0x0,0,0,0,0); -- Item - Shaman T16 Restoration 4P Bonus
