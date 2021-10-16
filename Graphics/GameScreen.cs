using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AdLib.Graphics
{
    public class GameScreen : RenderTarget2D
    {
        public ScreenScaleType ScaleMode;

        public GameScreen(GraphicsDevice device, int width, int height) : base(device, width, height)
        {
            ScaleMode = ScreenScaleType.Original;
        }

        public void GetScalingInfo(out Vector2 scale, out Vector2 position)
        {
            scale = new Vector2();
            position = new Vector2();
            throw new NotImplementedException();
        }
    }
}
