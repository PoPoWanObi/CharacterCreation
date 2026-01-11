using CharacterCreation.CampaignSystem;
using CharacterCreation.Settings;
using CharacterCreation.Util;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.Editor
{
    public static class CharacterEditorUtil
    {
        public static void ApplyCharacterChanges(CharacterObject character, BodyProperties properties, int race,
            bool isFemale, CharacterEditorStatePropertyType editorPropertyType = default)
        {
            // apply age changes
            var bodyAge = properties.DynamicProperties.Age;
            UnitEditorFunctions.ResetBirthDayForAge(character, bodyAge);
            if (DccSettings.Instance!.DebugMode)
            {
                var msg =
                    $"[CharacterCreation] Character {character.Name} expected age: {bodyAge}, actual: {character.Age}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
            }
            
            // apply body changes
            if (DccSettings.Instance.DebugMode)
            {
                var msg =
                    $"[CharacterCreation] Preparing edit to {character.GetName()}. Properties: ({character.GetBodyProperties(character.Equipment).ToString()}), Race: {character.Race.ToString()}, Female: {character.IsFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
                msg =
                    $"[CharacterCreation] Applying changes to {character.GetName()}. Properties: ({properties.ToString()}), Race: {race.ToString()}, Female: {isFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
            }
            
            if (character.IsHero)
            {
                //PropertyInfo staticBodyPropertyInfoOnHero = AccessTools.Property(typeof(Hero), "StaticBodyProperties");
                //staticBodyPropertyInfoOnHero.SetValue(__instance.HeroObject, properties.StaticProperties);
                character.HeroObject.StaticBodyProperties = properties.StaticProperties;
                character.HeroObject.Weight = properties.Weight;
                character.HeroObject.Build = properties.Build;
                character.Race = race;
                character.HeroObject.IsFemale = isFemale;
                CampaignEventDispatcher.Instance.OnPlayerBodyPropertiesChanged();
                
                if (DccSettings.Instance.DebugMode)
                {
                    var msg = HeroUpdatedMsgTextObject.ToString() + character.HeroObject.Name;
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Purple));
                    Debug.Print(msg);
                }
            }
            else
            {
                CharacterCreationCampaignBehavior.Instance?.SetBodyPropertiesOverride(character, properties, race,
                    isFemale, editorPropertyType);
            }
            
            if (DccSettings.Instance.DebugMode)
            {
                var msg =
                    $"[CharacterCreation] Changes applied to {character.GetName()}. Properties: ({character.GetBodyProperties(character.Equipment).ToString()}), Race: {character.Race.ToString()}, Female: {character.IsFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
            }
        }
    }
}