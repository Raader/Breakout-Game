using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D gameArea;
        Player player;
        Wall wall;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Global.scale * 160;
            graphics.PreferredBackBufferHeight = Global.scale * 192;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameArea = Content.Load<Texture2D>("Breakoutbackground");
            Texture2D playerTexture = Content.Load<Texture2D>("breakoutplayer");
            Texture2D brickTexture = Content.Load<Texture2D>("breakoutbrick");
            player = new Player();
            player.Initialize(playerTexture, new Vector2(75 * Global.scale, 156 * Global.scale));
            wall = new Wall();
            wall.Initialize(brickTexture, new Vector2(36 * Global.scale, 48 * Global.scale));
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(gameArea, new Rectangle(0, 0, gameArea.Width * Global.scale, gameArea.Height * Global.scale), Color.White);
            player.Draw(spriteBatch);
            wall.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }

    public interface GameObject
    {
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

    }

    class Wall : GameObject
    {
        Vector2 position;
        Brick[,] bricks;

        public void Initialize(Texture2D brickTexture, Vector2 position)
        {
            //init
            this.position = position;
            //construct bricks
            ConstructBricks(brickTexture);

        }

        private void ConstructBricks(Texture2D brickTexture)
        {
            bricks = new Brick[7, 11];
            Vector2 nextPos = position;
            for (int row = 0; row < 7; row++)
            {
                for (int column = 0; column < 11; column++)
                {
                    Brick brick = new Brick();
                    brick.Initialize(brickTexture, nextPos);
                    bricks[row, column] = brick;
                    nextPos.X += brickTexture.Width * Global.scale;

                }
                nextPos.Y += brickTexture.Height * Global.scale;
                nextPos.X = position.X;
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sprite)
        {
            foreach(Brick brick in bricks)
            {
                brick.Draw(sprite);
            }
        }
    }

    class Brick : GameObject
    {
        Vector2 position;
        Texture2D texture;

        public void Initialize(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, new Rectangle(position.ToPoint(), new Point(texture.Width * Global.scale, texture.Height * Global.scale)), Color.White);
        }
    }

    class Global
    {
        public static int scale = 2;
    }
}
