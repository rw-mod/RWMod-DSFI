using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace DSFI.JobGivers
{
    public class IdleJobGiver_ObservingAnimal : IdleJobGiver<IdleJobGiverDef>
    {
        static HashSet<Pawn> pawns = new HashSet<Pawn>();
        public override Job TryGiveJob(Pawn pawn)
        {
            if (!JoyUtility.EnjoyableOutsideNow(pawn.Map))
            {
                return null;
            }

            pawns.Clear();
            foreach (Thing thing in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, this.def.searchDistance, true))
            {
                Pawn targetPawn = thing as Pawn;
                if (targetPawn == null || !targetPawn.RaceProps.Animal)
                {
                    continue;
                }

                if (targetPawn.HostileTo(pawn))
                {
                    continue;
                }

                pawns.Add(targetPawn);
            }

            if (pawns.Any())
            {
                Pawn target = pawns.RandomElement();
                return new Job(IdleJobDefOf.IdleJob_ObservingAnimal, target)
                {
                    locomotionUrgency = modSettings.wanderMovePolicy
                };
            }

            return null;
        }
    }
}
