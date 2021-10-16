using Microsoft.Xna.Framework;

namespace AdLib
{
    public interface IUpdateManager : IManager<IUpdateManager>
    {
        public void Update(GameTime gameTime);
    }
}
