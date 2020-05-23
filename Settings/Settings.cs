using System.Xml.Serialization;
using MBOptionScreen.Attributes;
using MBOptionScreen.Attributes.v2;
using MBOptionScreen.Settings;

namespace CharacterCreation
{
    public partial class Settings : AttributeSettings<Settings>
    {
        public override string ModName => ModNameTextObject.ToString();
        public override string ModuleFolderName => SubModule.ModuleFolderName;

        [XmlElement]
        public override string Id { get; set; } = "DCCSettings_v2";

        [XmlElement]
        [SettingPropertyBool(DebugModeName, HintText = DebugModeHint, Order = 0, RequireRestart = false)]
        [SettingPropertyGroup(Section0)]
        public bool DebugMode { get; set; } = false;

        #region Overrides
        [SettingPropertyBool(IgnoreDailyTickName, HintText = IgnoreDailyTickHint, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup(Section1, IsMainToggle = true)]
        public bool IgnoreDailyTick { get; set; } = true;

        [XmlElement]
        [SettingPropertyBool(OverrideAgeName, HintText = OverrideAgeHint, Order = 2, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool OverrideAge { get; set; } = false;

        [SettingPropertyBool(DisableAutoAgingName, HintText = DisableAutoAgingHint, Order = 3, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool DisableAutoAging { get; set; } = false;
        #endregion

        #region AgeModel
        [SettingPropertyBool(CustomAgeModelName, HintText = CustomAgeModelHint, Order = 4, RequireRestart = true)]
        [SettingPropertyGroup(Section2, IsMainToggle = true)]
        public bool CustomAgeModel { get; set; } = false;

        [SettingPropertyInteger(BecomeInfantAgeName, 0, 3, HintText = BecomeInfantAgeHint, Order = 5, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeInfantAge { get; set; } = 3;

        [SettingPropertyInteger(BecomeChildAgeName, 1, 6, HintText = BecomeChildAgeHint, Order = 6, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeChildAge { get; set; } = 6;

        [SettingPropertyInteger(BecomeTeenagerAgeName, 2, 14, HintText = BecomeTeenagerAgeHint, Order = 7, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeTeenagerAge { get; set; } = 14;

        [SettingPropertyInteger(BecomeAdultAgeName, 3, 18, HintText = BecomeAdultAgeHint, Order = 8, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeAdultAge { get; set; } = 18;

        [SettingPropertyInteger(BecomeOldAgeName, 30, 60, HintText = BecomeOldAgeHint, Order = 9, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int BecomeOldAge { get; set; } = 47;

        [SettingPropertyInteger(MaxAgeName, 60, 128, HintText = MaxAgeHint, Order = 10, RequireRestart = false)]
        [SettingPropertyGroup(Section2)]
        public int MaxAge { get; set; } = 128;
        #endregion
    }
}
