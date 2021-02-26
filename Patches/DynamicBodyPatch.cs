using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Library;

namespace CharacterCreation.Patches
{
    public static class DynamicBodyPatch
    {
        [HarmonyPatch(typeof(DynamicBodyCampaignBehavior), "DailyTick")]
        public static bool Prefix()
        {
            if (DCCSettingsUtil.Instance.IgnoreDailyTick)
                return false;

            // there is probably a better way to patch this and still be able to enable/disable this patch at will. At least it still works.
            return true;
        }
    }
}