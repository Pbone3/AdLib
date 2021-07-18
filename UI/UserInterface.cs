using System;
using AdLib.DataStructures;

namespace AdLib.UI
{
    public abstract class UserInterface : UIElement
    {
        public virtual void Initialize()
        {
            Parent = null;
            Body = new Body(default, Main.VirtualWidth, Main.VirtualHeight);
            Identifier = Guid.NewGuid();
        }

        public virtual void OnOpen()
        {

        }
    }
}
