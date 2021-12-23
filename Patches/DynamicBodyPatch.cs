using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Library;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(DynamicBodyCampaignBehavior), "DailyTick")]
    public static class DynamicBodyPatch
    {
        public static bool Prefix()
        {
            if (DCCPerSaveSettings.SaveInstance != default && DCCPerSaveSettings.SaveInstance.IgnoreDailyTick || DCCSettingsUtil.Instance.IgnoreDailyTick)
                return false;

            // there is probably a better way to patch this and still be able to enable/disable this patch at will. At least it still works.
            return true;
        }
    }
}