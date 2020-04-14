using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace CharacterCreation.Content
{
    public class DCCBodyGenerator
    {
        public BasicCharacterObject Character { get; private set; }
        
        public DCCBodyGenerator(BasicCharacterObject troop)
        {
            this.Character = troop;
            this.SetCharacter(troop);
            this.IsFemale = this.Character.IsFemale;
        }
        
        public DCCFaceGenerationParams InitBodyGenerator()
        {
            this.CurrentBodyProperties = this.Character.GetBodyProperties(this.Character.Equipment, -1);
            DCCFaceGenerationParams DCCFaceGenerationParams = DCCFaceGenerationParams.Create();
            DCCFaceGenerationParams._currentGender = (this.Character.IsFemale ? 1 : 0);
            DCCFaceGenerationParams._curAge = this.Character.Age;
            DCCBodyProperties.GetParamsFromKey(ref DCCFaceGenerationParams, this.CurrentBodyProperties, this.Character.Equipment.EarsAreHidden);
            DCCFaceGenerationParams.SetGenderAndAdjustParams(DCCFaceGenerationParams._currentGender, (int)DCCFaceGenerationParams._curAge);
            return DCCFaceGenerationParams;
        }
        
        public void RefreshFace(DCCFaceGenerationParams DCCFaceGenerationParams)
        {
            DCCBodyProperties.ProduceNumericKeyWithParams(DCCFaceGenerationParams, this.Character.Equipment.EarsAreHidden, ref this.CurrentBodyProperties);
            this.IsFemale = (DCCFaceGenerationParams._currentGender == 1);
        }
        
        public void SetCharacter(BasicCharacterObject character)
        {
            this.Character = character;
            Game.Current.LastFaceEditedCharacter = character;
            MBDebug.Print("FaceGen set character> character face key: " + character.GetBodyProperties(character.Equipment, -1), 0, Debug.DebugColor.White, 17592186044416UL);
        }
        
        public void SaveCurrentCharacter()
        {
            this.Character.UpdatePlayerCharacterBodyProperties(this.CurrentBodyProperties, this.IsFemale);
            this.SaveTraitChanges();
        }
        
        private void SaveTraitChanges()
        {
        }
        
        public const string FaceGenTeethAnimationName = "facegen_teeth";
        
        public BodyProperties CurrentBodyProperties;
        
        public BodyProperties BodyPropertiesMin;
        
        public BodyProperties BodyPropertiesMax;
        
        public bool IsFemale;
    }
}
