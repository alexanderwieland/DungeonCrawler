using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public class TileLayer
    {
       public int index = 0;

        List<Texture2D> tiletextures = new List<Texture2D>();
        float alpha = 1f;

        public float Alpha
        {
            get { return alpha; }
            set { alpha = MathHelper.Clamp(value, 0f, 1f); }
        }

        public int WidthInArray
        {
            get { return map.GetLength(1); }
        }

        public int HeightInArray
        {
            get { return map.GetLength(0); }
        }

        public int WidthInPixels
        {
            get { return  map.GetLength(1) * Engine.TileWidth; }
        }

         public int HeightInPixels
        {
            get{return  map.GetLength(0) * Engine.TileHeight;}
        }

        int[,] map;

        public int[,] Map
        {
            get { return map; }
        }

        public TileLayer(int width, int height)
        {
            map = new int[height, width];
        }

        public TileLayer(int[,] existingmap)
        {
            map = (int[,])existingmap.Clone();
        }

        public static TileLayer FromFile(ContentManager content, string filename)
        {
            
            TileLayer tileLayer;
            
            bool readingTexture=false;
            bool readingLayer = false;
            List<string> texturenames = new List<string>();
            List<List<int>> tempLayout = new List<List<int>>();

            using (StreamReader reader= new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[Textures]"))
                    {
                        readingTexture = true;
                        readingLayer = false;
                    }
                    else if (line.Contains("[Layout]"))
                    {
                        readingTexture = false;
                        readingLayer = true;
                    }
                    else if (readingTexture)
                    {
                        texturenames.Add(line);
                    }
                    else if (readingLayer)
                    {
                        List<int>row = new List<int>();

                        string[] cells = line.Split(' ');

                        foreach (string c in cells)
                        {
                            if (!string.IsNullOrEmpty(c))
                                row.Add(int.Parse(c));
                        }

                        tempLayout.Add(row);
                    }

                }
            }

            int width = tempLayout[0].Count;
            int height = tempLayout.Count;

            tileLayer = new TileLayer(width, height);
            string s = filename.Substring(filename.Length-7, 1);
            tileLayer.index = int.Parse(s);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tileLayer.SetCellIndex(x, y, tempLayout[y][x]);
                }
            }

            tileLayer.LoadTileTextures(content, texturenames.ToArray());

            return tileLayer;
        }

        public void LoadTileTextures(ContentManager content, params string[] textureNames)
        {
            Texture2D texture;

            foreach (var item in textureNames)
            {
                texture = content.Load<Texture2D>(item);
                tiletextures.Add(texture);
            }

        }

        public void SetCellIndex(int x, int y, int cellIndex)
        {
            map[y, x] = cellIndex;
        }

        public void Draw(SpriteBatch batch, Camera camera)
        {
            batch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend,null,null,null,null,camera.TransFormMatrix);

            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int textureindex = map[y, x];

                    if (textureindex == -1)
                        continue;

                    Texture2D texture = tiletextures[textureindex];

                    batch.Draw(
                        texture,
                        new Rectangle(
                                x * Engine.TileWidth,
                                y * Engine.TileHeight,
                                Engine.TileHeight,
                                Engine.TileHeight),
                            new Color(new Vector4(1f,1f,1f,alpha)));
                }
            }

            batch.End();
        }

        public void AddTexture(Texture2D texture)
        {
            tiletextures.Add(texture);
        }
    }
}
