using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace DSFI
{
    public class IdleJobGiverDef : Def
    {
        public Type giverClass;
        public float probabilityWeight;
        public int usefulness;
        public float searchDistance = 16f;

        public WorkTags workTagsRequirement = WorkTags.None;
        public List<WorkTypeDef> workTypeRequirement = new List<WorkTypeDef>();
        public List<PawnCapacityDef> pawnCapacityRequirement = new List<PawnCapacityDef>();
        public List<SkillDef> relatedSkillPassion = new List<SkillDef>();
    }

    public class IdleJobGiverDef_WatchDoing : IdleJobGiverDef
    {
        public JobDef jobDef;
        public Type targetJobDriver;
    }

    public class IdleJobGiverDef_LookAroundRoom : IdleJobGiverDef
    {
        public int requiredOpinion;
    }

    public class IdleJobGiverDef_Thinking : IdleJobGiverDef
    {
        public int requiredOpinion;
    }
}