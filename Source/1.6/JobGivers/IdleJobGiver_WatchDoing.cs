﻿using RimWorld;
using Verse;
using Verse.AI;

namespace DSFI.JobGivers
{
    public class IdleJobGiver_WatchDoing : IdleJobGiver<IdleJobGiverDef_WatchDoing>
    {
        public override float GetWeight(Pawn pawn, Trait traitIndustriousness)
        {
            if (pawn.story.traits.HasTrait(TraitDefOf.Bloodlust))
            {
                return 0f;
            }

            if (pawn.story.traits.HasTrait(TraitDefOf.Kind))
            {
                return base.GetWeight(pawn, traitIndustriousness) * 1.8f;
            }

            return base.GetWeight(pawn, traitIndustriousness);
        }

        public override Job TryGiveJob(Pawn pawn)
        {
            Pawn targetPawn = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForGroup(ThingRequestGroup.Pawn), PathEndMode.Touch, TraverseParms.For(pawn, Danger.None, TraverseMode.ByPawn, false),
                maxDistance: this.def.searchDistance, validator: (Thing x) =>
                {
                    Pawn p = x as Pawn;
                    if (p.CurJob == null || p.CurJobDef.driverClass != this.def.targetJobDriver)
                    {
                        return false;
                    }

                    return true;
                }) as Pawn;

            if (targetPawn != null)
            {
                return new Job(this.def.jobDef, targetPawn)
                {
                    locomotionUrgency = modSettings.wanderMovePolicy
                };
            }

            return null;
        }
    }
}
