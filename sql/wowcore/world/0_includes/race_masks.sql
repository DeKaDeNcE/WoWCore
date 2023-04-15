# To convert Hex to Int in MySQL use: CONV(HEX(@VAR), 16, 10)

##############
# Race Masks #
##############

SET @RACE_MASK_NONE                =          0; #       0x00
SET @RACE_MASK_HUMAN               =          1; #       0x01
SET @RACE_MASK_ORC                 =          2; #       0x02
SET @RACE_MASK_DWARF               =          4; #       0x04
SET @RACE_MASK_NIGHT_ELF           =          8; #       0x08
SET @RACE_MASK_UNDEAD              =         16; #       0x10
SET @RACE_MASK_TAUREN              =         32; #       0x20
SET @RACE_MASK_GNOME               =         64; #       0x40
SET @RACE_MASK_TROLL               =        128; #       0x80
SET @RACE_MASK_GOBLIN              =        256; #      0x100
SET @RACE_MASK_BLOOD_ELF           =        512; #      0x200
SET @RACE_MASK_DRAENEI             =       1024; #      0x400
SET @RACE_MASK_DARK_IRON_DWARF     =       2048; #      0x800
SET @RACE_MASK_VULPERA             =       4096; #     0x1000
SET @RACE_MASK_MAGHAR_ORC          =       8192; #     0x2000
SET @RACE_MASK_MECHAGNOME          =      16384; #     0x4000
SET @RACE_MASK_DRACTHYR            =      98304; #    0x18000
SET @RACE_MASK_WORGEN              =    2097152; #   0x200000
SET @RACE_MASK_PANDAREN            =    8388608; #   0x800000
SET @RACE_MASK_NIGHTBORNE          =   67108864; #  0x4000000
SET @RACE_MASK_HIGHMOUNTAIN_TAUREN =  134217728; #  0x8000000
SET @RACE_MASK_VOID_ELF            =  268435456; # 0x10000000
SET @RACE_MASK_LIGHTFORGED_DRAENEI =  536870912; # 0x20000000
SET @RACE_MASK_ZANDALARI_TROLL     = 1073741824; # 0x40000000
SET @RACE_MASK_KUL_TIRAN           = 2147483648; # 0x80000000
SET @RACE_MASK_ALL                 = 2147483649; # 0x80000001