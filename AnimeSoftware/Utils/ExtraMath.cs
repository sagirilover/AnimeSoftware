using System;
using System.Numerics;

namespace AnimeSoftware.Utils
{
    public static unsafe class ExtraMath
    {
        public static float RadianToDegrees(float radians)
        {
            return radians * 180f / (float) Math.PI;
        }

        public static float DegreesToRadian(float degrees)
        {
            return degrees * (float) Math.PI / 180f;
        }

        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            else if (value.CompareTo(max) > 0)
                return max;
            else
                return value;
        }
    }
}