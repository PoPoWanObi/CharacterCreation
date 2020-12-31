using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace CharacterCreation.Patches
{
    public static class DynamicBodyPatch
    {
        public static bool Prefix(DynamicBodyCampaignBehavior __instance)
        {
            if (!DCCSettingsUtil.Instance.IgnoreDailyTick) 
                return true;

            // there is probably a better way to patch this and still be able to enable/disable this patch at will. At least it still works.
            return false;
        }
    }
}