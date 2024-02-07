using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using galagoMod.Euology;
using Hacknet;
using Pathfinder.Util;

namespace galagoMod
{
    [Pathfinder.Meta.Load.Action("StartEulogy")]
    public class StartEulogy : Pathfinder.Action.PathfinderAction
    {
        [XMLStorage]
        public float Seconds;
        public override void Trigger(object os_obj)
        {
            OS os = (OS)os_obj;
            Eulogy eTracer = new Eulogy();
            eTracer.Start(os, Seconds);
        }
    }
}
