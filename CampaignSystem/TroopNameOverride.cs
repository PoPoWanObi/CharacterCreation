using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.SaveSystem;

namespace CharacterCreation.CampaignSystem
{
    internal class TroopNameOverride
    {
        [SaveableField(0)]
        private string _unitId;

        [SaveableField(1)]
        private string _unitName;

        public string UnitId => _unitId;

        public string UnitName => _unitName;

        public TroopNameOverride(string unitId, string unitName)
        {
            _unitId = unitId;
            _unitName = unitName;
        }
    }
}
