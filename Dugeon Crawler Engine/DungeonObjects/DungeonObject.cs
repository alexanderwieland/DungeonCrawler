using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Crawler_Engine
{
    public abstract class DungeonObject
    {
        public Vector2 Position = Vector2.Zero;
        protected Rectangle frame;

        protected int tileX;
        protected int tileY;

        protected int pixelX;
        protected int pixelY;

        protected string name;

        protected Texture2D texture;

        public string Name
        {
            get { return name; }
        }

        public DungeonObject(
            int tx, int ty,
            string name,
            Texture2D texture,
            int frameheight,
            int framewidth,
            int xoffset,
            int yoffset)
        {
            this.tileX = tx/32;
            this.pixelX = tx ;

            this.tileY = ty/32;
            this.pixelY = ty ;

            this.Position = new Vector2((float)pixelX, (float)pixelY);

            this.name = name;
            this.texture = texture;

            Rectangle rect = new Rectangle();
            rect.Width = framewidth;
            rect.Height = frameheight;
            rect.X = xoffset;
            rect.Y = yoffset;
            this.frame = rect;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                    texture, Position, frame, Color.White);
        }

        public void Activate()
        {
            
        }
    }
}
