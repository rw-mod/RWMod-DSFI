using RimWorld;
using Verse;

namespace DSFI
{
    [DefOf]
    public static class IdleJobGiverDefOf
    {
        public static IdleJobGiverDef IdleJobGiver_Wander;
    }

    [DefOf]
    public static class IdleJobDefOf
    {
        public static JobDef IdleJob_TakeBreakTime;
        public static JobDef IdleJob_MendItem;
        public static JobDef IdleJob_ObservingAnimal;
        public static JobDef IdleJob_MessingAround;
        public static JobDef IdleJob_LookAroundRoom;
        public static JobDef IdleJob_PracticeMelee;
        public static JobDef IdleJob_ThrowingStone;
        public static JobDef IdleJob_Graffiti;
        public static JobDef IdleJob_CleaningGun;
        public static JobDef IdleJob_Gardening;
        public static JobDef IdleJob_Thinking;
    }

    [DefOf]
    public static class MoreTraitDefOf
    {
        public static TraitDef QuickSleeper;
        public static TraitDef Neurotic;
    }

    [DefOf]
    public static class MoteDefOf
    {
        public static ThingDef DSFI_Mote_Thought;
    }

    [DefOf]
    public static class DSFIThingDefOf
    {
        public static ThingDef DSFI_Scribbling;
    }

    [DefOf]
    public static class DSFIRoomRoleDefOf
    {
        public static RoomRoleDef DiningRoom;
        public static RoomRoleDef RecRoom;
    }

    [DefOf]
    public static class DSFIRenderSkipFlagDefOf
    {
        public static RenderSkipFlagDef Body;
    }
}
