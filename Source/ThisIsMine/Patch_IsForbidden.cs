using HarmonyLib;
using RimWorld;
using Verse;

namespace ThisIsMine
{
    [HarmonyPatch(typeof(ForbidUtility), "IsForbidden", typeof(Thing), typeof(Pawn))]
    public static class Patch_IsForbidden
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
}