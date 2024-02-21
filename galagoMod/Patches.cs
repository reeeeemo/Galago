using galagoMod.Eulogy;
using galagoMod.Executables;
using Hacknet;
using Microsoft.Xna.Framework;
using System;

namespace galagoMod
{
    [HarmonyLib.HarmonyPatch(typeof(OS), nameof(OS.drawModules))]
    public class OSDrawModules
    {
        static void Postfix()
        {
            TraceNetwork.eulogyTracer.Draw(GuiData.spriteBatch);
            //BombNetwork.lirazBomb.Draw(GuiData.spriteBatch);
        }
    }
}
