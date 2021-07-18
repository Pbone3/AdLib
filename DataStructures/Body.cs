using Microsoft.Xna.Framework;

namespace AdLib.DataStructures
{
    public struct Body
    {
        public Vector2 HalfSize => new Vector2(Width / 2f, Height / 2f);

        public float Left => Position.X;
        public float Right => Position.X + Width;
        public float Top => Position.Y;
        public float Bottom => Position.Y + Height;
        public Vector2 Center
        {
            get => Position + HalfSize;
            set => Position = value - HalfSize;
        }

        public Vector2 Position;
        public float Width;
        public float Height;

        public Body(Vector2 position, float width, float height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public bool Contains(Vector2 point) => Contains(point.X, point.Y);
        public bool Contains(Point point) => Contains(point.X, point.Y);
        public bool Contains(float x, float y) =>
            x >= Left && x <= Right &&
            y >= Top && y <= Bottom;
    }
}
