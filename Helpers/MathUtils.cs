namespace AdLib.Helpers
{
    public static class MathUtils
    {
        public static float InverseLerp(float min, float max, float value) => (value - min) / (max - min);
    }
}
