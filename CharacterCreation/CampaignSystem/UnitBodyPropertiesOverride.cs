using TaleWorlds.Core;
using TaleWorlds.SaveSystem;

namespace CharacterCreation.CampaignSystem
{
    public class UnitBodyPropertiesOverride
    {
        [SaveableProperty(0)]
        public string UnitId { get; private set; }
        
        [SaveableProperty(1)]
        public float Age { get; private set; }
        
        [SaveableProperty(2)]
        public float Weight { get; private set; }
        
        [SaveableProperty(3)]
        public float Build { get; private set; }
        
        [SaveableProperty(4)]
        public ulong KeyPart1 { get; private set; }
        
        [SaveableProperty(5)]
        public ulong KeyPart2 { get; private set; }
        
        [SaveableProperty(6)]
        public ulong KeyPart3 { get; private set; }
        
        [SaveableProperty(7)]
        public ulong KeyPart4 { get; private set; }
        
        [SaveableProperty(8)]
        public ulong KeyPart5 { get; private set; }
        
        [SaveableProperty(9)]
        public ulong KeyPart6 { get; private set; }
        
        [SaveableProperty(10)]
        public ulong KeyPart7 { get; private set; }
        
        [SaveableProperty(11)]
        public ulong KeyPart8 { get; private set; }

        [SaveableProperty(12)]
        public int Race { get; private set; }

        [SaveableProperty(13)]
        public bool IsFemale { get; private set; }

        public BodyProperties BodyProperties
        {
            get => new BodyProperties(new DynamicBodyProperties(Age, Weight, Build),
                new StaticBodyProperties(KeyPart1, KeyPart2, KeyPart3, KeyPart4, KeyPart5, KeyPart6, KeyPart7,
                    KeyPart8));
            private set
            {
                Age = value.Age;
                Weight = value.Weight;
                Build = value.Build;
                KeyPart1 = value.StaticProperties.KeyPart1;
                KeyPart2 = value.StaticProperties.KeyPart2;
                KeyPart3 = value.StaticProperties.KeyPart3;
                KeyPart4 = value.StaticProperties.KeyPart4;
                KeyPart5 = value.StaticProperties.KeyPart5;
                KeyPart6 = value.StaticProperties.KeyPart6;
                KeyPart7 = value.StaticProperties.KeyPart7;
                KeyPart8 = value.StaticProperties.KeyPart8;
            }
        }

        public UnitBodyPropertiesOverride(string unitId, BodyProperties bodyProperties, int race, bool isFemale)
        {
            UnitId = unitId;
            BodyProperties = bodyProperties;
            Race = race;
            IsFemale = isFemale;
        }
    }
}
