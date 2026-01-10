using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.Settings
{
    public class DccSettings : AttributeGlobalSettings<DccSettings>
    {
        public override string Id => "DccSettings";

        public override string DisplayName => DisplayNameTextObject.ToString();

        public override string FormatType => "json2";

        public override string FolderName => "DetailedCharacterCreation";

        [SettingPropertyBool(DebugModeName, HintText = DebugModeHint, Order = 0, RequireRestart = false)]
        [SettingPropertyGroup(Section0, GroupOrder = 0)]
        public bool DebugMode { get; set; } = false;

        [SettingPropertyBool(OptionsLabelName, HintText = OptionsLabelHint, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup(Section0)]
        public bool ShowOptionsLabel { get; set; } = true;

        #region Overrides
        [SettingPropertyBool(IgnoreDailyTickName, HintText = IgnoreDailyTickHint, Order = 0, RequireRestart = false)]
        [SettingPropertyGroup(Section1, GroupOrder = 1)]
        public bool IgnoreDailyTick { get; set; } = true;
        
        [SettingPropertyBool(PatchPlayerComingOfAgeIssuesName, HintText = PatchPlayerComingOfAgeIssuesHint, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool PatchPlayerComingOfAgeIssues { get; set; } = true;

        [SettingPropertyBool(AddFaceGenValuesName, HintText = AddFaceGenValuesHint, Order = 2, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool AddFaceGenValues { get; set; } = true;

        [SettingPropertyBool(PatchSavePreviewGenderBugName, HintText = PatchSavePreviewGenderBugHint, Order = 3, RequireRestart = true)]
        [SettingPropertyGroup(Section1)]
        public bool PatchSavePreviewGenderBug { get; set; } = true;
        #endregion

        #region AgeModel
        [SettingPropertyBool(CustomAgeModelName, HintText = CustomAgeModelHint, IsToggle = true, RequireRestart = true)]
        [SettingPropertyGroup(Section2, GroupOrder = 2)]
        public bool CustomAgeModel { get; set; } = false;

        [SettingPropertyInteger(BecomeInfantAgeName, 0, 3, HintText = BecomeInfantAgeHint, Order = 0, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeInfantAge { get; set; } = 3;

        [SettingPropertyInteger(BecomeChildAgeName, 1, 6, HintText = BecomeChildAgeHint, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeChildAge { get; set; } = 6;

        [SettingPropertyInteger(BecomeTeenagerAgeName, 2, 14, HintText = BecomeTeenagerAgeHint, Order = 2, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeTeenagerAge { get; set; } = 14;

        [SettingPropertyInteger(BecomeAdultAgeName, 3, 18, HintText = BecomeAdultAgeHint, Order = 3, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeAdultAge { get; set; } = 18;

        [SettingPropertyInteger(BecomeOldAgeName, 30, 60, HintText = BecomeOldAgeHint, Order = 4, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeOldAge { get; set; } = 47;

        [SettingPropertyInteger(MaxAgeName, 60, 128, HintText = MaxAgeHint, Order = 5, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int MaxAge { get; set; } = 128;
        #endregion

        #region Compatibility
        [SettingPropertyBool(EnableCompatibilityName, HintText = EnableCompatibilityHint, IsToggle = true, RequireRestart = true)]
        [SettingPropertyGroup(Section3, GroupOrder = 3)]
        public bool EnableCompatibility { get; set; } = true;

        // Currently unused
        // [SettingPropertyBool(EnableCharacterReloadCompatibilityName, HintText = EnableCharacterReloadCompatibilityHint, Order = 0, RequireRestart = true)]
        // [SettingPropertyGroup(Section3)]
        // public bool EnableCharacterReloadCompatibility { get; set; } = true;
        #endregion
    }
}
