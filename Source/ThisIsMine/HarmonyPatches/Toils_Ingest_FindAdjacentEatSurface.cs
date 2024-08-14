using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace ThisIsMine;

[HarmonyPatch(typeof(Toils_Ingest), nameof(Toils_Ingest.FindAdjacentEatSurface))]
public static class Toils_Ingest_FindAdjacentEatSurface
{
    private static bool Prefix(ref Toil __result, TargetIndex eatSurfaceInd, TargetIndex foodInd)
    {
        var toil = new Toil();
        toil.initAction = delegate
        {
            var actor = toil.actor;
            var position = actor.Position;
            var num = 0;
            IntVec3 intVec;
            while (true)
            {
                if (num >= 4)
                {
                    return;
                }

                intVec = position + new Rot4(num).FacingCell;
                if (CanEatOn(actor, intVec))
                {
                    break;
                }

                num++;
            }

            toil.actor.CurJob.SetTarget(eatSurfaceInd, intVec);
            toil.actor.jobs.curDriver.rotateToFace = eatSurfaceInd;
            var thing = toil.actor.CurJob.GetTarget(foodInd).Thing;
            if (thing.def.rotatable)
            {
                thing.Rotation = Rot4.FromIntVec3(intVec - toil.actor.Position);
            }
        };
        toil.defaultCompleteMode = ToilCompleteMode.Instant;
        __result = toil;
        return false;
    }

    private static bool CanEatOn(Pawn pawn, IntVec3 intVec)
    {
        var thingList = intVec.GetThingList(pawn.Map);
        foreach (var thing in thingList)
        {
            if (thing.def.surfaceType == SurfaceType.Eat && HarmonyInit.PawnCanHaveIt(pawn, thing))
            {
                return true;
            }
        }

        return false;
    }
}