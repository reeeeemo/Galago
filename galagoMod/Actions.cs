using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using galagoMod.Eulogy;
using Hacknet;
using Microsoft.Xna.Framework;
using Pathfinder.Event;
using Pathfinder.Event.Gameplay;
using Pathfinder.Meta.Load;
using Pathfinder.Util;
using galagoMod.Executables;
using Pathfinder.Executable;
using HarmonyLib;

namespace galagoMod
{
    // Starts Custom Tracer, called Eulogy
    [Action("StartEulogy")]
    public class StartEulogy : Pathfinder.Action.DelayablePathfinderAction
    {
        [XMLStorage]
        public float Seconds;

        // Action Trigger
        public override void Trigger(OS os)
        {
            TraceNetwork.eulogyTracer.Start(os, Seconds);
            Action<OSUpdateEvent> eulogyUpdateDelegate = UpdateEulogy;

            EventManager<OSUpdateEvent>.AddHandler(eulogyUpdateDelegate);
        }

        // Patch to Update Eulogy
        public void UpdateEulogy(OSUpdateEvent os)
        {
            if (TraceNetwork.eulogyTracer.active == 0)
            {
                EventManager<OSUpdateEvent>.RemoveHandler(UpdateEulogy);
                return;
            }
            TraceNetwork.eulogyTracer.Update((float)os.GameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    // Starts custom-made forkbomb, called LirazBomb
    [Action("StartLirazBomb")]
    public class StartLirazBomb : Pathfinder.Action.DelayablePathfinderAction
    {
        [XMLStorage]
        public float speed;

        public override void Trigger(OS os)
        {
            LirazBomb exe;
            if (speed > 1f)
                exe = new LirazBomb(speed);
            else
                exe = new LirazBomb();

            var location = new Rectangle(os.ram.bounds.X, os.ram.bounds.Y + RamModule.contentStartOffset,
                RamModule.MODULE_WIDTH, (int)OS.EXE_MODULE_HEIGHT);
            var args = new string[] { "arg1", "arg2" };

            os.AddGameExecutable(exe, location, args);

            os.launchExecutable(exe.IdentifierName, LirazBomb.binary, 0);
        }
    }

    // Disables ability to copy a file, by silencing connected computer.
    [Action("DisableCopyFile")]
    public class DisableCopyFile : Pathfinder.Action.DelayablePathfinderAction
    {
        public override void Trigger(OS os)
        {
            Console.WriteLine("copy galago started");
            PatchVariables.SCP_ENABLED = false;
        }
    }

    // Enables ability to copy a file, by unsilencing connected computer.
    [Action("EnableCopyFile")]
    public class EnableCopyFile : Pathfinder.Action.DelayablePathfinderAction
    {
        public override void Trigger(OS os)
        {
            Console.WriteLine("copy galago started");
            PatchVariables.SCP_ENABLED = true;
        }
    }

    // Sets color for terminal text!
    [Action("SetTerminalColor")]
    public class SetTerminalColor : Pathfinder.Action.DelayablePathfinderAction
    {
        [XMLStorage]
        public string Color;

        public override void Trigger(OS os)
        {
            os.terminalTextColor = Utils.convertStringToColor(Color);
        }
    }
}
