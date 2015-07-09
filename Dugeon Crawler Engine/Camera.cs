using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Crawler_Engine
{
    public class Camera
    {
        

        public Vector2 position = Vector2.Zero;

       

        

        public Matrix TransFormMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-position,0f));
            }
        }


    }
}
