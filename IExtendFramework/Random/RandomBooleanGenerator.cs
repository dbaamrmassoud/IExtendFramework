/*
 * User: elijah
 * Date: 3/8/2012
 * Time: 6:40 PM
 */
using System;

namespace IExtendFramework.Random
{
    /// <summary>
    /// Description of RandomBooleanGenerator.
    /// </summary>
    public class RandomBooleanGenerator
    {
        RandomNumberGenerator RNG = new RandomNumberGenerator(DateTime.Now.Millisecond);
        
        public RandomBooleanGenerator()
        {
        }
        
        /// <summary>
        /// Simple but powerful. What else needs said?
        /// </summary>
        /// <returns></returns>
        public bool Generate()
        {
            return RNG.Next(0, 2) == 0 ? true : false;
        }
    }
}
