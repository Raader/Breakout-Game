using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout_Game
{
    class Wall : IGameObject
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
            bricks = new Brick[8, 11];
            Vector2 nextPos = position;
            Color[] colors = new Color[] { Color.Red, Color.Orange, Color.Green, Color.Yellow };
            int nextColor = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 11; column++)
                {
                    Brick brick = new Brick();
                    brick.Initialize(brickTexture, nextPos,colors[nextColor]);
                    bricks[row, column] = brick;
                    nextPos.X += brickTexture.Width * Global.scale;

                }
                nextPos.Y += brickTexture.Height * Global.scale;
                nextPos.X = position.X;
                if((row + 1) % 2 == 0)
                {
                    nextColor++;
                }
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
}
