using HarmonyLib;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
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
            Hero selectedHero = __instance.GetHero();
            selectedHero.FirstName = selectedHero.Name;
            if (selectedHero.IsPartyLeader)
                selectedHero.PartyBelongedTo.Name = MobilePartyHelper.GeneratePartyName(selectedHero.CharacterObject);
        }
    }
}
