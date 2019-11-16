//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		PriorityQueue.cs
//	Description:	Customized queue that accounts for the priority of the objects contained 
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
using static Utils.Probability;

namespace Project4 {
	class ConventionRegistration {

		public List<String> PossibleIDs = GenerateList();
		public DateTime TimeStarted { get; set; }

		public DateTime ClosingTime { get; set; }

		public DateTime CurrentTime { get; set; }


		//public PriorityQueue<Events> RegistrationQueue { get; set; }

		public ConventionRegistration() {
			TimeStarted = DateTime.Today;
			TimeStarted = TimeStarted.AddHours(8.0);

			ClosingTime = DateTime.Today;
			ClosingTime = ClosingTime.AddHours(18.0);
			Registrant currReg;
			DateTime CurrentTime = TimeStarted;
			while(CurrentTime != ClosingTime) {
				int idIndex = Rand.Next(PossibleIDs.Count);
				String ID = PossibleIDs[idIndex];
				PossibleIDs.RemoveAt(idIndex);
				currReg = new Registrant(ID);
				//if enough time has passed for an arrival of a registrant occurs, then add registrant to shortest line
			}

		}

		private static List<String> GenerateList() {
			List<String> retList = new List<String>();
			for(int i = 1; i <= Poisson(1000); i++) {
				retList.Add(i.ToString().PadLeft(4, '0'));
			}
			return retList;
		}
	}
}
