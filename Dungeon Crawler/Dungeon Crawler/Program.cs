using System;

namespace Dungeon_Crawler
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static public bool startgame = false;
        static public int w = 1024;
        static public int h = 600;

        static void Main(string[] args)
        {
            using (MainMenu main = new MainMenu())
            {
                main.ShowDialog();
            }

            if(startgame)
                using (Game1 game = new Game1(w,h))
            {
                game.Run();
            }

           
        }
    }
#endif
}

