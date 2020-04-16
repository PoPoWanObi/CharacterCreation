using System;
using System.Xml.Serialization;

using ModLib;
using ModLib.Attributes;

namespace CharacterCreation
{
    public class Settings : SettingsBase
    {
        public const string InstanceID = "DCCSettings";
        private static Settings _instance = null;
        public override string ModName => "Detailed Character Creation";
        public override string ModuleFolderName => SubModule.ModuleFolderName;

        [XmlElement]
        public override string ID { get; set; } = InstanceID;

        public static Settings Instance
        {
            get
            {
                return (Settings)SettingsDatabase.GetSettings(InstanceID);
            }
        }

        [XmlElement]
        [SettingProperty("Enable debug output", "Enable the mod's debug output.")]
        [SettingPropertyGroup("Section 0: Debug Mode")]
        public bool DebugMode { get; set; } = false;

        #region Overrides
        [XmlElement]
        [SettingProperty("Overrides", "Keep this on to prevent the game from reverting your appearance.")]
        [SettingPropertyGroup("Section 1: Overrides", true)]
        public bool IgnoreDailyTick { get; set; } = true;
        [XmlElement]
        [SettingProperty("Override Age", "When enabled, this will prevent FaceGen from changing a hero's age.")]
        [SettingPropertyGroup("Section 1: Overrides", false)]
        public bool OverrideAge { get; set; } = false;
        [XmlElement]
        [SettingProperty("Disable Auto Aging", "Enable this to prevent the game from changing the age physical appearance.")]
        [SettingPropertyGroup("Section 1: Overrides", false)]
        public bool DisableAutoAging { get; set; } = false;
        #endregion

        #region AgeModel
        [XmlElement]
        [SettingProperty("Infant Age Stage", 0, 3, "Set the default infant stage age.")]
        [SettingPropertyGroup("Section 2: Age Model", false)]
        public int BecomeInfantAge { get; set; } = 3;
        [XmlElement]
        [SettingProperty("Child Age Stage", 1, 6, "Set the default child stage age.")]
        [SettingPropertyGroup("Section 2: Age Model", false)]
        public int BecomeChildAge { get; set; } = 6;
        [XmlElement]
        [SettingProperty("Teenager Age Stage", 2, 14, "Set the default teenager stage age.")]
        [SettingPropertyGroup("Section 2: Age Model", false)]
        public int BecomeTeenagerAge { get; set; } = 14;
        [XmlElement]
        [SettingProperty("Adult Age Stage", 3, 18, "Set the default adult stage age.")]
        [SettingPropertyGroup("Section 2: Age Model", false)]
        public int BecomeAdultAge { get; set; } = 18;
        [XmlElement]
        [SettingProperty("Old Age Stage", 30, 60, "Set the default old stage age.")]
        [SettingPropertyGroup("Section 2: Age Model", false)]
        public int BecomeOldAge { get; set; } = 47;
        [XmlElement]
        [SettingProperty("Max Age Stage", 60, 128, "Set the default max age.")]
        [SettingPropertyGroup("Section 2: Age Model", false)]
        public int MaxAge { get; set; } = 128;
        #endregion
    }
}
