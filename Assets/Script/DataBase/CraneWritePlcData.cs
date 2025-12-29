using System;
using System.Runtime.InteropServices;

[Serializable]
[StructLayout(LayoutKind.Explicit)]
public struct CraneWritePlcData
{
    [FieldOffset(0)]
    public byte RM_STX;
    [FieldOffset(2)]
    public ushort RM_Total_Length;
    [FieldOffset(4)]
    public short Data_Heart_Beat;
    [FieldOffset(6)]
    public short Data_Errorcode;
    [FieldOffset(8)]
    public short Data_SpareInt_1;
    [FieldOffset(10)]
    public byte System_CMD_Raw_10;
    // Bit 0: E_Stop
    public bool System_CMD_E_Stop => (System_CMD_Raw_10 & (1 << 0)) != 0;
    // Bit 1: Control_On
    public bool System_CMD_Control_On => (System_CMD_Raw_10 & (1 << 1)) != 0;
    // Bit 2: Control_Off
    public bool System_CMD_Control_Off => (System_CMD_Raw_10 & (1 << 2)) != 0;
    // Bit 3: Light_On
    public bool System_CMD_Light_On => (System_CMD_Raw_10 & (1 << 3)) != 0;
    // Bit 4: Light_Off
    public bool System_CMD_Light_Off => (System_CMD_Raw_10 & (1 << 4)) != 0;
    // Bit 5: Automatic_Mode
    public bool System_CMD_Automatic_Mode => (System_CMD_Raw_10 & (1 << 5)) != 0;
    // Bit 6: Transition_Mode
    public bool System_CMD_Transition_Mode => (System_CMD_Raw_10 & (1 << 6)) != 0;
    // Bit 7: Spare_bit4
    public bool System_CMD_Spare_bit4 => (System_CMD_Raw_10 & (1 << 7)) != 0;
    [FieldOffset(12)]
    public short System_CMD_Job_ID;
    [FieldOffset(14)]
    public short System_CMD_Job_Type;
    [FieldOffset(16)]
    public byte Job_CMD_Raw_16;
    // Bit 0: Job_Cancle
    public bool Job_CMD_Job_Cancle => (Job_CMD_Raw_16 & (1 << 0)) != 0;
    // Bit 1: Spare_bit1
    public bool Job_CMD_Spare_bit1 => (Job_CMD_Raw_16 & (1 << 1)) != 0;
    // Bit 2: Spare_bit2
    public bool Job_CMD_Spare_bit2 => (Job_CMD_Raw_16 & (1 << 2)) != 0;
    // Bit 3: Spare_bit3
    public bool Job_CMD_Spare_bit3 => (Job_CMD_Raw_16 & (1 << 3)) != 0;
    // Bit 4: Spare_bit4
    public bool Job_CMD_Spare_bit4 => (Job_CMD_Raw_16 & (1 << 4)) != 0;
    // Bit 5: Spare_bit5
    public bool Job_CMD_Spare_bit5 => (Job_CMD_Raw_16 & (1 << 5)) != 0;
    // Bit 6: Spare_bit6
    public bool Job_CMD_Spare_bit6 => (Job_CMD_Raw_16 & (1 << 6)) != 0;
    // Bit 7: Spare_bit7
    public bool Job_CMD_Spare_bit7 => (Job_CMD_Raw_16 & (1 << 7)) != 0;
    [FieldOffset(18)]
    public short Position_Block;
    [FieldOffset(20)]
    public short Num_Bay;
    [FieldOffset(22)]
    public short Num_Row;
    [FieldOffset(24)]
    public short Num_Tier;
    [FieldOffset(26)]
    public short Block_Map_0_Row_num;
    [FieldOffset(28)]
    public short Block_Map_0_Tier_num;
    [FieldOffset(30)]
    public float Block_Map_0_Max_Height;
    [FieldOffset(34)]
    public short Block_Map_1_Row_num;
    [FieldOffset(36)]
    public short Block_Map_1_Tier_num;
    [FieldOffset(38)]
    public float Block_Map_1_Max_Height;
    [FieldOffset(42)]
    public short Block_Map_2_Row_num;
    [FieldOffset(44)]
    public short Block_Map_2_Tier_num;
    [FieldOffset(46)]
    public float Block_Map_2_Max_Height;
    [FieldOffset(50)]
    public short Block_Map_3_Row_num;
    [FieldOffset(52)]
    public short Block_Map_3_Tier_num;
    [FieldOffset(54)]
    public float Block_Map_3_Max_Height;
    [FieldOffset(58)]
    public short Block_Map_4_Row_num;
    [FieldOffset(60)]
    public short Block_Map_4_Tier_num;
    [FieldOffset(62)]
    public float Block_Map_4_Max_Height;
    [FieldOffset(66)]
    public short Block_Map_5_Row_num;
    [FieldOffset(68)]
    public short Block_Map_5_Tier_num;
    [FieldOffset(70)]
    public float Block_Map_5_Max_Height;
    [FieldOffset(74)]
    public short Block_Map_6_Row_num;
    [FieldOffset(76)]
    public short Block_Map_6_Tier_num;
    [FieldOffset(78)]
    public float Block_Map_6_Max_Height;
    [FieldOffset(82)]
    public short Block_Map_7_Row_num;
    [FieldOffset(84)]
    public short Block_Map_7_Tier_num;
    [FieldOffset(86)]
    public float Block_Map_7_Max_Height;
    [FieldOffset(90)]
    public short Block_Map_8_Row_num;
    [FieldOffset(92)]
    public short Block_Map_8_Tier_num;
    [FieldOffset(94)]
    public float Block_Map_8_Max_Height;
    [FieldOffset(98)]
    public short Block_Map_9_Row_num;
    [FieldOffset(100)]
    public short Block_Map_9_Tier_num;
    [FieldOffset(102)]
    public float Block_Map_9_Max_Height;
    [FieldOffset(106)]
    public short Block_Map_10_Row_num;
    [FieldOffset(108)]
    public short Block_Map_10_Tier_num;
    [FieldOffset(110)]
    public float Block_Map_10_Max_Height;
    [FieldOffset(114)]
    public short Block_Map_10_Landing_Type;
    [FieldOffset(116)]
    public short Block_Map_10_Obstacle_Height;
    [FieldOffset(118)]
    public short Block_Map_10_Block;
    [FieldOffset(120)]
    public short Block_Map_10_Bay;
    [FieldOffset(122)]
    public short Block_Map_10_Row;
    [FieldOffset(124)]
    public short Block_Map_10_Tier;
    [FieldOffset(126)]
    public short Destination_Block_Map_0_Row_num;
    [FieldOffset(128)]
    public short Destination_Block_Map_0_Tier_num;
    [FieldOffset(130)]
    public float Destination_Block_Map_0_Max_Height;
    [FieldOffset(134)]
    public short Destination_Block_Map_1_Row_num;
    [FieldOffset(136)]
    public short Destination_Block_Map_1_Tier_num;
    [FieldOffset(138)]
    public float Destination_Block_Map_1_Max_Height;
    [FieldOffset(142)]
    public short Destination_Block_Map_2_Row_num;
    [FieldOffset(144)]
    public short Destination_Block_Map_2_Tier_num;
    [FieldOffset(146)]
    public float Destination_Block_Map_2_Max_Height;
    [FieldOffset(150)]
    public short Destination_Block_Map_3_Row_num;
    [FieldOffset(152)]
    public short Destination_Block_Map_3_Tier_num;
    [FieldOffset(154)]
    public float Destination_Block_Map_3_Max_Height;
    [FieldOffset(158)]
    public short Destination_Block_Map_4_Row_num;
    [FieldOffset(160)]
    public short Destination_Block_Map_4_Tier_num;
    [FieldOffset(162)]
    public float Destination_Block_Map_4_Max_Height;
    [FieldOffset(166)]
    public short Destination_Block_Map_5_Row_num;
    [FieldOffset(168)]
    public short Destination_Block_Map_5_Tier_num;
    [FieldOffset(170)]
    public float Destination_Block_Map_5_Max_Height;
    [FieldOffset(174)]
    public short Destination_Block_Map_6_Row_num;
    [FieldOffset(176)]
    public short Destination_Block_Map_6_Tier_num;
    [FieldOffset(178)]
    public float Destination_Block_Map_6_Max_Height;
    [FieldOffset(182)]
    public short Destination_Block_Map_7_Row_num;
    [FieldOffset(184)]
    public short Destination_Block_Map_7_Tier_num;
    [FieldOffset(186)]
    public float Destination_Block_Map_7_Max_Height;
    [FieldOffset(190)]
    public short Destination_Block_Map_8_Row_num;
    [FieldOffset(192)]
    public short Destination_Block_Map_8_Tier_num;
    [FieldOffset(194)]
    public float Destination_Block_Map_8_Max_Height;
    [FieldOffset(198)]
    public short Destination_Block_Map_9_Row_num;
    [FieldOffset(200)]
    public short Destination_Block_Map_9_Tier_num;
    [FieldOffset(202)]
    public float Destination_Block_Map_9_Max_Height;
    [FieldOffset(206)]
    public short Destination_Block_Map_10_Row_num;
    [FieldOffset(208)]
    public short Destination_Block_Map_10_Tier_num;
    [FieldOffset(210)]
    public float Destination_Block_Map_10_Max_Height;
    [FieldOffset(214)]
    public short Destination_Block_Map_10_Landing_Type;
    [FieldOffset(216)]
    public short Block_Map_10_Spare_Int;
    [FieldOffset(218)]
    public float Block_Map_10_Move_H_height;
    [FieldOffset(222)]
    public float Block_Map_10_Parked_Pos;
    [FieldOffset(226)]
    public float Destination_Block_Map_10_Obstacle_Height;
    [FieldOffset(230)]
    public short Block_Map_10_Spare_Int5;
    [FieldOffset(232)]
    public short Block_Map_10_Bay_Work_Type;
    [FieldOffset(234)]
    public short Block_Map_10_Weight;
    [FieldOffset(236)]
    public short Block_Map_10_Size;
    [FieldOffset(238)]
    public short Block_Map_10_Type;
    [FieldOffset(240)]
    public short Block_Map_10_Position;
    [FieldOffset(242)]
    public short Block_Map_10_Number;
    [FieldOffset(244)]
    public byte Code_0_Code;
    [FieldOffset(245)]
    public byte Code_1_Code;
    [FieldOffset(246)]
    public byte Code_2_Code;
    [FieldOffset(247)]
    public byte Code_3_Code;
    [FieldOffset(248)]
    public byte Code_4_Code;
    [FieldOffset(249)]
    public byte Code_5_Code;
    [FieldOffset(250)]
    public byte Code_6_Code;
    [FieldOffset(251)]
    public byte Code_7_Code;
    [FieldOffset(252)]
    public byte Code_8_Code;
    [FieldOffset(253)]
    public byte Code_9_Code;
    [FieldOffset(254)]
    public byte Code_10_Code;
    [FieldOffset(256)]
    public byte Truck_Raw_256;
    // Bit 0: Clearance
    public bool Code_10_Clearance => (Truck_Raw_256 & (1 << 0)) != 0;
    // Bit 1: SpareBit_0
    public bool Code_10_SpareBit_0 => (Truck_Raw_256 & (1 << 1)) != 0;
    // Bit 2: SpareBit_1
    public bool Code_10_SpareBit_1 => (Truck_Raw_256 & (1 << 2)) != 0;
    // Bit 3: SpareBit_2
    public bool Code_10_SpareBit_2 => (Truck_Raw_256 & (1 << 3)) != 0;
    // Bit 4: SpareBit_3
    public bool Code_10_SpareBit_3 => (Truck_Raw_256 & (1 << 4)) != 0;
    // Bit 5: SpareBit_4
    public bool Code_10_SpareBit_4 => (Truck_Raw_256 & (1 << 5)) != 0;
    // Bit 6: SpareBit_5
    public bool Code_10_SpareBit_5 => (Truck_Raw_256 & (1 << 6)) != 0;
    // Bit 7: SpareBit_6
    public bool Code_10_SpareBit_6 => (Truck_Raw_256 & (1 << 7)) != 0;
    [FieldOffset(258)]
    public byte LS_RFID_1_LS_RFID;
    [FieldOffset(259)]
    public byte LS_RFID_2_LS_RFID;
    [FieldOffset(260)]
    public byte LS_RFID_3_LS_RFID;
    [FieldOffset(261)]
    public byte LS_RFID_4_LS_RFID;
    [FieldOffset(262)]
    public byte LS_RFID_5_LS_RFID;
    [FieldOffset(263)]
    public byte LS_RFID_6_LS_RFID;
    [FieldOffset(264)]
    public byte LS_RFID_7_LS_RFID;
    [FieldOffset(265)]
    public byte LS_RFID_8_LS_RFID;
    [FieldOffset(266)]
    public byte LS_RFID_9_LS_RFID;
    [FieldOffset(267)]
    public byte LS_RFID_10_LS_RFID;
    [FieldOffset(268)]
    public byte LS_RFID_11_LS_RFID;
    [FieldOffset(269)]
    public byte LS_RFID_12_LS_RFID;
    [FieldOffset(270)]
    public byte LS_RFID_13_LS_RFID;
    [FieldOffset(271)]
    public byte LS_RFID_14_LS_RFID;
    [FieldOffset(272)]
    public byte LS_RFID_15_LS_RFID;
    [FieldOffset(274)]
    public byte SS_RFID_1_SS_RFID;
    [FieldOffset(275)]
    public byte SS_RFID_2_SS_RFID;
    [FieldOffset(276)]
    public byte SS_RFID_3_SS_RFID;
    [FieldOffset(277)]
    public byte SS_RFID_4_SS_RFID;
    [FieldOffset(278)]
    public short SS_RFID_4_Obstacle_High_Bay_Num;
    [FieldOffset(280)]
    public short SS_RFID_4_Obstacle_Low_Bay_Num;
    [FieldOffset(282)]
    public int SS_RFID_4_Parked_Gantry_Pos1;
    [FieldOffset(286)]
    public int SS_RFID_4_Parked_Trolley_Pos1;
    [FieldOffset(290)]
    public int SS_RFID_4_Parked_Gantry_Pos2;
    [FieldOffset(294)]
    public int SS_RFID_4_Parked_Trolley_Pos2;
    [FieldOffset(298)]
    public short SS_RFID_4_RCS_Num;
    [FieldOffset(300)]
    public byte SS_RFID_4_Check_Sum;
    [FieldOffset(301)]
    public byte SS_RFID_4_ETX;
    [FieldOffset(302)]
    public float SS_RFID_4_aG_Angle;
    [FieldOffset(306)]
    public float SS_RFID_4_aG_BPos_x;
    [FieldOffset(310)]
    public float SS_RFID_4_aG_BPos_z;
    [FieldOffset(314)]
    public float SS_RFID_4_aG_FPos_x;
    [FieldOffset(318)]
    public float SS_RFID_4_aG_FPos_z;
    [FieldOffset(322)]
    public float SS_RFID_4_aT_Pos;
    [FieldOffset(326)]
    public float SS_RFID_4_x;
    [FieldOffset(330)]
    public float SS_RFID_4_y;
    [FieldOffset(334)]
    public float SS_RFID_4_z;
    [FieldOffset(338)]
    public float Spreader_Ang_SS_RFID_4_x;
    [FieldOffset(342)]
    public float Spreader_Ang_SS_RFID_4_y;
    [FieldOffset(346)]
    public float Spreader_Ang_SS_RFID_4_z;
    [FieldOffset(350)]
    public float SS_RFID_4_MM_1_Pos;
    [FieldOffset(354)]
    public float SS_RFID_4_MM_2_Pos;
    [FieldOffset(358)]
    public float SS_RFID_4_MM_3_Pos;
    [FieldOffset(362)]
    public float SS_RFID_4_MM_4_Pos;
    [FieldOffset(366)]
    public float SS_RFID_4_ALS_1;
    [FieldOffset(370)]
    public float SS_RFID_4_ALS_2;
    [FieldOffset(374)]
    public float SS_RFID_4_ALS_3;
    [FieldOffset(378)]
    public float SS_RFID_4_ALS_4;
    [FieldOffset(382)]
    public float SS_RFID_4_ALS_5;
    [FieldOffset(386)]
    public float SS_RFID_4_ALS_6;
    [FieldOffset(390)]
    public float SS_RFID_4_Lidar1_Row1;
    [FieldOffset(394)]
    public float SS_RFID_4_Lidar1_Row2;
    [FieldOffset(398)]
    public float SS_RFID_4_Lidar1_Row3;
    [FieldOffset(402)]
    public float SS_RFID_4_Lidar1_Row4;
    [FieldOffset(406)]
    public float SS_RFID_4_Lidar1_Row5;
    [FieldOffset(410)]
    public float SS_RFID_4_Lidar1_Row6;
    [FieldOffset(414)]
    public float SS_RFID_4_Lidar1_Row7;
    [FieldOffset(418)]
    public float SS_RFID_4_Lidar1_Row8;
    [FieldOffset(422)]
    public float SS_RFID_4_Lidar1_Row9;
    [FieldOffset(426)]
    public byte SPRD_Status_Raw_426;
    // Bit 0: Landed
    public bool SS_RFID_4_Landed => (SPRD_Status_Raw_426 & (1 << 0)) != 0;
    // Bit 1: Rope_Slack
    public bool SS_RFID_4_Rope_Slack => (SPRD_Status_Raw_426 & (1 << 1)) != 0;
    // Bit 2: Tw_Locled
    public bool SS_RFID_4_Tw_Locled => (SPRD_Status_Raw_426 & (1 << 2)) != 0;
    // Bit 3: Tw_Unlocked
    public bool SS_RFID_4_Tw_Unlocked => (SPRD_Status_Raw_426 & (1 << 3)) != 0;
    // Bit 4: 20FT_on
    public bool SS_RFID_4__20FT_on => (SPRD_Status_Raw_426 & (1 << 4)) != 0;
    // Bit 5: 40FT_on
    public bool SS_RFID_4__40FT_on => (SPRD_Status_Raw_426 & (1 << 5)) != 0;
    // Bit 6: 45FT_on
    public bool SS_RFID_4__45FT_on => (SPRD_Status_Raw_426 & (1 << 6)) != 0;
    [FieldOffset(428)]
    public float SS_RFID_4_Truck_x;
    [FieldOffset(432)]
    public float SS_RFID_4_Truck_z;
    [FieldOffset(436)]
    public float SS_RFID_4_Truck_Angle;
    [FieldOffset(440)]
    public byte Truck_Status_Raw_440;
    // Bit 0: Trailer_Up
    public bool SS_RFID_4_Trailer_Up => (Truck_Status_Raw_440 & (1 << 0)) != 0;
    // Bit 1: Trailer_Down
    public bool SS_RFID_4_Trailer_Down => (Truck_Status_Raw_440 & (1 << 1)) != 0;
}
