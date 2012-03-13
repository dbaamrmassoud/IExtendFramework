/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 1:06 PM
 */
using System;
using IExtendFramework.Enums;

namespace IExtendFramework.School
{
    /// <summary>
    /// Description of Grade.
    /// </summary>
    public class Grade
    {
        public Grade()
        {
            GradeNumber = 0;
        }
        
        public Grade(int gradeNum)
        {
            GradeNumber = gradeNum;
        }
        
        public int GradeNumber
        {
            get; set;
        }
        
        //TODO: better name
        public Grades CurrentGrade
        {
            get
            {
                return (Grades)this.GradeNumber;
            }
            set
            {
                this.GradeNumber = (int)value;
            }
        }
        
        #region Operator Overloads
        public static implicit operator int(Grade g)
        {
            return g.GradeNumber;
        }
        
        public static explicit operator Grade(int i)
        {
            Grade g = new Grade();
            g.GradeNumber = i;
            return g;
        }
        
        public static bool operator ==(Grade a, Grade b)
        {
            if (a.GradeNumber == b.GradeNumber)
                return true;
            return false;
        }
        
        public static bool operator !=(Grade a, Grade b)
        {
            return !(a == b);
        }
        
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        #endregion
        
        /// <summary>
        /// Tries to infer the age from the current grade
        /// </summary>
        /// <param name="g"></param>
        /// <returns>the infered age, -1 if not successful</returns>
        public static int InferAgeFromGrade(Grade g)
        {
            int age = -1;
            
            // roughly 1 year per grade, so
            // Preschool = 3-4
            // Kindergarten = 5-6
            // 1st grade = 7
            // 2nd grade = 8
            // 3rd grade = 9
            // 4th grade = 10
            // 5th grade = 11
            // 6th grade = 12
            // 7th grade = 13
            // 8th grade = 14
            // 9th grade = 15
            // 10th grade = 16
            // 11th grade = 17
            // 12th grade = 18
            
            switch (g.CurrentGrade) {
                case IExtendFramework.School.Grades.Preschool:
                    age = 4;
                    break;
                case IExtendFramework.School.Grades.Kindergarten:
                    age = 6;
                    break;
                case IExtendFramework.School.Grades.First:
                    age = 7;
                    break;
                case IExtendFramework.School.Grades.Second:
                    age = 8;
                    break;
                case IExtendFramework.School.Grades.Third:
                    age = 9;
                    break;
                case IExtendFramework.School.Grades.Fourth:
                    age = 10;
                    break;
                case IExtendFramework.School.Grades.Fifth:
                    age = 11;
                    break;
                case IExtendFramework.School.Grades.Sixth:
                    age = 12;
                    break;
                case IExtendFramework.School.Grades.Seventh:
                    age = 13;
                    break;
                case IExtendFramework.School.Grades.Eighth:
                    age = 14;
                    break;
                case IExtendFramework.School.Grades.Ninth:
                    age = 15;
                    break;
                case IExtendFramework.School.Grades.Tenth:
                    age = 16;
                    break;
                case IExtendFramework.School.Grades.Eleventh:
                    age = 17;
                    break;
                case IExtendFramework.School.Grades.Twelfth:
                    age = 18;
                    break;
                default:
                    throw new Exception("Unknown Grade");
            }
            
            return age;
        }
        
        public static int InferYearFromGrade(Grade g)
        {
            int age = InferAgeFromGrade(g);
            DateTime now = DateTime.Now;
            return now.Year - age;
        }
    }
    
    public enum Grades
    {
        // No ReadableEnumAttribute for these - they dont need it.
        
        Preschool = -1,
        Kindergarten = 0,
        
        // ReadableEnums for these
        // these are grades from elementary school - high school
        
        [ReadableEnum("First grade")]
        First,
        [ReadableEnum("Second grade")]
        Second,
        [ReadableEnum("Third grade")]
        Third,
        [ReadableEnum("Fourth grade")]
        Fourth,
        [ReadableEnum("Fifth grade")]
        Fifth,
        [ReadableEnum("Sixth grade")]
        Sixth,
        [ReadableEnum("Seventh grade")]
        Seventh,
        [ReadableEnum("Eighth grade")]
        Eighth,
        [ReadableEnum("Ninth grade")]
        Ninth,
        [ReadableEnum("Tenth grade")]
        Tenth,
        [ReadableEnum("Eleventh grade")]
        Eleventh,
        [ReadableEnum("Twelfth grade")]
        Twelfth,
    }
}
