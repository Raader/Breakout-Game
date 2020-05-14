using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Breakout_Game
{
    class Ball : IGameObject
    {
        Random random;
        Vector2 position;
        Texture2D texture;
        public Rectangle hitBox;
        public Action outOfBounds;
        Rectangle bounds;
        Vector2 direction = new Vector2(1, -1);
        float speed = 60 * Global.scale;

        public void Initialize(Vector2 position, Texture2D texture, Rectangle bounds)
        {
            this.position = position;
            this.texture = texture;
            this.bounds = bounds;
            hitBox = new Rectangle(position.ToPoint(), new Point(texture.Width * Global.scale, texture.Height * Global.scale));
            random = new Random();
            direction.Normalize();
        }

        public void Update(GameTime gameTime)
        {
            position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds * direction;
            hitBox.Location = position.ToPoint();
            if (hitBox.Right >= bounds.Right)
            {
                Bounce(new Vector2(-1,0));
            }
            else if(position.ToPoint().X <= bounds.X)
            {
                Bounce(new Vector2(1, 0));
            }
            else if(position.ToPoint().Y <= bounds.Y)
            {
                Bounce(new Vector2(0, 1));
            }
            if (position.ToPoint().Y >= bounds.Bottom)
            {
                outOfBounds?.Invoke();
            }
        }

        public void Bounce(Vector2 normal)
        {           
            int ranI = random.Next(0, 101);
            if(ranI < 60)
            {
                direction = Vector2.Reflect(direction, normal);
            }
            else 
            {
                int i = random.Next(0, 2) == 0 ? 1 : -1;
                if (normal.X == 0)
                {
                    normal.X = i;
                }
                else
                {
                    normal.Y = i;
                }
                normal.Normalize();
                direction = normal;
            }
            //speed += 5;
        }

        public void Draw(SpriteBatch sprite)
        {
            
            sprite.Draw(texture, hitBox, Color.White);
        }
    }
}
