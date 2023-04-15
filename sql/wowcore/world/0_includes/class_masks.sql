# To convert Hex to Int in MySQL use: CONV(HEX(@VAR), 16, 10)

################
# Class Masks ##
################

SET @CLASS_MASK_ALL          =    0; #   0x00
SET @CLASS_MASK_WARRIOR      =    1; #   0x01
SET @CLASS_MASK_PALADIN      =    2; #   0x02
SET @CLASS_MASK_HUNTER       =    4; #   0x04
SET @CLASS_MASK_ROGUE        =    8; #   0x08
SET @CLASS_MASK_PRIEST       =   16; #   0x10
SET @CLASS_MASK_DEATH_KNIGHT =   32; #   0x20
SET @CLASS_MASK_SHAMAN       =   64; #   0x40
SET @CLASS_MASK_MAGE         =  128; #   0x80
SET @CLASS_MASK_WARLOCK      =  256; #  0x100
SET @CLASS_MASK_MONK         =  512; #  0x200
SET @CLASS_MASK_DRUID        = 1024; #  0x400
SET @CLASS_MASK_DEMON_HUNTER = 2048; #  0x800
SET @CLASS_MASK_EVOKER       = 4096; # 0x1000