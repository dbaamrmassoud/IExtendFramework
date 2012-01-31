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
	public class RandomStringGenerator
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
}
