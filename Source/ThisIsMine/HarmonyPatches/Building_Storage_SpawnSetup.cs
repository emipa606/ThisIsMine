using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ThisIsMine.HarmonyPatches;

[HarmonyPatch(typeof(Building_Storage), nameof(Building_Storage.SpawnSetup))]
public static class Building_Storage_SpawnSetup
{
    public static readonly HashSet<IntVec3> storageCells = [];

    private static void Postfix(Building_Storage __instance)
    {
        storageCells.AddRange(__instance.AllSlotCells());
    }
}