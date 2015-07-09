using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Dungeon_Crawler_Engine;

namespace Dungeon_Crawler
{
    public class WorldGenerator
    {
        Random randy;
        int[,] world0;
        int[,] fog0;

        int möglicherooms = 75;
        int raumlänge = 8;
        int anzrooms = 0;

        int worldIndex;

        public int[,] World0 
        {
            get { return world0; }
        }

        // 0 = Dark
        // 1 = Dirt
        // 2 = Wall

        List<Room> roomlist = new List<Room>();
        List<string> wallsList = new List<string>();

        public WorldGenerator()
        {
            
                      
        }

        public void GenerateMaps(int index)
        {
            möglicherooms = 75;
            raumlänge = 8;
            anzrooms = 0;
            worldIndex = index;

            randy = new Random();
            world0 = new int[randy.Next(40, 60), randy.Next(60, 80)];
            fog0 = new int[world0.GetLength(0), world0.GetLength(1)];
            for (int y = 0; y < world0.GetLength(1); y++)
            {
                for (int x = 0; x < world0.GetLength(0); x++)
                {
                    world0[x, y] = 0;
                }
            }

            FuelleMap(raumlänge, false);
            if (!PrüfeVerbundenheit())
            {
                möglicherooms++;
                FuelleMap(raumlänge, false);
                MachWegZwRooms();
            }
            if (!PrüfeVerbundenheit())
            {
                möglicherooms++;
                FuelleMap(raumlänge, false);
                MachWegZwRooms();

            }

            MacheFog();

            WriteMaps();
        }

        private void MacheFog()
        {
            
            for (int y = 0; y < world0.GetLength(1); y++)
            {
                for (int x = 0; x < world0.GetLength(0); x++)
                {
                    if (world0[x, y] == 1 || world0[x, y] == 2)
                        fog0[x, y] = 0;
                    else
                        fog0[x, y] = -1;
                }
                
            }


        }

        private void FuelleMap(int groesse, bool verkleinern)
        {
            int middleX = 1;
            int middleY = 1;

            Room room;
            //new Room(randy.Next(3, 10), randy.Next(3, 10));

            int xx = 0;
            int yy = 0;

            while (anzrooms < möglicherooms  && groesse>=5 )
            {
                room = new Room(randy.Next(3, groesse), randy.Next(3, groesse));
                string xy = Convert.ToString(randy.Next(1, world0.GetLength(0) - room.RoomMap.GetLength(0) - 5)) 
                    + ";" + Convert.ToString(randy.Next(1, world0.GetLength(1) - room.RoomMap.GetLength(1) - 5));

                int count = 0;
                for (int y = int.Parse(xy.Split(';')[1]); y < int.Parse(xy.Split(';')[1]) + room.RoomMap.GetLength(1); y++)
                {

                    for (int x = int.Parse(xy.Split(';')[0]); x < int.Parse(xy.Split(';')[0]) + room.RoomMap.GetLength(0); x++)
                    {
                        if (int.Parse(xy.Split(';')[1]) + room.RoomMap.GetLength(1) < world0.GetLength(1) && int.Parse(xy.Split(';')[0]) + room.RoomMap.GetLength(0) < world0.GetLength(0))
                        {
                            if (world0[x, y] == 0)
                            { count++; }
                        }
                    }

                }

                //wenn mehr als length/dimensionlenght 0 sind dann addroom
                if (count >= room.RoomMap.Length - room.RoomMap.GetLength(0))
                {
                    xx = 0;
                    yy = 0;
                    for (int y = int.Parse(xy.Split(';')[1]); y < int.Parse(xy.Split(';')[1]) + room.RoomMap.GetLength(1); y++)
                    {
                        xx = 0;
                        for (int x = int.Parse(xy.Split(';')[0]); x < int.Parse(xy.Split(';')[0]) + room.RoomMap.GetLength(0); x++)
                        {
                            world0[x, y] = room.RoomMap[xx, yy];

                            if (x == middleX || y == middleY || x == middleX + room.RoomMap.GetLength(0) - 1 || y == middleY + room.RoomMap.GetLength(1) - 1)
                            {
                                room.walls.Add(x + ";" + y);
                                wallsList.Add(x + ";" + y);
                            }

                            xx++;
                        }
                        yy++;
                    }
                    anzrooms++;
                   
                }

                if (verkleinern)
                    groesse--;
            }
           
        }

        private void MachWegZwRooms()
        {
            Random wegrandy = new Random();
            int regler = 2;

            for (int y = 0; y < world0.GetLength(1) - 4; y=y+1)
            {
                for (int x = 0; x < world0.GetLength(0) - 4; x = x + 1)
                {
                    if (world0[x, y] == 1 && world0[x + 1, y] == 2 && world0[x + 2, y] == 1 && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x + 1, y] = 1;
                    }
                    if (world0[x, y] == 1 && world0[x + 1, y] == 2 && world0[x + 2, y] == 2 && world0[x + 3, y] == 1 && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x + 1, y] = 1;
                        world0[x + 2, y] = 1;
                    }
                    if (world0[x, y] == 1 && world0[x, y + 1] == 2 && world0[x, y + 2] == 1 && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x, y + 1] = 1;
                    }
                    if (world0[x, y] == 1 && world0[x, y + 1] == 2 && world0[x, y + 2] == 2 && world0[x, y + 3] == 1 && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x, y + 1] = 1;
                        world0[x, y + 2] = 1;
                    }
                    if (world0[x, y] == 1 && world0[x + 1, y] == 2 && world0[x + 2, y] == 0 && world0[x + 3, y] == 2 && world0[x + 4, y] == 1
                        && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x + 1, y] = 1;
                        world0[x + 2, y] = 1;
                        world0[x + 3, y] = 1;

                        world0[x, y + 1] = 2;
                        world0[x + 1, y + 1] = 2;
                        world0[x + 2, y + 1] = 2;

                        world0[x, y - 1] = 2;
                        world0[x + 1, y - 1] = 2;
                        world0[x + 2, y - 1] = 2;
                    }
                    if (world0[x, y] == 1 && world0[x, y + 1] == 2 && world0[x, y + 2] == 0 && world0[x, y + 3] == 2 && world0[x, y + 4] == 1
                        && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x, y + 1] = 1;
                        world0[x, y + 2] = 1;
                        world0[x, y + 3] = 1;

                        world0[x + 1, y + 1] = 2;
                        world0[x + 1, y + 2] = 2;
                        world0[x + 1, y + 3] = 2;

                        world0[x - 1, y + 1] = 2;
                        world0[x - 1, y + 2] = 2;
                        world0[x - 1, y + 3] = 2;
                    }
                    if (world0[x, y] == 1 && world0[x + 1, y] == 2 && world0[x + 2, y] == 0 && world0[x + 3, y] == 0 && world0[x + 4, y] == 2
                        && world0[x + 5, y] == 1 && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x + 1, y] = 1;
                        world0[x + 2, y] = 1;
                        world0[x + 3, y] = 1;
                        world0[x + 4, y] = 1;

                        world0[x, y + 1] = 2;
                        world0[x + 1, y + 1] = 2;
                        world0[x + 2, y + 1] = 2;
                        world0[x + 3, y + 1] = 2;

                        world0[x, y - 1] = 2;
                        world0[x + 1, y - 1] = 2;
                        world0[x + 2, y - 1] = 2;
                        world0[x + 3, y - 1] = 2;
                    }
                    if (world0[x, y] == 1 && world0[x, y + 1] == 2 && world0[x, y + 2] == 0 && world0[x, y + 3] == 0 && world0[x, y + 4] == 2
                        && world0[x, y + 5] == 1 && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x, y + 1] = 1;
                        world0[x, y + 2] = 1;
                        world0[x, y + 3] = 1;
                        world0[x, y + 4] = 1;

                        world0[x + 1, y + 1] = 2;
                        world0[x + 1, y + 2] = 2;
                        world0[x + 1, y + 3] = 2;
                        world0[x + 1, y + 4] = 2;

                        world0[x - 1, y + 1] = 2;
                        world0[x - 1, y + 2] = 2;
                        world0[x - 1, y + 3] = 2;
                        world0[x - 1, y + 4] = 2;
                    }
                    if (world0[x, y] == 1 && world0[x + 1, y] == 2 && world0[x + 2, y] == 0 && world0[x + 3, y] == 0 && world0[x + 4, y] == 0
                        && world0[x + 5, y] == 2 && world0[x + 6, y] == 1 && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x + 1, y] = 1;
                        world0[x + 2, y] = 1;
                        world0[x + 3, y] = 1;
                        world0[x + 4, y] = 1;
                        world0[x + 5, y] = 1;

                        world0[x, y + 1] = 2;
                        world0[x + 1, y + 1] = 2;
                        world0[x + 2, y + 1] = 2;
                        world0[x + 3, y + 1] = 2;
                        world0[x + 4, y + 1] = 2;

                        world0[x, y - 1] = 2;
                        world0[x + 1, y - 1] = 2;
                        world0[x + 2, y - 1] = 2;
                        world0[x + 3, y - 1] = 2;
                        world0[x + 4, y - 1] = 2;
                    }
                    if (world0[x, y] == 1 && world0[x, y + 1] == 2 && world0[x, y + 2] == 0 && world0[x, y + 3] == 0 && world0[x, y + 4] == 0
                        && world0[x, y + 5] == 2 && world0[x, y + 6] == 1 && wegrandy.Next(0, 20) % regler == 0)
                    {
                        world0[x, y + 1] = 1;
                        world0[x, y + 2] = 1;
                        world0[x, y + 3] = 1;
                        world0[x, y + 4] = 1;
                        world0[x, y + 5] = 1;

                        world0[x + 1, y + 1] = 2;
                        world0[x + 1, y + 2] = 2;
                        world0[x + 1, y + 3] = 2;
                        world0[x + 1, y + 4] = 2;
                        world0[x + 1, y + 5] = 2;

                        world0[x - 1, y + 1] = 2;
                        world0[x - 1, y + 2] = 2;
                        world0[x - 1, y + 3] = 2;
                        world0[x - 1, y + 4] = 2;
                        world0[x - 1, y + 5] = 2;
                    }
                }
            }
        }

        private bool PrüfeVerbundenheit()
        {            
            int anzahlwände = 0;
            //Getwände
            for (int y = 0; y < world0.GetLength(1); y++)
            {
                for (int x = 0; x < world0.GetLength(0); x++)
                {
                    if (world0[x, y] == 2 && CheckIfEinBlack(x,y))
                    {
                        anzahlwände++;
                    }  
                }
            }

            int anzausenwände = CountConnectedWalls();

            if (anzahlwände == anzausenwände)
                return true;
            else
                return false;
        }

        private int CountConnectedWalls()
        {

            int startX = 0;
            int startY = 0;
            int anzWalls = 0;
            bool woswasi = false;
            for (int y = 0; y < world0.GetLength(1); y++)
            {
                for (int x = 0; x < world0.GetLength(0); x++)
                {
                    if (world0[x, y] == 2)
                    {
                        startX = x;
                        startY = y;
                        woswasi = true;
                        break;
                    }
                    
                }
                if(woswasi)
                break;
            }
            int currentX=startX;
            int currentY=startY;
          
            string richtung = "rechts";
            while ((startX != currentX || startY != currentY)||anzWalls==0)
            {   //Suche nach der nächsten ausenwand
                bool elementgefunend = false;

                switch (richtung)
                {
                    case "rechts":
                        if (world0[currentX, currentY - 1] == 2 && CheckIfEinBlack(currentX, currentY - 1) && elementgefunend == false && richtung != "runter")
                        {
                            currentY--;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "rauf";
                        }
                        if (world0[currentX + 1, currentY] == 2 && CheckIfEinBlack(currentX + 1, currentY) && elementgefunend == false && richtung != "links")
                        {
                            currentX++;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "rechts";
                        }
                        if (world0[currentX, currentY + 1] == 2 && CheckIfEinBlack(currentX, currentY + 1) && elementgefunend == false && richtung != "rauf")
                        {
                            currentY++;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "runter";
                        }
                        if (world0[currentX - 1, currentY] == 2 && CheckIfEinBlack(currentX - 1, currentY) && elementgefunend == false && richtung != "rechts")
                        {
                            currentX--;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "links";
                        }

                        break;

                    case "runter":

                        if (world0[currentX + 1, currentY] == 2 && CheckIfEinBlack(currentX + 1, currentY) && elementgefunend == false && richtung != "links")
                        {
                            currentX++;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "rechts";
                        }
                        if (world0[currentX, currentY + 1] == 2 && CheckIfEinBlack(currentX, currentY + 1) && elementgefunend == false && richtung != "rauf")
                        {
                            currentY++;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "runter";
                        }
                        if (world0[currentX - 1, currentY] == 2 && CheckIfEinBlack(currentX - 1, currentY) && elementgefunend == false && richtung != "rechts")
                        {
                            currentX--;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "links";
                        }
                        if (world0[currentX, currentY - 1] == 2 && CheckIfEinBlack(currentX, currentY - 1) && elementgefunend == false && richtung != "runter")
                        {
                            currentY--;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "rauf";
                        }
                        break;
                    case "links":

                        if (world0[currentX, currentY + 1] == 2 && CheckIfEinBlack(currentX, currentY + 1) && elementgefunend == false && richtung != "rauf")
                        {
                            currentY++;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "runter";
                        }
                        if (world0[currentX - 1, currentY] == 2 && CheckIfEinBlack(currentX - 1, currentY) && elementgefunend == false && richtung != "rechts")
                        {
                            currentX--;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "links";
                        }
                        if (world0[currentX, currentY - 1] == 2 && CheckIfEinBlack(currentX, currentY - 1) && elementgefunend == false && richtung != "runter")
                        {
                            currentY--;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "rauf";
                        }
                        if (world0[currentX + 1, currentY] == 2 && CheckIfEinBlack(currentX + 1, currentY) && elementgefunend == false && richtung != "links")
                        {
                            currentX++;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "rechts";
                        }
                        break;
                    case "rauf":

                        if (world0[currentX - 1, currentY] == 2 && CheckIfEinBlack(currentX - 1, currentY) && elementgefunend == false && richtung != "rechts")
                        {
                            currentX--;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "links";
                        }
                        if (world0[currentX, currentY - 1] == 2 && CheckIfEinBlack(currentX, currentY - 1) && elementgefunend == false && richtung != "runter")
                        {
                            currentY--;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "rauf";
                        }
                        if (world0[currentX + 1, currentY] == 2 && CheckIfEinBlack(currentX + 1, currentY) && elementgefunend == false && richtung != "links")
                        {
                            currentX++;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "rechts";
                        }
                        if (world0[currentX, currentY + 1] == 2 && CheckIfEinBlack(currentX, currentY + 1) && elementgefunend == false && richtung != "rauf")
                        {
                            currentY++;
                            anzWalls++;
                            elementgefunend = true;
                            richtung = "runter";
                        }
                        break;

                }

                

            }
            return anzWalls;
        }

        private bool CheckIfEinBlack(int x, int y)
        {
            if (world0[x+1, y] == 0 || world0[x+1, y+1] == 0 || world0[x, y+1] == 0 
                || world0[x-1, y+1] == 0 || world0[x-1, y] == 0 || world0[x-1, y-1] == 0 || world0[x, y-1] == 0 || world0[x+1, y-1] == 0)
                return true;
            else
                return false;
        }

        private void WriteMaps()
        {
            using (StreamWriter sw = new StreamWriter("Content/Layers/world"+worldIndex+".layer", false))
            {
                string s = "";

                sw.WriteLine("[Layout]");

                for (int y = 0; y < world0.GetLength(0) - 1; y++)
                {

                    s = "";
                    for (int x = 0; x < world0.GetLength(1) - 1; x++)
                    {
                        string a = Convert.ToString(world0[y, x]);

                        s += a + " ";

                    }
                    sw.WriteLine(s);
                }

                sw.WriteLine("[Textures]");

                sw.WriteLine("Tiles/se_free_dark_texture");
                sw.WriteLine("Tiles/se_free_dirt_texture");
                sw.WriteLine("Tiles/se_free_grass_texture");

                sw.Flush();
                sw.Close();
            }

            using (StreamWriter sw = new StreamWriter("Content/Layers/fog" + worldIndex + ".layer", false))
            {
                string s = "";

                sw.WriteLine("[Layout]");

                for (int y = 0; y < world0.GetLength(0) - 1; y++)
                {

                    s = "";
                    for (int x = 0; x < world0.GetLength(1) - 1; x++)
                    {
                        string a = Convert.ToString(fog0[y, x]);

                        s += a + " ";

                    }
                    sw.WriteLine(s);
                }

                sw.WriteLine("[Textures]");

                sw.WriteLine("Tiles/se_free_dark_texture");

                sw.Flush();
                sw.Close();
            }
        }
        
            
        
    }
}
