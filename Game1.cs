using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        Ball ball;
        Texture2D playerTexture;
        Texture2D brickTexture;
        Texture2D ballTexture;
        SpriteFont font;
        int score = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Global.scale * 160;
            graphics.PreferredBackBufferHeight = Global.scale * 192;
        }

        protected override void Initialize()
        {          
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameArea = Content.Load<Texture2D>("Breakoutbackground");
            playerTexture = Content.Load<Texture2D>("breakoutplayer");
            brickTexture = Content.Load<Texture2D>("breakoutbrick");
            ballTexture = Content.Load<Texture2D>("breakoutball2");
            font = Content.Load<SpriteFont>("myfont");
            InitGame();
        }

        private void InitGame()
        {
            score = 0;
            player = new Player();
            wall = new Wall();
            ball = new Ball();
            player.Initialize(playerTexture, new Vector2(75 * Global.scale, 156 * Global.scale));
            wall.Initialize(brickTexture, new Vector2(36 * Global.scale, 48 * Global.scale));
            ball.Initialize(new Vector2(50, 140) * Global.scale, ballTexture, new Rectangle(new Point(36 * Global.scale, 24 * Global.scale), new Point(87 * Global.scale, 151 * Global.scale)));
            ball.outOfBounds += ResetGame;
        }

        void ResetGame()
        {
            Console.WriteLine("reset");
            InitGame();
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
            if (player.hitBox.Intersects(ball.hitBox))
            {
                ball.Bounce(Physics.GetNormal(ball.hitBox.Center,player.hitBox));
            }
            else
            {
                Break();
            }

            player.Update(gameTime);
            ball.Update(gameTime);
            
            base.Update(gameTime);
        }

        private void Break()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 11; column++)
                {
                    Brick brick = wall.bricks[row, column];
                    if (brick != null && ball.hitBox.Intersects(brick.HitBox))
                    {
                        wall.bricks[row, column] = null;
                        Console.WriteLine("break");
                        Random random = new Random();
                        Vector2 normal = random.Next(0, 2) == 0 ? new Vector2(0, 1) : new Vector2(1, 0);
                        ball.Bounce(Physics.GetNormal(ball.hitBox.Center,brick.HitBox));
                        score++;
                        return;
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(gameArea, new Rectangle(0, 0, gameArea.Width * Global.scale, gameArea.Height * Global.scale), Color.White);
            player.Draw(spriteBatch);
            wall.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.DrawString(font, score.ToString(), new Vector2(48, 32) * Global.scale, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public interface IGameObject
    {
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

    }

    class Physics
    {
        public static Vector2 GetNormal(Point position, Rectangle hitBox)
        {
            if(position.Y >= hitBox.Y && position.Y <= hitBox.Bottom)
            {
                if(position.X < hitBox.Center.X)
                {
                    return new Vector2(-1, 0);
                }
                else if (position.X > hitBox.Center.X)
                {
                    return new Vector2(1, 0);
                }
            }
            else if(position.X >= hitBox.X && position.X <= hitBox.Right)
            {
                if(position.Y < hitBox.Center.Y)
                {
                    return new Vector2(0, -1);
                }
                else if( position.Y > hitBox.Center.Y)
                {
                    return new Vector2(0, 1);
                }
            }
            return new Vector2(0, 0);
        }
    }
}
