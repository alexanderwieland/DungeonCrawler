using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Dungeon_Crawler_Engine.DungeonObjects;
using Dungeon_Crawler_Engine;

namespace Dungeon_Crawler
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileMap tileMap = new TileMap();

        int tileMapListIndex = 0;
        List<TileMap> tileMapList = new List<TileMap>();

        AnimatedSprite player_char;
        UnAnimatedSprites unAnimatedSprites;
        Random r = new Random();
        private double elapsedTime;
        private int waitingTime = 100;

        List<AnimatedSprite> npcs = new List<AnimatedSprite>();
        

        Camera camera = new Camera();
        WorldGenerator wg = new WorldGenerator();
        //TileLayer tileLayer;
        TileLayer tileLayer_map;
        TileLayer tileLayer_fog;

        int[,] fogcleararray = new int[11, 11]
            {
                {0,0, 0, 0,-1,-1,-1, 0, 0, 0,0},
                {0,0, 0,-1,-1,-1,-1,-1, 0, 0,0},
                {0,0,-1,-1,-1,-1,-1,-1,-1, 0,0},
                {0,-1,-1,-1,-1,-1,-1,-1,-1,-1,0},
                {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
                {0,-1,-1,-1,-1,-1,-1,-1,-1,-1,0},
                {0,0,-1,-1,-1,-1,-1,-1,-1 ,0,0},
                {0,0, 0,-1,-1,-1,-1,-1, 0, 0,0},
                {0,0, 0, 0,-1,-1,-1, 0, 0, 0,0}
            };

        public TileLayer CurrentMap
        {
            get { return tileMapList[tileMapListIndex].Layers[0]; }
            set { tileMapList[tileMapListIndex].Layers[0] = value; }
        }

        public int TileMapListIndex
        {
            get { return tileMapListIndex; }
            set 
            {
                //wenn value um eins größer ist als die anz der maps dann adde neue map
                if (value == tileMapList.Count)
                {
                    wg.GenerateMaps(value);
                    tileLayer_map = TileLayer.FromFile(Content, "Content/Layers/world" + value + ".layer");

                    tileLayer_fog = TileLayer.FromFile(Content, "Content/Layers/fog" + value + ".layer");
                    tileLayer_fog.Alpha = 0.85f;

                    TileMap tm = new TileMap();

                    tm.Layers.Add(tileLayer_map);
                    tm.Layers.Add(tileLayer_fog);

                    tileMapList.Add(tm);
                }
                if (value <= tileMapList.Count && value >= 0)
                {
                    tileMapListIndex = value;
                    
                }
                
            }
        }

        public TileLayer CurrentFog
        {
            get { return tileMapList[tileMapListIndex].Layers[1]; }
            set { tileMapList[tileMapListIndex].Layers[1] = value; }
        }

        public TileMap CurrentTileMap
        {
            get { return tileMapList[tileMapListIndex]; }
            set { tileMapList[tileMapListIndex] = value; }
        }

        public Game1(int windowWidth, int windowHeight)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            
            
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;
            wg.GenerateMaps(tileMapListIndex);

            base.Initialize();

            

            

            
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileLayer_map = TileLayer.FromFile(Content, "Content/Layers/world" + tileMapListIndex + ".layer");

            tileLayer_fog = TileLayer.FromFile(Content, "Content/Layers/fog" + tileMapListIndex + ".layer");
            tileLayer_fog.Alpha = 0.85f;

            tileMap.Layers.Add(tileLayer_map);
            tileMap.Layers.Add(tileLayer_fog);

            tileMapList.Add(tileMap);

            unAnimatedSprites = new UnAnimatedSprites();
            LoadSprites();
            

            InitializeNPCs();
        }

        private void LoadSprites()
        {
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "DownStair", Content.Load<Texture2D>("Items/Stairs"), 0, 0);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "DownStair", Content.Load<Texture2D>("Items/Stairs"), 0, 0);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "DownStair", Content.Load<Texture2D>("Items/Stairs"), 0, 0);

            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "UpStair", Content.Load<Texture2D>("Items/Stairs"), Engine.TileHeight, 0);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "UpStair", Content.Load<Texture2D>("Items/Stairs"), Engine.TileHeight, 0);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "UpStair", Content.Load<Texture2D>("Items/Stairs"), Engine.TileHeight, 0);

            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "Mace", Content.Load<Texture2D>("Items/weapons"), 0 * Engine.TileHeight, 0);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "Sword", Content.Load<Texture2D>("Items/weapons"), 1 * Engine.TileHeight, 0);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "Axe", Content.Load<Texture2D>("Items/weapons"), 2 * Engine.TileHeight, 0);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "Dagger", Content.Load<Texture2D>("Items/weapons"), 3 * Engine.TileHeight, 0);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "Triant", Content.Load<Texture2D>("Items/weapons"), 0 * Engine.TileHeight, 1*Engine.TileWidth);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "Staff", Content.Load<Texture2D>("Items/weapons"), 1 * Engine.TileHeight, 1 * Engine.TileWidth);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "DoubleAxe", Content.Load<Texture2D>("Items/weapons"), 2 * Engine.TileHeight, 1 * Engine.TileWidth);
            unAnimatedSprites.AddObject(GetValidPositionForNPCs(), "Spear", Content.Load<Texture2D>("Items/weapons"), 3 * Engine.TileHeight, 1 * Engine.TileWidth);

            npcs.Add(new AnimatedSprite(Content.Load<Texture2D>("Sprites/smr1")));
            npcs.Add(new AnimatedSprite(Content.Load<Texture2D>("Sprites/npc4")));
            npcs.Add(new AnimatedSprite(Content.Load<Texture2D>("Sprites/dvl1")));
            npcs.Add(new AnimatedSprite(Content.Load<Texture2D>("Sprites/npc4")));
            npcs.Add(new AnimatedSprite(Content.Load<Texture2D>("Sprites/dvl1")));
            npcs.Add(new AnimatedSprite(Content.Load<Texture2D>("Sprites/npc4")));
            npcs.Add(new AnimatedSprite(Content.Load<Texture2D>("Sprites/dvl1")));

            player_char = new AnimatedSprite(Content.Load<Texture2D>("Sprites/scr1"));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }     

        protected override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime >= waitingTime)
            {


                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                KeyboardState keystate = Keyboard.GetState();

                Vector2 motion = Vector2.Zero;


                if (keystate.IsKeyDown(Keys.Enter))
                {
                    foreach (DungeonObject item in unAnimatedSprites.ObjectList)
                    {
                        if (player_char.Position == item.Position)
                        {

                            if (item.Name == "DownStair")
                            {
                                TileMapListIndex= TileMapListIndex+1;
                                player_char.Position=GetValidPositionForNPCs();
                                foreach (AnimatedSprite npc in npcs)
                                {
                                    npc.Position = GetValidPositionForNPCs();
                                }
                            }
                            else if (item.Name == "UpStair")
                            {
                                TileMapListIndex = TileMapListIndex - 1;
                                player_char.Position = GetValidPositionForNPCs();
                                foreach (AnimatedSprite npc in npcs)
                                {
                                    npc.Position = GetValidPositionForNPCs();
                                }
                            }
                            else
                                item.Activate();
                        }
                        
                    }
                }


                if (keystate.IsKeyDown(Keys.Up))
                    motion.Y -= 1;
                if (keystate.IsKeyDown(Keys.Down))
                    motion.Y += 1;
                if (keystate.IsKeyDown(Keys.Left))
                    motion.X -= 1;
                if (keystate.IsKeyDown(Keys.Right))
                    motion.X += 1;

                foreach (AnimatedSprite s in npcs)
                {
                    s.Update(gameTime);
                }


                MoveCharacter(gameTime, motion);
                player_char.Update(gameTime);


                if (player_char.Position.X < 0)
                    player_char.Position.X = 0;
                if (player_char.Position.Y < 0)
                    player_char.Position.Y = 0;

                if (player_char.Position.X > CurrentMap.WidthInPixels - player_char.CurrentAnimation.CurrentRect.Width)
                    player_char.Position.X = CurrentMap.WidthInPixels - player_char.CurrentAnimation.CurrentRect.Width;
                if (player_char.Position.Y > CurrentMap.HeightInPixels - player_char.CurrentAnimation.CurrentRect.Height)
                    player_char.Position.Y = CurrentMap.HeightInPixels - player_char.CurrentAnimation.CurrentRect.Height;



                int screenwidth = GraphicsDevice.Viewport.Width;
                int screenheight = GraphicsDevice.Viewport.Height;

                camera.position.X =
                    player_char.Position.X +
                    (player_char.CurrentAnimation.CurrentRect.Width / 2) - (screenwidth / 2);

                camera.position.Y =
                    player_char.Position.Y +
                    (player_char.CurrentAnimation.CurrentRect.Height / 2) - (screenheight / 2);


                ClearFog();

                //Camera darf nicht die Map verlassen

                //if (camera.position.X > tileLayer.WidthInPixels - screenwidth)
                //    camera.position.X = tileLayer.WidthInPixels - screenwidth;
                //if (camera.position.Y > tileLayer.HeightInPixels - screenheight)
                //    camera.position.Y = tileLayer.HeightInPixels - screenheight;

                //if (camera.position.X < 0)
                //    camera.position.X = 0;
                //if (camera.position.Y < 0)
                //    camera.position.Y = 0;


                base.Update(gameTime);

                elapsedTime -= waitingTime;
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            CurrentTileMap.Draw(spriteBatch, camera);

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, camera.TransFormMatrix);

            unAnimatedSprites.Draw(spriteBatch,CurrentFog);

            player_char.Draw(spriteBatch);

            foreach (AnimatedSprite s in npcs)
            {
                if (CurrentFog.Map[(int) s.Position.Y/Engine.TileHeight,(int) s.Position.X/Engine.TileHeight] == -1)
                {
                    s.Draw(spriteBatch);
                    s.IsAnimationg = false;
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
            
        }

        private void ClearFog()
        {
            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 11; x++)
                {
                    if (fogcleararray[y, x] == -1 && (player_char.Position.Y / Engine.TileHeight) - 5 + y > 0 && (player_char.Position.X / Engine.TileHeight) - 5 + x > 0)
                        CurrentFog.Map[(int)(player_char.Position.Y / Engine.TileHeight) - 5 + y, (int)(player_char.Position.X / Engine.TileHeight) - 5 + x]
                            = fogcleararray[y, x];
                }
            }
        }

        private void InitializeNPCs()
        {

            FrameAnimationClass up = new FrameAnimationClass(2, Engine.TileHeight, Engine.TileHeight, 0, 0);
            player_char.Animations.Add("Up", up);

            FrameAnimationClass down = new FrameAnimationClass(2, Engine.TileHeight, Engine.TileHeight, 64, 0);
            player_char.Animations.Add("Down", down);

            FrameAnimationClass left = new FrameAnimationClass(2, Engine.TileHeight, Engine.TileHeight, 128, 0);
            player_char.Animations.Add("Left", left);

            FrameAnimationClass right = new FrameAnimationClass(2, Engine.TileHeight, Engine.TileHeight, 64 + 128, 0);
            player_char.Animations.Add("Right", right);

            player_char.CurrentAnimationName = "Down";
            player_char.Position = GetValidPositionForNPCs();



            foreach (AnimatedSprite npc in npcs)
            {
                npc.Animations.Add("Up", (FrameAnimationClass)up.Clone());
                npc.Animations.Add("Down", (FrameAnimationClass)down.Clone());
                npc.Animations.Add("Left", (FrameAnimationClass)left.Clone());
                npc.Animations.Add("Right", (FrameAnimationClass)right.Clone());

                int animation = r.Next(3);

                switch (animation)
                {
                    case 0:
                        npc.CurrentAnimationName = "Up";
                        break;
                    case 1:
                        npc.CurrentAnimationName = "Down";
                        break;
                    case 2:
                        npc.CurrentAnimationName = "Left";
                        break;
                    case 3:
                        npc.CurrentAnimationName = "Right";
                        break;

                }

                npc.Position = GetValidPositionForNPCs();

            }


        }

        private Vector2 GetValidPositionForNPCs()
        {
            Vector2 v2 = new Vector2(
                    (int)(r.Next(1, CurrentMap.WidthInArray - 1)),
                    (int)(r.Next(1, CurrentMap.HeightInArray - 1)));

            while (CurrentMap.Map[(int)v2.Y, (int)v2.X] != 1)
            {
                v2 = new Vector2(
                    (int)(r.Next(CurrentMap.WidthInArray - 1)),
                    (int)(r.Next((CurrentMap.HeightInArray - 1))));
            }
            v2.X *= Engine.TileHeight;
            v2.Y *= Engine.TileHeight;
            return v2;
        }

        private void MoveCharacter(GameTime gameTime, Vector2 motion)
        {
            if (motion != Vector2.Zero)
            {
                //motion.Normalize();

                float motionAngle = (float)Math.Atan2(motion.Y, motion.X);

                if (motionAngle >= -MathHelper.PiOver4 && motionAngle <= MathHelper.PiOver4)
                {
                    player_char.CurrentAnimationName = "Right";
                    motion = new Vector2(Engine.TileWidth, 0f);
                }
                else if (motionAngle >= MathHelper.PiOver4 && motionAngle <= 3f * MathHelper.PiOver4)
                {
                    player_char.CurrentAnimationName = "Down";
                    motion = new Vector2(0f, Engine.TileWidth);
                }
                else if (motionAngle <= -MathHelper.PiOver4 && motionAngle >= -3f * MathHelper.PiOver4)
                {
                    player_char.CurrentAnimationName = "Up";
                    motion = new Vector2(0f, -Engine.TileWidth);
                }
                else
                {
                    player_char.CurrentAnimationName = "Left";
                    motion = new Vector2(-Engine.TileWidth, 0f);
                }

                player_char.IsAnimationg = true;

                if (CurrentMap.Map[(int)((player_char.Position.Y / Engine.TileHeight) + motion.Y / Engine.TileHeight), (int)((player_char.Position.X / Engine.TileHeight) + motion.X / Engine.TileHeight)] == 1)
                    player_char.Position += motion * player_char.Speed;
            }
            else
            {
                player_char.IsAnimationg = false;
            }
        }

    }
}
