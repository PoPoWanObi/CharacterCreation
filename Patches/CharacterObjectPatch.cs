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
    static class CharacterObjectPatch
    {
        // Inherits from CampaignBehaviorBase
        [HarmonyPatch(typeof(CharacterObject), nameof(CharacterObject.UpdatePlayerCharacterBodyProperties))]
        public static class UpdatePlayerCharacterBodyProperties
        {
            static bool Prefix(CharacterObject __instance, BodyProperties properties, ref bool isFemale)
            {
                try
                {
                    //var piSBP = typeof(Hero).GetProperty("StaticBodyProperties", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    //var piSBB = typeof(Hero).GetProperty("DynamicBodyProperties", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    
                    if (__instance.IsHero)
                    {
                        if (Settings.Instance != null && Settings.Instance.DebugMode)
                            InformationManager.DisplayMessage(new InformationMessage("Hero updated: " + __instance.HeroObject.Name, ColorManager.Purple));
                        //__instance.HeroObject.StaticBodyProperties = __properties.StaticProperties;
                        //piSBP.SetValue(__instance.HeroObject, properties.StaticProperties);
                        AccessTools.Property(typeof(Hero), "StaticBodyProperties").SetValue(__instance.HeroObject, properties.StaticProperties);
                        __instance.HeroObject.DynamicBodyProperties = properties.DynamicProperties;
                        __instance.HeroObject.UpdatePlayerGender(isFemale);

                        if (Settings.Instance != null && !Settings.Instance.OverrideAge)
                        {
                            float age = properties.DynamicProperties.Age;
                            __instance.HeroObject.BirthDay = HeroHelper.GetRandomBirthDayForAge(age);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An exception occurred whilst trying to apply the changes.\n\nException:\n{ex.Message}\n\n{ex.InnerException?.Message}");
                }
                return true;
            }
        }

        static bool Prepare()
        {
            return Settings.Instance != null && Settings.Instance.OverrideAge;
        }
    }
}
