using System.Collections.Generic;
using TaleWorlds.SaveSystem;

namespace CharacterCreation.Common.CampaignSystem;

public sealed class DccSaveableTypeDefiner : SaveableTypeDefiner
{
    public DccSaveableTypeDefiner() : base(0x3418FDD0) { }

    protected override void DefineClassTypes()
    {
        AddClassDefinition(typeof(UnitBodyPropertiesOverride), 1);
        AddClassDefinition(typeof(TroopNameOverride), 2);
        AddClassDefinition(typeof(UnitTagOverride), 3);
    }

    protected override void DefineContainerDefinitions()
    {
        ConstructContainerDefinition(typeof(Dictionary<string, UnitBodyPropertiesOverride>));
        ConstructContainerDefinition(typeof(Dictionary<string, TroopNameOverride>));
        ConstructContainerDefinition(typeof(Dictionary<string, UnitTagOverride>));
    }
}