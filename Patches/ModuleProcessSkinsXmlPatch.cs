using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using HarmonyLib;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(Module), "CreateProcessedSkinsXMLForNative")]
    public static class ModuleProcessSkinsXmlPatch
    {
        private static readonly TextObject DebugString =
            new TextObject("{=CharacterCreation_ProcessSkinsXmlDebug}[CharacterCreation] Patching skins.xml loader to reverse file loading order");

        [HarmonyPrefix]
        public static bool Prefix(ref string __result, ref string baseSkinsXmlPath)
        {
            Debug.Print(DebugString.ToString());

            // Why do I have to clone and modify two separate methods? - Designer225
            List<string> elementNameList = new List<string>();
            List<Tuple<string, string>> toBeMerged = new List<Tuple<string, string>>();
            List<string> xsltList = new List<string>();

            List<MbObjectXmlInformation> mbprojXmlList = XmlResource.MbprojXmls.Where(x => x.Id == "soln_skins").ToList();
            mbprojXmlList.Reverse(); // restored for compatibility reason with mods that modify skin.xml that load later.

            //for (int i = 0; i < mbprojXmlList.Count; i++)
            //{
            //    var mbproj = mbprojXmlList[i];

            //    if (mbproj.ModuleName == "Native")
            //    {
            //        mbprojXmlList.RemoveAt(i);
            //        mbprojXmlList.Add(mbproj);
            //        break;
            //    }
            //}

            foreach (MbObjectXmlInformation mbprojXml in mbprojXmlList)
            {
                if (File.Exists(ModuleInfo.GetXmlPathForNative(mbprojXml.ModuleName, mbprojXml.Name)))
                {
                    elementNameList.Add(mbprojXml.Name);
                    toBeMerged.Add(Tuple.Create(ModuleInfo.GetXmlPathForNative(mbprojXml.ModuleName, mbprojXml.Name), string.Empty));
                }
                string xsltPathForNative = ModuleInfo.GetXsltPathForNative(mbprojXml.ModuleName, mbprojXml.Name);
                
                xsltList.Add(File.Exists(xsltPathForNative) ? xsltPathForNative : string.Empty);
            }
            XmlDocument mergedXmlForNative = MBObjectManager.CreateMergedXmlFile(toBeMerged, xsltList, true);

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            mergedXmlForNative.WriteTo(xmlTextWriter);
            baseSkinsXmlPath = elementNameList.First();
            __result = stringWriter.ToString();

            return false;
        }
    }
}
