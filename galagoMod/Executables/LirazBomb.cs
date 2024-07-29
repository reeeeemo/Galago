using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Hacknet;
using HarmonyLib;
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Pathfinder;
using Pathfinder.Action;
using Pathfinder.Event;
using Pathfinder.Event.Gameplay;
using Pathfinder.Executable;
using Pathfinder.Meta.Load;
using Pathfinder.Port;

namespace galagoMod.Executables
{
    
    public class LirazBomb : GameExecutable 
    {
        public static float RAM_CHANGE_PS = 90f;

        public float speed = 1f;

        public int targetRamUse = 999999999;

        public string runnerIP = "";

        public static string binary = "";

        public int binaryScroll = 0;

        public int charsWide = 0;

        public bool frameSwitch = false;

        static string[] args = new string[3];

        public bool wasKilled = false;

        public LirazBomb() : base()
        {
            ramCost = 6;
            runnerIP = "UNKNOWN";
            IdentifierName = "LirazBomb";
        }

        public LirazBomb(string ipFrom) : base()
        {
            ramCost = 6;
            runnerIP = ipFrom;
            IdentifierName = "LirazBomb";
        }

        public LirazBomb(float _speed) : base()
        {
            ramCost = 6;
            runnerIP = "UNKNOWN";
            IdentifierName = "LirazBomb";
            speed = _speed;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            if (binary.Equals(""))
            {
                binary = Computer.generateBinaryString(5064);
            }

            float num = 7.55f;
            num = GuiData.detailfont.MeasureString("0").X - 0.15f;
            charsWide = (int)((float)bounds.Width / num + 0.5f);
        }

        public override void Update(float t)
        {
            base.Update(t);
            if (frameSwitch)
            {
                binaryScroll++;
                if (binaryScroll >= binary.Length - (charsWide + 1))
                {
                    binaryScroll = 0;
                }
            }

            frameSwitch = !frameSwitch;
            if (targetRamUse == ramCost)
            {
                return;
            }
            
            int num = (int)(t * RAM_CHANGE_PS) * (int)speed;
            Console.WriteLine(num);
            if (os.ramAvaliable < num)
            {
                Result = CompletionResult.Success;
                os.Flags.AddFlag("LbombDone");
                return;
            }

            ramCost += num;
            if (ramCost > targetRamUse)
            {
                ramCost = targetRamUse;
            }
        }

        public override void Draw(float t)
        {
            base.Draw(t);
            drawOutline();
            float num = 8f;
            int num2 = binaryScroll;
            if (num2 >= binary.Length - (charsWide + 1))
            {
                num2 = 0;
            }

            Color customColor = new Color(160, 116, 247);

            Vector2 position = new Vector2(bounds.X, bounds.Y);
            while (position.Y < (float)(bounds.Y + bounds.Height) - num)
            {
                spriteBatch.DrawString(GuiData.detailfont, binary.Substring(num2, charsWide), position, customColor);
                num2 += charsWide;
                if (num2 >= binary.Length - (charsWide + 1))
                {
                    num2 = 0;
                }

                position.Y += num;
            }
        }
    }
}
