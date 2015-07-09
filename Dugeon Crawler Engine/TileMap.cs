using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dungeon_Crawler_Engine
{
    public class TileMap
    {
        public List<TileLayer> Layers = new List<TileLayer>();

        public void Draw(SpriteBatch batch, Camera camera)
        {
            foreach (TileLayer layer in Layers)
            {
                layer.Draw(batch, camera);
            }
        }

    }
}
