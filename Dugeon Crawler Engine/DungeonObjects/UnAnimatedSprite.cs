using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dungeon_Crawler_Engine.DungeonObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Crawler_Engine
{
    public class UnAnimatedSprites
    {

        List<DungeonObject> objectList;

        public List<DungeonObject> ObjectList
        {
            get { return objectList; }
        }

        public UnAnimatedSprites()
        {
            objectList = new List<DungeonObject>();

            
            
        }

        public void AddObject(Vector2 v2, string name, Texture2D texture, int offsetx, int offsety)
        {
            objectList.Add(new Stair((int)v2.X,(int)v2.Y,name,texture,Engine.TileHeight,Engine.TileWidth,offsetx,offsety));
        }

        public void Draw(SpriteBatch spriteBatch,TileLayer fog_map)
        {
            foreach (Stair item in objectList)
            {
                if (fog_map.Map[(int)item.Position.Y / Engine.TileHeight, (int)item.Position.X / Engine.TileHeight] == -1)
                    item.Draw(spriteBatch);
            }
        }
    }
}
