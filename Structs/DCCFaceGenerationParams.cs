using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace CharacterCreation.Content
{
    [EngineStruct("Face_generation_params")]
    public struct DCCFaceGenerationParams
    {
        public static DCCFaceGenerationParams Create()
        {
            DCCFaceGenerationParams result;
            result.seed_ = 0;
            result._curBeard = 0;
            result._currentHair = 0;
            result._curEyebrow = 0;
            result._isHairFlipped = false;
            result._currentGender = 0;
            result._curFaceTexture = 0;
            result._curMouthTexture = 0;
            result._curFaceTattoo = 0;
            result._currentVoice = 0;
            result.hair_filter_ = 0;
            result.beard_filter_ = 0;
            result.tattoo_filter_ = 0;
            result.face_texture_filter_ = 0;
            result._tattooZeroProbability = 0f;
            result.KeyWeights = new float[320];
            result._curAge = 0f;
            result._curWeight = 0f;
            result._curBuild = 0f;
            result._curSkinColorOffset = 0f;
            result._curHairColorOffset = 0f;
            result._curEyeColorOffset = 0f;
            result.face_dirt_amount_ = 0f;
            result._curFaceTattooColorOffset1 = 0f;
            result._heightMultiplier = 0f;
            result._voicePitch = 0f;
            return result;
        }
        
        public void SetGenderAndAdjustParams(int gender, int curAge)
        {
            this._currentGender = gender;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            float num8 = 0f;
            DCCBodyProperties.GetParamsMax(gender, curAge, ref num, ref num2, ref num3, ref num4, ref num7, ref num6, ref num5, ref num8);
            this._currentHair = MBMath.ClampInt(this._currentHair, 0, num - 1);
            this._curBeard = MBMath.ClampInt(this._curBeard, 0, num2 - 1);
            this._curFaceTexture = MBMath.ClampInt(this._curFaceTexture, 0, num3 - 1);
            this._curMouthTexture = MBMath.ClampInt(this._curMouthTexture, 0, num4 - 1);
            this._curFaceTattoo = MBMath.ClampInt(this._curFaceTattoo, 0, num7 - 1);
            this._currentVoice = MBMath.ClampInt(this._currentVoice, 0, num6 - 1);
            this._voicePitch = MBMath.ClampFloat(this._voicePitch, 0f, 1f);
            this._curEyebrow = MBMath.ClampInt(this._curEyebrow, 0, num5 - 1);
        }
        
        public void SetRandomParamsExceptKeys(int gender, int minAge, out float scale)
        {
            int maxValue = 0;
            int maxValue2 = 0;
            int maxValue3 = 0;
            int maxValue4 = 0;
            int maxValue5 = 0;
            int maxValue6 = 0;
            int maxValue7 = 0;
            scale = 0f;
            DCCBodyProperties.GetParamsMax(gender, minAge, ref maxValue, ref maxValue2, ref maxValue3, ref maxValue4, ref maxValue7, ref maxValue6, ref maxValue5, ref scale);
            this._currentHair = MBRandom.RandomInt(maxValue);
            this._curBeard = MBRandom.RandomInt(maxValue2);
            this._curFaceTexture = MBRandom.RandomInt(maxValue3);
            this._curMouthTexture = MBRandom.RandomInt(maxValue4);
            this._curFaceTattoo = MBRandom.RandomInt(maxValue7);
            this._currentVoice = MBRandom.RandomInt(maxValue6);
            this._voicePitch = MBRandom.RandomFloat;
            this._curEyebrow = MBRandom.RandomInt(maxValue5);
            this._curSkinColorOffset = MBRandom.RandomFloat;
            this._curHairColorOffset = MBRandom.RandomFloat;
            this._curEyeColorOffset = MBRandom.RandomFloat;
            this._curFaceTattooColorOffset1 = MBRandom.RandomFloat;
            this._heightMultiplier = MBRandom.RandomFloat;
        }
        
        public int seed_;
        
        public int _curBeard;
        
        public int _currentHair;
        
        public int _curEyebrow;
        
        public bool _isHairFlipped;
        
        public int _currentGender;
        
        public int _curFaceTexture;
        
        public int _curMouthTexture;
        
        public int _curFaceTattoo;
        
        public int _currentVoice;
        
        public int hair_filter_;
        
        public int beard_filter_;
        
        public int tattoo_filter_;
        
        public int face_texture_filter_;
        
        public float _tattooZeroProbability;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 320)]
        public float[] KeyWeights;
        
        public float _curAge;
        
        public float _curWeight;
        
        public float _curBuild;
        
        public float _curSkinColorOffset;
        
        public float _curHairColorOffset;
        
        public float _curEyeColorOffset;
        
        public float face_dirt_amount_;
        
        public float _curFaceTattooColorOffset1;
        
        public float _heightMultiplier;
        
        public float _voicePitch;
    }
}