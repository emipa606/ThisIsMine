using HarmonyLib;
using RimWorld;
using Verse;

namespace ThisIsMine;

[HarmonyPatch(typeof(MinifyUtility), "MakeMinified")]
public static class Patch_MakeMinified
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