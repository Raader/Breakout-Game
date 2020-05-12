using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout_Game
{
    class Ball : IGameObject
    {
        Vector2 position;
        Texture2D texture;
        public Rectangle hitBox;
        Rectangle bounds;
        Vector2 direction = new Vector2(-1, -1);
        float speed = 60;

        public void Initialize(Vector2 position, Texture2D texture, Rectangle bounds)
        {
            this.position = position;
            this.texture = texture;
            this.bounds = bounds;
            hitBox = new Rectangle(position.ToPoint(), new Point(texture.Width * Global.scale, texture.Height * Global.scale));
            direction.Normalize();
        }

        public void Update(GameTime gameTime)
        {
            position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds * direction;
            if(position.ToPoint().X >= bounds.Right)
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
        }

        public void Bounce(Vector2 normal)
        {
            direction = Vector2.Reflect(direction, normal);
        }

        public void Draw(SpriteBatch sprite)
        {
            hitBox.Location = position.ToPoint();
            sprite.Draw(texture, hitBox, Color.White);
        }
    }
}
