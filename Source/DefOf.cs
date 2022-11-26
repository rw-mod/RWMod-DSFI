﻿using RimWorld;
using Verse;

namespace DSFI
{
    [DefOf]
    public static class IdleJobDefOf
    {
        public static JobDef IdleJob_TakeNap;
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
}
