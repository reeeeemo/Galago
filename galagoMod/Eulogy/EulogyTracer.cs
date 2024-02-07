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

        public void Start(OS os, float seconds)
        {
            this.os = os;
            color = Color.Green;
            breakSound = os.content.Load<SoundEffect>("SFX/DoomShock");
            totalTimer = seconds;
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
            UpdateImpactEffects(t);
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

            float percent = timer / totalTimer * 100.0f;
            float beepPeriod = percent < 45.0f ? (percent < 15.0f ? 1f : 5f) : 10f;
            if (percent % beepPeriod > lastFrameTime % beepPeriod)
            {
                TraceTracker.beep.Play(0.5f, 0, 0);
                os.warningFlash();
            }
            lastFrameTime = percent;
        }

        public void Draw(SpriteBatch sb)
        {
            if (active == 0) return;
            string text = (timer / totalTimer * 100.0).ToString("00.00");
            Vector2 vec2 = TraceTracker.font.MeasureString(text);
            Vector2 position = new Vector2(10f, sb.GraphicsDevice.Viewport.Height - vec2.Y);
            if (os.traceTracker.active) position.Y -= vec2.Y + 14f;
            sb.DrawString(TraceTracker.font, text, position, color);
            position.Y -= 25f;
            sb.DrawString(TraceTracker.font, prefix, position, color, 0.0f, Vector2.Zero, new Vector2(0.3f), SpriteEffects.None, 0.5f);
        }

        public void UpdateImpactEffects(float t)
        {
            for (int index = 0; index < ImpactEffects.Count; ++index)
            {
                TraceKillExe.PointImpactEffect impEffect = ImpactEffects[index];
                impEffect.timeEnabled += t;
                if (impEffect.timeEnabled > 5f)
                {
                    ImpactEffects.RemoveAt(index);
                    --index;
                }
                else ImpactEffects[index] = impEffect;
            }
        }

        public void DrawImpactEffects(SpriteBatch sb, List<TraceKillExe.PointImpactEffect> Effects)
        {
            foreach (TraceKillExe.PointImpactEffect effect in Effects)
            {
                Color color = Color.Lerp(Hacknet.Utils.AddativeWhite, Hacknet.Utils.AddativeRed, (float)(0.600000023841858 + 0.400000005960464 * (double)Hacknet.Utils.LCG.NextFloatScaled())) * (float)(0.600000023841858 + 0.400000005960464 * (double)Hacknet.Utils.LCG.NextFloatScaled());
                Vector2 location = effect.location;
                float num1 = Hacknet.Utils.QuadraticOutCurve(effect.timeEnabled / DLCIntroExe.NodeImpactEffectTransInTime);
                float num2 = Hacknet.Utils.QuadraticOutCurve(Hacknet.Utils.QuadraticOutCurve(effect.timeEnabled / (DLCIntroExe.NodeImpactEffectTransInTime + DLCIntroExe.NodeImpactEffectTransOutTime)));
                float num3 = Hacknet.Utils.QuadraticOutCurve((effect.timeEnabled - DLCIntroExe.NodeImpactEffectTransInTime) / DLCIntroExe.NodeImpactEffectTransOutTime);
                effect.cne.color = color * num1;
                effect.cne.ScaleFactor = num2 * effect.scaleModifier;
                if (effect.timeEnabled > DLCIntroExe.NodeImpactEffectTransInTime)
                    effect.cne.color = color * (1f - num3);
                if (num1 >= 0.0f && effect.HasHighlightCircle)
                    sb.Draw(circle, location, new Rectangle?(), color * (float)(1.0 - (double)num1 - ((double)num3 >= 0.0 ? 1.0 - (double)num3 : 0.0)), 0.0f, new Vector2(circle.Width / 2f, circle.Height / 2f), (num1 / circle.Width * 60f), SpriteEffects.None, 0.7f);
                effect.cne.draw(sb, location);
            }
        }
    }
    

}
