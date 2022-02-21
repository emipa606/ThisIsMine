using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace ThisIsMine;

public class CompCanBelongToRoomOwners : ThingComp
{
    public static readonly HashSet<Thing> privateThings = new HashSet<Thing>();
    public IntVec3 belongsToCell = IntVec3.Invalid;
    public bool belongsToRoomOwners;

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        base.PostSpawnSetup(respawningAfterLoad);
        if (belongsToRoomOwners)
        {
            privateThings.Add(parent);
        }
    }

    public override void PostDrawExtraSelectionOverlays()
    {
        base.PostDrawExtraSelectionOverlays();
        if (belongsToCell.IsValid)
        {
            GenDraw.DrawLineBetween(parent.TrueCenter(), belongsToCell.ToVector3());
        }
    }

    public static TargetingParameters ForRoom()
    {
        var targetingParameters = new TargetingParameters { canTargetLocations = true };
        return targetingParameters;
    }

    public override IEnumerable<Gizmo> CompGetGizmosExtra()
    {
        var command_Action = new Command_Action
        {
            defaultLabel = belongsToRoomOwners
                ? "TIM.BelongsToRoomPrivate".Translate()
                : "TIM.BelongsToRoomCommon".Translate(),
            icon = belongsToRoomOwners
                ? ContentFinder<Texture2D>.Get("UI/Commands/Icon-Public")
                : ContentFinder<Texture2D>.Get("UI/Commands/Icon-Assign_to_Bed"),
            defaultDesc = "TIM.BelongsToRoomDesc".Translate(),
            action = delegate
            {
                belongsToRoomOwners = !belongsToRoomOwners;
                if (belongsToRoomOwners)
                {
                    privateThings.Add(parent);
                }
                else
                {
                    privateThings.Remove(parent);
                }
            },
            hotKey = KeyBindingDefOf.Misc2
        };
        yield return command_Action;

        yield return new Command_Action
        {
            action = delegate
            {
                Find.Targeter.BeginTargeting(ForRoom(), delegate(LocalTargetInfo x) { belongsToCell = x.Cell; },
                    null, null);
            },
            defaultLabel = "TIM.ConnectToRoom".Translate(),
            hotKey = KeyBindingDefOf.Misc3,
            defaultDesc = "TIM.ConnectToRoomDesc".Translate(),
            icon = ContentFinder<Texture2D>.Get("UI/Commands/AssignToRoomIcon", false)
        };
        yield return new Command_Action
        {
            action = delegate
            {
                belongsToRoomOwners = false;
                belongsToCell = IntVec3.Invalid;
            },
            defaultLabel = "TIM.UnassignFromRoom".Translate(),
            hotKey = KeyBindingDefOf.Misc5,
            defaultDesc = "TIM.UnassignFromRoomDesc".Translate(),
            icon = ContentFinder<Texture2D>.Get("UI/Commands/Icon-Unassign_from_room", false)
        };
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref belongsToRoomOwners, "belongsToRoomOwners");
        Scribe_Values.Look(ref belongsToCell, "belongsToCell");
    }
}