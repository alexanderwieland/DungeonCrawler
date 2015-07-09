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
    public class AnimatedSprite
    {
        public Dictionary<string, FrameAnimationClass> Animations =
            new Dictionary<string, FrameAnimationClass>();

        string currentAnimation = null;

        Texture2D texture;

        public Vector2 Position = Vector2.Zero;

        bool animationg = true;

        float speed = 1;

        public float Speed
        {
            get { return speed; }
            set { speed = (float)Math.Max(value, 0.1f); }
        }

        public bool IsAnimationg
        {
            get { return animationg; }
            set { animationg = value; }
        }

        public FrameAnimationClass CurrentAnimation
        {
            get
            {
                if (!string.IsNullOrEmpty(currentAnimation))
                {
                    return Animations[currentAnimation];
                }
                else
                    return null;
            }
            
        }

        public string CurrentAnimationName
        {
            get
            {
                return currentAnimation;
            }
            set
            {
                if (Animations.ContainsKey(value))
                    currentAnimation = value;
            }

        }

        public AnimatedSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsAnimationg)
                return;

            FrameAnimationClass animation = CurrentAnimation;

            if (animation == null)
            {
                if (Animations.Count > 0)
                {
                    string[] keys = new string[Animations.Count];
                    Animations.Keys.CopyTo(keys, 0);

                    currentAnimation = keys[0];
                    animation = CurrentAnimation;
                }
                else
                    return;
            }

            animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            FrameAnimationClass animation = CurrentAnimation;

            if(animation != null)
            {
                spriteBatch.Draw(
                    texture, Position, animation.CurrentRect, Color.White);
            }
        }
    }
}
