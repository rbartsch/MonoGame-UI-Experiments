using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

// For ECS, probably have two components, one that is Container+Area, and one that is ScrollElement component.
// ScrollElement component needs to be able to be just text, image, etc. Therefore it will not rely on texture height/width
// but defined x, y coords and width/height of its own and an entity will be made up of it and maybe a text component, sprite component, etc?

namespace UITests {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game2 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int prevMouseScrollValue;
        int currMouseScrollValue;

        TextArea textArea;

        private KeyboardState oldState;

        public Game2() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            if (!graphics.IsFullScreen)
                Window.Position = new Point((GraphicsDevice.DisplayMode.Width / 2) - graphics.PreferredBackBufferWidth / 2, (GraphicsDevice.DisplayMode.Height / 2) - graphics.PreferredBackBufferHeight / 2);

            IsMouseVisible = true;
            currMouseScrollValue = Mouse.GetState().ScrollWheelValue;
            prevMouseScrollValue = currMouseScrollValue;

            //text2D = new Text2D("Terminus", "The quick brown fox jumps over the lazy dog. THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG. 1234567890 . : , ; ' \" ( ! ? ) + - * / = \\ A B C D E F G H I J K L M N O P Q R S T U V W X Y Z a b c d e f g h i j k l m n o p q r s t u v w x y z ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz", new Vector2(0, 0), Color.White);
            textArea = new TextArea(new Rectangle(100, 100, 400, 100), TextAlignment.Left, false, "Terminus", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", new Vector2(0, 0), Color.White);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textArea.Load(Content);
            textArea.WrapText(true);
            textArea.AlignText(TextAlignment.Left);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        void ButtonTestPrintNum(object sender, EventArgs e, int n) {
            Console.WriteLine("BUTTON TEST: " + n);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                Console.WriteLine("Controller detected");

            prevMouseScrollValue = currMouseScrollValue;
            currMouseScrollValue = Mouse.GetState().ScrollWheelValue;

            KeyboardState newState = Keyboard.GetState();  // get the newest state

            oldState = newState;

            base.Update(gameTime);
        }

        private static Texture2D debugDrawRect;
        private void DrawRectangle(Rectangle coords, Color color) {
            if (debugDrawRect == null) {
                debugDrawRect = new Texture2D(graphics.GraphicsDevice, 1, 1);
                debugDrawRect.SetData(new[] { Color.White });
            }
            spriteBatch.Draw(debugDrawRect, coords, color);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(new Color(38, 33, 68, 255));

            // Normal image
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null, null, null);
            // Debug
            DrawRectangle(textArea.Area, Color.Red);
            spriteBatch.End();

            // Layer depth does work between different batches, they are isolated to each!

            // Scrollable area
            RasterizerState r = new RasterizerState {
                ScissorTestEnable = true
            };

            // For UI we should use SpriteSortMode.Immediate so that we can use multiple scissor rectangles in a sprite batch otherwise deferred only uses last one used at End()
            // https://github.com/MonoGame/MonoGame/issues/217

            // gui scroll system batch
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, r, null, null);
            spriteBatch.End();

            // other gui batch
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null, null, null);
            textArea.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}