using System;
using System.Collections.Generic;
using Hacknet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Pathfinder;
using Pathfinder.Port;


namespace galagoMod.Euology
{
    public class Eulogy
    {
        public OS os;
        public Computer source;
        public float totalTimer;
        public float timer;
        public float lastFrameTime;
        public byte active = 0; // 2 = active, 1 = rebooting, 0 = none.
        public SpriteFont font;
        public SoundEffect beep;
        public SoundEffect breakSound;
        public Color color;
        public string prefix;
        public List<TraceKillExe.PointImpactEffect> ImpactEffects = new List<TraceKillExe.PointImpactEffect>(); // I am still not sure what this is LOL
        public Texture2D circle;

        public void Start(OS os, Computer source)
        {
            this.os = os;
            this.source = source;
            color = Color.Green;
            breakSound = os.content.Load<SoundEffect>("SFX/DoomShock");
            lastFrameTime = 0f;
            active = 2;
            os.warningFlash();
            Console.WriteLine("WARNING: EULOGY STARTED.... " + timer);
            prefix = "EULOGY : ";
        }

        public void Stop()
        {
            active = 0;
        }

        public void Update(float t)
        {
            UpdateImpactEffects();
            if (active == 0) return;
            timer -= t * (Settings.AllTraceTimeSlowed ? 0.55f : 1f) * os.traceTracker.trackSpeedFactor; // counting down timer
            if (active == 2)
            {
                if (timer <= 0f) // uh oh crash time!
                {
                    active = 0;
                    timer = 0f;
                    os.timerExpired();
                }
            }
            else if (timer <= 0f) RebootCompleted();

            float percent = timer / totalTimer * 100.0f;
            //float beepPeriod = percent < 45.0f ? (percent);

        }

        private void RebootCompleted()
        {
            throw new NotImplementedException();
        }

        public void UpdateImpactEffects()
        {
            throw new NotImplementedException();
        }
    }
    

}
