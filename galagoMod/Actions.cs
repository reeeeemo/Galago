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

namespace galagoMod
{
    [Action("StartEulogy")]
    public class StartEulogy : Pathfinder.Action.PathfinderAction
    {
        [XMLStorage]
        public float Seconds;

        // Action Trigger
        public override void Trigger(object os_obj)
        {
            OS os = (OS)os_obj;
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

    [Action("StartLirazBomb")]
    public class StartLirazBomb : Pathfinder.Action.PathfinderAction
    {
        public override void Trigger(object os_obj)
        {
            OS os = (OS)os_obj;
            //BombNetwork.lirazBomb.Start(os, os.getExeBounds());
            //Action<OSUpdateEvent> lirazBombDelegate = UpdateLiraz;


            os.launchExecutable("LirazBomb", LirazBomb.binary, 0);

            //HackerScriptExecuter.runScript("forkbomb", os);

            //EventManager<OSUpdateEvent>.AddHandler(lirazBombDelegate);
        }

        //public void UpdateLiraz(OSUpdateEvent os)
        //{
            //BombNetwork.lirazBomb.Update((float)os.GameTime.ElapsedGameTime.TotalSeconds);
        //}
    }
}
