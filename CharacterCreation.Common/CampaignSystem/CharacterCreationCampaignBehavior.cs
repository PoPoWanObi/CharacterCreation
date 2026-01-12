using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CharacterCreation.Common.Settings;
using CharacterCreation.Common.Util;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace CharacterCreation.Common.CampaignSystem;

public partial class CharacterCreationCampaignBehavior : CampaignBehaviorBase
{
    private static DelegateRef? _delegateRef;
    
    public static CharacterCreationCampaignBehavior? Instance { get; private set; }

    public static void SetDelegateRef(in DelegateRef delegateRef)
    {
        if (_delegateRef.HasValue) return;
        _delegateRef = delegateRef;
    }

    private CampaignBehaviorData _data;
        
    // for internal backup only
    private readonly Dictionary<string, UnitBodyPropertiesBase> _troopBaseVersion;
    private readonly Dictionary<string, TextObject> _troopBaseName;

    public ref readonly Dictionary<string, UnitBodyPropertiesOverride> BodyPropertiesOverrideMin =>
        ref _data.BodyPropertiesOverrideMin;

    public ref readonly Dictionary<string, UnitBodyPropertiesOverride> BodyPropertiesOverrideMax =>
        ref _data.BodyPropertiesOverrideMax;

    public ref readonly Dictionary<string, UnitTagOverride> TagOverrides => ref _data.TagOverrides;

    public ref readonly Dictionary<string, TroopNameOverride> TroopNameOverride => ref _data.TroopNameOverride;

    public ref readonly Dictionary<string, UnitBodyPropertiesBase> TroopBaseVersion => ref _troopBaseVersion;

    public ref readonly Dictionary<string, TextObject> TroopBaseName => ref _troopBaseName;

    public CharacterCreationCampaignBehavior()
    {
        _data = CampaignBehaviorData.Create();
        _troopBaseVersion = new Dictionary<string, UnitBodyPropertiesBase>();
        _troopBaseName = new Dictionary<string, TextObject>();
        Instance = this;
    }

    ~CharacterCreationCampaignBehavior()
    {
        if (Instance == this) Instance = null;
    }

    public override void RegisterEvents()
    {
        CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, OnGameStart);
    }

    private void OnGameStart(CampaignGameStarter gameStarter) => _delegateRef?.OnGameStartAction(this, gameStarter);

    public override void SyncData(IDataStore dataStore)
    {
        dataStore.SyncData("dcc_propertiesMin", ref _data.BodyPropertiesOverrideMin);
        dataStore.SyncData("dcc_propertiesMax", ref _data.BodyPropertiesOverrideMax);
        dataStore.SyncData("dcc_tags", ref _data.TagOverrides);
        dataStore.SyncData("dcc_troopnames", ref _data.TroopNameOverride);
    }

    public void SetBodyPropertiesOverride(
        CharacterObject unit,
        BodyProperties bodyProperties,
        int race,
        bool isFemale,
        BodyPropertyType editPropertyType
    ) => _delegateRef?.SetBodyPropertiesOverrideAction(new BehaviorActionData(this, unit), bodyProperties, race,
        isFemale, editPropertyType);

    public void SetTagOverride(
        CharacterObject unit,
        string? hairTags = null,
        string? beardTags = null,
        string? tattooTags = null
    ) => _delegateRef?.SetTagOverrideAction(new BehaviorActionData(this, unit), hairTags, beardTags, tattooTags);

    public bool HasBodyPropertiesOverride(CharacterObject unit) => _troopBaseVersion.ContainsKey(unit.StringId);

    public void UndoBodyPropertiesOverride(CharacterObject unit) =>
        _delegateRef?.UndoBodyPropertiesOverrideAction(new BehaviorActionData(this, unit));

    public void SetUnitNameOverride(CharacterObject unit, string name) =>
        _delegateRef?.SetUnitNameOverrideAction(new BehaviorActionData(this, unit), name);

    public bool HasUnitNameOverride(CharacterObject unit) => _data.TroopNameOverride.ContainsKey(unit.StringId);

    public void UndoUnitNameOverride(CharacterObject unit) =>
        _delegateRef?.UndoUnitNameOverrideAction(new BehaviorActionData(this, unit));
}