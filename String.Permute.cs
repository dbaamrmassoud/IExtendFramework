using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace IExtendFramework.Text
{
	/// <summary>
	/// A String Permutation Utilities
	/// </summary>
	/// <remarks></remarks>
	public class Permutation
	{

		static List<string> Strings = new List<string>();
		/// <summary>
		/// A String Permutation Function
		/// </summary>
		/// <param name="stringToPermute">This is the String to Permute</param>
		/// <returns></returns>
		/// <remarks>This may take a while, depending the strings Length, so I
		/// recommend using a BackgroundWorker or a something of that sort
		/// </remarks>
		public static List<string> Permute(string stringToPermute)
		{
			Strings.Clear();
			internal_Permute("", stringToPermute);
			return Strings;
		}

		private static void internal_Permute(string beginningS, string endingS)
		{
			string newS = null;

			if (endingS.Length <= 1) {
				string wholeS = beginningS + endingS;
				Strings.Add(wholeS);
			} else {
				for (int i = 0; i <= endingS.Length - 1; i++) {
					newS = endingS.Substring(0, i);
					if (i + 1 <= endingS.Length) {
						newS += endingS.Substring(i + 1);
					}
					internal_Permute(beginningS + endingS[i], newS);
				}
			}

		}
	}

	/// <summary>
	/// A Class to find a Strings factorial
	/// </summary>
	/// <remarks></remarks>
	class StringFactorials
	{
		/// <summary>
		/// Get the factorial of a string (the Factorial)
		/// </summary>
		/// <param name="inputStringLength">the Length of a String</param>
		/// <returns>The factorial</returns>
		/// <remarks></remarks>
		public static double FindFactorialOfString(int inputStringLength)
		{
			double RV = 1;
			double i = 0;
			for (i = 1; i <= inputStringLength; i++) {
				RV *= i;
			}
			return RV;
		}
	}
}

