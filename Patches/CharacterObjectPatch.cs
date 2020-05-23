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
    [HarmonyPatch(typeof(CharacterObject), nameof(CharacterObject.UpdatePlayerCharacterBodyProperties))]
    static class CharacterObjectPatch
    {
        static bool Prefix(CharacterObject __instance, BodyProperties properties, ref bool isFemale)
        {
            try
            {
                if (__instance.IsHero)
                {
                    if (Settings.Instance != null && Settings.Instance.DebugMode)
                        InformationManager.DisplayMessage(new InformationMessage("Hero updated: " + __instance.HeroObject.Name, ColorManager.Purple));
                    
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

        static bool Prepare()
        {
            return Settings.Instance != null && Settings.Instance.OverrideAge;
        }
    }
}
