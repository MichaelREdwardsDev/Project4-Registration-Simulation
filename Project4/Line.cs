/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		Line.cs
//	Description:	Holds a Priority Queue as well as an ID allowing for ease of differentiations between lines
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

namespace Project4 {
	/// <summary>
	/// Encapsulates the PriorityQueue, as well as it's Count and ID for ease of differentiation between lines
	/// </summary>
	class Line {
		/// <summary>
		/// The queue containing the registrants already in this particular line
		/// </summary>
		public Queue<Registrant> Registrants { get; set; } = new Queue<Registrant>();
		private Registrant top = new Registrant(), previousTop;
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
		/// <summary>
		/// Line Overloaded Constructor - Takes the ID for a line particular line
		/// </summary>
		/// <param name="lineID">the line id</param>
		public Line(int lineID) {
			LineID = lineID;
			Enqueue(top);
		}
		/// <summary>
		/// Adds a registrant to the line
		/// </summary>
		/// <param name="reg">The registrant to be added</param>
		public void Enqueue(Registrant reg) {
			Registrants.Enqueue(reg);
		}
		/// <summary>
		/// Removes the Registrant from the front of the line
		/// </summary>
		public void Dequeue() {
			Registrants.Dequeue();
		}        
		/// <summary>
		/// Peeks this instance.
		/// </summary>
		/// <returns>Registrants.Peek()</returns>
		public Registrant Peek() {
			return Registrants.Peek();
		}        
		/// <summary>
		/// Checks to see if the top of the queue has changed.
		/// </summary>
		/// <returns>true or false</returns>
		public bool TopChanged() {
			if(previousTop != top) {
				previousTop = top;
				return true;
			}
			return false;
		}
	}
}
