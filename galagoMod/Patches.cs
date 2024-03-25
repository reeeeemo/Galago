using galagoMod.Eulogy;
using galagoMod.Executables;
using Hacknet;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System;

namespace galagoMod
{
    static class PatchVariables
    {
        public static bool SCP_ENABLED = false;
    }

    [HarmonyLib.HarmonyPatch(typeof(OS), nameof(OS.drawModules))]
    public class OSDrawModules
    {
        static void Postfix()
        {
            TraceNetwork.eulogyTracer.Draw(GuiData.spriteBatch);
        }
    }

    [HarmonyPatch]
    public class ComputerCopyPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Computer), nameof(Computer.canCopyFile))]
        private static bool CanCopyFilePrefix(Computer __instance, ref bool __result)
        {
            if (!PatchVariables.SCP_ENABLED)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }

}
