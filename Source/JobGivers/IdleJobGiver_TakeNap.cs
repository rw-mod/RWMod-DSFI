﻿using RimWorld;
using Verse;
using Verse.AI;

namespace DSFI.JobGivers
{
    public class IdleJobGiver_TakeNap : IdleJobGiver<IdleJobGiverDef_TakeNap>
    {
        public override float GetWeight(Pawn pawn, Trait traitIndustriousness)
        {
            if (pawn.story.traits.HasTrait(TraitDefOf.Undergrounder) || pawn.story.traits.HasTrait(MoreTraitDefOf.QuickSleeper))
            {
                return base.GetWeight(pawn, traitIndustriousness) * 1.5f;
            }

            return base.GetWeight(pawn, traitIndustriousness);
        }

        public override Job TryGiveJob(Pawn pawn)
        {
            Need_Rest need = pawn.needs.rest;
            if (need == null || need.CurLevelPercentage > this.def.restRequirement)
            {
                return null;
            }

            float dayP = GenLocalDate.DayPercent(pawn.Map);
            if (dayP < 0.42f && dayP > 0.7f)
            {
                return null;
            }

            Building_Bed bed = RestUtility.FindBedFor(pawn);
            if (bed != null)
            {
                return new Job(IdleJobDefOf.IdleJob_TakeNap, bed)
                {
                    locomotionUrgency = modSettings.wanderMovePolicy
                };
            }

            return null;
        }
    }
}
