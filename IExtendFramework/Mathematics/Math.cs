/*
 * User: elijah
 * Date: 3/10/2012
 * Time: 11:38 AM
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace IExtendFramework.Mathematics
{
    /// <summary>
    /// Useful math functions not include in the standard math library
    /// </summary>
    public static partial class Math
    {
        /// <summary>
        /// Finds out if a number is a prime number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsPrime(Number num)
        {
            bool ret = true;
            for (int i = num - 1; i > 1; i--)
            {
                if (num % i == 0)
                {
                    ret = false;
                    break; // this way not too much extra memory is taken
                }
            }
            return ret;
        }

        /// <summary>
        /// Square's a number
        /// </summary>
        /// <returns></returns>
        public static Number Square(Number num)
        {
            return num * num;
        }

        /// <summary>
        /// Cubes a number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Number Cube(Number num)
        {
            return num * num * num;
        }

        /// <summary>
        /// Raises the number to the specified power
        /// </summary>
        /// <param name="num"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Number SquareToN(Number num, Number n)
        {
            if (n == 0)
                return 0;

            int ret = num;
            for (int i = 0; i < n; i++)
                ret *= num;
            return ret;
        }
    }
}
