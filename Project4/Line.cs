//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		Line.cs
//	Description:	Holds a Priority Queue as well as an ID allowing for ease of differentiations between lines
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Michael Edwards, edwardsmr@etsu.edu
//	Created:		Sunday November 14, 2019
//	Copyright:		Michael Edwards, 2019
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4 {
	/// <summary>
	/// Encapsulates the PriorityQueue, as well as it's Count and ID for ease of differentiation between lines
	/// </summary>
	class Line {
		/// <summary>
		/// The queue containing the registrants already in this particular line
		/// </summary>
		public PriorityQueue<Registrant> Registrants { get; set; } = new PriorityQueue<Registrant>();
		/// <summary>
		/// The number of registrants in the line
		/// </summary>
		public int Count { get {
				return Registrants.Count;
			}
		}
		/// <summary>
		/// The ID for a particular line, allows ease of differentiation between lines
		/// </summary>
		public int LineID { get; set; }
		public Line(int lineID) {
			LineID = lineID;
		}
		public void Enqueue(Registrant reg) {
			Registrants.Enqueue(reg);
		}
	}
}
