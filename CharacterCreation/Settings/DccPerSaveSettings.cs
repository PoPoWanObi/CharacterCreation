using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.PerSave;
using TaleWorlds.Localization;
using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.Settings
{
    public class DccPerSaveSettings : AttributePerSaveSettings<DccPerSaveSettings>
    {
        public static DccPerSaveSettings? SaveInstance
        {
            get
            {
                try
                {
                    return Instance;
                }
                catch
                {
                    return null;
                }
            }
        }

        public override string Id => "DCCPerSaveSettings";

        public override string DisplayName => PerSaveDisplayNameTextObject.ToString();

        [SettingPropertyBool(IgnoreDailyTickName, HintText = IgnoreDailyTickHint, Order = 0, RequireRestart = false)]
        public bool IgnoreDailyTick { get; set; } = DccSettings.Instance?.IgnoreDailyTick ?? false;

        [SettingPropertyBool(PatchPlayerComingOfAgeIssuesName, HintText = PatchPlayerComingOfAgeIssuesHint, Order = 1, RequireRestart = false)]
        public bool PatchPlayerComingOfAgeIssues { get; set; } =
            DccSettings.Instance?.PatchPlayerComingOfAgeIssues ?? false;

        [SettingPropertyBool(OverrideAgeName, HintText = OverrideAgeHint, Order = 2, RequireRestart = false)]
        public bool OverrideAge { get; set; } = false;

        [SettingPropertyBool(DisableAutoAgingName, HintText = DisableAutoAgingHint, Order = 3, RequireRestart = false)]
        public bool DisableAutoAging { get; set; } = false;
    }
}
