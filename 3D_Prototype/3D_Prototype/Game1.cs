﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Prototype
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // ExampleCube for testing purposes
        //Model exampleCube;

        public Game1()
        {
            Singleton.Instance.graphics = new GraphicsDeviceManager(this);
            Singleton.Instance.graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Camera
            Singleton.Instance.camera = new Camera(Singleton.Instance.graphics.GraphicsDevice.Viewport, 
                new Vector3(0, 300, 400), new Vector3(0, 0, 0));

            // Player
            Singleton.Instance.playerCube = new PlayerCube(new Vector3(0, 0, 0));

            // Obstacles
            Singleton.Instance.currObstacleMap = new ObstacleMap(500f);

            // Ground
            Singleton.Instance.ground = new Ground(30);

            // HighScoreSystem
            Singleton.Instance.highScoreSystem = new HighScoreSystem();

            // Screens
            Singleton.Instance.startScreen = new StartScreen();
            Singleton.Instance.deathScreen = new DeathScreen();

            // GameState
            Singleton.Instance.currGameState =
                Singleton.Gamestates.start;
            

            //Turn off culling
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Singleton.Instance.spriteBatch = new SpriteBatch(GraphicsDevice);

            /* HOW TO HANDLE 3D MODELS:
            All models 50x50x50 models from Blender
            need to be scaled by x50 and rotated around
            the x.axis in -90°. */

            // TODO: use this.Content to load your game content here

            //exampleCube = Content.Load<Model>("ExampleCube/MonoCube");

            // Player
            Singleton.Instance.playerCube.PlayerModel = this.Content.Load<Model>("Player/coloredDice");

            // Obstacle
            Singleton.Instance.obstaclePyramidModel = this.Content.Load<Model>("Obstacle/coloredPyramid");

            // Ground
            Singleton.Instance.ground.CheckerboardTexture = this.Content.Load<Texture2D>("Ground/checkerboard");

            // HighScoreSystem
            Singleton.Instance.highScoreSystem.Font = this.Content.Load<SpriteFont>("Fonts/HighScoreFont");

            //// Screens

            // Startscreen
            Singleton.Instance.startScreen.GameTitleFont = this.Content.Load<SpriteFont>("Fonts/GameTitleFont");
            Singleton.Instance.startScreen.HeadingsFont = this.Content.Load<SpriteFont>("Fonts/HeadingsFont");
            Singleton.Instance.startScreen.NormalFont = this.Content.Load<SpriteFont>("Fonts/NormalFont");
            Singleton.Instance.startScreen.AssigneTextOrigins();

            // Endscreen

            Singleton.Instance.deathScreen.FailFont = this.Content.Load<SpriteFont>("Fonts/GameTitleFont");
            Singleton.Instance.deathScreen.HeadingsFont = this.Content.Load<SpriteFont>("Fonts/HeadingsFont");
            Singleton.Instance.deathScreen.NormalFont = this.Content.Load<SpriteFont>("Fonts/NormalFont");
            Singleton.Instance.deathScreen.AssigneTextOrigins();


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // keyboard update
            Singleton.Instance.keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // GameState based Updates
            switch (Singleton.Instance.currGameState)
            {
                case Singleton.Gamestates.start:
                    break;
                case Singleton.Gamestates.playing:

                    // Player
                    Singleton.Instance.playerCube.Update();

                    // DeathScreen
                    Singleton.Instance.deathScreen.Update();

                    // Obstacles
                    Singleton.Instance.currObstacleMap.Update();

                    // Ground
                    Singleton.Instance.ground.Update();
                    
                    // Camera
                    Singleton.Instance.camera.Update(
                        Singleton.Instance.playerCube.PlayerPosition.X);

                    break;

                case Singleton.Gamestates.death:
                    break;
            }

            // GameState based Transitions
            switch (Singleton.Instance.currGameState)
            {
                case Singleton.Gamestates.start:

                    if (Singleton.Instance.keyboardState.IsKeyDown(Keys.Enter))
                    {
                        Singleton.Instance.currGameState = 
                            Singleton.Gamestates.playing;
                    }

                    break;

                case Singleton.Gamestates.playing:

                    // handled through OnCollision call

                    break;

                case Singleton.Gamestates.death:

                    if (Singleton.Instance.keyboardState.IsKeyDown(Keys.Enter))
                    {
                        Singleton.Instance.currGameState =
                            Singleton.Gamestates.playing;
                    }

                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            /* HOW TO DRAW 3D MODELS:
            To draw any 3D Model simply call the
            DrawModel(Model _model, Vector3 _modelPosition) method
            located in the Camera class and accesable through
            the SingletonInstance.camera variable. */


            // TODO: Add your drawing code here

            // GameState based Draw
            switch (Singleton.Instance.currGameState)
            {
                case Singleton.Gamestates.start:

                    // 2D Stuff
                    Singleton.Instance.spriteBatch.Begin();

                    // Start screen
                    Singleton.Instance.startScreen.Draw();

                    Singleton.Instance.spriteBatch.End();

                    break;

                case Singleton.Gamestates.playing:

                    // 3D Stuff

                    // Ground
                    Singleton.Instance.ground.Draw();

                    // Player
                    Singleton.Instance.playerCube.Draw();

                    // Obstacles
                    Singleton.Instance.currObstacleMap.Draw();


                    // 2D Stuff
                    Singleton.Instance.spriteBatch.Begin();

                    // HighScoreSystem
                    Singleton.Instance.highScoreSystem.Draw();

                    Singleton.Instance.spriteBatch.End();

                    break;

                case Singleton.Gamestates.death:

                    // 2D Stuff
                    Singleton.Instance.spriteBatch.Begin();

                    // Death screen
                    Singleton.Instance.deathScreen.Draw();

                    Singleton.Instance.spriteBatch.End();

                    break;
            }
            

            // Resetting rendering to be able to use 3D again
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            //Turn off culling
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            base.Draw(gameTime);
        }
    }
}
