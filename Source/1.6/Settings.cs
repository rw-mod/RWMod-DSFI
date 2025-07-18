using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace DSFI
{
    public class DSFISettings : ModSettings
    {
        public float wanderMultiplier = 1.0f;
        public LocomotionUrgency wanderMovePolicy = LocomotionUrgency.Walk;

        public Dictionary<string, bool> idleJobActivated = new Dictionary<string, bool>();

        public override void ExposeData()
        {
            Scribe_Values.Look(ref wanderMultiplier, "wanderMultiplier", 1.0f);
            Scribe_Values.Look(ref wanderMovePolicy, "wanderMovePolicy", LocomotionUrgency.Walk);

            Scribe_Collections.Look(ref idleJobActivated, "idleJobActivated", keyLookMode: LookMode.Value, valueLookMode: LookMode.Value);
            base.ExposeData();
        }
    }

    public class DSFIMod : Mod
    {
        DSFISettings settings;

        public DSFIMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<DSFISettings>();

        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            //#region wanderMultiplier
            //listing.Label("DSFI_ConfigWanderMultiplier".Translate(), tooltip: "DSFI_TT_ConfigWanderMultiplier".Translate());
            //settings.wanderMultiplier = Widgets.HorizontalSlider(listing.GetRect(22f), settings.wanderMultiplier, 0.01f, 2f, false, rightAlignedLabel: string.Format("{0:f2}", settings.wanderMultiplier));
            //#endregion

            //listing.Gap();

            #region wanderMovePolicy
            listing.Label("DSFI_ConfigWanderMovePolicy".Translate());

            bool wanderMoveWalk = settings.wanderMovePolicy == LocomotionUrgency.Walk;
            Widgets.CheckboxLabeled(listing.GetRect(22f), "DSFI_ConfigWanderMovePolicy_Walk".Translate(), ref wanderMoveWalk);
            if (wanderMoveWalk)
            {
                settings.wanderMovePolicy = LocomotionUrgency.Walk;
            }

            bool wanderMoveRun = settings.wanderMovePolicy == LocomotionUrgency.Jog;
            Widgets.CheckboxLabeled(listing.GetRect(22f), "DSFI_ConfigWanderMovePolicy_Run".Translate(), ref wanderMoveRun);
            if (wanderMoveRun)
            {
                settings.wanderMovePolicy = LocomotionUrgency.Jog;
            }
            #endregion

            listing.Gap();

            #region idleJobSettings
            listing.Label("DSFI_ConfigActivatedIdleJob".Translate());

            if (settings.idleJobActivated == null)
            {
                settings.idleJobActivated = new Dictionary<string, bool>();
            }

            foreach (var jobGiverDef in DefDatabase<IdleJobGiverDef>.AllDefsListForReading)
            {
                if (jobGiverDef == IdleJobGiverDefOf.IdleJobGiver_Wander) { continue; }

                if (!settings.idleJobActivated.TryGetValue(jobGiverDef.defName, out var activated))
                {
                    activated = true;
                    settings.idleJobActivated.Add(jobGiverDef.defName, activated);
                }

                listing.CheckboxLabeled(jobGiverDef.LabelCap, ref activated, 22f);
                settings.idleJobActivated[jobGiverDef.defName] = activated;
            }
            #endregion

            listing.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Do Something for Idle";
        }
    }
}
