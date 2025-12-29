using System;
using System.Runtime.InteropServices;

[Serializable]
[StructLayout(LayoutKind.Explicit)]
public struct CranePlcReadData
{
    [FieldOffset(0)]
    public float Crane_Vel_sG_Vel_Backward;
    [FieldOffset(4)]
    public float Crane_Vel_sG_Vel_Forward;
    [FieldOffset(8)]
    public float Crane_Vel_sT_Vel;
    [FieldOffset(12)]
    public float Crane_Vel_sH_Vel;
    [FieldOffset(16)]
    public float Micromotion_Vel_MM_1_Vel;
    [FieldOffset(20)]
    public float Micromotion_Vel_MM_2_Vel;
    [FieldOffset(24)]
    public float Micromotion_Vel_MM_3_Vel;
    [FieldOffset(28)]
    public float Micromotion_Vel_MM_4_Vel;
    [FieldOffset(32)]
    public byte Spreader_Width_Cmd_Raw_32;
    // Bit 0: 20FT
    public bool Spreader_Width_Cmd__20FT => (Spreader_Width_Cmd_Raw_32 & (1 << 0)) != 0;
    // Bit 1: 40FT
    public bool Spreader_Width_Cmd__40FT => (Spreader_Width_Cmd_Raw_32 & (1 << 1)) != 0;
    // Bit 2: 45FT
    public bool Spreader_Width_Cmd__45FT => (Spreader_Width_Cmd_Raw_32 & (1 << 2)) != 0;
    [FieldOffset(34)]
    public byte Twist_Lock_Cmd_Raw_34;
    // Bit 0: TL_Lock
    public bool Twist_Lock_Cmd_TL_Lock => (Twist_Lock_Cmd_Raw_34 & (1 << 0)) != 0;
    // Bit 1: TL_Unlock
    public bool Twist_Lock_Cmd_TL_Unlock => (Twist_Lock_Cmd_Raw_34 & (1 << 1)) != 0;
    [FieldOffset(36)]
    public short Camera_Cam1;
    [FieldOffset(38)]
    public short Camera_Cam2;
    [FieldOffset(40)]
    public short Camera_Cam3;
    [FieldOffset(42)]
    public short Camera_Cam4;
}
