using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.Library;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(ClanLordItemVM))]
    class ClanLordItemVMPatch
    {
        [HarmonyPatch("OnNamingHeroOver")]
        [HarmonyPostfix]
        public static void OnNamingHeroOverPostfix(ClanLordItemVM __instance, string suggestedName)
        {
            if (!CampaignUIHelper.IsStringApplicableForHeroName(suggestedName)) return;
            __instance.GetHero().FirstName = __instance.GetHero().Name;
        }
    }
}
