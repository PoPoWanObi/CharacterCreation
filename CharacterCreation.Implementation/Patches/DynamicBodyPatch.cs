using CharacterCreation.Common.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem.CampaignBehaviors;

namespace CharacterCreation.Implementation.Patches;

[HarmonyPatch(typeof(DynamicBodyCampaignBehavior), "DailyTick")]
public static class DynamicBodyPatch
{
    public static bool Prefix()
    {
        if (DccPerSaveSettings.SaveInstance != null && DccPerSaveSettings.SaveInstance.IgnoreDailyTick)
            return false;

        // there is probably a better way to patch this and still be able to enable/disable this patch at will. At least it still works.
        return true;
    }
}