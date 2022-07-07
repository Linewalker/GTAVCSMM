using System;
using System.Runtime.CompilerServices;

namespace GameMath
{
    /// <summary>
    ///     Provides static methods for 2d math.
    /// </summary>
    public static class Math2
    {
        /// <summary>
        ///     Calculates the distance of a point to the center of a circle.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="circleCenter">The circle center.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceInCircle(Vector2 point, Vector2 circleCenter)
        {
            return Math.Sqrt((circleCenter.X - point.X) * (circleCenter.X - point.X) +
                             (circleCenter.Y - point.Y) * (circleCenter.Y - point.Y));
        }

        /// <summary>
        ///     Calculates the distance of a point to the center of the screen.
        /// </summary>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        /// <param name="screen">The screen.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceOnScreen(float X, float Y, Vector2 screen)
        {
            return (float) Math.Sqrt(Math.Pow(Y - screen.Y / 2, 2) + Math.Pow(X - screen.X / 2, 2));
        }

        /// <summary>
        ///     Calculates the distance of a point to the center of the screen.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="screen">The screen.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceOnScreen(Vector2 point, Vector2 screen)
        {
            return (float) Math.Sqrt(Math.Pow(point.Y - screen.Y / 2, 2) + Math.Pow(point.X - screen.X / 2, 2));
        }

        /// <summary>
        ///     Determines whether a given point is inside of a circle.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="circleCenter">The circle center.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>
        ///     <c>true</c> if [is point in circle] [the specified point]; otherwise, <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPointInCircle(Vector2 point, Vector2 circleCenter, int radius)
        {
            return Math.Sqrt((circleCenter.X - point.X) * (circleCenter.X - point.X) +
                             (circleCenter.Y - point.Y) * (circleCenter.Y - point.Y)) < radius;
        }
    }
}