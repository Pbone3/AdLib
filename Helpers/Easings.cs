using System;

namespace AdLib.Helpers
{
    // Thanks to https://easings.net/
    // 0 is the beginning, 1 is the end
    public static class Easings
    {
        public const double TwoPiOver3 = 2d * Math.PI / 3d;
        public const double TwoPiOver4Point5 = (2d * Math.PI) / 4.5d;

        public const double EaseInBack_Overshoot = 1.70158d;
        public const double EaseInBack_OvershootPlus1 = EaseInBack_Overshoot + 1d;
        public const double EaseInBack_SomethingOrOther = EaseInBack_Overshoot * 1.525d;

        public static double EaseInSine(double v) => 1d - Math.Cos((v * Math.PI) / 2d);
        public static double EaseOutSine(double v) => Math.Sin((v * Math.PI) / 2d);
        public static double EaseInOutSine(double v) => -(Math.Cos(Math.PI * v) - 1d) / 2d;

        public static double EaseInCubic(double v) => v * v * v;
        public static double EaseOutCubic(double v) => 1d - Math.Pow(1d - v, 3d);
        public static double EaseInOutCubic(double v) => v < 0.5d
            ? 4d * v * v * v
            : 1d - Math.Pow(-2d * v + 2d, 3d) / 2d;

        public static double EaseInQuint(double v) => v * v * v * v * v;
        public static double EaseOutQuint(double v) => 1d - Math.Pow(1d - v, 5);
        public static double EaseInOutQuint(double v) => v < 0.5d
            ? 16d * v * v * v * v * v
            : 1d - Math.Pow(-2d * v + 2d, 5d) / 2d;

        public static double EaseInCirc(double v) => 1d - Math.Sqrt(1d - Math.Pow(v, 2d));
        public static double EaseOutCirc(double v) => Math.Sqrt(1d - Math.Pow(v - 1d, 2d));
        public static double EaseInOutCirc(double v) => v < 0.5d
            ? (1d - Math.Sqrt(1d - Math.Pow(2d * v, 2d))) / 2d
            : (Math.Sqrt(1d - Math.Pow(-2d * v + 2d, 2d)) + 1d) / 2d;

        public static double EaseInElastic(double v) => v == 0d
            ? 0d
            : v == 1d
            ? 1d
            : -Math.Pow(2d, 10d * v - 10d) * Math.Sin((v * 10d - 10.75d) * TwoPiOver3);
        public static double EaseOutElastic(double v) => v == 0d
            ? 0d
            : v == 1d
            ? 1d
            : Math.Pow(2d, -10d * v) * Math.Sin((v * 10d - 0.75d) * TwoPiOver3) + 1d;
        public static double EaseInOutElastic(double v) => v == 0d
            ? 0d
            : v == 1d
            ? 1d
            : v < 0.5d
            ? -(Math.Pow(2d, 20d * v - 10d) * Math.Sin((20d * v - 11.125d) * TwoPiOver4Point5)) / 2d
            : (Math.Pow(2d, -20d * v + 10d) * Math.Sin((20d * v - 11.125d) * TwoPiOver4Point5)) / 2d + 1d;

        public static double EaseInQuad(double v) => v * v;
        public static double EaseOutQuad(double v) => 1d - (1d - v) * (1d - v);
        public static double EaseInOutQuad(double v) => v < 0.5d
            ? 2d * v * v
            : 1d - Math.Pow(-2d * v + 2d, 2d) / 2d;

        public static double EaseInQuart(double v) => v * v * v * v;
        public static double EaseOutQuart(double v) => 1d - Math.Pow(1d - v, 4d);
        public static double EaseInOutQuart(double v) => v < 0.5d
            ? 8d * v * v * v * v
            : 1d - Math.Pow(-2d * v + 2d, 4d) / 2d;

        public static double EaseInExpo(double v) => v == 0d ? 0d : Math.Pow(2d, 10d * v - 10d);
        public static double EaseOutExpo(double v) => v == 1d ? 1d : 1d - Math.Pow(2d, -10d * v);
        public static double EaseInOutExpo(double v) => v == 0d
            ? 0d
            : v == 1d
            ? 1d
            : v < 0.5d ? Math.Pow(2d, 20d * v - 10d) / 2d
            : (2d - Math.Pow(2d, -20d * v + 10d)) / 2d;
        
        public static double EaseInBack(double v) => EaseInBack_OvershootPlus1 * v * v * v - EaseInBack_Overshoot * v * v;
        public static double EaseOutBack(double v) => 1d + EaseInBack_OvershootPlus1 * Math.Pow(v - 1d, 3d) + EaseInBack_Overshoot * Math.Pow(v - 1d, 2d);
        public static double EaseInOutBack(double v) => v < 0.5d
            ? (Math.Pow(2d * v, 2d) * ((EaseInBack_SomethingOrOther + 1d) * 2d * v - EaseInBack_SomethingOrOther + 1d)) / 2d
            : (Math.Pow(2d * v - 2d, 2d) * ((EaseInBack_SomethingOrOther + 1d) * (v * 2d - 2d) + EaseInBack_SomethingOrOther + 1d) + 2d) / 2d;

        // These three are really wierd, look into it
        public static double EaseInBounce(double v) => 0f;
        public static double EaseOutBounce(double v) => 0f;
        public static double EaseInOutBounce(double v) => 0f;
    }
}
