////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		PriorityQueue.cs
//	Description:	Customized queue that accounts for the priority of the objects contained 
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Utils.Probability;

namespace Project4 {
	/// <summary>
	/// Customized queue that accounts for the priority of the objects contained
	/// </summary>
	class ConventionRegistration {
		private static int NumOfLines = 11;
		private List<String> PossibleIDs = GenerateList();
		public List<Line> Lines = OpenLines(NumOfLines);
		public List<ListBox> ListBoxes;
		private Registrant[] currentRegistrants = new Registrant[NumOfLines];

		/// <summary>
		/// Gets or sets the time started.
		/// </summary>
		/// <value>
		/// The time started.
		/// </value>
		public DateTime TimeStarted { get; set; }

		/// <summary>
		/// Gets or sets the closing time.
		/// </summary>
		/// <value>
		/// The closing time.
		/// </value>
		public DateTime ClosingTime { get; set; }

		/// <summary>
		/// Gets or sets the current time.
		/// </summary>
		/// <value>
		/// The current time.
		/// </value>
		public DateTime CurrentTime { get; set; }

		/// <summary>
		/// Gets or sets the events.
		/// </summary>
		/// <value>
		/// The events.
		/// </value>
		public PriorityQueue<Event> Events { get; set; } = new PriorityQueue<Event>();

		public int EventCount = 0, ArrivalCount = 0, DepartureCount = 0, LongestQueue = 0;

		private bool SimRunning = true;
		public delegate void SetListBoxDelegate();

		/// <summary>
		/// Initializes a new instance of the <see cref="ConventionRegistration"/> class.
		/// </summary>
		/// <param name="form">The form.</param>
		public ConventionRegistration(RegistrationSimulationForm form) {
			ListBoxes = GetListBoxes(form);
			TimeStarted = DateTime.Today;
			TimeStarted = TimeStarted.AddHours(8.0);
			ClosingTime = DateTime.Today.AddHours(18.0);
		}

		/// <summary>
		/// Handles the registrants in the simulation.
		/// </summary>
		/// <param name="form">The form.</param>
		public void HandleRegistrants(RegistrationSimulationForm form) {
			Task entrance = HandleEntrees(form);
			Task.Delay(1000);
			//Task windows = HandleWindows(form);
			Task departures = HandleDepartures(form);
		}

		/// <summary>
		/// Method for when person arrives in the simulation.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <returns></returns>
		public Task HandleEntrees(RegistrationSimulationForm form) {
			Task entrance = Task.Factory.StartNew(() => {
				Registrant currReg = new Registrant();
				DateTime nextEntrance = CurrentTime = TimeStarted;
				int idIndex;
				String currID;
				while((CurrentTime < ClosingTime && PossibleIDs.Count > 0)) {
					form.CurrentTimeLabel.Text = CurrentTime.ToLongTimeString();
					form.textBoxEvents.Text = EventCount.ToString();
					if(CurrentTime >= nextEntrance && CurrentTime <= ClosingTime) {
						ArrivalCount++;
						EventCount++;
						form.textBoxArrivals.Text = ArrivalCount.ToString();
						idIndex = Rand.Next(PossibleIDs.Count);
						currID = PossibleIDs[idIndex];
						PossibleIDs.Remove(currID);
						currReg = new Registrant(currID);
						currReg.LineID = currReg.PickLine(Lines);
						currentRegistrants[idIndex] = currReg;
						try {
							MessageBox.Show(Lines[idIndex].ToString());
						} catch(Exception ex) {
							MessageBox.Show(ex.Message);
						}
						ListBoxes[currReg.LineID].Items.Add(currReg.RegistrantID);
						if(Events.Count == 0) {
							EventCount++;
							Events.Enqueue(new Event(idIndex, "departure", currReg, CurrentTime));
						}
						Events.Enqueue(new Event(idIndex, "arrival", currReg, CurrentTime));
						nextEntrance = CurrentTime + new TimeSpan(0, 0, Rand.Next(75));
					}
					CurrentTime += new TimeSpan(0, 0, 1);
					Thread.Sleep(1);
				}
			});
			return entrance;
		}

		/// <summary>
		/// Method for when someone departs the window in the simulation.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <returns></returns>
		private Task HandleDepartures(RegistrationSimulationForm form) {
			Task departure = Task.Factory.StartNew(() => {
				Event tempEvent;
				Registrant tempReg;
				Thread.Sleep(1000);
			});
			return departure;
		}

		/// <summary>
		/// Method that deals with the windows that have dealt with person in simulation.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <returns>windo</returns>
		public Task HandleWindows(RegistrationSimulationForm form) {
			Task window = Task.Factory.StartNew(() => {
				Thread.Sleep(3000);
				while(SimRunning) {
					foreach(Line line in Lines) {
						//MessageBox.Show(line.Peek().CompletionTime.ToString());
					}
				}
			});
			return window;
		}

		/*		private void storage() {
					if(Events.Peek().EventType == "arrival") {
						tempEvent = Events.Dequeue();
						Events.Enqueue(new Event(Int32.Parse(tempEvent.Registrant.RegistrantID), "departure", tempEvent.Registrant, CurrentTime));
					} else if(Events.Peek().Time <= CurrentTime) {
						tempEvent = Events.Dequeue();
						MessageBox.Show(tempEvent.EventType + " " + tempEvent.Time.ToString() + " " + tempEvent.Registrant.RegistrantID);
						currReg = tempEvent.Registrant;
						Lines[currReg.LineID].Dequeue();
						ListBoxes[currReg.LineID].Items.Remove(currReg.RegistrantID);
					}
				}*/

		/// <summary>
		/// Method for available lines in the simulation.
		/// </summary>
		/// <param name="numOfLines">The number of lines.</param>
		/// <returns>retList</returns>
		private static List<Line> OpenLines(int numOfLines) {
			List<Line> retList = new List<Line>();
			for(int i = 0; i < numOfLines; i++) {
				retList.Add(new Line(i));
			}
			return retList;
		}

		/// <summary>
		/// Generates the list of items that have been Poisson Distributed.
		/// </summary>
		/// <returns>retList</returns>
		private static List<String> GenerateList() {
			List<String> retList = new List<String>();
			for(int i = 1; i <= Poisson(1000); i++) {
				retList.Add(i.ToString().PadLeft(4, '0'));
			}
			return retList;
		}

		/// <summary>
		/// Gets the list boxes from the form.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <returns>retList</returns>
		private List<ListBox> GetListBoxes(RegistrationSimulationForm form) {
			List<ListBox> retList = new List<ListBox>();
			foreach(Control box in form.Controls) {
				if(box.GetType().Name == "ListBox") {
					retList.Add(box as ListBox);
				}
			}
			return retList.OrderBy(id => id.AccessibleName).ToList();
		}
	}
}
