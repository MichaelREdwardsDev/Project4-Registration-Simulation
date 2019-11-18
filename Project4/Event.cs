////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		Events.cs
//	Description:	Possible Events, contains what time the events happen to be added to the current time in the simulations
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
    /// Possible Events, contains what time the events happen to be added to the current time in the simulations
    /// </summary>
    /// <seealso cref="System.IComparable" />
   public class Event :IComparable {        
		/// <summary>
		/// Gets or sets the registrant.
		/// </summary>
		/// <value>
		/// The registrant.
		/// </value>
		public Registrant Registrant { get; set; }		
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public DateTime Time { get; set; } = new DateTime();

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>
        /// The type of the event.
        /// </value>
        public String EventType { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        public int EventID { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public int Priority { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="registrant">The registrant.</param>
        /// <param name="currTime">The current time.</param>
        public Event(int ID, String eventType, Registrant registrant, DateTime eventTime) {
			EventID = ID;
			EventType = eventType;
			Registrant = registrant;
			Time = eventTime;
		}

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
        /// <returns>int</returns>
        public int CompareTo(object obj) {
			if(Time < (obj as Event).Time)
				return 1;
			else
				return -1;
		}
	}
}
