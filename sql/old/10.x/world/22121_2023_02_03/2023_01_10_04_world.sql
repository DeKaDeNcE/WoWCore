SET @CGUID := 1252102;

DELETE FROM `creature` WHERE `guid` BETWEEN @CGUID+0 AND @CGUID+58;
INSERT INTO `creature` (`guid`, `id`, `map`, `zoneId`, `areaId`, `spawnDifficulties`, `PhaseId`, `PhaseGroup`, `modelid`, `equipment_id`, `position_x`, `position_y`, `position_z`, `orientation`, `spawntimesecs`, `wander_distance`, `currentwaypoint`, `curhealth`, `curmana`, `MovementType`, `npcflag`, `unit_flags`, `dynamicflags`, `VerifiedBuild`) VALUES
(@CGUID+0, 193420, 2444, 13646, 13646, '0', 0, 0, 0, 0, -3992.65478515625, 3934.012939453125, 54.18722152709960937, 3.069311857223510742, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Desiccated Deer (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+1, 193422, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4047.505126953125, 3964.41064453125, 7.48000955581665039, 1.762005329132080078, 120, 10, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Starving Bullfrog (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+2, 193422, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4059.233154296875, 3966.88427734375, 7.824388980865478515, 4.522541522979736328, 120, 10, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Starving Bullfrog (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+3, 193430, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4074.775146484375, 3905.54931640625, 95.81743621826171875, 0.108882822096347808, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Prowling Vulture (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying, 391254 - Hearty)
(@CGUID+4, 193422, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4051.557373046875, 3957.600830078125, 8.641984939575195312, 1.113409042358398437, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Starving Bullfrog (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+5, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4058.271240234375, 3991.028076171875, 6.536451339721679687, 5.454276561737060546, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+6, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4072.2744140625, 3998.241455078125, 6.581168651580810546, 2.295199155807495117, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+7, 193420, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4007.579345703125, 3899.174072265625, 62.01842498779296875, 6.140183925628662109, 120, 8, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Desiccated Deer (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+8, 193430, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4099.5810546875, 3990.951416015625, 23.30870628356933593, 4.2374725341796875, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Prowling Vulture (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+9, 193430, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4029.580810546875, 3978.274169921875, 23.13451385498046875, 2.780228853225708007, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Prowling Vulture (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+10, 193422, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4049.691162109375, 3963.354248046875, 7.638170242309570312, 2.223871231079101562, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Starving Bullfrog (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+11, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4073.572265625, 4005.052001953125, 5.172490596771240234, 5.590213775634765625, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+12, 193430, 2444, 13646, 13646, '0', 0, 0, 0, 0, -3993.262451171875, 3916.241455078125, 92.90773773193359375, 0.879073977470397949, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Prowling Vulture (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+13, 3300, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4060.529541015625, 3891.59228515625, 55.70665740966796875, 1.05819404125213623, 120, 10, 0, 1, 0, 1, 0, 0, 0, 47213), -- Adder (Area: The Azure Span - Difficulty: 0)
(@CGUID+14, 193428, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4036.578125, 3915.1318359375, 54.47333908081054687, 6.164649486541748046, 120, 10, 0, 25194, 0, 1, 0, 0, 0, 47213), -- Feral Hyena (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+15, 193425, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4090.5400390625, 3903.424560546875, 49.78695297241210937, 0, 120, 0, 0, 8, 0, 0, 0, 0, 0, 47213), -- Cricket (Area: The Azure Span - Difficulty: 0)
(@CGUID+16, 193420, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4082.835205078125, 3891.080078125, 54.14113616943359375, 0.713500142097473144, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Desiccated Deer (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+17, 193422, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4076.081298828125, 3976.283935546875, 8.256417274475097656, 5.586947441101074218, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Starving Bullfrog (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+18, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4003.82763671875, 3973.758056640625, 6.925649642944335937, 0.296429932117462158, 120, 3, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+19, 193430, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4035.1494140625, 3917.4833984375, 65.74069976806640625, 1.020711898803710937, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Prowling Vulture (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+20, 193427, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4086.182373046875, 3934.548583984375, 39.93729019165039062, 4.733905315399169921, 120, 6, 0, 67184, 0, 1, 0, 0, 0, 47213), -- Tired Goat (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+21, 193422, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4019.229248046875, 3960.302001953125, 8.368265151977539062, 1.408004164695739746, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Starving Bullfrog (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+22, 193422, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4073.237548828125, 3957.878662109375, 10.33957767486572265, 2.889983654022216796, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Starving Bullfrog (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying, 391254 - Hearty)
(@CGUID+23, 193425, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4040.986083984375, 3871.5712890625, 66.25270843505859375, 0, 120, 0, 0, 8, 0, 0, 0, 0, 0, 47213), -- Cricket (Area: The Azure Span - Difficulty: 0)
(@CGUID+24, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -3993.2119140625, 4010.025146484375, 5.85227203369140625, 0, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+25, 193422, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4119.73388671875, 4002.0146484375, 17.2222900390625, 0.925929129123687744, 120, 10, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Starving Bullfrog (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying) (possible waypoints or random movement)
(@CGUID+26, 189104, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4083.87548828125, 3989.029541015625, 7.964840888977050781, 3.564179658889770507, 120, 0, 0, 5, 0, 0, 0, 0, 0, 47213), -- Swoglet (Area: The Azure Span - Difficulty: 0)
(@CGUID+27, 193420, 2444, 13646, 13646, '0', 0, 0, 0, 0, -3973.513427734375, 3943.9453125, 58.53246688842773437, 0.119506552815437316, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Desiccated Deer (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+28, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4098.98095703125, 4024.223876953125, 4.660459518432617187, 0, 120, 3, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+29, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4118.50732421875, 4044.02001953125, 3.888620376586914062, 4.276706218719482421, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+30, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4123.71728515625, 4063.4931640625, 5.195278167724609375, 0, 120, 0, 0, 41990, 0, 0, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+31, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4148.21630859375, 4087.65576171875, 5.547315597534179687, 5.580812931060791015, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+32, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4177.2998046875, 4109.30908203125, 5.731780052185058593, 5.40484476089477539, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+33, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4163.5546875, 4107.37646484375, 5.904422760009765625, 5.705774784088134765, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+34, 193431, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4195.4482421875, 4112.31982421875, 6.795674800872802734, 2.41677403450012207, 120, 4, 0, 41990, 0, 1, 0, 0, 0, 47213), -- Hungry Nibbler (Area: The Azure Span - Difficulty: 0) (Auras: 383173 - Decaying)
(@CGUID+35, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4335.07861328125, 4164.87744140625, -5.5806283950805664, 4.623964786529541015, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+36, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4298.93994140625, 4144.77099609375, -2.66345906257629394, 2.673876285552978515, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+37, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4374.47802734375, 4042.186767578125, -5.12008428573608398, 4.436548233032226562, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+38, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4309.77294921875, 4131.78564453125, -3.59631562232971191, 3.08854985237121582, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+39, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4341.98681640625, 4132.080078125, -8.95381736755371093, 1.482818603515625, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+40, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4362.0693359375, 4147.90869140625, -0.92139774560928344, 0, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+41, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4415.98095703125, 3978.945556640625, -5.76335573196411132, 4.800996780395507812, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+42, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4252.8818359375, 4173.20849609375, -3.50305008888244628, 3.621284008026123046, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+43, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4284.7294921875, 4175.8369140625, -12.0217924118041992, 2.763803958892822265, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+44, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4258.283203125, 4175.35986328125, -2.82229781150817871, 4.720258712768554687, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+45, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4337.3388671875, 4189.61572265625, -4.88733100891113281, 2.690948724746704101, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+46, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4244.85205078125, 4168.0087890625, -1.81792449951171875, 6.12347269058227539, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+47, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4469.40869140625, 3992.227783203125, -3.79094147682189941, 6.126138210296630859, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+48, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4498.9013671875, 4217.85546875, -1.93943262100219726, 4.29195261001586914, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+49, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4416.22216796875, 3961.996337890625, -5.33586883544921875, 1.436498641967773437, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+50, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4221.7734375, 4192.2216796875, -5.18214082717895507, 1.618283033370971679, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+51, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4479.20166015625, 3954.068603515625, -5.02875947952270507, 0.811742007732391357, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+52, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4460.7177734375, 4258.74169921875, -2.18315052986145019, 0.8019486665725708, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+53, 187828, 2444, 13646, 13837, '0', 0, 0, 0, 0, -4439.5986328125, 4291.5849609375, -4.80098533630371093, 0.555408060550689697, 120, 6, 0, 83980, 0, 1, 0, 0, 0, 47213), -- Frostfin Minnow (Area: Iskaara - Difficulty: 0)
(@CGUID+54, 192059, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4074.645263671875, 4246.1923828125, 40.74866867065429687, 2.796933174133300781, 120, 4, 0, 5, 0, 1, 0, 0, 0, 47213), -- Palamanther (Area: The Azure Span - Difficulty: 0)
(@CGUID+55, 197541, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4066.197998046875, 4184.125, 46.88548660278320312, 0, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Coastal Grizzly (Area: The Azure Span - Difficulty: 0) (Auras: 377846 - Cosmetic - Sleep Zzz Breakable (With Aggro Change)
(@CGUID+56, 197555, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4147.08349609375, 4229.84619140625, 42.88596725463867187, 0, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Arboreal Grazer (Area: The Azure Span - Difficulty: 0)
(@CGUID+57, 197555, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4148.40087890625, 4236.45166015625, 42.97222137451171875, 0, 120, 0, 0, 83980, 0, 0, 0, 0, 0, 47213), -- Arboreal Grazer (Area: The Azure Span - Difficulty: 0)
(@CGUID+58, 189104, 2444, 13646, 13646, '0', 0, 0, 0, 0, -4171.1962890625, 4122.14453125, 9.207894325256347656, 4.683420658111572265, 120, 4, 0, 5, 0, 1, 0, 0, 0, 47213); -- Swoglet (Area: The Azure Span - Difficulty: 0)

DELETE FROM `creature_addon` WHERE `guid` BETWEEN @CGUID+0 AND @CGUID+58;
INSERT INTO `creature_addon` (`guid`, `path_id`, `mount`, `bytes1`, `bytes2`, `emote`, `aiAnimKit`, `movementAnimKit`, `meleeAnimKit`, `visibilityDistanceType`, `auras`) VALUES
(@CGUID+55, 0, 0, 0, 1, 0, 0, 0, 0, 0, '377846'); -- Coastal Grizzly - 377846 - Cosmetic - Sleep Zzz Breakable (With Aggro Change

UPDATE `creature_template` SET `minlevel`=68, `maxlevel`=68, `faction`=190, `BaseAttackTime`=2000, `unit_flags`=32768, `unit_flags2`=2048, `HoverHeight`=1.20000004768371582 WHERE `entry`=187828; -- Frostfin Minnow
UPDATE `creature_template` SET `faction`=188, `speed_walk`=0.400000005960464477, `speed_run`=0.285714298486709594, `BaseAttackTime`=2000, `unit_flags`=33280, `unit_flags2`=2048 WHERE `entry`=192059; -- Palamanther
UPDATE `creature_template` SET `faction`=190, `BaseAttackTime`=2000, `unit_flags`=32768, `unit_flags2`=2048 WHERE `entry`=197546; -- Coastal Grizzly Cub
UPDATE `creature_template` SET `minlevel`=68, `maxlevel`=68, `faction`=190, `BaseAttackTime`=2000, `unit_flags`=32768, `unit_flags2`=2048 WHERE `entry`=197555; -- Arboreal Grazer
UPDATE `creature_template` SET `minlevel`=68, `maxlevel`=68, `faction`=16, `BaseAttackTime`=2000, `unit_flags`=32768, `unit_flags2`=2048 WHERE `entry`=197541; -- Coastal Grizzly

-- Creature Movement
DELETE FROM `creature_template_movement` WHERE `CreatureId` = 187828;
INSERT INTO `creature_template_movement` (`CreatureId`, `Ground`, `Swim`, `Flight`, `Rooted`, `Chase`, `Random`, `InteractionPauseTimer`) VALUES
(187828, 0, 1, 0, 0, 0, 0, NULL);

DELETE FROM `creature_template_scaling` WHERE (`Entry`=135238 AND `DifficultyID`=0);
INSERT INTO `creature_template_scaling` (`Entry`, `DifficultyID`, `LevelScalingDeltaMin`, `LevelScalingDeltaMax`, `ContentTuningID`, `VerifiedBuild`) VALUES
(135238, 0, 0, 0, 483, 47213);
