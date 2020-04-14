using CharacterCreation.Engine;
using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;

namespace CharacterCreation.Structs
{
    [DCCEngineStruct("Deform_Key_Data")]
    public struct DCCDeformKeyData
    {
        public int GroupId;
        
        public int KeyTimePoint;
        
        public float Min;
        
        public float Max;
        
        public float Value;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string Id;
    }
}
