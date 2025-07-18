using System.Linq;
using Verse;
using Verse.AI;

namespace DSFI
{
    public static class IdleJobUtility
    {
        public static Building FindNearSittableForIdle(Pawn pawn)
        {
            if (!pawn.IsColonist) { return null; }

            var map = pawn.Map;
            if (map == null) { return null; }

            var allNearSittables = AIUtility.FindThingsFromNearPassableRegions(pawn, 3)
                .Where(v => v is Building building && v.Map == pawn.Map && building.def.building.isSittable && building.def.building.isEdifice && pawn.CanReach(building, PathEndMode.OnCell, Danger.None))
                .Cast<Building>()
                .ToList();

            return allNearSittables.Where(v => pawn.CanReserveSittableOrSpot(v.Position)).OrderBy(v => pawn.Position.DistanceToSquared(v.Position)).FirstOrDefault();
        }
    }
}
