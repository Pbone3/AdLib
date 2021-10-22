using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdLib
{
    public interface IDrawable
    {
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
