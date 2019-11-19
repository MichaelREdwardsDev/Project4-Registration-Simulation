////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		Registrant.cs
//	Description:	Registrant object to hold information of a specific registrant
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Michael Edwards, edwardsmr@etsu.edu, Elizabeth Jennings, jenningsel@etsu.edu, William Jennings, jenningsw@etsu.edu
//	Created:		Sunday November 14, 2019
//	Copyright:		Michael Edwards, Elizabeth Jennings, William Jennings, 2019
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utils.Probability;
using System.Windows.Forms;

namespace Project4 {
	/// <summary>
	/// A Registrant to be put through the registration system, determines the time taken to complete the registration process. as well as picks the shortest line
	/// </summary>
	public class Registrant {
		/// <summary>
		/// Registrant's ID number
		/// </summary>
		public String RegistrantID { get; set; }
		/// <summary>
		/// How long the registrant takes to complete the registration.
		/// </summary>
		public TimeSpan CompletionTime { get; set; }
		/// <summary>
		/// Gets or sets the line identifier.
		/// </summary>
		public int LineID { get; set; }
		/// <summary>
		/// Registrant Default Constructor - Initializes a new default instance of the <see cref="Registrant"/> class.
		/// </summary>
		public Registrant() {
			RegistrantID = "";
			CompletionTime = new TimeSpan(0);
		}
		/// <summary>
		/// Registrant Overloaded Constructor - takes in Registrant's ID number and their priority
		/// </summary>
		/// <param name="registrantID"></param>
		/// <param name="priority"></param>
		public Registrant(String registrantID, TimeSpan expectedDuration) {
			RegistrantID = registrantID;
			CompletionTime = DetermineCompletionTime(expectedDuration);
		}
		/// <summary>
		/// Picks a the leftmost shortest line and adds the registrant to that queue
		/// </summary>
		/// <param name="lines">The lines.</param>
		/// <returns>Shortest Line ID</returns>
		public int PickLine(List<Line> lines) {
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
		public TimeSpan DetermineCompletionTime(TimeSpan expectedDuration) {
			int timeInSeconds = (int)NegEx(expectedDuration.TotalSeconds);
			if(timeInSeconds < 90)
				return new TimeSpan(0, 0, 90);
			else
				return new TimeSpan(0, 0, timeInSeconds);
		}
	}
}