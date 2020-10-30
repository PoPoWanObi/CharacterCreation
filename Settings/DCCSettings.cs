using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace CharacterCreation
{
    static class DCCSettingsUtil
    {
        private static IDCCSettings instance;

        public static IDCCSettings Instance
        {
            get
            {
                if (instance == null)
                    instance = DCCSettings.Instance as IDCCSettings ?? new DCCDefaultSettings();
                return instance;
            }
        }
    }

    interface IDCCSettings
    {
        bool DebugMode { get; set; }

        bool IgnoreDailyTick { get; set; }

        bool OverrideAge { get; set; }

        bool DisableAutoAging { get; set; }

        bool CustomAgeModel { get; set; }

        int BecomeInfantAge { get; set; }

        int BecomeChildAge { get; set; }

        int BecomeTeenagerAge { get; set; }

        int BecomeAdultAge { get; set; }

        int BecomeOldAge { get; set; }

        int MaxAge { get; set; }
    }

    class DCCDefaultSettings : IDCCSettings
    {
        public bool DebugMode { get; set; } = false;

        public bool IgnoreDailyTick { get; set; } = true;

        public bool OverrideAge { get; set; } = false;

        public bool DisableAutoAging { get; set; } = false;

        public bool CustomAgeModel { get; set; } = false;

        public int BecomeInfantAge { get; set; }

        public int BecomeChildAge { get; set; }

        public int BecomeTeenagerAge { get; set; }

        public int BecomeAdultAge { get; set; }

        public int BecomeOldAge { get; set; }

        public int MaxAge { get; set; }

        public DCCDefaultSettings()
        {
            DefaultAgeModel baseAgeModel = new DefaultAgeModel();
            BecomeInfantAge = baseAgeModel.BecomeInfantAge;
            BecomeChildAge = baseAgeModel.BecomeChildAge;
            BecomeTeenagerAge = baseAgeModel.BecomeTeenagerAge;
            BecomeAdultAge = baseAgeModel.HeroComesOfAge;
            BecomeOldAge = baseAgeModel.BecomeOldAge;
            MaxAge = baseAgeModel.MaxAge;
        }
    }

    partial class DCCSettings : AttributeGlobalSettings<DCCSettings>, IDCCSettings
    {
        public override string Id => "DCCSettings";
        public override string DisplayName => DisplayNameTextObject.ToString();
        public override string FolderName => "DetailedCharacterCreation";

        [SettingPropertyBool(DebugModeName, HintText = DebugModeHint, Order = 0, RequireRestart = false)]
        [SettingPropertyGroup(Section0)]
        public bool DebugMode { get; set; } = false;

        #region Overrides
        [SettingPropertyBool(IgnoreDailyTickName, HintText = IgnoreDailyTickHint, IsToggle = true, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool IgnoreDailyTick { get; set; } = true;

        [SettingPropertyBool(OverrideAgeName, HintText = OverrideAgeHint, Order = 2, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool OverrideAge { get; set; } = false;

        [SettingPropertyBool(DisableAutoAgingName, HintText = DisableAutoAgingHint, Order = 3, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool DisableAutoAging { get; set; } = false;
        #endregion

        #region AgeModel
        [SettingPropertyBool(CustomAgeModelName, HintText = CustomAgeModelHint, IsToggle = true, Order = 4, RequireRestart = true)]
        [SettingPropertyGroup(Section2)]
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
