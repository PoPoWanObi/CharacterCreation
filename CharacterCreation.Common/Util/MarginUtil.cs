using System;
using TaleWorlds.Engine;

namespace CharacterCreation.Common.Util;

public static class MarginUtil
{
    // I think this is probably a better approach than the margin
    // library, however, I'm sure someone with more experience in
    // this field could improve this. It's accurate enough to 
    // work, though ~ PoPo

    public static int GetTopMarginForAspectRatio()
    {
        // This isn't entirely accurate, but it's very close -- should suffice
        const double a = 686.6034216162005;
        const double b = -2.6609724694782133;

        return (int)(a * Math.Pow(Screen.AspectRatio, b) + 10);
    }

    public static int GetRightMarginForAspectRatio()
    {
        const double a = 80.0;
        const double b = 262.5;

        return (int)(a * Screen.AspectRatio + b ) - 25;
    }
}