using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dungeon_Crawler_Engine
{
    public class Room
    {

        int[,] roommap;

        public bool istverbunden = false;

        public List<string> walls = new List<string>();

        public int[,] RoomMap
        {
            get { return roommap; }
        }

        public Room(int ra, int rb)
        {
            roommap = new int[ra, rb];

            for (int y = 0; y < roommap.GetLength(1); y++)
            {
                for (int x = 0; x < roommap.GetLength(0); x++)
                {
                    if (x == 0 || y == 0 || x == roommap.GetLength(0) - 1 || y == roommap.GetLength(1) - 1)
                    {
                        roommap[x, y] = 2;
                    }
                    else
                    {
                        roommap[x, y] = 1;
                    }
                }
            }
        }


    }
}
