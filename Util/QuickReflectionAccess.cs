using HarmonyLib;
using SandBox.GauntletUI.Encyclopedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace CharacterCreation.Util
{
    internal static class QuickReflectionAccess
    {
        public static readonly FieldInfo GauntletMapEncyclopediaViewData = AccessTools.Field(typeof(GauntletMapEncyclopediaView), "_encyclopediaData");
        public static readonly FieldInfo EncyclopediaDataDatasource = AccessTools.Field(typeof(EncyclopediaData), "_activeDatasource");
        public static readonly MethodInfo BasicCharacterObjectSetNameMethod = AccessTools.Method(typeof(BasicCharacterObject), "SetName");

        public static void UpdateName(this BasicCharacterObject charObj, TextObject name)
        {
            var func = AccessTools.MethodDelegate<Action<TextObject>>(BasicCharacterObjectSetNameMethod, charObj);
            func(name);
        }
    }

    public static class PatchUtility
    {
        internal static bool Matches(this CodeInstruction instruction, OpCode opcode) => instruction.opcode == opcode;

        internal static bool Matches<T>(this CodeInstruction instruction, OpCode opcode, T operand)
            => instruction.Matches(opcode) && instruction.operand is T t && t.Equals(operand);
    }
}
