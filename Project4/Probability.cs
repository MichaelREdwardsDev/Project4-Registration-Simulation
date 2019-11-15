////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		Probability.cs
//	Description:	 
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
	public static class Probability {
		public static Random Rand = new Random();
		public static double NegEx(double ExpectedValue) {
			return -ExpectedValue * Math.Log(Rand.NextDouble(), Math.E);
		}

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
