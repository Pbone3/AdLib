using Microsoft.Xna.Framework;

namespace AdLib.EntityFramework
{
    public abstract class Behaviour
    {
        public virtual void Update(Entity entity, GameTime gameTime) { }
    }
}