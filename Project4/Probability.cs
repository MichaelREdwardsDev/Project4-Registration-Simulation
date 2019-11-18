////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		Probability.cs
//	Description:	Holds several static methods to determine values based off types of distributions
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Michael Edwards, edwardsmr@etsu.edu, Elizabeth Jennings, jenningsel@etsu.edu, William Jennings, jenningsw@etsu.edu
//	Created:		Sunday November 14, 2019
//	Copyright:		Michael Edwards, 2019
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils {
	/// <summary>
	/// Contains several static functions that return a value based on types of distributions
	/// </summary>
	public static class Probability {

		public static Random Rand = new Random();

		/// <summary>
		/// Negative exponential probability distribution
		/// </summary>
		/// <param name="ExpectedValue">The expected value.</param>
		/// <returns>-ExpectedValue * Math.Log(Rand.NextDouble(), Math.E)</returns>
		public static double NegEx(double ExpectedValue) {
			return -ExpectedValue * Math.Log(Rand.NextDouble(), Math.E);
		}

		/// <summary>
		/// Poisson distributes the specified expected value.
		/// </summary>
		/// <param name="expectedValue">The expected value.</param>
		/// <returns>Count</returns>
		public static int Poisson(double expectedValue) {
			double dLimit = -expectedValue;
			double dSum = Math.Log(Rand.NextDouble());
			int count;
			for(count = 0; dSum > dLimit; count++)
				dSum += Math.Log(Rand.NextDouble());
			return count;
		}
	}
}
