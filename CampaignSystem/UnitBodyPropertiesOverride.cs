using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.SaveSystem;

namespace CharacterCreation.CampaignSystem
{
    internal class UnitBodyPropertiesOverride
    {
        [SaveableField(0)]
        private string _unitId;

        [SaveableField(1)]
        private BodyProperties _bodyProperties;

        public string UnitID => _unitId;

        public BodyProperties BodyProperties => _bodyProperties;

        public UnitBodyPropertiesOverride(string unitID, BodyProperties bodyProperties)
        {
            _unitId = unitID;
            _bodyProperties = bodyProperties;
        }
    }
}
