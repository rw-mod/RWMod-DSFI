using Verse;
using Verse.AI;

namespace DSFI.JobGivers
{
    public class IdleJobGiver_CleaningGun : IdleJobGiver<IdleJobGiverDef>
    {
        public override Job TryGiveJob(Pawn pawn)
        {
            Thing weapon = null;
            if (pawn.equipment != null && pawn.equipment.Primary != null && pawn.equipment.Primary.def.IsRangedWeapon)
            {
                weapon = pawn.equipment.Primary;
            }

            if (weapon == null)
            {
                return null;
            }
            
            if (weapon.HitPoints >= weapon.MaxHitPoints)
            {
                return null;
            }

            return new Job(IdleJobDefOf.IdleJob_CleaningGun, weapon)
            {
                locomotionUrgency = modSettings.wanderMovePolicy
            };
        }
    }
}
