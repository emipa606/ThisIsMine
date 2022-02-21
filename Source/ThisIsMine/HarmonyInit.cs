using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ThisIsMine;

[StaticConstructorOnStartup]
public static class HarmonyInit
{
    static HarmonyInit()
    {
        var harmony = new Harmony("Elseud.ThisIsMine");
        harmony.PatchAll();
        foreach (var furniture in DefDatabase<ThingDef>.AllDefs.Where(x =>
                     x.thingCategories?.Contains(ThingCategoryDef.Named("BuildingsFurniture")) ?? false))
        {
            if (furniture.IsBed)
            {
                continue;
            }

            if (furniture.comps is null)
            {
                furniture.comps = new List<CompProperties>();
            }

            furniture.comps.Add(new CompProperties_CanBelongToRoomOwners());
        }

        if (!ModLister.IdeologyInstalled)
        {
            return;
        }

        ThingDef.Named("SleepAccelerator").comps.Add(new CompProperties_CanBelongToRoomOwners());
    }

    public static bool PawnCanHaveIt(Pawn pawn, Thing thing)
    {
        if (CompCanBelongToRoomOwners.privateThings.Contains(thing))
        {
            var comp = thing.TryGetComp<CompCanBelongToRoomOwners>();
            if (comp.belongsToCell.IsValid)
            {
                var room = comp.belongsToCell.GetRoom(thing.Map);
                if (room != null && room.Owners.Contains(pawn))
                {
                    return true;
                }
            }
            else if (thing.GetRoom() != null && thing.GetRoom().Owners.Contains(pawn))
            {
                return true;
            }

            return false;
        }

        if (thing.def.category != ThingCategory.Item)
        {
            return true;
        }

        if (Patch_SpawnSetup.storageCells.Contains(thing.Position))
        {
            var container = thing.Position.GetFirstThing<Building_Storage>(pawn.Map);
            if (container != null && container != thing && !PawnCanHaveIt(pawn, container))
            {
                return false;
            }
        }
        else
        {
            if (thing.ParentHolder is Building_Storage container2 && !PawnCanHaveIt(pawn, container2))
            {
                return false;
            }
        }

        return true;
    }
}