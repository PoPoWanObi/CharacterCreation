using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.SaveSystem;

namespace CharacterCreation.CampaignSystem
{
    public sealed class DCCSaveableTypeDefiner : SaveableTypeDefiner
    {
        public DCCSaveableTypeDefiner() : base(0x3418FDD0) { }

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
}
