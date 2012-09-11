using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IExtendFramework.Mathematics;

namespace IExtendFramework
{
    /* Number types:
     - int
     - uint
     - double, 
     - decimal
     - short
     - long, 
     - float/single
     - ushort
     - ulong
     ? byte
     ? sbyte
     ? char
     */

    /// <summary>
    /// A global number type that can be used (implicitly) among all the base number types, including byte and char
    /// </summary>
    public class Number : IConvertible
    {
        /// <summary>
        /// Maximum possible value
        /// </summary>
        public static Number MaxValue
        {
            get
            {
                return BigDecimal.PositiveInfinity;
            }
        }
        /// <summary>
        /// Minimum possible value
        /// </summary>
        public static Number MinValue
        {
            get
            {
                return BigDecimal.NegativeInfinity;
            }
        }
        /// <summary>
        /// A value extremely close to negative zero
        /// </summary>
        public static Number NearNegativeZero
        {
            get
            {
                return -0.000000000000000000000000001m;
            }
        }
        /// <summary>
        /// A value extremely close to positive zero
        /// </summary>
        public static Number NearPositiveZero
        {
            get
            {
                return 0.000000000000000000000000001m;
            }
        }
        /// <summary>
        /// A representation of Not-A-Number
        /// </summary>
        public static Number NaN
        {
            get
            {
                return BigDecimal.NaN;
            }
        }

        public RoundType RoundingMethod { get; set; }

        private BigDecimal value;
        public Number()
        {
            RoundingMethod = RoundType.Normal;
            value = -0;
        }

        public Number(object initialValue)
        {
            value = BigDecimal.Create(new BigDecimal.Config(), initialValue.ToString());
        }

        #region Operators/Overloads
        public static implicit operator Number(Int32 i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(UInt32 i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(Double i)
        {
            return new Number() { value = (decimal)i };
        }
        public static implicit operator Number(Decimal i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(Int16 i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(Int64 i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(float i)
        {
            return new Number() { value = (decimal)i };
        }
        public static implicit operator Number(UInt16 i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(UInt64 i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(Byte i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(Char i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(sbyte i)
        {
            return new Number() { value = i };
        }
        public static implicit operator Number(BigDecimal d)
        {
            return new Number() { value = d };
        }

        public static implicit operator Int32(Number n)
        {
            return n.ToInt();
        }
        public static implicit operator UInt32(Number n)
        {
            return n.ToUInt();
        }
        public static implicit operator Double(Number n)
        {
            return n.ToDouble();
        }
        public static implicit operator Decimal(Number n)
        {
            return n.ToDecimal();
        }
        public static implicit operator Int16(Number n)
        {
            return n.ToShort();
        }
        public static implicit operator Int64(Number n)
        {
            return n.ToLong();
        }
        public static implicit operator Single(Number n)
        {
            return n.ToFloat();
        }
        public static implicit operator UInt16(Number n)
        {
            return n.ToUShort();
        }
        public static implicit operator UInt64(Number n)
        {
            return n.ToULong();
        }
        public static implicit operator Byte(Number n)
        {
            return n.ToByte();
        }
        public static implicit operator Char(Number n)
        {
            return n.ToChar();
        }
        public static implicit operator SByte(Number n)
        {
            return n.ToSByte();
        }
        public static implicit operator BigDecimal(Number n)
        {
            return n.value;
        }

        public static Number operator +(Number a, Number b)
        {
            return BigDecimal.Add(new BigDecimal.Config(), a.value, b.value);
        }
        public static Number operator -(Number a, Number b)
        {
            return BigDecimal.Subtract(new BigDecimal.Config(), a.value, b.value);
        }
        public static Number operator -(Number a)
        {
            return BigDecimal.Negate(new BigDecimal.Config(), a.value);
        }
        public static Number operator /(Number a, Number b)
        {
            BigDecimal rem;
            return BigDecimal.Divide(new BigDecimal.Config(), a.value, b.value, 0, out rem);
        }
        public static Number operator *(Number a, Number b)
        {
            return BigDecimal.Multiply(new BigDecimal.Config(), a.value, b.value);
        }
        public static Number operator %(Number a, Number b)
        {
            BigDecimal rem;
            BigDecimal.Divide(new BigDecimal.Config(), a.value, b.value, 0, out rem);
            return rem;
        }

        public static bool operator ==(Number a, Number b)
        {
            return a.value == b.value;
        }
        public static bool operator !=(Number a, Number b)
        {
            return a.value != b.value;
        }
        public static bool operator <(Number a, Number b)
        {
            return a.value.ToAdvancedString() < b.value.ToAdvancedString();
        }
        public static bool operator >(Number a, Number b)
        {
            return a.value.ToAdvancedString() > b.value.ToAdvancedString();
        }
        public static bool operator <=(Number a, Number b)
        {
            return a.value.ToAdvancedString() <= b.value.ToAdvancedString();
        }
        public static bool operator >=(Number a, Number b)
        {
            return a.value.ToAdvancedString() >= b.value.ToAdvancedString();
        }

        public override bool Equals(object obj)
        {
            return value.Equals(obj);
        }
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        public override string ToString()
        {
            return value.ToString();
        }
        #endregion

        #region Conversions
        /// <summary>
        /// Converts the number to a char
        /// </summary>
        /// <returns></returns>
        public char ToChar()
        {
            return char.Parse(value.ToString());
        }
        /// <summary>
        /// Converts the number to a byte
        /// </summary>
        /// <returns></returns>
        public byte ToByte()
        {
            return byte.Parse(value.ToString());
        }
        /// <summary>
        /// Converts the number to a decimal
        /// </summary>
        /// <returns></returns>
        public decimal ToDecimal()
        {
            return decimal.Parse(value.ToString());
        }
        /// <summary>
        /// Converts the number to a double
        /// </summary>
        /// <returns></returns>
        public double ToDouble()
        {
            return double.Parse(value.ToString());
        }
        /// <summary>
        /// Converts the number to a float/single
        /// </summary>
        /// <returns></returns>
        public float ToFloat()
        {
            return float.Parse(value.ToString());
        }
        /// <summary>
        /// Converts the number to an integer
        /// </summary>
        /// <returns></returns>
        public int ToInt()
        {
            return int.Parse(Round());
        }
        /// <summary>
        /// Converts the number to a UInt
        /// </summary>
        /// <returns></returns>
        public uint ToUInt()
        {
            return uint.Parse(Round());
        }
        /// <summary>
        /// Converts the number to a short
        /// </summary>
        /// <returns></returns>
        public short ToShort()
        {
            return short.Parse(Round());
        }
        /// <summary>
        /// Converts the number to a long
        /// </summary>
        /// <returns></returns>
        public long ToLong()
        {
            return long.Parse(Round());
        }
        /// <summary>
        /// Converts the number to a UInt32
        /// </summary>
        /// <returns></returns>
        public ushort ToUShort()
        {
            return ushort.Parse(Round());
        }
        /// <summary>
        /// Converts the number to a UInt64
        /// </summary>
        /// <returns></returns>
        public ulong ToULong()
        {
            return ulong.Parse(Round());
        }
        /// <summary>
        /// Converts the number to an SByte
        /// </summary>
        /// <returns></returns>
        public sbyte ToSByte()
        {
            return sbyte.Parse(value.ToString());
        }
        /// <summary>
        /// Returns the type code of the object
        /// </summary>
        /// <returns></returns>
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Decimal;
        }
        /// <summary>
        /// Converts the number to a boolean
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return value != (BigDecimal)0;
        }
        /// <summary>
        /// Converts the number to a byte
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return ToByte();
        }
        /// <summary>
        /// Converts the number to a char
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return ToChar();
        }
        /// <summary>
        /// throws an InvalidCastException
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }
        /// <summary>
        /// Converts the number to a decimal
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return ToDecimal();
        }
        /// <summary>
        /// Converts the number to a double
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return ToDouble();
        }
        /// <summary>
        /// Converts the number to an Int16
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return ToShort();
        }
        /// <summary>
        /// Converts the number to an integer
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return ToInt();
        }
        /// <summary>
        /// Converts the number to an Int64
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return ToLong();
        }
        /// <summary>
        /// Converts the number to an SByte
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return ToSByte();
        }
        /// <summary>
        /// Converts the number to a single
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return ToFloat();
        }
        /// <summary>
        /// Converts the number to a string
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }
        /// <summary>
        /// throws an InvalidCastException
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            throw new InvalidCastException();
        }
        /// <summary>
        /// Converts the number to a UInt16
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return ToUShort();
        }
        /// <summary>
        /// Converts the number to a UInt32
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return ToUInt();
        }
        /// <summary>
        /// Converts the number to a UInt64
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return ToULong();
        }
        #endregion

        /// <summary>
        /// VERY inefficient but works. TODO: remake more efficiently
        /// </summary>
        /// <returns></returns>
        string Round()
        {
            string num = value.ToString();
            if (num.Contains("."))
            {
                switch (this.RoundingMethod)
                {

                    case RoundType.RoundUp:
                        int num3 = int.Parse(num.Substring(0, num.IndexOf(".")));
                        return (num3 + 1).ToString();
                    case RoundType.RoundDown:
                    case RoundType.Truncate:
                        return num.Substring(0, num.IndexOf("."));
                    case RoundType.Normal:
                        int num4 = int.Parse(num.Substring(num.IndexOf(".") + 1, 1));
                        if (num4 <= 4)
                            return num.Substring(0, num.IndexOf("."));
                        else
                            return (int.Parse(num.Substring(0, num.IndexOf("."))) + 1).ToString();
                    default:
                        throw new Exception("Invalid RoundingMethod!");
                }
            }
            else
                return num;
        }

        public bool IsNaN()
        {
            return BigDecimal.IsNaN(value);
        }

        /// <summary>
        /// Rounding types
        /// </summary>
        public enum RoundType
        {
            /// <summary>
            /// Always rounds down. The same as truncate
            /// </summary>
            RoundDown,
            /// <summary>
            /// Always rounds up.
            /// </summary>
            RoundUp,
            /// <summary>
            /// The normal math rounding. If &lt;=4, round down, otherwise round up
            /// </summary>
            Normal,
            /// <summary>
            /// Removes the whole decimal section.
            /// </summary>
            Truncate
        }
    }
}
