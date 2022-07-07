using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GameMath
{
    /// <summary>
    ///     Represents a 2 dimensional vector
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
    {
        /// <summary>
        ///     Represents an ampty vector2
        /// </summary>
        public static readonly Vector2 Empty;

        /// <summary>
        ///     Represents an invalid vector2
        /// </summary>
        public static readonly Vector2 Invalid = new Vector2(float.NaN, float.NaN);

        /// <summary>
        ///     The size of a vector2 in memory
        /// </summary>
        public static readonly int Size = 8;

        /// <summary>
        ///     The x
        /// </summary>
        public float X;

        /// <summary>
        ///     The y
        /// </summary>
        public float Y;

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
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                }

                return index < 0 ? X : Y;
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        if (index < 0) X = value;
                        else Y = value;
                        break;
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="array">The array.</param>
        public Vector2(float[] array)
        {
            if (array == null || array.Length < 2)
            {
                X = float.NaN;
                Y = float.NaN;
                return;
            }

            X = array[0];
            Y = array[1];
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Vector2(double x, double y)
        {
            X = (float) x;
            Y = (float) y;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public Vector2(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 8)
            {
                X = 0.0f;
                Y = 0.0f;
            }

            X = BitConverter.ToSingle(bytes, 0);
            Y = BitConverter.ToSingle(bytes, 4);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> struct.
        /// </summary>
        /// <param name="dword">The dword.</param>
        public Vector2(int dword)
        {
            X = dword & 65535; // LO-WORD
            Y = (dword >> 16) & 65535; // HI-WORD
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
            if (obj is Vector2)
                return (Vector2) obj == this;

            return base.Equals(obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "{X = " + X + ", Y = " + Y + "}";
        }

        /// <summary>
        ///     Adds the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Add(Vector2 vec)
        {
            X += vec.X;
            Y += vec.Y;
        }

        /// <summary>
        ///     Adds the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Add(float vec)
        {
            X += vec;
            Y += vec;
        }

        /// <summary>
        ///     Adds the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Add(int vec)
        {
            X += vec;
            Y += vec;
        }

        /// <summary>
        ///     Subtracts the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Subtract(Vector2 vec)
        {
            X -= vec.X;
            Y -= vec.Y;
        }

        /// <summary>
        ///     Subtracts the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Subtract(float vec)
        {
            X -= vec;
            Y -= vec;
        }

        /// <summary>
        ///     Subtracts the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Subtract(int vec)
        {
            X -= vec;
            Y -= vec;
        }

        /// <summary>
        ///     Multiplies the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Multiply(Vector2 vec)
        {
            X *= vec.X;
            Y *= vec.Y;
        }

        /// <summary>
        ///     Multiplies the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Multiply(float vec)
        {
            X *= vec;
            Y *= vec;
        }

        /// <summary>
        ///     Multiplies the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Multiply(int vec)
        {
            X *= vec;
            Y *= vec;
        }

        /// <summary>
        ///     Divides the specified vec.
        /// </summary>
        /// <param name="vec">The vec.</param>
        public void Divide(Vector2 vec)
        {
            if (vec.X == 0.0f) vec.X = float.Epsilon;
            if (vec.Y == 0.0f) vec.Y = float.Epsilon;

            X /= vec.X;
            Y /= vec.Y;
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
        }

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Vector2 Clone()
        {
            return new Vector2(X, Y);
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            X = 0.0f;
            Y = 0.0f;
        }

        /// <summary>
        ///     Distances to.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <returns></returns>
        public float DistanceTo(Vector2 vec)
        {
            return (this - vec).Length();
        }

        /// <summary>
        ///     Dots the product.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <returns></returns>
        public float DotProduct(Vector2 vec)
        {
            return X * vec.X + Y * vec.Y;
        }

        /// <summary>
        ///     Crosses the product.
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <returns></returns>
        public Vector2 CrossProduct(Vector2 vec)
        {
            return new Vector2(X * vec.Y - Y * vec.X, Y * vec.Y - X * vec.X);
        }

        /// <summary>
        ///     Gets the bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[8];
            Buffer.BlockCopy(BitConverter.GetBytes(X), 0, bytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Y), 0, bytes, 4, 4);
            return bytes;
        }

        /// <summary>
        ///     Returns the vecors length
        /// </summary>
        /// <returns></returns>
        public float Length()
        {
            return (float) Math.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        ///     Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return X == 0.0f && Y == 0.0f;
        }

        /// <summary>
        ///     Reals the is empty.
        /// </summary>
        /// <returns></returns>
        public bool RealIsEmpty()
        {
            return X < float.Epsilon && X > -float.Epsilon && Y < float.Epsilon && Y > -float.Epsilon;
        }

        /// <summary>
        ///     Determines whether [is na n].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is na n]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNaN()
        {
            return float.IsNaN(X) || float.IsNaN(Y);
        }

        /// <summary>
        ///     Determines whether this instance is infinity.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is infinity; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInfinity()
        {
            return float.IsInfinity(X) || float.IsInfinity(Y);
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
        ///     Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator +(Vector2 left, float right)
        {
            return new Vector2(left.X + right, left.Y + right);
        }

        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator +(Vector2 left, int right)
        {
            return new Vector2(left.X + right, left.Y + right);
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator -(Vector2 left, float right)
        {
            return new Vector2(left.X - right, left.Y - right);
        }

        /// <summary>
        ///     Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator -(Vector2 left, int right)
        {
            return new Vector2(left.X - right, left.Y - right);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator *(Vector2 left, float right)
        {
            return new Vector2(left.X * right, left.Y * right);
        }

        /// <summary>
        ///     Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator *(Vector2 left, int right)
        {
            return new Vector2(left.X * right, left.Y * right);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X / right.X, left.Y / right.Y);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator /(Vector2 left, float right)
        {
            return new Vector2(left.X / right, left.Y / right);
        }

        /// <summary>
        ///     Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static Vector2 operator /(Vector2 left, int right)
        {
            return new Vector2(left.X / right, left.Y / right);
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(Vector2 left, float right)
        {
            return left.X == right && left.Y == right;
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(Vector2 left, int right)
        {
            return left.X == right && left.Y == right;
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(Vector2 left, float right)
        {
            return left.X != right || left.Y != right;
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(Vector2 left, int right)
        {
            return left.X != right || left.Y != right;
        }

        /// <summary>
        ///     Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <(Vector2 left, Vector2 right)
        {
            return left.X < right.X && left.Y < right.Y;
        }

        /// <summary>
        ///     Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <(Vector2 left, float right)
        {
            return left.X < right && left.Y < right;
        }

        /// <summary>
        ///     Implements the operator &lt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <(Vector2 left, int right)
        {
            return left.X < right && left.Y < right;
        }

        /// <summary>
        ///     Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >(Vector2 left, Vector2 right)
        {
            return left.X > right.X && left.Y > right.Y;
        }

        /// <summary>
        ///     Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >(Vector2 left, float right)
        {
            return left.X > right && left.Y > right;
        }

        /// <summary>
        ///     Implements the operator &gt;.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >(Vector2 left, int right)
        {
            return left.X > right && left.Y > right;
        }

        /// <summary>
        ///     Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <=(Vector2 left, Vector2 right)
        {
            return left < right || left == right;
        }

        /// <summary>
        ///     Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <=(Vector2 left, float right)
        {
            return left < right || left == right;
        }

        /// <summary>
        ///     Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator <=(Vector2 left, int right)
        {
            return left < right || left == right;
        }

        /// <summary>
        ///     Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >=(Vector2 left, Vector2 right)
        {
            return left > right || left == right;
        }

        /// <summary>
        ///     Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >=(Vector2 left, float right)
        {
            return left > right || left == right;
        }

        /// <summary>
        ///     Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator >=(Vector2 left, int right)
        {
            return left > right || left == right;
        }

        /// <summary>
        ///     Distances the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector2 left, Vector2 right)
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
        public static float DotProduct(Vector2 left, Vector2 right)
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
        public static Vector2 CrossProduct(Vector2 left, Vector2 right)
        {
            return left.CrossProduct(right);
        }
    }
}