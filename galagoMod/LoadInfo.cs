using BepInEx.Hacknet;
using BepInEx;
using Microsoft.Xna.Framework.Graphics;

namespace galagoMod
{
    [BepInPlugin(ModGUID, ModName, ModVer)]
    public class GalagoPlugin : HacknetPlugin
    {
        public const string ModGUID = "com.Windows10CE.Template";
        public const string ModName = "Galago";
        public const string ModVer = "1.0.0";

        public override bool Load()
        {
            return true;
        }
    }
}
