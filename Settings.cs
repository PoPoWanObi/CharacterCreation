using System;
using System.Xml.Serialization;

using CharacterCreation.Lib;
using CharacterCreation.Lib.Interfaces;

namespace CharacterCreation
{
    public class Settings : ILoadable
    {
        private const string instanceID = "CharacterCreationSettings";
        private static Settings _instance = null;
        

        [XmlElement("ID")]
        public string ID { get; set; } = instanceID;
        public bool DebugMode { get; set; } = true;

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Loader.Get<Settings>(instanceID);
                    if (_instance == null)
                        throw new Exception("Unable to find settings in Loader");
                }
                return _instance;
            }
        }

        #region Overrides
        [XmlElement]
        public bool OverrideAge { get; set; } = true;
        [XmlElement]
        public bool IgnoreDailyTick { get; set; } = true;
        #endregion

        #region AgeModel
        [XmlElement]
        public int BecomeInfantAge { get; set; } = 3;
        [XmlElement]
        public int BecomeChildAge { get; set; } = 6;
        [XmlElement]
        public int BecomeTeenagerAge { get; set; } = 14;
        [XmlElement]
        public int BecomeAdultAge { get; set; } = 18;
        [XmlElement]
        public int BecomeOldAge { get; set; } = 47;
        [XmlElement]
        public int MaxAge { get; set; } = 128;
        #endregion
    }
}
