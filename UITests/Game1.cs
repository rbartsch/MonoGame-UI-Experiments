using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

/// <summary>
/// This is just a verbose project of quick GUI tests/additions
/// </summary>

namespace UITests {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int prevMouseScrollValue;
        int currMouseScrollValue;

        Texture2D testImage;

        ScrollPanel scrollPanelA;
        ScrollPanel scrollPanelB;
        ScrollPanel scrollPanelC;

        List<ScrollPanel> scrollPanels;

        Button button;

        Scrollbar scrollbarA;
        Scrollbar scrollbarB;
        Scrollbar scrollbarC;

        TextArea textArea;
        TextArea textInfo;
        TextArea buttonText;

        private KeyboardState oldState;

        public Game1() {
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

            scrollbarA = new Scrollbar(new Rectangle(356, 100, 16, 256), new Image2D("scrollbar-handle", new Vector2(2, 0), Color.White, 0f), new Image2D("scrollbar", new Vector2(0, 0), Color.White, 0));
            scrollbarB = new Scrollbar(new Rectangle(696, 100, 16, 96), new Image2D("scrollbar-handle", new Vector2(2, 0), Color.White, 0f), new Image2D("scrollbar-96", new Vector2(0, 0), Color.White, 0));
            scrollbarC = new Scrollbar(new Rectangle(896, 200, 16, 96), new Image2D("scrollbar-handle", new Vector2(2, 0), Color.White, 0f), new Image2D("scrollbar-96", new Vector2(0, 0), Color.White, 0));

            scrollPanelA = new ScrollPanel(new ScrollMask(new Rectangle(100, 100, 256, 256), new Image2D("scroll-bg", new Vector2(0,0), Color.White, 0f)), 12, null, scrollbarA);
            scrollPanelB = new ScrollPanel(new ScrollMask(new Rectangle(600, 100, 100, 100), new Image2D("scroll-bg-96", new Vector2(0,0), Color.White, 0f)), 12, null, scrollbarB);
            scrollPanelC = new ScrollPanel(new ScrollMask(new Rectangle(800, 200, 100, 100), new Image2D("scroll-bg-96", new Vector2(0,0), Color.White, 0f)), 12, null, scrollbarC);

            scrollPanels = new List<ScrollPanel>();
            scrollPanels.Add(scrollPanelA);
            scrollPanels.Add(scrollPanelB);
            scrollPanels.Add(scrollPanelC);

            textArea = new TextArea(new Rectangle(0, 0, 200, 50), TextAlignment.Left, false, "Terminus", "The quick brown fox jumps over the lazy dog. THE QUICK BROWN FOX JUMPS OVER THE LAZY DOG. 1234567890 . : , ; ' \" ( ! ? ) + - * / = \\", new Vector2(0, 0), Color.White);
            textInfo = new TextArea(new Rectangle(350, 25, 200, 50), TextAlignment.Left, false, "Terminus", "These are scroll systems, you can click and drag the scroll bar or move mouse positions within the scroll area and use your mouse wheel to scroll.", new Vector2(0, 0), Color.White);
            buttonText = new TextArea(new Rectangle(14, 7, 200, 50), TextAlignment.Left, false, "Terminus", "This is a button\n\nYou can see the response in the console output", new Vector2(0, 0), Color.White);

            button = new Button(new Image2D("scroll-btn", new Vector2(0, 0), Color.White, 0f), new Rectangle(0, 0, 128, 32), Color.White, Color.Red, Color.Green);
            button.Click += delegate (object s, EventArgs e) {
                ButtonTestPrintNum(s, e, new Random().Next(0, 1000));
            };

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testImage = Content.Load<Texture2D>("Test_0");

            for(int i = 0; i < scrollPanels.Count; i++) {
                scrollPanels[i].ScrollMask.BackgroundImage.Load(Content);
                scrollPanels[i].ScrollContainer.BackgroundImage.Load(Content);
            }

            button.Image.Load(Content);

            scrollbarA.BackgroundImage.Load(Content);
            scrollbarA.HandleImage.Load(Content);
            scrollbarB.BackgroundImage.Load(Content);
            scrollbarB.HandleImage.Load(Content);
            scrollbarC.BackgroundImage.Load(Content);
            scrollbarC.HandleImage.Load(Content);

            textArea.Load(Content);
            textInfo.Load(Content);
            buttonText.Load(Content);
            textArea.WrapText(true);
            textInfo.WrapText(true);

            // Verbosity for testing purposes
            scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 96, 96, new Vector2(0, 0)));
            scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(48, 48), Color.White));
            scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 96)));
            scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 144)));
            scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 192)));
            scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 240)));
            scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 288)));
            scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 336)));
            scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 400, 100, new Vector2(0, 384)));
            scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(textArea);

            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 0)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 48)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 96)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 144)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 192)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 240)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 288)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 336)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 384)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[1].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, 432)));
            scrollPanels[1].ScrollContainer.GetElement(scrollPanels[1].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));

            scrollPanels[2].ScrollContainer.AddElementSequential(new ScrollElement(Content, 48, 48));
            scrollPanels[2].ScrollContainer.GetElement(scrollPanels[2].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[2].ScrollContainer.AddElementSequential(new ScrollElement(Content, 48, 48));
            scrollPanels[2].ScrollContainer.GetElement(scrollPanels[2].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[2].ScrollContainer.AddElementSequential(new ScrollElement(Content, 48, 48));
            scrollPanels[2].ScrollContainer.GetElement(scrollPanels[2].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            scrollPanels[2].ScrollContainer.AddElementSequential(new ScrollElement(Content, 48, 48));
            scrollPanels[2].ScrollContainer.GetElement(scrollPanels[2].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));

            for (int i = 0; i < scrollPanels.Count; i++) {
                scrollPanels[i].UpdateScrollLength();
            }
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

            for(int i = 0; i < scrollPanels.Count; i++) {
                scrollPanels[i].Update(Mouse.GetState().Position, prevMouseScrollValue, currMouseScrollValue);
            }

            prevMouseScrollValue = currMouseScrollValue;
            currMouseScrollValue = Mouse.GetState().ScrollWheelValue;

            KeyboardState newState = Keyboard.GetState();  // get the newest state

            if (oldState.IsKeyUp(Keys.Q) && newState.IsKeyDown(Keys.Q)) {
                scrollPanels[0].ScrollContainer.AddElement(new ScrollElement(Content, 48, 48, new Vector2(0, scrollPanels[0].ScrollContainer.Area.Height)));
                scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1).AddGUIDrawable(new Image2D("Test_1", new Vector2(0, 0), Color.White));
            }
            if (oldState.IsKeyUp(Keys.W) && newState.IsKeyDown(Keys.W)) {
                scrollPanels[0].ScrollContainer.RemoveElement(scrollPanels[0].ScrollContainer.GetElement(scrollPanels[0].ScrollContainer.Elements.Count - 1));
            }

            oldState = newState;

            button.Update(Mouse.GetState());

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
            spriteBatch.Draw(testImage, new Vector2(150, 50), Color.White);
            // Debug
            //for(int i = 0; i < scrollPanels.Count; i++) {
            //    DrawRectangle(scrollPanels[i].ScrollMask.Area, Color.Fuchsia);
            //    DrawRectangle(scrollPanels[i].ScrollContainer.Area, Color.Aqua);
            //    for(int j = 0; j < scrollPanels[i].ScrollContainer.Elements.Count; j++) {
            //        DrawRectangle(new Rectangle((int)scrollPanels[i].ScrollContainer.Elements[j].Position.X, (int)scrollPanels[i].ScrollContainer.Elements[j].Position.Y, scrollPanels[i].ScrollContainer.Elements[j].Width, scrollPanels[i].ScrollContainer.Elements[j].Height), Color.Red);
            //    }
            //}
            spriteBatch.End();

            // Layer depth does not work between different batches, they are isolated to each!

            // Scrollable area
            RasterizerState r = new RasterizerState {
                ScissorTestEnable = true
            };

            // For UI we should use SpriteSortMode.Immediate so that we can use multiple scissor rectangles in a sprite batch otherwise deferred only uses last one used at End()
            // https://github.com/MonoGame/MonoGame/issues/217

            // gui scroll system batch
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, r, null, null);
            for(int i = 0; i < scrollPanels.Count; i++) {
                spriteBatch.GraphicsDevice.ScissorRectangle = scrollPanels[i].ScrollMask.Area;
                scrollPanels[i].ScrollMask.BackgroundImage.Draw(spriteBatch);
                scrollPanels[i].ScrollContainer.BackgroundImage.Draw(spriteBatch);
                for(int j = 0; j < scrollPanels[i].ScrollContainer.Elements.Count; j++) {
                    for(int n = 0; n < scrollPanels[i].ScrollContainer.Elements[j].GetGUIDrawableCount(); n++) {
                        scrollPanels[i].ScrollContainer.Elements[j].GetGUIDrawable(n).Draw(spriteBatch);
                    }
                    //spriteBatch.Draw(scrollPanels[i].ScrollContainer.GetElement(j).Texture2D, scrollPanels[i].ScrollContainer.GetElement(j).Position, Color.White);
                }
            }
            spriteBatch.End();

            // other gui batch
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, null, null, null);
            button.Image.Draw(spriteBatch);
            scrollbarA.BackgroundImage.Draw(spriteBatch);
            scrollbarA.HandleImage.Draw(spriteBatch);
            scrollbarB.BackgroundImage.Draw(spriteBatch);
            scrollbarB.HandleImage.Draw(spriteBatch);
            scrollbarC.BackgroundImage.Draw(spriteBatch);
            scrollbarC.HandleImage.Draw(spriteBatch);
            //text2D.Draw(spriteBatch);
            textInfo.Draw(spriteBatch);
            buttonText.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}