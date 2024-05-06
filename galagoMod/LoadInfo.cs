using BepInEx.Hacknet;
using BepInEx;
using Microsoft.Xna.Framework.Graphics;
using galagoMod.Executables;
using Hacknet;
using Pathfinder.Executable;

namespace galagoMod
{
    [BepInPlugin(ModGUID, ModName, ModVer)]
    public class GalagoPlugin : HacknetPlugin
    {
        public const string ModGUID = "com.reeeeemo.galago";
        public const string ModName = "Galago";
        public const string ModVer = "1.0.1";

        public override bool Load()
        {
            HarmonyInstance.PatchAll();


            return true;
        }
    }
}
