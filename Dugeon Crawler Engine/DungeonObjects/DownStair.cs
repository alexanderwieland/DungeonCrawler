using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Crawler_Engine.DungeonObjects
{
    public class Stair:DungeonObject
    {
         

        public Stair(
           int tx, int ty,
           string name,
           Texture2D texture,
           int frameheight,
           int framewidth,
           int xoffset,
           int yoffset)
            :base(tx,ty,name,texture,frameheight,framewidth,xoffset,yoffset)
        {
            this.tileX = tx / 32;
            this.pixelX = tx;

            this.tileY = ty / 32;
            this.pixelY = ty;

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
    }
}
