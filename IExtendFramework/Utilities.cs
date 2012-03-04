/*
 * Created by SharpDevelop.
 * User: elijah
 * Date: 12/5/2011
 * Time: 1:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using IExtendFramework.Collections.Generic;
using IExtendFramework.Collections.Specialized;

namespace IExtendFramework
{
    /// <summary>
    /// A delegate that can be used when invoking a Wpf Control using Dispatcher
    /// </summary>
    public delegate void WpfInvokeControlDelegate();
    
    /// <summary>
    /// Useful functions
    /// </summary>
    public static class Utilities
    {
        public static string CharListToString(List<char> c)
        {
            if (c.Count == 0)
                return "";
            
            StringBuilder sb = new StringBuilder();
            foreach (char c2 in c)
                sb.Append(c2);
            return sb.ToString();
        }
        
        public static string FormatByteToSize(long bytes)
        {
            const long scale = 1024;
            
            string[] orders = new string[] { "EB", "PB", "TB", "GB", "MB", "KB", "Bytes" };
            
            var max = (long) Math.Pow(scale, (orders.Length - 1));
            
            // Go from Large to small
            foreach (string order in orders)
            {
                if (bytes > max)
                {
                    return string.Format("{0:##.##} {1}", Decimal.Divide(bytes, max), order);
                }
                
                max /= scale;
            }
            return bytes.ToString() + " Unknown size";
        }

        public static string FormatSpeed(long bytes)
        {
            return FormatByteToSize(bytes) + "/s";
        }

        public static string MD5Hash(string fileName)
        {
            string result = string.Empty;
            using (System.IO.FileStream fs = System.IO.File.OpenRead(fileName))
            {
                System.Security.Cryptography.MD5 sscMD5 = System.Security.Cryptography.MD5.Create();
                
                byte[] mHash = sscMD5.ComputeHash(fs);
                
                result = Convert.ToBase64String(mHash);
            }
            return result;
        }

        public static void SafeFileDelete(string path)
        {
            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }
        }

        public static void SafeDirectoryDelete(string path, bool recursive)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive);
            }
        }
        
        public static string ByteToString(byte[] i)
        {
            return new ASCIIEncoding().GetString(i);
        }
        
        public static byte[] StringToByte(string i)
        {
            return new ASCIIEncoding().GetBytes(i.ToCharArray());
        }
        
        public static string PrettyByteToString(byte[] i)
        {
            string o = "{ ";
            // stick all these bytes in here
            foreach (byte b in i)
                o += b.ToString() + ", ";
            // remove last ', '
            o = o.Substring(0, o.LastIndexOf(","));
            o += " }";
            return o;
        }
        
        public static Icon BitmapToIcon(Bitmap i)
        {
            return Icon.FromHandle(i.GetHicon());
        }
        

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static int URShift(int number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2 << ~bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static int URShift(int number, long bits)
        {
            return URShift(number, (int)bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static long URShift(long number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2L << ~bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        public static long URShift(long number, long bits)
        {
            return URShift(number, (int)bits);
        }

        /// <summary>
        /// Fills the array with an specific value from an specific index to an specific index.
        /// </summary>
        /// <param name="array">The array to be filled.</param>
        /// <param name="fromindex">The first index to be filled.</param>
        /// <param name="toindex">The last index to be filled.</param>
        /// <param name="val">The value to fill the array with.</param>
        public static void Fill<T>(T[] array, int fromindex, int toindex, T val) where T : struct
        {
            if (array.Length == 0)
            {
                throw new NullReferenceException();
            }
            if (fromindex > toindex)
            {
                throw new ArgumentException();
            }
            if ((fromindex < 0) || ((System.Array)array).Length < toindex)
            {
                throw new IndexOutOfRangeException();
            }
            for (int index = (fromindex > 0) ? fromindex-- : fromindex; index < toindex; index++)
            {
                array[index] = val;
            }
        }

        /// <summary>
        /// Fills the array with an specific value.
        /// </summary>
        /// <param name="array">The array to be filled.</param>
        /// <param name="val">The value to fill the array with.</param>
        public static void Fill<T>(T[] array, T val) where T : struct
        {
            Fill(array, 0, array.Length, val);
        }

        /// <summary> Read a int value from the byte array at the given position (Big Endian)
        /// 
        /// </summary>
        /// <param name="array">the array to read from
        /// </param>
        /// <param name="pos">the offset
        /// </param>
        /// <returns> the value
        /// </returns>
        public static int readIntBigEndian(byte[] array, int pos)
        {
            int temp = 0;
            temp |= array[pos] & 0xff;
            temp <<= 8;
            temp |= array[pos + 1] & 0xff;
            temp <<= 8;
            temp |= array[pos + 2] & 0xff;
            temp <<= 8;
            temp |= array[pos + 3] & 0xff;
            return temp;
        }

        /// <summary> Read a short value from the byte array at the given position (little
        /// Endian)
        /// 
        /// </summary>
        /// <param name="array">the array to read from
        /// </param>
        /// <param name="pos">the offset
        /// </param>
        /// <returns> the value
        /// </returns>
        public static short readShortLittleEndian(byte[] array, int pos)
        {
            return BitConverter.ToInt16(array, pos);
        }

        /// <summary> Read an int value from the byte array at the given position (little
        /// Endian)
        /// 
        /// </summary>
        /// <param name="array">the array to read from
        /// </param>
        /// <param name="pos">the offset
        /// </param>
        /// <returns> the value
        /// </returns>
        public static int readIntLittleEndian(byte[] array, int pos)
        {
            return BitConverter.ToInt32(array, pos);
        }

        /// <summary> Write an int value into the byte array at the given position (Big endian)
        /// 
        /// </summary>
        /// <param name="array">the array
        /// </param>
        /// <param name="pos">the offset
        /// </param>
        /// <param name="value">the value to write
        /// </param>
        public static void writeIntBigEndian(byte[] array, int pos, int value)
        {
            array[pos] = (byte)((Utilities.URShift(value, 24)) & 0xff);
            array[pos + 1] = (byte)((Utilities.URShift(value, 16)) & 0xff);
            array[pos + 2] = (byte)((Utilities.URShift(value, 8)) & 0xff);
            array[pos + 3] = (byte)((value) & 0xff);
        }

        /// <summary> Write a short value into the byte array at the given position (little
        /// endian)
        /// 
        /// </summary>
        /// <param name="array">the array
        /// </param>
        /// <param name="pos">the offset
        /// </param>
        /// <param name="value">the value to write
        /// </param>
        #if SILVERLIGHT || MONO || PORTABLE
        public static void WriteLittleEndian(byte[] array, int pos, short value)
        {
            byte[] newBytes = BitConverter.GetBytes(value);
            Array.Copy(newBytes, 0, array, pos, newBytes.Length);
        }
        #else
        unsafe public static void WriteLittleEndian(byte[] array, int pos, short value)
        {
            fixed (byte* numRef = &(array[pos]))
            {
                *((short*)numRef) = value;
            }
        }
        #endif

        /// <summary> Increment a short value at the specified position by the specified amount
        /// (little endian).
        /// </summary>
        public static void incShortLittleEndian(byte[] array, int pos, short incrementValue)
        {
            short existingValue = BitConverter.ToInt16(array, pos);
            existingValue += incrementValue;
            WriteLittleEndian(array, pos, existingValue);
            //int c = Utilities.URShift(((array[pos] & 0xff) + (dv & 0xff)), 8);
            //array[pos] = (byte)(array[pos] + (dv & 0xff));
            //if ((c > 0) || ((dv & 0xff00) != 0))
            //{
            //    array[pos + 1] = (byte)(array[pos + 1] + ((Utilities.URShift(dv, 8)) & 0xff) + c);
            //}
        }

        /// <summary> Write an int value into the byte array at the given position (little
        /// endian)
        /// 
        /// </summary>
        /// <param name="array">the array
        /// </param>
        /// <param name="pos">the offset
        /// </param>
        /// <param name="value">the value to write
        /// </param>
        #if SILVERLIGHT || MONO || PORTABLE
        public static void WriteLittleEndian(byte[] array, int pos, int value)
        {
            byte[] newBytes = BitConverter.GetBytes(value);
            Array.Copy(newBytes, 0, array, pos, newBytes.Length);
        }
        #else
        unsafe public static void WriteLittleEndian(byte[] array, int pos, int value)
        {
            fixed (byte* numRef = &(array[pos]))
            {
                *((int*)numRef) = value;
            }
        }
        #endif
        
        public static byte[] UInt32ToBigEndianBytes(uint x)
        {
            return new byte[] {
                (byte)((x >> 24) & 0xff),
                (byte)((x >> 16) & 0xff),
                (byte)((x >> 8) & 0xff),
                (byte)(x & 0xff) };
        }

        public static DateTime DosDateToDateTime(UInt16 iDate, UInt16 iTime)
        {
            int year = iDate / 512 + 1980;
            int month = iDate % 512 / 32;
            int day = iDate % 512 % 32;
            int hour = iTime / 2048;
            int minute = iTime % 2048 / 32;
            int second = iTime % 2048 % 32 * 2;

            if (iDate == UInt16.MaxValue || month == 0 || day == 0)
            {
                year = 1980;
                month = 1;
                day = 1;
            }

            if (iTime == UInt16.MaxValue)
            {
                hour = minute = second = 0;
            }

            DateTime dt;
            try
            {
                dt = new DateTime(year, month, day, hour, minute, second);
            }
            catch
            {
                dt = new DateTime();
            }
            return dt;
        }


        public static DateTime DosDateToDateTime(UInt32 iTime)
        {
            return DosDateToDateTime((UInt16)(iTime / 65536),
                                     (UInt16)(iTime % 65536));
        }

        public static DateTime DosDateToDateTime(Int32 iTime)
        {
            return DosDateToDateTime((UInt32)iTime);
        }
    }
}
