using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System;
using Microsoft.Xna.Framework.Audio;
namespace lightgame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Matrix cameraMatrix;
        Random rnd = new Random();

        PenumbraComponent penumbra;
        
        public Game1()
        {

            this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            penumbra = new PenumbraComponent(this);
            Components.Add(penumbra);
            penumbra.AmbientColor = Color.Black;

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
            Play.goalsound = Content.Load<SoundEffect>("sounds/goal");
            Play.teleportsound = Content.Load<SoundEffect>("sounds/teleport");

            Drawing.playertexture = Content.Load<Texture2D>("textures/player");
            Drawing.balltexture = Content.Load<Texture2D>("textures/ball");
            Drawing.whitetexture = Content.Load<Texture2D>("textures/white");
            Drawing.font = Content.Load<SpriteFont>("fonts/font1");
            
            Mapcreator.maptexture[0] = Content.Load<Texture2D>("maps/map0");
            Mapcreator.maptexture[1] = Content.Load<Texture2D>("maps/map1");
            Mapcreator.maptexture[2] = Content.Load<Texture2D>("maps/map2");
            Mapcreator.maptexture[3] = Content.Load<Texture2D>("maps/map3");
            Mapcreator.maptexture[4] = Content.Load<Texture2D>("maps/map4");

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            // TODO: use this.Content to load your game content here
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
            Play.game(penumbra);

            Player.light.Color = Player.color;
            Player.light.Position = new Vector2(Player.x * 32 - 16, Player.y * 32 - 16);
            cameraMatrix = Camera.matrix(GraphicsDevice.Viewport);
            Drawing.ups = (float)Math.Round(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Drawing.drawgame(spriteBatch, GraphicsDevice, cameraMatrix,penumbra,gameTime);
            Drawing.fps = (float)Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
