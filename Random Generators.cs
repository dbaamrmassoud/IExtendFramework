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
    /// A Random String Generator. Has a certain chance of being a CAPITAL letter.
    /// </summary>
    /// <remarks></remarks>
    class RandomStringGenerator
    {
        RandomNumberGenerator RNG = new RandomNumberGenerator(DateTime.Now.Millisecond);
        string[] Letters = {
            "q",
            "w",
            "e",
            "r",
            "t",
            "y",
            "u",
            "i",
            "o",
            "p",
            "a",
            "s",
            "d",
            "f",
            "g",
            "h",
            "j",
            "k",
            "l",
            "z",
            "x",
            "c",
            "v",
            "b",
            "n",
            "m"

        };
        /// <summary>
        /// The actual function that generates the Random String
        /// </summary>
        /// <param name="StrLength">The length of the Random String</param>
        /// <returns>This is the random String</returns>
        /// <remarks></remarks>
        public string GenerateString(int StrLength)
        {
            string Str = "";
            if (StrLength < 1) {
                throw new Exception("Invalid Minimum String Length. Minimum: 1");
            }
            for (int i = 1; i <= StrLength; i++) {
                char Cha = '\0';
                Cha = Letters[RNG.Generate(0, Letters.Count())].ToCharArray()[0];
                int A = RNG.Generate(0, 2);
                if (A == 1)
                    Cha = char.ToUpper(Cha);
                Str += Cha;
            }
            return Str;
        }
    }

    /// <summary>
    /// A Random Number Generator
    /// </summary>
    /// <remarks></remarks>
    class RandomNumberGenerator : System.Random
    {
        System.Random RNG = new System.Random(DateTime.Now.Millisecond);
        private System.ComponentModel.BackgroundWorker _internalWorker = new System.ComponentModel.BackgroundWorker();
        private System.ComponentModel.BackgroundWorker Worker {
            get { return _internalWorker; }
            set {
                if (_internalWorker != null) {
                    _internalWorker.DoWork -= Worker_DoWork;
                    _internalWorker.ProgressChanged -= Worker_ReportProgress;
                }
                _internalWorker = value;
                if (_internalWorker != null) {
                    _internalWorker.DoWork += Worker_DoWork;
                    _internalWorker.ProgressChanged += Worker_ReportProgress;
                }
            }
        }
        /// <summary>
        /// This Generates the Number
        /// </summary>
        /// <param name="Minimum">The Minimum number</param>
        /// <param name="Maximum">The Maximum number</param>
        /// <returns>The randomized number</returns>
        /// <remarks></remarks>
        public int Generate(int Minimum, int Maximum)
        {
            return RNG.Next(Minimum, Maximum);
        }

        public RandomNumberGenerator()
        {
            Worker.RunWorkerAsync();
        }

        public RandomNumberGenerator(int _Seed)
        {
            Worker.RunWorkerAsync();
            RNG = new System.Random(_Seed);
        }

        private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Worker.WorkerReportsProgress = true;
            while (true) {
                Worker.ReportProgress(0);
                System.Threading.Thread.Sleep(100);
            }
        }

        private void Worker_ReportProgress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            RNG = new System.Random(RNG.Next());
        }
        
        public override int Next()
        {
            return RNG.Next();
        }
        
        public override int Next(int minValue, int maxValue)
        {
            return Generate(minValue, maxValue);
        }
        
        public override int Next(int maxValue)
        {
            return RNG.Next(maxValue);
        }
        
        public override string ToString()
        {
            return "[IExtendFramework.RandomNumberGenerator: Inherits System.Random]";
        }
        
        public override double NextDouble()
        {
            return RNG.NextDouble();
        }

    }

    /// <summary>
    /// A Random Date Generator
    /// </summary>
    /// <remarks></remarks>
    class RandomDateGenerator
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
