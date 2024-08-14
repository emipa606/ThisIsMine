using HarmonyLib;
using RimWorld;
using Verse;

namespace ThisIsMine.HarmonyPatches;

[HarmonyPatch(typeof(MinifyUtility), nameof(MinifyUtility.MakeMinified_NewTemp))]
public static class MinifyUtility_MakeMinified_NewTemp
{
    private static void Postfix(ref MinifiedThing __result)
    {
        if (__result == null)
        {
            return;
        }

        var thing = __result.GetInnerIfMinified();
        var canBelongComp = thing.TryGetComp<CompCanBelongToRoomOwners>();
        if (canBelongComp == null)
        {
            return;
        }

        if (CompCanBelongToRoomOwners.privateThings.Contains(thing))
        {
            CompCanBelongToRoomOwners.privateThings.Remove(thing);
        }

        canBelongComp.belongsToCell = IntVec3.Invalid;
        canBelongComp.belongsToRoomOwners = false;
    }
}