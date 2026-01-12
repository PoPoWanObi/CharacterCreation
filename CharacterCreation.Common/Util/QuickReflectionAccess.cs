using System;
using System.Reflection;
using HarmonyLib;
using SandBox.GauntletUI.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace CharacterCreation.Common.Util;

public static class QuickReflectionAccess
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