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
        }

        protected override void DefineStructTypes()
        {
            AddStructDefinition(typeof(StaticBodyProperties), 2);
            AddStructDefinition(typeof(DynamicBodyProperties), 3);
            AddStructDefinition(typeof(BodyProperties), 4);
        }

        protected override void DefineContainerDefinitions()
        {
            ConstructContainerDefinition(typeof(Dictionary<string, UnitBodyPropertiesOverride>));
        }
    }
}
