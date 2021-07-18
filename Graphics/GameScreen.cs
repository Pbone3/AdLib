using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AdLib.Graphics
{
    public class GameScreen : RenderTarget2D
    {
        public GameScreen(GraphicsDevice device, int width, int height) : base(device, width, height)
        {

        }

        public Vector2 GetScale()
        {
            Vector2 scale = new Vector2();
            throw new NotImplementedException();
        }
    }
}
