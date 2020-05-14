using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout_Game
{
    class Player : IGameObject
    {
        Texture2D texture;
        public Vector2 position;
        public Rectangle hitBox;
        float speed = 50 * Global.scale;
        float dir = 0;
        float minX =36 * Global.scale;
        float maxX = 116 * Global.scale;

        public void Initialize(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            hitBox = new Rectangle(position.ToPoint(), new Point(texture.Width * Global.scale, texture.Height * Global.scale));
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
            hitBox.Location = position.ToPoint();
            sprite.Draw(texture, hitBox, Color.White);
        }
    }
}
