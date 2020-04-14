using System;
using System.Collections.Generic;
using TaleWorlds.Engine;

namespace CharacterCreation.Content
{
    public static class DCCAPI
    {
        private static T GetObject<T>() where T : class
        {
            object obj;
            if (DCCAPI._objects.TryGetValue(typeof(T).FullName, out obj))
            {
                return obj as T;
            }
            return default(T);
        }
        
        internal static void SetObjects(Dictionary<string, object> objects)
        {
            DCCAPI._objects = objects;
            DCCAPI.IDCCFaceGen = DCCAPI.GetObject<IDCCFaceGen>();
        }
        
        internal static IDCCFaceGen IDCCFaceGen;
        
        private static Dictionary<string, object> _objects;
    }
}
