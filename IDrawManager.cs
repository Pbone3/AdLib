using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdLib
{
    public interface IDrawManager : IManager<IDrawManager>
    {
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
