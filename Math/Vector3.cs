using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GameMath
{
    /// <summary>
    ///     Represents a 3 dimensional vector
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        /// <summary>
        ///     The empty
        /// </summary>
        public static readonly Vector3 Empty;

        /// <summary>
        ///     The zero
        /// </summary>
        public static readonly Vector3 Zero;

        /// <summary>
        ///     The invalid
        /// </summary>
        public static readonly Vector3 Invalid = new Vector3(float.NaN, float.NaN, float.NaN);

        /// <summary>
        ///     The size
        /// </summary>
        public static readonly int Size = 12;

        /// <summary>
        ///     The x
        /// </summary>
        public float X;

        /// <summary>
        ///     The y
        /// </summary>
        public float Y;

        /// <summary>
        ///     The z
        /// </summary>
        public float Z;

        /// <summary>
        ///     Gets or sets the <see cref="System.Single" /> at the specified index.
        /// </summary>
        /// <value>
        ///     The <see cref="System.Single" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public float this[int index]
        {
            get
            {
                if (index < 0 || index > 2) return float.NaN;

                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                }

                return Z;
            }
            set
            {
                if (index < 0 || index > 2) return;

                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        Z = value;
                        break;
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public Vector3(double x, double y, double z)
        {
            X = (float) x;
            Y = (float) y;
            Z = (float) z;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="array">The array.</param>
        public Vector3(float[] array)
        {
            if (array == null || array.Length != 3)
            {
                X = float.NaN;
                Y = float.NaN;
                Z = float.NaN;
                return;
            }

            X = array[0];
            Y = array[1];
            Z = array[2];
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> struct.
        /// </summary>
        /// <param name="array">The array.</param>
        public Vector3(byte[] array)
        {
            if (array == null || array.Length != 12)
            {
                X = float.NaN;
                Y = float.NaN;
                Z = float.NaN;
                return;
            }

            X = BitConverter.ToSingle(array, 0);
            Y = BitConverter.ToSingle(array, 4);
            Z = BitConverter.ToSingle(array, 8);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is Vector3 && Equals((Vector3) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "{X = " + X + ", Y = " + Y + ", Z = " + Z + "}";
        }

        /// <summary>
        ///     Adds the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Add(Vector3 vec)
        {
            X += vec.X;
            Y += vec.Y;
            Z += vec.Z;
        }

        /// <summary>
        ///     Adds the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Add(float vec)
        {
            X += vec;
            Y += vec;
            Z += vec;
        }

        /// <summary>
        ///     Adds the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Add(int vec)
        {
            X += vec;
            Y += vec;
            Z += vec;
        }

        /// <summary>
        ///     Subtracts the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Subtract(Vector3 vec)
        {
            X -= vec.X;
            Y -= vec.Y;
            Z -= vec.Z;
        }

        /// <summary>
        ///     Subtracts the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Subtract(float vec)
        {
            X -= vec;
            Y -= vec;
            Z -= vec;
        }

        /// <summary>
        ///     Subtracts the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Subtract(int vec)
        {
            X -= vec;
            Y -= vec;
            Z -= vec;
        }

        /// <summary>
        ///     Multiplies the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Multiply(Vector3 vec)
        {
            X *= vec.X;
            Y *= vec.Y;
            Z *= vec.Z;
        }

        /// <summary>
        ///     Multiplies the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Multiply(float vec)
        {
            X *= vec;
            Y *= vec;
            Z *= vec;
        }

        /// <summary>
        ///     Multiplies the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Multiply(int vec)
        {
            X *= vec;
            Y *= vec;
            Z *= vec;
        }

        /// <summary>
        ///     Divides the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Divide(Vector3 vec)
        {
            if (vec.X == 0.0f) vec.X = float.Epsilon;
            if (vec.Y == 0.0f) vec.Y = float.Epsilon;
            if (vec.Z == 0.0f) vec.Z = float.Epsilon;

            X /= vec.X;
            Y /= vec.Y;
            Z /= vec.Z;
        }

        /// <summary>
        ///     Divides the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Divide(float vec)
        {
            if (vec == 0.0f) vec = float.Epsilon;

            X /= vec;
            Y /= vec;
            Z /= vec;
        }

        /// <summary>
        ///     Divides the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Divide(int vec)
        {
            float tmp = vec;
            if (tmp == 0.0f) tmp = float.Epsilon;

            X /= tmp;
            Y /= tmp;
            Z /= tmp;
        }

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Vector3 Clone()
        {
            return new Vector3(X, Y, Z);
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
        }

        /// <summary>
        ///     Distances to.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <returns></returns>
        public float DistanceTo(Vector3 vec)
        {
            return (this - vec).Length();
        }

        /// <summary>
        ///     Lengthes this instance.
        /// </summary>
        /// <returns></returns>
        public float Length()
        {
            return (float) Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        ///     Lengthes the squared.
        /// </summary>
        /// <returns></returns>
        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        /// <summary>
        ///     Dots the product.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public float DotProduct(Vector3 right)
        {
            return X * right.X + Y * right.Y + Z * right.Z;
        }

        /// <summary>
        ///     Crosses the product.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public Vector3 CrossProduct(Vector3 right)
        {
            return new Vector3
            {
                X = Y * right.Z - Z * right.Y,
                Y = Z * right.X - X * right.Z,
                Z = X * right.Y - Y * right.X
            };
        }

        /// <summary>
        ///     Lerps the specified right.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public Vector3 Lerp(Vector3 right, float amount)
        {
            return new Vector3(X + (right.X - X) * amount, Y + (right.Y - Y) * amount, Z + (right.Z - Z) * amount);
        }

        /// <summary>
        ///     Gets the bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(X), 0, bytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Y), 0, bytes, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Z), 0, bytes, 8, 4);
            return bytes;
        }

        /// <summary>
        ///     Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return X == 0.0f && Y == 0.0f && Z == 0.0f;
        }

        /// <summary>
        ///     Reals the is empty.
        /// </summary>
        /// <returns></returns>
        public bool RealIsEmpty()
        {
            return X < float.Epsilon && X > -float.Epsilon && Y < float.Epsilon && Y > -float.Epsilon &&
                   Z < float.Epsilon && Z > -float.Epsilon;
        }

        /// <summary>
        ///     Determines whether [is na n].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is na n]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNaN()
        {
            return float.IsNaN(X) || float.IsNaN(Y) || float.IsNaN(Z);
        }

        /// <summary>
        ///     Determines whether this instance is infinity.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is infinity; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInfinity()
        {
            return float.IsInfinity(X) || float.IsInfinity(Y) || float.IsInfinity(Z);
        }

        /// <summary>
        ///     Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            if (IsNaN()) return false;

            return !IsInfinity();
        }

        /// <summary>
        ///     Angles the clamp.
        /// </summary>
        /// <returns></returns>
        public bool AngleClamp()
        {
            if (!IsValid()) return false;

            X = MathF.Clamp(X, -89.0f, 89.0f);

            Z = 0.0f;

            return IsValid();
        }

        /// <summary>
        ///     Angles the clamp.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        public bool AngleClamp(float min, float max)
        {
            if (!IsValid()) return false;

            X = MathF.Clamp(X, min, max);

            Z = 0.0f;

            return IsValid();
        }

        /// <summary>
        ///     Angles the normalize.
        /// </summary>
        /// <returns></returns>
        public bool AngleNormalize()
        {
            if (!IsValid()) return false;

            Y = MathF.Normalize(Y, -180.0f, 180.0f, 360.0f);

            Z = 0.0f;

            return IsValid();
        }

        /// <summary>
        ///     Angles the normalize.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="norm">The norm.</param>
        /// <returns></returns>
        public bool AngleNormalize(float min, float max, float norm)
        {
            if (!IsValid()) return false;

            Y = MathF.Normalize(Y, min, max, norm);

            Z = 0.0f;

            return IsValid();
        }

        /// <summary>
        ///     Angles the clamp and normalize.
        /// </summary>
        /// <returns></returns>
        public bool AngleClampAndNormalize()
        {
            if (!IsValid()) return false;

            X = MathF.Clamp(X, -89.0f, 89.0f);
            Y = MathF.Normalize(Y, -180.0f, 180.0f, 360.0f);
            Z = 0.0f;

            return IsValid();
        }

        /// <summary>
        ///     Vectors the normalize.
        /// </summary>
        /// <returns></returns>
        public bool VectorNormalize()
        {
            if (!IsValid()) return false;

            float length = Length();

            if (length == 0.0f) return true;

            X /= length;
            Y /= length;
            Z /= length;

            return IsValid();
        }

        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator +(Vector3 left, float right)
        {
            return new Vector3(left.X + right, left.Y + right, left.Z + right);
        }

        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator +(Vector3 left, int right)
        {
            return new Vector3(left.X + right, left.Y + right, left.Z + right);
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator -(Vector3 left, float right)
        {
            return new Vector3(left.X - right, left.Y - right, left.Z - right);
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator -(Vector3 left, int right)
        {
            return new Vector3(left.X - right, left.Y - right, left.Z - right);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator *(Vector3 left, float right)
        {
            return new Vector3(left.X * right, left.Y * right, left.Z * right);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator *(Vector3 left, int right)
        {
            return new Vector3(left.X * right, left.Y * right, left.Z * right);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator /(Vector3 left, float right)
        {
            return new Vector3(left.X / right, left.Y / right, left.Z / right);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector3 operator /(Vector3 left, int right)
        {
            return new Vector3(left.X / right, left.Y / right, left.Z / right);
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.X == right.X
                   && left.Y == right.Y
                   && left.Z == right.Z;
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(Vector3 left, float right)
        {
            return left.X == right
                   && left.Y == right
                   && left.Z == right;
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(Vector3 left, int right)
        {
            return left.X == right
                   && left.Y == right
                   && left.Z == right;
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return left.X != right.X
                   || left.Y != right.Y
                   || left.Z != right.Z;
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(Vector3 left, float right)
        {
            return left.X != right
                   || left.Y != right
                   || left.Z != right;
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(Vector3 left, int right)
        {
            return left.X != right
                   || left.Y != right
                   || left.Z != right;
        }

        /// <summary>
        ///     Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <(Vector3 left, Vector3 right)
        {
            return left.X < right.X
                   && left.Y < right.Y
                   && left.Z < right.Z;
        }

        /// <summary>
        ///     Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <(Vector3 left, float right)
        {
            return left.X < right
                   && left.Y < right
                   && left.Z < right;
        }

        /// <summary>
        ///     Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <(Vector3 left, int right)
        {
            return left.X < right
                   && left.Y < right
                   && left.Z < right;
        }

        /// <summary>
        ///     Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >(Vector3 left, Vector3 right)
        {
            return left.X > right.X
                   && left.Y > right.Y
                   && left.Z > right.Z;
        }

        /// <summary>
        ///     Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >(Vector3 left, float right)
        {
            return left.X > right
                   && left.Y > right
                   && left.Z > right;
        }

        /// <summary>
        ///     Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >(Vector3 left, int right)
        {
            return left.X > right
                   && left.Y > right
                   && left.Z > right;
        }

        /// <summary>
        ///     Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <=(Vector3 left, Vector3 right)
        {
            return left == right || left < right;
        }

        /// <summary>
        ///     Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <=(Vector3 left, float right)
        {
            return left == right || left < right;
        }

        /// <summary>
        ///     Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <=(Vector3 left, int right)
        {
            return left == right || left < right;
        }

        /// <summary>
        ///     Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >=(Vector3 left, Vector3 right)
        {
            return left == right || left > right;
        }

        /// <summary>
        ///     Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >=(Vector3 left, float right)
        {
            return left == right || left > right;
        }

        /// <summary>
        ///     Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >=(Vector3 left, int right)
        {
            return left == right || left > right;
        }

        /// <summary>
        ///     Distances the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector3 left, Vector3 right)
        {
            return left.DistanceTo(right);
        }

        /// <summary>
        ///     Dots the product.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DotProduct(Vector3 left, Vector3 right)
        {
            return left.DotProduct(right);
        }

        /// <summary>
        ///     Crosses the product.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 CrossProduct(Vector3 left, Vector3 right)
        {
            return left.CrossProduct(right);
        }

        /// <summary>
        ///     Lerps the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Lerp(Vector3 left, Vector3 right, float amount)
        {
            return left.Lerp(right, amount);
        }

        /// <summary>
        ///     Angles the clamp.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AngleClamp(Vector3 vec)
        {
            var tmp = vec.Clone();
            tmp.AngleClamp();
            return tmp;
        }

        /// <summary>
        ///     Angles the clamp.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AngleClamp(Vector3 vec, float min, float max)
        {
            var tmp = vec.Clone();
            tmp.AngleClamp(min, max);
            return tmp;
        }

        /// <summary>
        ///     Angles the normalize.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AngleNormalize(Vector3 vec)
        {
            var tmp = vec.Clone();
            tmp.AngleNormalize();
            return tmp;
        }

        /// <summary>
        ///     Angles the normalize.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="norm">The norm.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AngleNormalize(Vector3 vec, float min, float max, float norm)
        {
            var tmp = vec.Clone();
            tmp.AngleNormalize(min, max, norm);
            return tmp;
        }
    }
}