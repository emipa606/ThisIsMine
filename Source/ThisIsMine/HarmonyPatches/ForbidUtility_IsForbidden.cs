using HarmonyLib;
using RimWorld;
using Verse;

namespace ThisIsMine.HarmonyPatches;

[HarmonyPatch(typeof(ForbidUtility), nameof(ForbidUtility.IsForbidden), typeof(Thing), typeof(Pawn))]
public static class ForbidUtility_IsForbidden
{
    private static void Postfix(ref bool __result, Thing t, Pawn pawn)
    {
        if (__result)
        {
            return;
        }

        if (!HarmonyInit.PawnCanHaveIt(pawn, t))
        {
            __result = true;
        }
    }
}