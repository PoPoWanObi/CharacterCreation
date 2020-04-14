using System;
using System.Xml.Serialization;

using ModLib;
using ModLib.Attributes;

namespace CharacterCreation
{
    public class Settings : SettingsBase
    {
        private const string instanceID = "DCCSettings";
        private static Settings _instance = null;
        public override string ModName => "Detailed Character Creation";
        public override string ModuleFolderName => SubModule.ModuleFolderName;

        [XmlElement]
        public override string ID { get; set; } = instanceID;

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FileDatabase.Get<Settings>(instanceID);
                    if (_instance == null)
                    {
                        _instance = new Settings();
                        SettingsDatabase.SaveSettings(_instance);
                    }
                }

                return _instance;
            }
        }

        [XmlElement]
        [SettingProperty("Enable debug output", "When enabled, shows debug output on mod activities.")]
        [SettingPropertyGroup("Section 0: Debug Mode")]
        public bool DebugMode { get; set; } = false;

        #region Overrides
        [XmlElement]
        [SettingProperty("Override Age", "When enabled, this will prevent FaceGen from changing a hero's age.")]
        [SettingPropertyGroup("", false)]
        public bool OverrideAge { get; set; } = false;
        [XmlElement]
        [SettingProperty("Ignore Daily Tick", "Only disable this if you want to enable automatic aging, weight and build.")]
        [SettingPropertyGroup("", false)]
        public bool IgnoreDailyTick { get; set; } = true;
        [XmlElement]
        [SettingProperty("Disable Auto Aging", "Enable this to prevent the game from changing the age physical appearance.")]
        [SettingPropertyGroup("", false)]
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
        [SettingProperty("Teenager Age Stage", 1, 14, "Set the default teenager stage age.")]
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
