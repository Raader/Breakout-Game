using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout_Game
{
    class Brick : IGameObject
    {
        Vector2 position;
        Texture2D texture;
        Color color;

        public void Initialize(Texture2D texture, Vector2 position,Color color)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
        }
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(texture, new Rectangle(position.ToPoint(), new Point(texture.Width * Global.scale, texture.Height * Global.scale)), color);
        }
    }
}
