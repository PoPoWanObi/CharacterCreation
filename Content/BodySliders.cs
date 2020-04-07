using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace CharacterCreation.Content
{
    public class BodySliders
    {
        public static bool FromXmlNode(XmlNode node, out BodyProperties bodyProperties)
        {
            XmlAttributeCollection attributes = node.Attributes;
            DynamicBodyProperties dynamicBodyProperties;
            if (attributes != null && attributes.Count == 5 && node.Attributes["age"].Value != null && node.Attributes["weight"].Value != null && node.Attributes["build"].Value != null)
            {
                float age;
                float weight;
                float build;
                if (!float.TryParse(node.Attributes["age"].Value, out age) || !float.TryParse(node.Attributes["weight"].Value, out weight) || !float.TryParse(node.Attributes["build"].Value, out build))
                {
                    dynamicBodyProperties = default(DynamicBodyProperties);
                }
                else
                {
                    dynamicBodyProperties = new DynamicBodyProperties(age, weight, build);
                }
            }
            else
            {
                dynamicBodyProperties = default(DynamicBodyProperties);
            }
            StaticBodyProperties staticBodyProperties;
            if (StaticBodyProperties.FromXmlNode(node, out staticBodyProperties))
            {
                bodyProperties = new BodyProperties(dynamicBodyProperties, staticBodyProperties);
                return true;
            }
            bodyProperties = default(BodyProperties);
            return false;
        }

        public static bool FromString(string keyValue, out BodyProperties bodyProperties)
        {
            if (keyValue.StartsWith("<BodyProperties ", StringComparison.InvariantCultureIgnoreCase) || keyValue.StartsWith("<BodyPropertiesMax ", StringComparison.InvariantCultureIgnoreCase))
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
                    xmlDocument.LoadXml(keyValue);
                }
                catch (XmlException)
                {
                    bodyProperties = default(BodyProperties);
                    return false;
                }
                if (xmlDocument.FirstChild.Name.Equals("BodyProperties", StringComparison.InvariantCultureIgnoreCase) || xmlDocument.FirstChild.Name.Equals("BodyPropertiesMax", StringComparison.InvariantCultureIgnoreCase))
                {
                    BodyProperties.FromXmlNode(xmlDocument.FirstChild, out bodyProperties);
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
                    bodyProperties = new BodyProperties(new DynamicBodyProperties(age, weight, build), bodyProperties.StaticProperties);
                    return true;
                }
                bodyProperties = default(BodyProperties);
                return false;
            }
            bodyProperties = default(BodyProperties);
            return false;
        }
    }
}
