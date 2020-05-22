using System.Xml.Serialization;
using MBOptionScreen.Attributes;
using MBOptionScreen.Attributes.v2;
using MBOptionScreen.Settings;

namespace CharacterCreation
{
    public class Settings : AttributeSettings<Settings>
    {
        public override string ModName => "Detailed Character Creation";
        public override string ModuleFolderName => SubModule.ModuleFolderName;

        [XmlElement]
        public override string Id { get; set; } = "DCCSettings_v2";

        [XmlElement]
        [SettingPropertyBool("Enable debug output", HintText = "Enable DCC's debug output. Does NOT require restart.", Order = 0, RequireRestart = false)]
        [SettingPropertyGroup("Section 0: Debug Mode")]
        public bool DebugMode { get; set; } = false;

        #region Overrides
        [SettingPropertyBool("Overrides", HintText = "Keep this on to prevent the game from reverting your appearance. REQUIRES restart.", Order = 1, RequireRestart = true)]
        [SettingPropertyGroup("Section 1: Overrides", IsMainToggle = true)]
        public bool IgnoreDailyTick { get; set; } = true;
        [XmlElement]
        [SettingPropertyBool("Override Age", HintText = "When enabled, this will prevent FaceGen from changing a hero's age. REQUIRES restart.", Order = 2, RequireRestart = true)]
        [SettingPropertyGroup("Section 1: Overrides")]
        public bool OverrideAge { get; set; } = false;
        [SettingPropertyBool("Disable Auto Aging", HintText = "Enable this to prevent the game from changing the age physical appearance. Does NOT require restart.", Order = 3, RequireRestart = false)]
        [SettingPropertyGroup("Section 1: Overrides")]
        public bool DisableAutoAging { get; set; } = false;
        #endregion

        #region AgeModel
        [SettingPropertyBool("Custom Age Model", HintText = "Enable this to use a custom age model. Disable if another mod uses a custom age model. REQUIRES restart.", Order = 4, RequireRestart = true)]
        [SettingPropertyGroup("Section 2: Age Model", IsMainToggle = true)]
        public bool CustomAgeModel { get; set; } = false;
        [SettingPropertyInteger("Infant Age Stage", 0, 3, HintText = "Set the default infant stage age. Does NOT require restart.", Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Section 2: Age Model")]
        public int BecomeInfantAge { get; set; } = 3;
        [SettingPropertyInteger("Child Age Stage", 1, 6, HintText = "Set the default child stage age. Does NOT require restart.", Order = 6, RequireRestart = false)]
        [SettingPropertyGroup("Section 2: Age Model")]
        public int BecomeChildAge { get; set; } = 6;
        [SettingPropertyInteger("Teenager Age Stage", 2, 14, HintText = "Set the default teenager stage age. Does NOT require restart.", Order = 7, RequireRestart = false)]
        [SettingPropertyGroup("Section 2: Age Model")]
        public int BecomeTeenagerAge { get; set; } = 14;
        [SettingPropertyInteger("Adult Age Stage", 3, 18, HintText = "Set the default adult stage age. Does NOT require restart.", Order = 8, RequireRestart = false)]
        [SettingPropertyGroup("Section 2: Age Model")]
        public int BecomeAdultAge { get; set; } = 18;
        [SettingPropertyInteger("Old Age Stage", 30, 60, HintText = "Set the default old stage age. Does NOT require restart.", Order = 9, RequireRestart = false)]
        [SettingPropertyGroup("Section 2: Age Model")]
        public int BecomeOldAge { get; set; } = 47;
        [SettingPropertyInteger("Max Age Stage", 60, 128, HintText = "Set the default max age. Does NOT require restart.", Order = 10, RequireRestart = false)]
        [SettingPropertyGroup("Section 2: Age Model")]
        public int MaxAge { get; set; } = 128;
        #endregion
    }
}
