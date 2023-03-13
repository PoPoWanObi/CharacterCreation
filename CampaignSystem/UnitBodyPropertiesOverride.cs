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
        private float _age;

        [SaveableField(2)]
        private float _weight;

        [SaveableField(3)]
        private float _build;

        [SaveableField(4)]
        private ulong _keyPart1;

        [SaveableField(5)]
        private ulong _keyPart2;

        [SaveableField(6)]
        private ulong _keyPart3;

        [SaveableField(7)]
        private ulong _keyPart4;

        [SaveableField(8)]
        private ulong _keyPart5;

        [SaveableField(9)]
        private ulong _keyPart6;

        [SaveableField(10)]
        private ulong _keyPart7;

        [SaveableField(11)]
        private ulong _keyPart8;

        [SaveableField(12)]
        private int _race;

        [SaveableField(13)] 
        private bool _isFemale;

        public string UnitID => _unitId;

        public BodyProperties BodyProperties => new BodyProperties(
                new DynamicBodyProperties(_age, _weight, _build),
                new StaticBodyProperties(_keyPart1, _keyPart2, _keyPart3, _keyPart4, _keyPart5, _keyPart6, _keyPart7, _keyPart8));

        public int Race => _race;

        public bool IsFemale => _isFemale;

        public UnitBodyPropertiesOverride(string unitID, BodyProperties bodyProperties, int race, bool isFemale)
        {
            _unitId = unitID;
            _age = bodyProperties.Age;
            _weight = bodyProperties.Weight;
            _build = bodyProperties.Build; ;
            _keyPart1 = bodyProperties.KeyPart1;
            _keyPart2 = bodyProperties.KeyPart2;
            _keyPart3 = bodyProperties.KeyPart3;
            _keyPart4 = bodyProperties.KeyPart4;
            _keyPart5 = bodyProperties.KeyPart5;
            _keyPart6 = bodyProperties.KeyPart6;
            _keyPart7 = bodyProperties.KeyPart7;
            _keyPart8 = bodyProperties.KeyPart8;
            _race = race;
            _isFemale = isFemale;
        }
    }
}
