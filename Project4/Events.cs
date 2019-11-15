////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		Events.cs
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

namespace Project4 {
	class Events : IComparable {
		private DateTime Time = new DateTime();
		public String EventType { get; set; }
		public int EventID { get; set; }

		public Events(String eventType, Registrant registrant, DateTime currTime) {
			EventType = eventType;
			Time = DetermineEventTime(eventType, registrant, currTime);
		}

		private DateTime DetermineEventTime(String eventType, Registrant registrant, DateTime currTime) {
			if(eventType == "arrival")
				return currTime;
			else
				return currTime + registrant.CompletionTime;
		}

		public int CompareTo(object obj) {
			throw new NotImplementedException();
		}
	}
}
