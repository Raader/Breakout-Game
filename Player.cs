using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout_Game
{
    class Player : GameObject
    {
        Texture2D texture;
        Vector2 position;
        float speed = 50;
        float dir = 0;
        float minX =72;
        float maxX = 232;
        public void Initialize(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            ChangeDirection(state);
            Move(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            position.X += dir * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(position.X > maxX)
            {
                position.X = maxX;
            }
            else if(position.X < minX)
            {
                position.X = minX;
            }
        }

        private void ChangeDirection(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.D))
            {
                dir = 1;
            }
            else if (state.IsKeyDown(Keys.A))
            {
                dir = -1;
            }
            else
            {
                dir = 0;
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, new Rectangle(position.ToPoint(), new Point(texture.Width * Global.scale, texture.Height * Global.scale)), Color.White);
        }
    }
}
