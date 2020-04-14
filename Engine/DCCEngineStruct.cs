using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreation.Engine
{
    public class DCCEngineStruct : Attribute
    {
        public string EngineType { get; set; }
        
        public string AlternateDotNetType { get; set; }
        
        public DCCEngineStruct(string engineType)
        {
            this.EngineType = engineType;
            this.AlternateDotNetType = null;
        }
        
        public DCCEngineStruct(string engineType, string alternateDotNetType)
        {
            this.EngineType = engineType;
            this.AlternateDotNetType = alternateDotNetType;
        }
    }
}
