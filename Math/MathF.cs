using System;
using System.Runtime.CompilerServices;

namespace GameMath
{
    /// <summary>
    ///     Provides methods for float math
    /// </summary>
    public static class MathF
    {
        private static readonly Random RandomNumberGenerator = new Random();

        /// <summary>
        ///     PI
        /// </summary>
        public static readonly float Pi = 3.14159274f;

        /// <summary>
        ///     DEG_2_RAD
        /// </summary>
        public static readonly float Deg2Rad = (float) (Math.PI / 180f);

        /// <summary>
        ///     RAD_2_DEG
        /// </summary>
        public static readonly float Rad2Deg = (float) (180.0f / Math.PI);

        /// <summary>
        ///     Returns the absolute value
        /// </summary>
        /// <param name="x">value</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float x)
        {
            return Math.Abs(x);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float x)
        {
            return (float) Math.Acos(x);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float x)
        {
            return (float) Math.Cos(x);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IeeeRemainder(float x, float y)
        {
            return (float) Math.IEEERemainder(x, y);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, float y)
        {
            return (float) Math.Pow(x, y);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float x)
        {
            return (float) Math.Sin(x);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float x)
        {
            return (float) Math.Sqrt(x);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float x)
        {
            return (float) Math.Tan(x);
        }

        /// <summary>
        ///     Clamps the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;

            return value > max ? max : value;
        }

        /// <summary>
        ///     Degreeses to radians.
        /// </summary>
        /// <param name="deg">The deg.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DegreesToRadians(float deg)
        {
            return deg * Deg2Rad;
        }

        /// <summary>
        ///     Normalizes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize(float value, float min, float max)
        {
            float norm = max < 0.0f ? max * -1.0f : max;

            while (value < min)
                value += norm;
            while (value > max)
                value -= norm;

            return value;
        }

        /// <summary>
        ///     Normalizes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="norm">The norm.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize(float value, float min, float max, float norm)
        {
            while (value < min)
                value += norm;
            while (value > max)
                value -= norm;

            return value;
        }

        /// <summary>
        ///     Radianses to degrees.
        /// </summary>
        /// <param name="rad">The RAD.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RadiansToDegrees(float rad)
        {
            return rad * Rad2Deg;
        }

        /// <summary>
        ///     Random float.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomFloat(float min, float max)
        {
            return (float) RandomNumberGenerator.NextDouble() * (max - min) + min;
        }

        /// <summary>
        ///     Random int.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomInt(int min, int max)
        {
            return RandomNumberGenerator.Next(min, max);
        }
    }
}