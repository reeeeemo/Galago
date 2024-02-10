﻿using galagoMod.Euology;
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
        }
    }
}
