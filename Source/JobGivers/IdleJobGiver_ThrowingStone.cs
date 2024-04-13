using RimWorld;
using Verse;
using Verse.AI;

namespace DSFI.JobGivers
{
    public class IdleJobGiver_ThrowingStone : IdleJobGiver<IdleJobGiverDef>
    {
        public override Job TryGiveJob(Pawn pawn)
        {
            if (!JoyUtility.EnjoyableOutsideNow(pawn.Map))
            {
                return null;
            }

            IntVec3 position = AIUtility.FindRandomSpotOutsideColony(pawn, def.searchDistance, canReach: false, canReserve: false);
            if (!position.IsValid)
            {
                return null;
            }

            IntVec3 standPosition = IntVec3.Invalid;
            if (!AIUtility.FindAroundSpotFromTarget(pawn, position, 4.0f, 3.0f, canSee: true, canReach: true, canReserve: true).TryRandomElement(out standPosition))
            {
                return null;
            }
            
            return new Job(IdleJobDefOf.IdleJob_ThrowingStone, position, standPosition)
            {
                locomotionUrgency = modSettings.wanderMovePolicy
            };
        }
    }
}
