using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using go_engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace go_lan_frontend
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Camera camera;

        private GameModel board;

        public GameManager Manager { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            IsMouseVisible = true;
            camera = new Camera(new Vector3(-8, -516, -424), GraphicsDevice.Viewport);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            board = new GameModel(Content.Load<Model>("Models/goBoard"));
            board.Texture = Content.Load<Texture2D>("Textures/Oak1");
            board.SetModelEffect(Content.Load<Effect>("Effects/BasicDiffuse"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                camera.Position += new Vector3(1, 0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                camera.Position += new Vector3(-1, 0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                camera.Position += new Vector3(0, 1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                camera.Position += new Vector3(0, -1, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                camera.Position += new Vector3(0, 0, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                camera.Position += new Vector3(0, 0, -1);

            base.Update(gameTime);
        }
        
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            DrawModel(board);
            
            base.Draw(gameTime);
        }


        /// <summary>
        /// Draw model with Basic Effect
        /// </summary>
        /// <param name="model">GameModel for Drawing</param>
        private void DrawModel(GameModel model)
        {
            foreach (ModelMesh mesh in model.Model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    effect.Parameters["World"].SetValue(model.World);
                    effect.Parameters["View"].SetValue(camera.View);
                    effect.Parameters["Projection"].SetValue(camera.Projection);
                    effect.Parameters["TextureMap"].SetValue(model.Texture);
                }
                mesh.Draw();
            }
        }
    }
}
