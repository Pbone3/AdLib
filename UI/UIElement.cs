using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using AdLib.DataStructures;

namespace AdLib.UI
{
    public abstract class UIElement
    {
        public UIElement Parent;
        public Dictionary<Guid, UIElement> Children = new Dictionary<Guid, UIElement>();
        public Body Body = new Body();

        public Guid Identifier;

        public UIElement()
        {
            Identifier = Guid.NewGuid();
        }

        public virtual void UpdateSelf(GameTime gameTime) { }
        public void Update(GameTime gameTime)
        {
            UpdateSelf(gameTime);
            UpdateChildren(gameTime);
        }
        public void UpdateChildren(GameTime gameTime)
        {
            foreach (KeyValuePair<Guid, UIElement> child in Children)
                child.Value.Update(gameTime);
        }

        public virtual void DrawSelf(SpriteBatch spriteBatch) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawSelf(spriteBatch);
            DrawChildren(spriteBatch);
        }
        public void DrawChildren(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<Guid, UIElement> child in Children)
                child.Value.Draw(spriteBatch);
        }

        public void AddChild(UIElement child)
        {
            Children.Add(child.Identifier, child);
            child.Parent = this;
        }
        public void RemoveChild(UIElement child)
        {
            Children.Remove(child.Identifier);
            child.Parent = null;
        }
    }
}
