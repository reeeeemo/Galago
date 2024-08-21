using galagoMod.Eulogy;
using galagoMod.Executables;
using Hacknet;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace galagoMod
{
    static class PatchVariables
    {
        public static bool SCP_ENABLED = false;
        public static List<KeyValuePair<string, Color>> History = new List<KeyValuePair<string, Color>>();

        
        public static void AddToHistory(List<string> lines, Color color)
        {
            foreach (string line in lines)
            {
                History.Add(new KeyValuePair<string, Color>(line, color));
            }
        }

        public static void AddSameLineToHistory(string text, Color color, Rectangle bounds)
        {
            int size = History.Count;
            if (size <= 0 || GuiData.tinyfont.MeasureString(History[size - 1] + text).X > (float)(bounds.Width - 6))
            { 
                Console.WriteLine("added" + text);
                History.Add(new KeyValuePair<string, Color>(text, color));
            } else
            {
                string updatedKey = History[size - 1].Key + text;

                Console.WriteLine("updated" + updatedKey);
                History[size - 1] = new KeyValuePair<string, Color>(updatedKey, color);
            }
        }
    }

    [HarmonyPatch(typeof(OS), nameof(OS.drawModules))]
    public class OSDrawModules
    {
        static void Postfix()
        {
            TraceNetwork.eulogyTracer.Draw(GuiData.spriteBatch);
        }
    }

    #region Terminal Color Patches
    [HarmonyPatch(typeof(Terminal), nameof(Terminal.Draw))]
    public class TerminalDrawColor
    {
        static void Postfix(Terminal __instance)
        {
            float tinyFontCharHeight = GuiData.ActiveFontConfig.tinyFontCharHeight;
            int num = (int)((float)(__instance.bounds.Height - 12) / (tinyFontCharHeight + 1f));
            num -= 3;
            num = Math.Min(num, PatchVariables.History.Count);
            Vector2 input = new Vector2(__instance.bounds.X + 4, (float)(__instance.bounds.Y + __instance.bounds.Height) - tinyFontCharHeight * 5f);
            for (int num2 = PatchVariables.History.Count; num2 > PatchVariables.History.Count - num; num2--)
            {
                try
                {
                    __instance.spriteBatch.DrawString(GuiData.tinyfont, PatchVariables.History[num2 - 1].Key, Utils.ClipVec2ForTextRendering(input), PatchVariables.History[num2 - 1].Value);
                    input.Y -= tinyFontCharHeight + 1f;
                }
                catch (Exception)
                {
                }
            }
        }
    }

    [HarmonyPatch(typeof(Terminal), nameof(Terminal.executeLine))]
    public class TerminalExecuteLine
    {
        static void Postfix(Terminal __instance)
        {
            var history = __instance.history;
            PatchVariables.AddToHistory(history, __instance.os.terminalTextColor);
            __instance.history.Clear();
        }
    }

    [HarmonyPatch(typeof(Terminal), nameof(Terminal.write))]
    public class TerminalWrite
    {
        static bool Prefix(Terminal __instance, string text)
        {
            PatchVariables.AddSameLineToHistory(text, __instance.os.terminalTextColor, __instance.bounds);
            __instance.history.Clear();
            return false;
        }
    }

    [HarmonyPatch(typeof(Terminal), nameof(Terminal.writeLine))]
    public class TerminalWriteLine
    {
        static void Postfix(Terminal __instance)
        {
            var history = __instance.history;
            PatchVariables.AddToHistory(history, __instance.os.terminalTextColor);
            __instance.history.Clear();
        }
    }

    [HarmonyPatch(typeof(Terminal), nameof(Terminal.NonThreadedInstantExecuteLine))]
    public class TerminalExecuteLineNonThreaded
    {
        static void Prefix(Terminal __instance)
        {
            var history = __instance.history;
            PatchVariables.AddToHistory(history, __instance.os.terminalTextColor);
            __instance.history.Clear();
        }
    }

    [HarmonyPatch(typeof(Terminal), nameof(Terminal.reset))]
    public class TerminalReset
    {
        static void Postfix(Terminal __instance)
        {
            PatchVariables.History.Clear();
        }
    }
    #endregion

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
