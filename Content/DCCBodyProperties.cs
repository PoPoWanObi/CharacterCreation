using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using CharacterCreation.Content;
using CharacterCreation.Structs;

namespace CharacterCreation.Content
{
    public static class DCCBodyProperties
    {
        public static int GetNumEditableDeformKeys(bool initialGender, int age)
        {
            return DCCAPI.IDCCFaceGen.GetNumEditableDeformKeys(initialGender, (float)age);
        }
        
        public static void GetParamsFromKey(ref DCCFaceGenerationParams DCCFaceGenerationParams, BodyProperties bodyProperties, bool earsAreHidden)
        {
            DCCAPI.IDCCFaceGen.GetParamsFromKey(ref DCCFaceGenerationParams, ref bodyProperties, earsAreHidden);
        }
        
        public static void GetParamsMax(int curGender, int curAge, ref int hairNum, ref int beardNum, ref int faceTextureNum, ref int mouthTextureNum, ref int faceTattooNum, ref int soundNum, ref int eyebrowNum, ref float scale)
        {
            DCCAPI.IDCCFaceGen.GetParamsMax(curGender, (float)curAge, ref hairNum, ref beardNum, ref faceTextureNum, ref mouthTextureNum, ref faceTattooNum, ref soundNum, ref eyebrowNum, ref scale);
        }
        
        public static void GetZeroProbabilities(int curGender, float curAge, ref float tattooZeroProbability)
        {
            DCCAPI.IDCCFaceGen.GetZeroProbabilities(curGender, curAge, ref tattooZeroProbability);
        }
        
        public static void ProduceNumericKeyWithParams(DCCFaceGenerationParams DCCFaceGenerationParams, bool earsAreHidden, ref BodyProperties bodyProperties)
        {
            DCCAPI.IDCCFaceGen.ProduceNumericKeyWithParams(ref DCCFaceGenerationParams, earsAreHidden, ref bodyProperties);
        }
        
        public static void ProduceNumericKeyWithDefaultValues(ref BodyProperties initialBodyProperties, bool earsAreHidden, int gender, int age)
        {
            DCCAPI.IDCCFaceGen.ProduceNumericKeyWithDefaultValues(ref initialBodyProperties, earsAreHidden, gender, (float)age);
        }
        
        public static BodyProperties GetRandomBodyProperties(bool isFemale, BodyProperties bodyPropertiesMin, BodyProperties bodyPropertiesMax, int hairCoverType, int seed, string hairTags, string beardTags, string tatooTags)
        {
            BodyProperties result = default(BodyProperties);
            DCCAPI.IDCCFaceGen.GetRandomBodyProperties(isFemale ? 1 : 0, ref bodyPropertiesMin, ref bodyPropertiesMax, hairCoverType, seed, hairTags, beardTags, tatooTags, ref result);
            return result;
        }
        
        public static DCCDeformKeyData GetDCCDeformKeyData(int keyNo, int gender, int age)
        {
            DCCDeformKeyData result = default(DCCDeformKeyData);
            DCCAPI.IDCCFaceGen.GetDCCDeformKeyData(keyNo, ref result, gender, (float)age);
            return result;
        }
        
        public static int GetFaceGenInstancesLength(int gender, int age)
        {
            return DCCAPI.IDCCFaceGen.GetFaceGenInstancesLength(gender, (float)age);
        }
        
        public static bool EnforceConstraints(ref DCCFaceGenerationParams DCCFaceGenerationParams)
        {
            return DCCAPI.IDCCFaceGen.EnforceConstraints(ref DCCFaceGenerationParams);
        }
        
        public static float GetScaleFromKey(int gender, BodyProperties bodyProperties)
        {
            return DCCAPI.IDCCFaceGen.GetScaleFromKey(gender, ref bodyProperties);
        }
        
        public static int GetHairColorCount(int curGender, int age)
        {
            return DCCAPI.IDCCFaceGen.GetHairColorCount(curGender, (float)age);
        }
        
        public static List<uint> GetHairColorGradientPoints(int curGender, int age)
        {
            int hairColorCount = DCCBodyProperties.GetHairColorCount(curGender, age);
            List<uint> list = new List<uint>();
            Vec3[] array = new Vec3[hairColorCount];
            DCCAPI.IDCCFaceGen.GetHairColorGradientPoints(curGender, (float)age, array);
            foreach (Vec3 vec in array)
            {
                list.Add(MBMath.ColorFromRGBA(vec.x, vec.y, vec.z, 1f));
            }
            return list;
        }
        
        public static int GetTatooColorCount(int curGender, int age)
        {
            return DCCAPI.IDCCFaceGen.GetTatooColorCount(curGender, (float)age);
        }
        
        public static List<uint> GetTatooColorGradientPoints(int curGender, int age)
        {
            int tatooColorCount = DCCBodyProperties.GetTatooColorCount(curGender, age);
            List<uint> list = new List<uint>();
            Vec3[] array = new Vec3[tatooColorCount];
            DCCAPI.IDCCFaceGen.GetTatooColorGradientPoints(curGender, (float)age, array);
            foreach (Vec3 vec in array)
            {
                list.Add(MBMath.ColorFromRGBA(vec.x, vec.y, vec.z, 1f));
            }
            return list;
        }
        
        public static int GetSkinColorCount(int curGender, int age)
        {
            return DCCAPI.IDCCFaceGen.GetSkinColorCount(curGender, (float)age);
        }
        
        public static List<uint> GetSkinColorGradientPoints(int curGender, int age)
        {
            int skinColorCount = DCCBodyProperties.GetSkinColorCount(curGender, age);
            List<uint> list = new List<uint>();
            Vec3[] array = new Vec3[skinColorCount];
            DCCAPI.IDCCFaceGen.GetSkinColorGradientPoints(curGender, (float)age, array);
            foreach (Vec3 vec in array)
            {
                list.Add(MBMath.ColorFromRGBA(vec.x, vec.y, vec.z, 1f));
            }
            return list;
        }
        
        public static List<bool> GetVoiceTypeUsableForPlayerData(int curGender, float age, int voiceTypeCount)
        {
            bool[] array = new bool[voiceTypeCount];
            DCCAPI.IDCCFaceGen.GetVoiceTypeUsableForPlayerData(curGender, age, array);
            return new List<bool>(array);
        }
        
        public static void SetHair(ref BodyProperties bodyProperties, int hair, int beard, int tattoo)
        {
            DCCFaceGenerationParams DCCFaceGenerationParams = DCCFaceGenerationParams.Create();
            DCCBodyProperties.GetParamsFromKey(ref DCCFaceGenerationParams, bodyProperties, false);
            if (hair > -1)
            {
                DCCFaceGenerationParams._currentHair = hair;
            }
            if (beard > -1)
            {
                DCCFaceGenerationParams._curBeard = beard;
            }
            if (tattoo > -1)
            {
                DCCFaceGenerationParams._curFaceTattoo = tattoo;
            }
            DCCBodyProperties.ProduceNumericKeyWithParams(DCCFaceGenerationParams, false, ref bodyProperties);
        }
        
        public static void SetBody(ref BodyProperties bodyProperties, int build, int weight)
        {
            DCCFaceGenerationParams DCCFaceGenerationParams = DCCFaceGenerationParams.Create();
            DCCBodyProperties.GetParamsFromKey(ref DCCFaceGenerationParams, bodyProperties, false);
            DCCBodyProperties.ProduceNumericKeyWithParams(DCCFaceGenerationParams, false, ref bodyProperties);
        }
        
        public static void SetPigmentation(ref BodyProperties bodyProperties, int skinColor, int hairColor, int eyeColor)
        {
            DCCFaceGenerationParams DCCFaceGenerationParams = DCCFaceGenerationParams.Create();
            DCCBodyProperties.GetParamsFromKey(ref DCCFaceGenerationParams, bodyProperties, false);
            DCCBodyProperties.ProduceNumericKeyWithParams(DCCFaceGenerationParams, false, ref bodyProperties);
        }
        
        public static void GenerateParentKey(BodyProperties childBodyProperties, ref BodyProperties motherBodyProperties, ref BodyProperties fatherBodyProperties)
        {
            DCCFaceGenerationParams DCCFaceGenerationParams = DCCFaceGenerationParams.Create();
            DCCFaceGenerationParams DCCFaceGenerationParams2 = DCCFaceGenerationParams.Create();
            DCCFaceGenerationParams DCCFaceGenerationParams3 = DCCFaceGenerationParams.Create();
            DCCBodyProperties.GenerationType[] array = new DCCBodyProperties.GenerationType[4];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (DCCBodyProperties.GenerationType)MBRandom.RandomInt(2);
            }
            DCCBodyProperties.GetParamsFromKey(ref DCCFaceGenerationParams, childBodyProperties, false);
            int faceGenInstancesLength = DCCBodyProperties.GetFaceGenInstancesLength(DCCFaceGenerationParams._currentGender, (int)DCCFaceGenerationParams._curAge);
            for (int j = 0; j < faceGenInstancesLength; j++)
            {
                DCCDeformKeyData DCCDeformKeyData = DCCBodyProperties.GetDCCDeformKeyData(j, DCCFaceGenerationParams._currentGender, (int)DCCFaceGenerationParams._curAge);
                if (DCCDeformKeyData.GroupId >= 0 && DCCDeformKeyData.GroupId != 0 && DCCDeformKeyData.GroupId != 5 && DCCDeformKeyData.GroupId != 6)
                {
                    float num = MBRandom.RandomFloat * Math.Min(DCCFaceGenerationParams.KeyWeights[j], 1f - DCCFaceGenerationParams.KeyWeights[j]);
                    if (array[DCCDeformKeyData.GroupId - 1] == DCCBodyProperties.GenerationType.FromMother)
                    {
                        DCCFaceGenerationParams3.KeyWeights[j] = DCCFaceGenerationParams.KeyWeights[j];
                        DCCFaceGenerationParams2.KeyWeights[j] = DCCFaceGenerationParams.KeyWeights[j] + num;
                    }
                    else if (array[DCCDeformKeyData.GroupId - 1] == DCCBodyProperties.GenerationType.FromFather)
                    {
                        DCCFaceGenerationParams2.KeyWeights[j] = DCCFaceGenerationParams.KeyWeights[j];
                        DCCFaceGenerationParams3.KeyWeights[j] = DCCFaceGenerationParams.KeyWeights[j] + num;
                    }
                    else
                    {
                        DCCFaceGenerationParams3.KeyWeights[j] = DCCFaceGenerationParams.KeyWeights[j] + num;
                        DCCFaceGenerationParams2.KeyWeights[j] = DCCFaceGenerationParams.KeyWeights[j] - num;
                    }
                }
            }
            DCCFaceGenerationParams2._curAge = DCCFaceGenerationParams._curAge + (float)MBRandom.RandomInt(18, 25);
            float num2;
            DCCFaceGenerationParams2.SetRandomParamsExceptKeys(0, (int)DCCFaceGenerationParams2._curAge, out num2);
            DCCFaceGenerationParams2._curFaceTattoo = 0;
            DCCFaceGenerationParams3._curAge = DCCFaceGenerationParams._curAge + (float)MBRandom.RandomInt(18, 22);
            float num3;
            DCCFaceGenerationParams3.SetRandomParamsExceptKeys(1, (int)DCCFaceGenerationParams3._curAge, out num3);
            DCCFaceGenerationParams3._curFaceTattoo = 0;
            DCCFaceGenerationParams3._heightMultiplier = DCCFaceGenerationParams2._heightMultiplier * MBRandom.RandomFloatRanged(0.7f, 0.9f);
            if (DCCFaceGenerationParams3._currentHair == 0)
            {
                DCCFaceGenerationParams3._currentHair = 1;
            }
            float num4 = MBRandom.RandomFloat * Math.Min(DCCFaceGenerationParams._curSkinColorOffset, 1f - DCCFaceGenerationParams._curSkinColorOffset);
            if (MBRandom.RandomInt(2) == 1)
            {
                DCCFaceGenerationParams2._curSkinColorOffset = DCCFaceGenerationParams._curSkinColorOffset + num4;
                DCCFaceGenerationParams3._curSkinColorOffset = DCCFaceGenerationParams._curSkinColorOffset - num4;
            }
            else
            {
                DCCFaceGenerationParams2._curSkinColorOffset = DCCFaceGenerationParams._curSkinColorOffset - num4;
                DCCFaceGenerationParams3._curSkinColorOffset = DCCFaceGenerationParams._curSkinColorOffset + num4;
            }
            DCCBodyProperties.ProduceNumericKeyWithParams(DCCFaceGenerationParams3, false, ref motherBodyProperties);
            DCCBodyProperties.ProduceNumericKeyWithParams(DCCFaceGenerationParams2, false, ref fatherBodyProperties);
        }
        
        public static BodyProperties GetBodyPropertiesWithAge(ref BodyProperties bodyProperties, float age)
        {
            DCCFaceGenerationParams DCCFaceGenerationParams = default(DCCFaceGenerationParams);
            DCCBodyProperties.GetParamsFromKey(ref DCCFaceGenerationParams, bodyProperties, false);
            DCCFaceGenerationParams._curAge = age;
            BodyProperties result = default(BodyProperties);
            DCCBodyProperties.ProduceNumericKeyWithParams(DCCFaceGenerationParams, false, ref result);
            return result;
        }
        
        public enum GenerationType
        {
            FromMother,
            FromFather,
            Count
        }
    }
}
