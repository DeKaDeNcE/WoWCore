# To convert Hex to Int use: CONV(HEX(@VAR), 16, 10)

####################
# Action Bar Types #
####################

SET @ACTION_BAR_TYPE_SPELL     =   0; # 0x00
SET @ACTION_BAR_TYPE_C         =   1; # 0x01
SET @ACTION_BAR_TYPE_EQSET     =  32; # 0x20
SET @ACTION_BAR_TYPE_DROPDOWN  =  48; # 0x30
SET @ACTION_BAR_TYPE_MACRO     =  64; # 0x40
SET @ACTION_BAR_TYPE_CMACRO    = @ACTION_BAR_TYPE_C + @ACTION_BAR_TYPE_MACRO; # 65
SET @ACTION_BAR_TYPE_COMPANION =  80; # 0x50
SET @ACTION_BAR_TYPE_MOUNT     =  96; # 0x60
SET @ACTION_BAR_TYPE_ITEM      = 128; # 0x80

####################
# Action Bar Slots #
####################

SET @ACTION_BAR_1_SLOT_1       =  0;
SET @ACTION_BAR_1_SLOT_2       =  1;
SET @ACTION_BAR_1_SLOT_3       =  2;
SET @ACTION_BAR_1_SLOT_4       =  3;
SET @ACTION_BAR_1_SLOT_5       =  4;
SET @ACTION_BAR_1_SLOT_6       =  5;
SET @ACTION_BAR_1_SLOT_7       =  6;
SET @ACTION_BAR_1_SLOT_8       =  7;
SET @ACTION_BAR_1_SLOT_9       =  8;
SET @ACTION_BAR_1_SLOT_0       =  9;
SET @ACTION_BAR_1_SLOT_MINUS   = 10;
SET @ACTION_BAR_1_SLOT_EQUALS  = 11;