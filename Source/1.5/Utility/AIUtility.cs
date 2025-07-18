using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace DSFI
{
    public static class AIUtility
    {
        public static IntVec3 FindRandomSpotOutsideColony(Pawn pawn, float distance = -1f, bool canReach = false, bool canReserve = false)
        {
            IntVec3 position = IntVec3.Invalid;
            RCellFinder.TryFindRandomSpotJustOutsideColony(pawn.Position, pawn.Map, pawn, out position, (IntVec3 x) =>
            {
                if (!x.InBounds(pawn.Map) || !x.Walkable(pawn.Map))
                {
                    return false;
                }

                if (x.IsForbidden(pawn))
                {
                    return false;
                }

                if (distance > 0f && x.DistanceToSquared(pawn.Position) > distance * distance)
                {
                    return false;
                }

                if (canReach && !pawn.CanReach(x, PathEndMode.OnCell, Danger.None))
                {
                    return false;
                }

                if (canReserve && !pawn.CanReserve(x))
                {
                    return false;
                }

                return true;
            });
            
            return position;
        }

        public static IEnumerable<IntVec3> FindAroundSpotFromTarget(Pawn pawn, IntVec3 target, float maxRadius, float minRadius, bool canSee = true, bool canReach = false, bool canReserve = false)
        {
            return GenRadial.RadialCellsAround(target, maxRadius, true).Where((IntVec3 x) =>
            {
                if (!x.InBounds(pawn.Map) || !x.Walkable(pawn.Map))
                {
                    return false;
                }

                if (x.IsForbidden(pawn))
                {
                    return false;
                }

                if (x.DistanceToSquared(target) <= minRadius * minRadius)
                {
                    return false;
                }

                if (canReach && !pawn.CanReach(x, PathEndMode.OnCell, Danger.None))
                {
                    return false;
                }

                if (canReserve && !pawn.CanReserve(x))
                {
                    return false;
                }

                if (canSee & !GenSight.LineOfSight(target, x, pawn.Map))
                {
                    return false;
                }

                return true;
            });
        }

        public static IntVec3 FindRandomSpotInZone(Pawn pawn, Zone zone, bool canReach = false, bool canReserve = false)
        {
            IntVec3 position = IntVec3.Invalid;
            if (zone.cells.Where((IntVec3 x) =>
            {
                if (!x.InBounds(pawn.Map) || !x.Walkable(pawn.Map))
                {
                    return false;
                }

                if (x.IsForbidden(pawn))
                {
                    return false;
                }

                if (canReach && !pawn.CanReach(x, PathEndMode.OnCell, Danger.None))
                {
                    return false;
                }

                if (canReserve && !pawn.CanReserve(x))
                {
                    return false;
                }

                return true;

            }).TryRandomElement(out position))
            {
                return position;
            }
            else
            {
                return IntVec3.Invalid;
            }
        }

        private static void TraverseNearPassableRegionRecursive(HashSet<Region> set, Region r, int n)
        {
            if (n <= 0) { return; }
            foreach (var t in r.Neighbors)
            {
                set.Add(t);
                TraverseNearPassableRegionRecursive(set, t, n - 1);
            }
        }

        private static void TraverseNearPassableDistrictRecursive(HashSet<District> set, District v, int n)
        {
            if (n <= 0) { return; }
            foreach (var t in v.Neighbors)
            {
                set.Add(t);
                TraverseNearPassableDistrictRecursive(set, t, n - 1);
            }
        }

        public static IEnumerable<Thing> FindThingsFromNearPassableRegions(Pawn pawn, int depth = 3)
        {
            var originalRegion = pawn.GetRegion();
            if (originalRegion == null) { return Enumerable.Empty<Thing>(); }

            var allTargetRegions = new HashSet<Region>() { originalRegion };
            TraverseNearPassableRegionRecursive(allTargetRegions, originalRegion, depth);

#if DEBUG
            Log.Message($"FindThingsFromNearPassableRegions origin: {originalRegion} nears: {string.Join(",", allTargetRegions.Select(v => v.ToString()))}");
#endif

            var allThings = allTargetRegions.SelectMany(v => v.ListerThings.AllThings);
            return allThings;
        }
    }
}
