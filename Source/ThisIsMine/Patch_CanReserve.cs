using HarmonyLib;
using Verse;
using Verse.AI;

namespace ThisIsMine;

[HarmonyPatch(typeof(ReservationManager), "CanReserve")]
public static class Patch_CanReserve
{
    private static void Postfix(ref bool __result, Pawn claimant, LocalTargetInfo target)
    {
        if (!__result)
        {
            return;
        }

        if (target.HasThing && !HarmonyInit.PawnCanHaveIt(claimant, target.Thing))
        {
            __result = false;
        }
    }
}