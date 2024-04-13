using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace DSFI
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rimworld.gguake.dsfi");

            harmony.Patch(AccessTools.Method(typeof(PawnRenderer), nameof(PawnRenderer.RenderCache)),
                transpiler: new HarmonyMethod(typeof(HarmonyPatches), nameof(PawnRenderer_RenderCache_Transpiler)));
        }

        private static IEnumerable<CodeInstruction> PawnRenderer_RenderCache_Transpiler(IEnumerable<CodeInstruction> codeInstructions, ILGenerator iLGenerator)
        {
            var instructions = codeInstructions.ToList();

            var index = instructions.FirstIndexOf(v => v.opcode == OpCodes.Call && v.OperandIs(AccessTools.Method(typeof(PawnRenderer), "GetDrawParms"))) + 2;

            var label = iLGenerator.DefineLabel();
            instructions[index].labels.Add(label);

            instructions.InsertRange(index, new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(PortraitIconMaterialCache), nameof(PortraitIconMaterialCache.isDrawing))),
                new CodeInstruction(OpCodes.Brfalse_S, label),
                new CodeInstruction(OpCodes.Ldloca_S, 3),
                new CodeInstruction(OpCodes.Ldflda, AccessTools.Field(typeof(PawnDrawParms), nameof(PawnDrawParms.skipFlags))),
                new CodeInstruction(OpCodes.Dup),
                new CodeInstruction(OpCodes.Ldind_I8),
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(DSFIRenderSkipFlagDefOf), nameof(DSFIRenderSkipFlagDefOf.Body))),
                new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(RenderSkipFlagDef), "mask")),
                new CodeInstruction(OpCodes.Or),
                new CodeInstruction(OpCodes.Stind_I8),
            });

            return instructions;
        }
    }
}
