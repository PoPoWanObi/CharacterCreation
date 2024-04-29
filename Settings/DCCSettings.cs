using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;

namespace CharacterCreation
{
    static class DCCSettingsUtil
    {
        private static IDCCSettings? instance;

        public static int ExceptionCount { get; internal set; }

        private static FileInfo ConfigFile { get; } = new FileInfo(Path.Combine(BasePath.Name, "Modules", "CharacterCreation.config.xml"));

        public static IDCCSettings Instance
        {
            get
            {
                // attempt to load MCM config
                try
                {
                    instance = DCCSettings.Instance ?? instance;
                }
                catch (Exception e)
                {
                    if (ExceptionCount < 100) // don't need this populating the log file
                    {
                        ExceptionCount++;
                        Debug.Print(string.Format("[CharacterCreation] Failed to obtain MCM config, defaulting to config file.\n\nError: {1}\n\n{2}",
                            ConfigFile.FullName, e.Message, e.StackTrace));
                    }
                }

                // load config file if MCM config load fails
                if (instance == default)
                {
                    var serializer = new XmlSerializer(typeof(DCCDefaultSettings));
                    if (ConfigFile.Exists)
                    {
                        try
                        {
                            using (var stream = ConfigFile.OpenText())
                                instance = serializer.Deserialize(stream) as DCCDefaultSettings;
                        }
                        catch (Exception e)
                        {
                            Debug.Print(string.Format("[CharacterCreation] Failed to load file {0}\n\nError: {1}\n\n{2}",
                                ConfigFile.FullName, e.Message, e.StackTrace));
                        }
                    }

                    if (instance == default) instance = new DCCDefaultSettings();
                    using (var stream = ConfigFile.Open(FileMode.Create))
                    {
                        var xmlWritter = new XmlTextWriter(stream, Encoding.UTF8)
                        {
                            Formatting = Formatting.Indented,
                            Indentation = 4
                        };
                        serializer.Serialize(xmlWritter, instance);
                    }
                }
                return instance;
            }
        }
    }

    interface IDCCSettings
    {
        bool DebugMode { get; set; }

        bool ShowOptionsLabel { get; set; }

        bool IgnoreDailyTick { get; set; }

        bool PatchAgeNotUpdatingOnCharEdit { get; set; }

        bool FixCharEditEffectOnNPC { get; set; }

        bool AddFaceGenValues { get; set; }

        bool PatchSavePreviewGenderBug { get; set; }

        bool CustomAgeModel { get; set; }

        int BecomeInfantAge { get; set; }

        int BecomeChildAge { get; set; }

        int BecomeTeenagerAge { get; set; }

        int BecomeAdultAge { get; set; }

        int BecomeOldAge { get; set; }

        int MaxAge { get; set; }

        bool EnableCompatibility { get; set; }

        bool EnableCharacterReloadCompatibility { get; set; }
    }

    [XmlRoot("CharacterCreation", IsNullable = false)]
    public class DCCDefaultSettings : IDCCSettings
    {
        [XmlElement(DataType = "boolean")]
        public bool DebugMode { get; set; } = false;

        [XmlElement(DataType = "boolean")]
        public bool ShowOptionsLabel { get; set; } = true;

        [XmlElement(DataType = "boolean")]
        public bool IgnoreDailyTick { get; set; } = true;

        [XmlElement(DataType = "boolean")]
        public bool PatchAgeNotUpdatingOnCharEdit { get; set; } = true;

        [XmlElement(DataType = "boolean")]
        public bool FixCharEditEffectOnNPC { get; set; } = true;

        [XmlElement(DataType = "boolean")]
        public bool AddFaceGenValues { get; set; } = true;

        [XmlElement(DataType = "boolean")]
        public bool PatchSavePreviewGenderBug { get; set; } = true;

        [XmlElement(DataType = "boolean")]
        public bool CustomAgeModel { get; set; } = false;

        [XmlElement(DataType = "int")]
        public int BecomeInfantAge { get; set; }

        [XmlElement(DataType = "int")]
        public int BecomeChildAge { get; set; }

        [XmlElement(DataType = "int")]
        public int BecomeTeenagerAge { get; set; }

        [XmlElement(DataType = "int")]
        public int BecomeAdultAge { get; set; }

        [XmlElement(DataType = "int")]
        public int BecomeOldAge { get; set; }

        [XmlElement(DataType = "int")]
        public int MaxAge { get; set; }

        [XmlElement(DataType = "boolean")]
        public bool EnableCompatibility { get; set; } = true;

        [XmlElement(DataType = "boolean")]
        public bool EnableCharacterReloadCompatibility { get; set; } = false;

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

        public override string FormatType => "json2";

        public override string FolderName => "DetailedCharacterCreation";

        [SettingPropertyBool(DebugModeName, HintText = DebugModeHint, Order = 0, RequireRestart = false)]
        [SettingPropertyGroup(Section0, GroupOrder = 0)]
        public bool DebugMode { get; set; } = false;

        [SettingPropertyBool(OptionsLabelName, HintText = OptionsLabelHint, Order = 0, RequireRestart = false)]
        [SettingPropertyGroup(Section0, GroupOrder = 0)]
        public bool ShowOptionsLabel { get; set; } = true;

        #region Overrides
        [SettingPropertyBool(IgnoreDailyTickName, HintText = IgnoreDailyTickHint, Order = 0, RequireRestart = false)]
        [SettingPropertyGroup(Section1, GroupOrder = 1)]
        public bool IgnoreDailyTick { get; set; } = true;

        [SettingPropertyBool(PatchAgeNotUpdatingOnCharEditName, HintText = PatchAgeNotUpdatingOnCharEditHint, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool PatchAgeNotUpdatingOnCharEdit { get; set; } = true;

        [SettingPropertyBool(FixCharEditEffectOnNPCName, HintText = FixCharEditEffectOnNPCHint, Order = 2, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool FixCharEditEffectOnNPC { get; set; } = true;

        [SettingPropertyBool(AddFaceGenValuesName, HintText = AddFaceGenValuesHint, Order = 3, RequireRestart = false)]
        [SettingPropertyGroup(Section1)]
        public bool AddFaceGenValues { get; set; } = true;

        [SettingPropertyBool(PatchSavePreviewGenderBugName, HintText = PatchSavePreviewGenderBugHint, Order = 4, RequireRestart = false)]
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

        [SettingPropertyBool(EnableCharacterReloadCompatibilityName, HintText = EnableCharacterReloadCompatibilityHint, Order = 0, RequireRestart = true)]
        [SettingPropertyGroup(Section3)]
        public bool EnableCharacterReloadCompatibility { get; set; } = true;
        #endregion
    }
}
