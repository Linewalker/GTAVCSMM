using System.Runtime.InteropServices;

namespace GameMath
{
    /// <summary>
    ///     Represents a 3x4 fields Matrix.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix3x4
    {
        /// <summary>
        ///     The M11
        /// </summary>
        public float M11;

        /// <summary>
        ///     The M12
        /// </summary>
        public float M12;

        /// <summary>
        ///     The M13
        /// </summary>
        public float M13;

        /// <summary>
        ///     The M14
        /// </summary>
        public float M14;

        /// <summary>
        ///     The M21
        /// </summary>
        public float M21;

        /// <summary>
        ///     The M22
        /// </summary>
        public float M22;

        /// <summary>
        ///     The M23
        /// </summary>
        public float M23;

        /// <summary>
        ///     The M24
        /// </summary>
        public float M24;

        /// <summary>
        ///     The M31
        /// </summary>
        public float M31;

        /// <summary>
        ///     The M32
        /// </summary>
        public float M32;

        /// <summary>
        ///     The M33
        /// </summary>
        public float M33;

        /// <summary>
        ///     The M34
        /// </summary>
        public float M34;

        /// <summary>
        ///     Gets the left.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetLeft()
        {
            return GetRight() * -1f;
        }

        /// <summary>
        ///     Gets the right.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetRight()
        {
            return new Vector3(M11, M21, M31);
        }

        /// <summary>
        ///     Gets up.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetUp()
        {
            return new Vector3(M12, M22, M33);
        }

        /// <summary>
        ///     Gets down.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDown()
        {
            return GetUp() * -1f;
        }

        /// <summary>
        ///     Gets the forward.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetForward()
        {
            return GetBackward() * -1f;
        }

        /// <summary>
        ///     Gets the backward.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetBackward()
        {
            return new Vector3(M13, M23, M33);
        }
    }
}