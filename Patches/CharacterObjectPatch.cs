using System;
using System.Windows.Forms;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;
using System.Reflection;
using Helpers;

namespace CharacterCreation.Patches
{
    class CharacterObjectPatch
    {
        // Inherits from CampaignBehaviorBase
        [HarmonyPatch(typeof(CharacterObject), "UpdatePlayerCharacterBodyProperties")]
        public class UpdatePlayerCharacterBodyProperties
        {
            static bool Prefix(CharacterObject __instance, BodyProperties properties, ref bool isFemale)
            {
                try
                {
                    var piSBP = typeof(Hero).GetProperty("StaticBodyProperties", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    //var piSBB = typeof(Hero).GetProperty("DynamicBodyProperties", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                    if (__instance.IsHero)
                    {
                        InformationManager.DisplayMessage(new InformationMessage("Character updated: " + __instance.HeroObject.Name, Color.FromUint(4282569842U)));
                        //__instance.HeroObject.StaticBodyProperties = __properties.StaticProperties;
                        piSBP.SetValue(__instance.HeroObject, properties.StaticProperties);
                        __instance.HeroObject.DynamicBodyProperties = properties.DynamicProperties;
                        __instance.HeroObject.UpdatePlayerGender(isFemale);

                        float age = properties.DynamicProperties.Age;
                        __instance.HeroObject.BirthDay = HeroHelper.GetRandomBirthDayForAge(age);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An exception occurred whilst trying to apply the changes.\n\nException:\n{ex.Message}\n\n{ex.InnerException?.Message}");
                }
                return false;
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.OverrideAge;
        }
    }
}
