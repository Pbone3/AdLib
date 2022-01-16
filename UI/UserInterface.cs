using System;
using AdLib.DataStructures;

namespace AdLib.UI
{
    public abstract class UserInterface : UIElement
    {
        private int GameWidth;
        private int GameHeight;

        public UserInterface(int gameWidth, int gameHeight)
        {
            GameWidth = gameWidth;
            GameHeight = gameHeight;
        }

        public virtual void Initialize()
        {
            Parent = null;
            Body = new Body(default, GameWidth, GameHeight);
            Identifier = Guid.NewGuid();
        }

        public virtual void OnOpen()
        {

        }
    }
}
