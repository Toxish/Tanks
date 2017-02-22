using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tankowar
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        enum GameState
        {
            MainMenu,
            Gameplay,
            EndOfGame
        }

        public class Tank
        {

        }
        enum direct { up, down, left, right };
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture, bullet, enemy;
        Vector2 position = Vector2.Zero;
        Vector2 enemypos = new Vector2();
        Vector2 position1 = new Vector2();
        float speed = 4f, speedb = 16f, espeed = 4f;
        int frameWidth = 40;
        int frameHeight = 40;
        Point currentFrame = new Point(0, 0);
        Point spriteSize = new Point(4, 1);
        Point ecurrentFrame = new Point(0, 0);
        Point espriteSize = new Point(4, 1);
        bool block = false, shot = false;
        direct drcbul = 0;
        direct drctank = 0;
        Color color = Color.White;
        private GameState gameScreen = GameState.MainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 30);
            enemypos = new Vector2(Window.ClientBounds.Width - 40, 0);
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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
            texture = Content.Load<Texture2D>("tank");
            enemy = Content.Load<Texture2D>("enemy");
            bullet = Content.Load<Texture2D>("bullet");

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
            base.Update(gameTime);
            switch (gameScreen)
            {
                case GameState.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameState.Gameplay:
                    UpdateGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    UpdateEndOfGame(gameTime);
                    break;
            }
        }

        void UpdateMainMenu(GameTime gameTime)
        {
            // Respond to user input for menu selections, etc
            //if (pushedStartGameButton)
            //    _state = GameState.GamePlay;
        }

        void UpdateGameplay(GameTime gameTime)
        {
            // Respond to user actions in the game.
            // Update enemies
            // Handle collisions


            KeyboardState keyboardState = Keyboard.GetState();
            if (Collide())
            {
                if (drctank == direct.up)
                {
                    position.Y += speed;
                }
                if (drctank == direct.down)
                {
                    position.Y -= speed;
                }
                if (drctank == direct.left)
                {
                    position.X += speed;
                }
                if (drctank == direct.right)
                {
                    position.X -= speed;
                }

            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (keyboardState.IsKeyUp(Keys.Left) || keyboardState.IsKeyUp(Keys.Right) || keyboardState.IsKeyUp(Keys.Up) || keyboardState.IsKeyUp(Keys.Down))
            {
                block = false;
            }
            if (keyboardState.IsKeyDown(Keys.Left) && !block && position.X > 0)
            {
                block = true;
                position.X -= speed;
                currentFrame.X = 3;
                drctank = direct.left;
            }
            if (keyboardState.IsKeyDown(Keys.Right) && !block && position.X < Window.ClientBounds.Width - 40)
            {
                block = true;
                position.X += speed;
                currentFrame.X = 2;
                drctank = direct.right;
            }
            if (keyboardState.IsKeyDown(Keys.Up) && !block && position.Y > 0)
            {
                block = true;
                position.Y -= speed;
                currentFrame.X = 0;
                drctank = direct.up;
            }
            if (keyboardState.IsKeyDown(Keys.Down) && !block && position.Y < Window.ClientBounds.Height - 40)
            {
                block = true;
                position.Y += speed;
                currentFrame.X = 1;
                drctank = direct.down;
            }
            if (keyboardState.IsKeyDown(Keys.Space) && !shot)
            {
                shot = true;
                if (drctank == direct.up)
                {
                    position1.X = position.X + 18;
                    position1.Y = position.Y + 13;
                    drcbul = direct.up;
                }
                if (drctank == direct.down)
                {
                    position1.X = position.X + 18;
                    position1.Y = position.Y + 25;
                    drcbul = direct.down;
                }
                if (drctank == direct.left)
                {
                    position1.X = position.X + 8;
                    position1.Y = position.Y + 18;
                    drcbul = direct.left;
                }
                if (drctank == direct.right)
                {
                    position1.X = position.X + 20;
                    position1.Y = position.Y + 18;
                    drcbul = direct.right;
                }
            }
            if (shot)
            {
                if (CollideBullet())
                {

                }
                if (drcbul == direct.up)
                {
                    position1.Y -= speedb;
                }
                if (drcbul == direct.down)
                {
                    position1.Y += speedb;
                }
                if (drcbul == direct.right)
                {
                    position1.X += speedb;
                }
                if (drcbul == direct.left)
                {
                    position1.X -= speedb;
                }
            }
            if (position1.X > Window.ClientBounds.Width || position1.X < 0 || position1.Y > Window.ClientBounds.Height || position1.Y < 0)
            {
                shot = false;
            }

            //if (playerDied)
            //    _state = GameState.EndOfGame;
        }

        void UpdateEndOfGame(GameTime gameTime)
        {
            // Update scores
            // Do any animations, effects, etc for getting a high score
            // Respond to user input to restart level, or go back to main menu
            //if (pushedMainMenuButton)
            //    _state = GameState.MainMenu;
            //else if (pushedRestartLevelButton)
            //{
            //    ResetLevel();
            //    _state = GameState.Gameplay;
            //}
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            switch (gameScreen)
            {
                case GameState.MainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case GameState.Gameplay:
                    DrawGameplay(gameTime);
                    break;
                case GameState.EndOfGame:
                    DrawEndOfGame(gameTime);
                    break;
            }
            GraphicsDevice.Clear(color);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(texture, position,
                new Rectangle(currentFrame.X * frameWidth,
                    currentFrame.Y * frameHeight,
                    frameWidth, frameHeight),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
            spriteBatch.Draw(enemy, enemypos,
                new Rectangle(ecurrentFrame.X * frameWidth,
                    ecurrentFrame.Y * frameHeight,
                    frameWidth, frameHeight),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
            if (shot)
            {
                spriteBatch.Draw(bullet, position1, Color.White);
            }
            spriteBatch.End();
        }

        void DrawMainMenu(GameTime gameTime)
        {
            // Draw the main menu, any active selections, etc
        }

        void DrawGameplay(GameTime gameTime)
        {
            // Draw the background the level
            // Draw enemies
            // Draw the player
            // Draw particle effects, etc
        }

        void DrawEndOfGame(GameTime gameTime)
        {
            // Draw text and scores
            // Draw menu for restarting level or going back to main menu
        }

        protected bool Collide()
        {
            Rectangle goodSpriteRect = new Rectangle((int)position.X,
                (int)position.Y, spriteSize.X * 10 + 1, spriteSize.Y * 40 + 2);
            Rectangle evilSpriteRect = new Rectangle((int)enemypos.X,
                (int)enemypos.Y, espriteSize.X * 10 + 1, espriteSize.Y * 40 + 2);

            return goodSpriteRect.Intersects(evilSpriteRect);
        }
        protected bool CollideBullet()
        {
            Rectangle boolSpriteRect = new Rectangle((int)position1.X,
              (int)position1.Y, 2, 2);
            Rectangle evilSpriteRect = new Rectangle((int)enemypos.X,
                (int)enemypos.Y, espriteSize.X * 10 + 1, espriteSize.Y * 40 + 2);

            return evilSpriteRect.Intersects(boolSpriteRect);
        }
    }
}
