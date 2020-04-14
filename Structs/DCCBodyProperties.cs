using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace CharacterCreation.Structs
{
    [Serializable]
    public struct DCCBodyProperties
    {
        public StaticBodyProperties StaticProperties
        {
            get
            {
                return this._StaticBodyProperties;
            }
        }
        
        public DynamicBodyProperties DynamicProperties
        {
            get
            {
                return this._DynamicBodyProperties;
            }
        }
        
        public float Age
        {
            get
            {
                return this._DynamicBodyProperties.Age;
            }
        }
        
        public float Weight
        {
            get
            {
                return this._DynamicBodyProperties.Weight;
            }
        }
        
        public float Build
        {
            get
            {
                return this._DynamicBodyProperties.Build;
            }
        }
        
        public ulong KeyPart1
        {
            get
            {
                return this._StaticBodyProperties.KeyPart1;
            }
        }
        
        public ulong KeyPart2
        {
            get
            {
                return this._StaticBodyProperties.KeyPart2;
            }
        }
        
        public ulong KeyPart3
        {
            get
            {
                return this._StaticBodyProperties.KeyPart3;
            }
        }
        
        public ulong KeyPart4
        {
            get
            {
                return this._StaticBodyProperties.KeyPart4;
            }
        }
        
        public ulong KeyPart5
        {
            get
            {
                return this._StaticBodyProperties.KeyPart5;
            }
        }
        
        public ulong KeyPart6
        {
            get
            {
                return this._StaticBodyProperties.KeyPart6;
            }
        }
        
        public ulong KeyPart7
        {
            get
            {
                return this._StaticBodyProperties.KeyPart7;
            }
        }
        
        public ulong KeyPart8
        {
            get
            {
                return this._StaticBodyProperties.KeyPart8;
            }
        }
        
        public DCCBodyProperties(DynamicBodyProperties DynamicBodyProperties, StaticBodyProperties StaticBodyProperties)
        {
            this._DynamicBodyProperties = DynamicBodyProperties;
            this._StaticBodyProperties = StaticBodyProperties;
        }
        
        public static bool FromXmlNode(XmlNode node, out DCCBodyProperties DCCBodyProperties)
        {
            XmlAttributeCollection attributes = node.Attributes;
            DynamicBodyProperties DynamicBodyProperties;
            if (attributes != null && attributes.Count == 5 && node.Attributes["age"].Value != null && node.Attributes["weight"].Value != null && node.Attributes["build"].Value != null)
            {
                float age;
                float weight;
                float build;
                if (!float.TryParse(node.Attributes["age"].Value, out age) || !float.TryParse(node.Attributes["weight"].Value, out weight) || !float.TryParse(node.Attributes["build"].Value, out build))
                {
                    DynamicBodyProperties = default(DynamicBodyProperties);
                }
                else
                {
                    DynamicBodyProperties = new DynamicBodyProperties(age, weight, build);
                }
            }
            else
            {
                DynamicBodyProperties = default(DynamicBodyProperties);
            }
            StaticBodyProperties StaticBodyProperties;
            if (StaticBodyProperties.FromXmlNode(node, out StaticBodyProperties))
            {
                DCCBodyProperties = new DCCBodyProperties(DynamicBodyProperties, StaticBodyProperties);
                return true;
            }
            DCCBodyProperties = default(DCCBodyProperties);
            return false;
        }
        
        public static bool FromString(string keyValue, out DCCBodyProperties DCCBodyProperties)
        {
            if (keyValue.StartsWith("<DCCBodyProperties ", StringComparison.InvariantCultureIgnoreCase) || keyValue.StartsWith("<DCCBodyPropertiesMax ", StringComparison.InvariantCultureIgnoreCase))
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.LoadXml(keyValue);
                }
                catch (XmlException)
                {
                    DCCBodyProperties = default(DCCBodyProperties);
                    return false;
                }
                if (xmlDocument.FirstChild.Name.Equals("DCCBodyProperties", StringComparison.InvariantCultureIgnoreCase) || xmlDocument.FirstChild.Name.Equals("DCCBodyPropertiesMax", StringComparison.InvariantCultureIgnoreCase))
                {
                    DCCBodyProperties.FromXmlNode(xmlDocument.FirstChild, out DCCBodyProperties);
                    float age = 20f;
                    float weight = 0f;
                    float build = 0f;
                    if (xmlDocument.FirstChild.Attributes["age"] != null)
                    {
                        float.TryParse(xmlDocument.FirstChild.Attributes["age"].Value, out age);
                    }
                    if (xmlDocument.FirstChild.Attributes["weight"] != null)
                    {
                        float.TryParse(xmlDocument.FirstChild.Attributes["weight"].Value, out weight);
                    }
                    if (xmlDocument.FirstChild.Attributes["build"] != null)
                    {
                        float.TryParse(xmlDocument.FirstChild.Attributes["build"].Value, out build);
                    }
                    DCCBodyProperties = new DCCBodyProperties(new DynamicBodyProperties(age, weight, build), DCCBodyProperties.StaticProperties);
                    return true;
                }
                DCCBodyProperties = default(DCCBodyProperties);
                return false;
            }
            DCCBodyProperties = default(DCCBodyProperties);
            return false;
        }
        
        public static DCCBodyProperties GetRandomDCCBodyProperties(bool isFemale, DCCBodyProperties DCCBodyPropertiesMin, DCCBodyProperties DCCBodyPropertiesMax, int hairCoverType, int seed, string hairTags, string beardTags, string tattooTags)
        {
            return FaceGen.GetRandomBodyProperties(isFemale, DCCBodyPropertiesMin, DCCBodyPropertiesMax, hairCoverType, seed, hairTags, beardTags, tattooTags);
        }
        
        public static bool operator ==(DCCBodyProperties a, DCCBodyProperties b)
        {
            return a == b || (a != null && b != null && a._StaticBodyProperties == b._StaticBodyProperties && a._DynamicBodyProperties == b._DynamicBodyProperties);
        }
        
        public static bool operator !=(DCCBodyProperties a, DCCBodyProperties b)
        {
            return !(a == b);
        }
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(150);
            stringBuilder.Append("<DCCBodyProperties version=\"4\" ");
            stringBuilder.Append(this._DynamicBodyProperties.ToString() + " ");
            stringBuilder.Append(this._StaticBodyProperties.ToString());
            stringBuilder.Append(" />");
            return stringBuilder.ToString();
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is DCCBodyProperties))
            {
                return false;
            }
            DCCBodyProperties DCCBodyProperties = (DCCBodyProperties)obj;
            return EqualityComparer<DynamicBodyProperties>.Default.Equals(this._DynamicBodyProperties, DCCBodyProperties._DynamicBodyProperties) && EqualityComparer<StaticBodyProperties>.Default.Equals(this._StaticBodyProperties, DCCBodyProperties._StaticBodyProperties);
        }
        
        public override int GetHashCode()
        {
            return (2041866711 * -1521134295 + EqualityComparer<DynamicBodyProperties>.Default.GetHashCode(this._DynamicBodyProperties)) * -1521134295 + EqualityComparer<StaticBodyProperties>.Default.GetHashCode(this._StaticBodyProperties);
        }
        
        public DCCBodyProperties ClampForMultiplayer()
        {
            float age = MathF.Clamp(this.DynamicProperties.Age, 20f, 128f);
            return new DCCBodyProperties(new DynamicBodyProperties(age, 0.5f, 0.5f), this.StaticProperties);
        }
        
        public static DCCBodyProperties Default
        {
            get
            {
                return new DCCBodyProperties(new DynamicBodyProperties(20f, 0f, 0f), default(StaticBodyProperties));
            }
        }
        
        private readonly DynamicBodyProperties _DynamicBodyProperties;
        
        private readonly StaticBodyProperties _StaticBodyProperties;
    }
}
