using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Dungeon_Crawler_Engine
{
    public class FrameAnimationClass : ICloneable
    {
        Rectangle[] frames;
        int currentFrame = 0;

        double frameLength = 0.01;
        double timer = 0;

        public int FramesPerSecond
        {
            get { return (int) (1 / frameLength); }
            set { frameLength = Math.Max(1f / (float)value,.001f); }
        }

        public Rectangle CurrentRect
        {
            get { return frames[currentFrame]; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = (int)MathHelper.Clamp(value, 0, frames.Length - 1); }
        }

        public FrameAnimationClass(
            int numberofframes,
            int frameheight,
            int framewidth,
            int xoffset,
            int yoffset)
        {
            frames = new Rectangle[numberofframes];

            for (int i = 0; i < numberofframes; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = framewidth;
                rect.Height = frameheight;
                rect.X = xoffset + (i * framewidth);
                rect.Y = yoffset;

                frames[i] = rect;

                
            }
        }

        public void Update(GameTime gameTime)
        {
            timer += (double)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= frameLength)
            {
                timer = 0;
                currentFrame = (currentFrame + 1) % frames.Length;
            }
        }

        private FrameAnimationClass()
        {

        }

        public object Clone()
        {
            FrameAnimationClass f = new FrameAnimationClass();

            f.frameLength = frameLength;
            f.frames = frames;

            return f;
        }
    }
}
