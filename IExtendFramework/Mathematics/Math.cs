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
    /// Useful math functions not include in the standard library
    /// </summary>
    public class Math
    {
        /// <summary>
        /// Finds out if a number is a prime number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsPrime(int num)
        {
            bool ret = true;
            for (int i = num - 1; i > 1; i--)
            {
                if (num % i == 0)
                {
                    ret = false;
                    break; // this way not much extra memory is taken
                }
            }
            return ret;
        }
        
        /// <summary>
        /// Square's a number
        /// </summary>
        /// <returns></returns>
        public static int Sqr(int num)
        {
            return num * num;
        }
        
        public static int SquareToN(int num, int n)
        {
            if (n == 0)
                return 0;
            
            int ret = num;
            for (int i = 0; i < n; i++)
                ret *= num;
            return ret;
        }
        
        #region NOT MINE
        /// <summary>
        /// Sine that returns the result in InDegrees.
        /// </summary>
        /// <param name="x">Value input</param>
        public static double SinInDegrees(double x)
        {
            return System.Math.Sin(x * System.Math.PI / 180);
        }

        /// <summary>
        /// Inverse Sine that returns the result in InDegrees.
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ASinInDegrees(double x)
        {
            return System.Math.Asin(x) * 180 / System.Math.PI;
        }

        /// <summary>
        /// Cosine that returns the result in InDegrees.
        /// </summary>
        /// <param name="x">Value input</param>
        public static double CosInDegrees(double x)
        {
            return System.Math.Cos(x * System.Math.PI / 180);
        }

        /// <summary>
        /// Inverse Cosine that returns the result in InDegrees.
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ACosInDegrees(double x)
        {
            return System.Math.Acos(x) * 180/System.Math.PI;
        }

        /// <summary>
        /// Tangent that returns the result in InDegrees.
        /// </summary>
        /// <param name="x">Value input</param>
        public static double TanInDegrees(double x)
        {
            return System.Math.Tan(x * System.Math.PI / 180);
        }

        /// <summary>
        /// Inverse Tangent that returns the result in InDegrees.
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ATanInDegrees(double x)
        {
            return System.Math.Atan(x) * 180/System.Math.PI;
        }

        #region Sec, SecDec, ASec, ASecInDegrees, Sech, ASech
        /// <summary>
        /// Secant that returns values in Radians
        /// </summary>
        /// <param name="x">Value input</param>
        public static double Sec(double x)
        {
            return 1 / System.Math.Cos(x);
        }

        /// <summary>
        /// Secant that returns values in InDegrees
        /// </summary>
        /// <param name="x">Value input</param>
        public static double SecInDegrees(double x)
        {
            //Perform multiplication before the calculation.
            //This is because a secant does not produce an angle, but a ratio.
            double secantInDegrees = (x * 180 / System.Math.PI);
            return 1 / System.Math.Cos(secantInDegrees);
        }

        /// <summary>
        /// Inverse Secant that returns values in Radians
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ASec(double x)
        {
            return 2 * System.Math.Atan(1) - System.Math.Atan(System.Math.Sign(x) / System.Math.Sqrt(x * x - 1));
        }

        /// <summary>
        /// Inverse Secant that returns values in InDegrees
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ASecInDegrees(double x)
        {
            return 2 * System.Math.Atan(1) - System.Math.Atan(System.Math.Sign(x) / System.Math.Sqrt(x * x - 1)) * 180 / System.Math.PI;
        }

        /// <summary>
        /// Hyperbolic Secant
        /// </summary>
        /// <param name="x">Value input</param>
        public static double Sech(double x)
        {
            return 2 / (System.Math.Exp(x) + System.Math.Exp(-x));
        }

        /// <summary>
        /// Inverse Hyperbolic Secant
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ASech(double x)
        {
            return System.Math.Log((System.Math.Sqrt(-x * x + 1) + 1) / x);
        }
        #endregion

        #region Csc, CscInDegrees, ACsc, ACscInDegrees, Csch, ACsch
        /// <summary>
        /// Cosecant that returns values in Radians
        /// </summary>
        /// <param name="x">Value input</param>
        public static double Csc(double x)
        {
            return 1 / System.Math.Sin(x);
        }

        /// <summary>
        /// Cosecant that returns values in InDegrees
        /// </summary>
        /// <param name="x">Value input</param>
        public static double CscInDegrees(double x)
        {
            return 1 / SinInDegrees(x);
        }

        /// <summary>
        /// Inverse Cosecant that returns values in radians
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ACsc(double x)
        {
            return System.Math.Atan(System.Math.Sign(x) / System.Math.Sqrt(x * x - 1));
        }

        /// <summary>
        /// Inverse Cosecant that returns values in InDegrees
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ACscInDegrees(double x)
        {
            return ATanInDegrees(System.Math.Sign(x) / System.Math.Sqrt(x * x - 1));
        }

        /// <summary>
        /// Hyperbolic Cosecant
        /// </summary>
        /// <param name="x">Value input</param>
        public static double Csch(double x)
        {
            return 2 / (System.Math.Exp(x) - System.Math.Exp(-x));
        }

        /// <summary>
        /// Inverse Hyperbolic Cosecant
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ACsch(double x)
        {
            return System.Math.Log((System.Math.Sign(x) * System.Math.Sqrt(x * x + 1) + 1) / x);
        }

        #endregion

        #region Cot, CotInDegrees, ACot, ACotInDegrees, Coth, ACoth
        /// <summary>
        /// Cotangent that returns values in Radians
        /// </summary>
        /// <param name="x">Value input</param>
        public static double Cot(double x)
        {
            return 1/ System.Math.Tan(x);
        }

        /// <summary>
        /// Cotangent that returns values in InDegrees
        /// </summary>
        /// <param name="x">Value input</param>
        public static double CotInDegrees(double x)
        {
            return 1 / TanInDegrees(x);
        }

        /// <summary>
        /// Inverse Cotangent that returns values in Radians
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ACot(double x)
        {
            return 2 * System.Math.Atan(1) - System.Math.Atan(x);
        }

        /// <summary>
        /// Inverse Cotangent that returns values in InDegrees
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ACotInDegrees(double x)
        {
            return 2 * ATanInDegrees(1) - ATanInDegrees(x);
        }

        /// <summary>
        /// Hyperbolic Cotangent
        /// </summary>
        /// <param name="x">Value input</param>
        public static double Coth(double x)
        {
            return (System.Math.Exp(x) + System.Math.Exp(-x)) / (System.Math.Exp(x) - System.Math.Exp(-x));
        }

        /// <summary>
        /// Inverse Hyperbolic Cotangent
        /// </summary>
        /// <param name="x">Value input</param>
        public static double ACoth(double x)
        {
            return System.Math.Log((x + 1) / (x - 1)) / 2;
        }
        #endregion
        #endregion
    }
}
