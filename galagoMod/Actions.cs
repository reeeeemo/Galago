using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using galagoMod.Euology;
using Hacknet;
using Pathfinder.Event;
using Pathfinder.Event.Gameplay;
using Pathfinder.Meta.Load;
using Pathfinder.Util;

namespace galagoMod
{
    [Action("StartEulogy")]
    public class StartEulogy : Pathfinder.Action.PathfinderAction
    {
        static Eulogy eTracer = new Eulogy();
        [XMLStorage]
        public float Seconds;
        public override void Trigger(object os_obj)
        {
            OS os = (OS)os_obj;
            eTracer.Start(os, Seconds);
            Action<OSUpdateEvent> eulogyUpdateDelegate = UpdateEulogy;

            EventManager<OSUpdateEvent>.AddHandler(eulogyUpdateDelegate);
        }

        public void UpdateEulogy(OSUpdateEvent os)
        {
            eTracer.Update((float)os.GameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
