////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		Registrant.cs
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
using static Utils.Probability;

namespace Project4 {
	/// <summary>
	/// A Registrant to be put through the registration system, determines the time taken to complete the registration process. as well as picks the shortest line
	/// </summary>
	class Registrant{
		/// <summary>
		/// Registrant's ID number
		/// </summary>
		public int RegistrantID { get; set; }
		/// <summary>
		/// How long the registrant takes to complete the registration.
		/// </summary>
		public TimeSpan CompletionTime { get; set; }

		/// <summary>
		/// Registrant Overloaded Constructor - takes in Registrant's ID number and their priority
		/// </summary>
		/// <param name="registrantID"></param>
		/// <param name="priority"></param>
		public Registrant(int registrantID) {
			RegistrantID = registrantID;
			CompletionTime = DetermineCompletionTime();
		}

        /// <summary>
        /// Picks a line that has the shortest line and if all lines have the same amount
        /// of people in it, then it will go to the left-most queue.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <returns>Shortest Line ID</returns>
        public int Pickline(List<Line> lines) {
			Line shortestLine = null;
			Nullable<int> lowestLineCount = null;
			foreach(Line line in lines) {
				if(lowestLineCount == null || lowestLineCount > line.Count) {
					lowestLineCount = line.Count;
					shortestLine = line;
				}
			}
			shortestLine.Enqueue(this);
			return shortestLine.LineID;
		}

        /// <summary>
        /// Determines the completion time.
        /// </summary>
        /// <returns>Time Span</returns>
        public TimeSpan DetermineCompletionTime() {
			//270 seconds
			int timeInSeconds = (int)NegEx(270);
			if(timeInSeconds < 90)
				return new TimeSpan(0, 0, 90);
			else
				return new TimeSpan(0, 0, timeInSeconds);
		}
	}
}
