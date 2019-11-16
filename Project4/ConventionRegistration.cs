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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Utils.Probability;

namespace Project4
{
	class ConventionRegistration {
		private List<String> PossibleIDs = GenerateList();
		public List<Line> Lines = OpenLines(11);
		public List<ListBox> ListBoxes;
		public DateTime TimeStarted { get; set; }

		public DateTime ClosingTime { get; set; }

		public DateTime CurrentTime { get; set; }
		public PriorityQueue<Event> Events { get; } = new PriorityQueue<Event>();
		public int EventCount = 0, ArrivalCount = 0, DepartureCount = 0, LongestQueue = 0;
        public ConventionRegistration(RegistrationSimulationForm form)
        {
			ListBoxes = GetListBoxes(form);
            TimeStarted = DateTime.Today;
            TimeStarted = TimeStarted.AddHours(8.0);
			ClosingTime = DateTime.Today.AddHours(18.0);
		}
		/*public async void HandleDepartures(RegistrationSimulationForm form) {
			*//*Registrant atWindow = new Registrant();
			if(Events.Peek().EventType == "departure") {
				try {
					atWindow = Events.Dequeue().Registrant;
					ListBoxes[atWindow.LineID].Items.Remove(atWindow.RegistrantID);
				} catch(Exception) {

				}
			}
			if()*//*
			await Task.Delay(atWindow.CompletionTime);
		}*/
		public async void HandleEntrances(RegistrationSimulationForm form) {
			Registrant currReg;
			CurrentTime = TimeStarted;
			int idIndex;
			String currID;
			DateTime NextEntrance = CurrentTime;
			while(CurrentTime < ClosingTime && PossibleIDs.Count > 0) {
				form.CurrentTimeLabel.Text = CurrentTime.ToLongTimeString();
				form.textBoxEvents.Text = EventCount.ToString();
				if(CurrentTime >= NextEntrance) {
					ArrivalCount++;
					EventCount++;
					form.textBoxArrivals.Text = ArrivalCount.ToString();
					idIndex = Rand.Next(PossibleIDs.Count);
					currID = PossibleIDs[idIndex];
					PossibleIDs.Remove(currID);
					currReg = new Registrant(currID);
					currReg.LineID = currReg.Pickline(Lines);
					ListBoxes[currReg.LineID].Items.Add(currReg.RegistrantID);
					Events.Enqueue(new Event(idIndex, "arrival", currReg, CurrentTime));
					NextEntrance = CurrentTime + new TimeSpan(0, 0, Rand.Next(75));
				}
				await Task.Delay(1);
				CurrentTime += new TimeSpan(0, 0, 1);
			}
		}

		private static List<Line> OpenLines(int numOfLines) {
			List<Line> retList = new List<Line>();
			for(int i = 0; i < numOfLines; i++) {
				retList.Add(new Line(i));
			}
			return retList;
		}

		private static List<String> GenerateList() {
			List<String> retList = new List<String>();
			for(int i = 1; i <= Poisson(1000); i++) {
				retList.Add(i.ToString().PadLeft(4, '0'));
			}
			return retList;
		}

		private List<ListBox> GetListBoxes(RegistrationSimulationForm form) {
			List<ListBox> retList = new List<ListBox>();
			foreach(Control box in form.Controls) {
				if(box.GetType().Name == "ListBox")
					retList.Add(box as ListBox);
			}
			return retList;
		}
	}
}
