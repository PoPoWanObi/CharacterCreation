using CharacterCreation.Util;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.PerSave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace CharacterCreation
{
    class DCCPerSaveSettings : AttributePerSaveSettings<DCCPerSaveSettings>
    {
        public static DCCPerSaveSettings SaveInstance
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

        private const string
            DisplayNameText = "{=CharacterCration_ModNameText}Detailed Character Creation (Per-Save)",
            IgnoreDailyTickName = "{=CharacterCreation_IgnoreDailyTickName}Suppress Daily Tick",
            IgnoreDailyTickHint = "{=CharacterCreation_IgnoreDailyTickHint}Keep this on to prevent the game from reverting your appearance. Does NOT require restart.",
            OverrideAgeName = "{=CharacterCreation_OverrideAgeName}Override Age",
            OverrideAgeHint = "{=CharacterCreation_OverrideAgeHint}When enabled, this will prevent the game from aging the player hero. Does NOT require restart and takes effect upon save load or daily. Overridden by 'Disable Auto Aging'.",
            DisableAutoAgingName = "{=CharacterCreation_DisableAutoAgingName}Disable Auto Aging",
            DisableAutoAgingHint = "{=CharacterCreation_DisableAutoAgingHint}Enable this to prevent the game from changing the age physical appearance. Does NOT require restart and takes effect upon save load or hour tick.";

        private static readonly TextObject
            DisplayNameTextObject = new TextObject(DisplayNameText);

        public override string Id => "DCCPerSaveSettings";

        public override string DisplayName => DisplayNameTextObject.ToString();

        [SettingPropertyBool(IgnoreDailyTickName, HintText = IgnoreDailyTickHint, Order = 1, RequireRestart = false)]
        public bool IgnoreDailyTick { get; set; } = DCCSettingsUtil.Instance?.IgnoreDailyTick ?? false;

        [SettingPropertyBool(OverrideAgeName, HintText = OverrideAgeHint, Order = 2, RequireRestart = false)]
        public bool OverrideAge { get; set; } = false;

        [SettingPropertyBool(DisableAutoAgingName, HintText = DisableAutoAgingHint, Order = 3, RequireRestart = false)]
        public bool DisableAutoAging
        {
            get => CampaignOptions.IsLifeDeathCycleDisabled;
            set
            {
                CampaignOptions.IsLifeDeathCycleDisabled = value;
                SettingsEffects.Instance?.SetAutoAging();
            }
        }
    }
}
