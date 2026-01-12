using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.Common.CampaignSystem;

public partial class CharacterCreationCampaignBehavior
{
    public readonly struct BehaviorActionData
    {
        public readonly CharacterCreationCampaignBehavior Behavior;
        public readonly CharacterObject Character;

        public BehaviorActionData(CharacterCreationCampaignBehavior behavior, CharacterObject character)
        {
            Behavior = behavior;
            Character = character;
        }
    }
    
    public readonly struct DelegateRef
    {
        public readonly Action<CharacterCreationCampaignBehavior, CampaignGameStarter> OnGameStartAction;

        public readonly Action<BehaviorActionData, BodyProperties, int, bool, BodyPropertyType>
            SetBodyPropertiesOverrideAction;

        public readonly Action<BehaviorActionData, string?, string?, string?> SetTagOverrideAction;

        public readonly Action<BehaviorActionData> UndoBodyPropertiesOverrideAction;

        public readonly Action<BehaviorActionData, string> SetUnitNameOverrideAction;

        public readonly Action<BehaviorActionData> UndoUnitNameOverrideAction;

        public DelegateRef(
            Action<CharacterCreationCampaignBehavior, CampaignGameStarter> onGameStartAction,
            Action<BehaviorActionData, BodyProperties, int, bool, BodyPropertyType> setBodyPropertiesOverrideAction,
            Action<BehaviorActionData, string?, string?, string?> setTagOverrideAction,
            Action<BehaviorActionData> undoBodyPropertiesOverrideAction,
            Action<BehaviorActionData, string> setUnitNameOverrideAction,
            Action<BehaviorActionData> undoUnitNameOverrideAction
        )
        {
            OnGameStartAction = onGameStartAction;
            SetBodyPropertiesOverrideAction = setBodyPropertiesOverrideAction;
            SetTagOverrideAction = setTagOverrideAction;
            UndoBodyPropertiesOverrideAction = undoBodyPropertiesOverrideAction;
            SetUnitNameOverrideAction = setUnitNameOverrideAction;
            UndoUnitNameOverrideAction = undoUnitNameOverrideAction;
        }
    }
}