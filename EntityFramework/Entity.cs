using AdLib.Collections;
using Microsoft.Xna.Framework;

namespace AdLib.EntityFramework
{
    public class Entity
    {
        public BehaviourCollection Behaviours;
        public PropertyCollection Properties;

        public void Update(GameTime gameTime)
        {
            Behaviours.UpdateAll(this, gameTime);
        }
    }
}
