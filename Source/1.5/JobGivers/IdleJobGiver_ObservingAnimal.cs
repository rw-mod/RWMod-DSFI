using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace DSFI.JobGivers
{
    public class IdleJobGiver_ObservingAnimal : IdleJobGiver<IdleJobGiverDef>
    {
        public override Job TryGiveJob(Pawn pawn)
        {
            if (!JoyUtility.EnjoyableOutsideNow(pawn.Map))
            {
                return null;
            }

            var animal = GenClosest.ClosestThingReachable(
                pawn.Position,
                pawn.Map,
                ThingRequest.ForGroup(ThingRequestGroup.Pawn),
                PathEndMode.Touch,
                TraverseParms.For(pawn, Danger.None),
                searchRegionsMax: 20,
                validator: (thing) => thing is Pawn p && p.AnimalOrWildMan() && !p.HostileTo(pawn) && Rand.Bool);

            if (animal != null)
            {
                return new Job(IdleJobDefOf.IdleJob_ObservingAnimal, animal)
                {
                    locomotionUrgency = modSettings.wanderMovePolicy
                };
            }

            return null;
        }
    }
}
