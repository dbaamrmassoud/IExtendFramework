using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
namespace IExtendFramework.Random
{
    
    /// <summary>
    /// A Random Date Generator
    /// </summary>
    /// <remarks></remarks>
    public class RandomDateGenerator
    {

        RandomNumberGenerator RNG = new RandomNumberGenerator(DateTime.Now.Millisecond);
        /// <summary>
        /// The Function that generates the Date
        /// </summary>
        /// <param name="mindate">The Minimum Date</param>
        /// <param name="maxDate">The Maximum Date</param>
        /// <returns>The Randomized Date</returns>
        /// <remarks></remarks>
        public System.DateTime Generate(System.DateTime mindate, System.DateTime maxDate)
        {
            System.DateTime RandomDate = new System.DateTime();
            int Month = 0;
            int Year = 0;
            int Day = 0;
            int Minute = 0;
            int Hour = 0;
            int Second = 0;
            int Millisecond = 0;
            try {
                Month = RNG.Next(mindate.Month, maxDate.Month);
            } catch (Exception) {
                Month = RNG.Next();
            }
            try {
                Year = RNG.Next(mindate.Year, maxDate.Year);
            } catch (Exception) {
                Year = RNG.Next(System.DateTime.Now.Year);
            }
            try {
                Day = RNG.Next(mindate.Day, maxDate.Day);
            } catch (Exception) {
                Day = RNG.Next(30);
            }
            try {
                Minute = RNG.Next(mindate.Minute, maxDate.Minute);
            } catch (Exception) {
                Minute = RNG.Next(59);
            }
            try {
                Hour = RNG.Next(mindate.Hour, maxDate.Hour);
            } catch (Exception) {
                Hour = RNG.Next(23);
            }
            try {
                Second = RNG.Next(mindate.Second, maxDate.Second);
            } catch (Exception) {
                Second = RNG.Next(59);
            }
            try {
                Millisecond = RNG.Next(mindate.Millisecond, maxDate.Millisecond);
            } catch (Exception) {
                Millisecond = RNG.Next(999);
            }
            RandomDate = new System.DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond);

            return RandomDate;
        }
    }

}
