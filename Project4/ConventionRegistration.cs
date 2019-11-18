////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		ConventionRegistration.cs
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
	public class ConventionRegistration {
		public MainForm Simulation { get; set; }
		/// <summary>
		/// The number of lines
		/// </summary>
		private int NumOfLines;
		/// <summary>
		/// The count of the expected Registrants
		/// </summary>
		public int ExpectedRegistrants { get; set; }
		/// <summary>
		/// The possible i ds
		/// </summary>
		public List<String> PossibleIDs { get; set; }
		/// <summary>
		/// The list boxes
		/// </summary>
		public List<ListBox> ListBoxes;
		/// <summary>
		/// The lines
		/// </summary>
		public List<Line> Lines;
		/// <summary>
		/// Gets or sets the time started.
		/// </summary>
		/// <value>
		/// The time started.
		/// </value>
		public DateTime OpeningTime { get; set; }
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
		public PriorityQueue<Event> Events { get; set; }
		/// <summary>
		/// The event count
		/// </summary>
		public int EventCount = 0, ArrivalCount = 0, DepartureCount = 0, LongestQueue = 0;
		/// <summary>
		/// Boolean to determine if the simulation has completed
		/// </summary>
		public bool SimulationRunning { get; set; } = true;
		/// <summary>
		/// The expected duration to complete a registration
		/// </summary>
		public TimeSpan ExpectedCheckoutDuration { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="ConventionRegistration"/> class.
		/// </summary>
		/// <param name="form">The form.</param>
		public ConventionRegistration(MainForm form) {
			Simulation = form;
			NumOfLines = GetNumOfLines(form);
			ListBoxes = GetListBoxes(form);
			Lines = OpenLines(NumOfLines, form);
			ExpectedRegistrants = GetExpectedRegistrants(form);
			PossibleIDs = GenerateList(ExpectedRegistrants);
			OpeningTime = GetOpeningTime(form);
			CurrentTime = OpeningTime;
			ClosingTime = GetClosingTime(form);
			ExpectedCheckoutDuration = GetExpectedCheckoutDuration(form);
			RunSimulation();
		}
		/// <summary>
		/// Retrieves the expected time from the form
		/// </summary>
		/// <param name="form">The main form for the program</param>
		/// <returns>The TimeSpan entered in the GUI</returns>
		private TimeSpan GetExpectedCheckoutDuration(MainForm form) {
			return new TimeSpan((int)form.HourBox.Value, (int)form.MinuteBox.Value, (int)form.SecondBox.Value);
		}
		/// <summary>
		/// Retrieves the Number Of Lines entered in the form
		/// </summary>
		/// <param name="form">The main form for the program</param>
		/// <returns>The entered number of lines</returns>
		private int GetNumOfLines(MainForm form) {
			return (int)form.NumWindowsBox.Value;
		}
		/// <summary>
		/// Retreives the opening Time from the form
		/// </summary>
		/// <param name="form">The main form for the program</param>
		/// <returns>The time entered int the GUI</returns>
		private DateTime GetOpeningTime(MainForm form) {
			return form.StartTimePicker.Value;
		}
		/// <summary>
		/// Retrieves the Closing time from the form
		/// </summary>
		/// <param name="form">The main form for the program</param>
		/// <returns>The Clsoing time from the GUI</returns>
		private DateTime GetClosingTime(MainForm form) {
			return form.EndTimePicker.Value;
		}
		/// <summary>
		/// Gets the expected registrant count from the GUI
		/// </summary>
		/// <param name="form">The main form for the program</param>
		/// <returns>The expected amount entered in the GUI</returns>
		private int GetExpectedRegistrants(MainForm form) {
			return (int)form.NumRegistrantsBox.Value;
		}
		/// <summary>
		/// Runs the simulation
		/// </summary>
		/// <returns>A task, simulation ran on a separate thread so the use of Thread.Sleep() does not freeze the main thread running the GUI</returns>
		public Task RunSimulation() {
			Task run = Task.Factory.StartNew(() => { // Create the task
				CurrentTime = OpeningTime; // Set the Current Time to the Opening time
				Events = new PriorityQueue<Event>(); // Init the Evengs Queue
				GenerateRegistrantEvents(); //  add arrivals in the queue
				Registrant currReg; // Declare a temporary Registrant object
				while(Events.Count > 0) { // Continue the simulation until all events have been handled
					if(Events.Peek().EventType == "departure" && Events.Peek().Time <= CurrentTime) { // If the event at the top of the queue is a departure
						DepartureCount++; // Increment departure
						Event DepEvent = Events.Dequeue(); // Dequeue the event
						currReg = DepEvent.Registrant; // set the currReg to the registrant for that event
						Lines[currReg.LineID].Dequeue(); // Dequeue the registrant from their line
						ListBoxes[currReg.LineID].Invoke((MethodInvoker)delegate { // Remove the registrant from the box in the GUI
							ListBoxes[currReg.LineID].Items.RemoveAt(0);
						});
						try { // Try to retrieve a new registrant to determine the next departure event for this line
							currReg = Lines[currReg.LineID].Peek();
							EventCount++;
							Events.Enqueue(new Event(Int32.Parse(currReg.RegistrantID), "departure", currReg, CurrentTime + currReg.CompletionTime));
						} catch(Exception) { // If there isn't a registrant
							UpdateGUI();
							continue; // ignore the Empty Queue exception
						}
					} else if(Events.Peek().Time <= CurrentTime) { // if the event is not a departure, it's an arrival
						Event ArrEvent = Events.Dequeue(); // Dequeue the event
						if(CurrentTime <= ClosingTime) { // If the convention is still open
							EventCount++; // increment the Event and arrival counts
							ArrivalCount++;
							currReg = ArrEvent.Registrant; // Then set the current registrant to that relatedd to the arrival
							currReg.LineID = currReg.PickLine(Lines); // Registrant picks the shortest line
							ListBoxes[currReg.LineID].Invoke((MethodInvoker)delegate { // Update the listbox in the GUI
								ListBoxes[currReg.LineID].Items.Add(currReg.RegistrantID);
							});
							if(Lines[currReg.LineID].Count == 1) { // if the line has only one registrant(The newly added one)
								currReg = Lines[currReg.LineID].Peek(); // Ensure the registrant is the one at top
								EventCount++; // Add a departure event for that registrant
								Events.Enqueue(new Event(Int32.Parse(currReg.RegistrantID), "departure", currReg, CurrentTime + currReg.CompletionTime));
							}
						} else if(ArrivalCount == DepartureCount && CurrentTime > ClosingTime) { // if all registrants have arrived(that got to the convention in time), and have been handled
							UpdateGUI();
							break; // The simulation is done
						}
					}
					foreach(Line line in Lines) { // foreach line
						if(line.Count > LongestQueue) { // Keep track of the longest line so far
							LongestQueue = line.Count;
						}
					}
					CurrentTime += new TimeSpan(0, 0, 1); // Increment the clock by one second every iteration
					UpdateGUI(); // Keep the GUI updated per iteration
				}
				MessageBox.Show("Simulation Complete!"); // Notify for completion
				SimulationRunning = false; // Set the simulation bool to false
			});
			return run; // Return the task
		}
		/// <summary>
		/// Generates all registration events to be handled prior to the simulation handling any events
		/// </summary>
		private void GenerateRegistrantEvents() {
			TimeSpan interval;
			DateTime previousTime = OpeningTime;
			foreach(string regID in PossibleIDs) {
				interval = new TimeSpan(0, 0, Rand.Next(90));
				Registrant currReg = new Registrant(PossibleIDs[Rand.Next(PossibleIDs.Count)], ExpectedCheckoutDuration);
				Events.Enqueue(new Event(Rand.Next(PossibleIDs.Count), "arrival", currReg, previousTime += interval));
			}
		}
		/// <summary>
		/// Updates many of the GUI controls(delegates used to be able to safely call on a different thread)
		/// </summary>
		public void UpdateGUI() {
			Simulation.textBoxArrivals.Invoke((MethodInvoker)delegate {
				Simulation.textBoxArrivals.Text = ArrivalCount.ToString();
			});
			Simulation.CurrentTimeLabel.Invoke((MethodInvoker)delegate {
				Simulation.CurrentTimeLabel.Text = CurrentTime.ToLongTimeString();
			});
			Simulation.textBoxEvents.Invoke((MethodInvoker)delegate {
				Simulation.textBoxEvents.Text = EventCount.ToString();
			});
			Simulation.textBoxDepartures.Invoke((MethodInvoker)delegate {
				Simulation.textBoxDepartures.Text = DepartureCount.ToString();
			});
			Simulation.textBoxEvents.Invoke((MethodInvoker)delegate {
				Simulation.textBoxEvents.Text = EventCount.ToString();
			});
			Simulation.LongestQueueBox.Invoke((MethodInvoker)delegate {
				Simulation.LongestQueueBox.Text = LongestQueue.ToString();
			});
		}
		/// <summary>
		/// Method for available lines in the simulation. Opens lines by adding them to the list of lines, as well as makes the GUI boxes visible
		/// </summary>
		/// <param name="numOfLines">The number of lines.</param>
		/// <returns>retList</returns>
		private List<Line> OpenLines(int numOfLines, MainForm form) {
			List<Line> retList = new List<Line>();
			try {
				for(int i = 0; i < numOfLines; i++) {
					retList.Add(new Line(i));
					ListBoxes[i].Visible = true;
				}
			} catch(IndexOutOfRangeException) {
				MessageBox.Show("Please enter no more than 33 lines");
			}
			return retList;
		}
		/// <summary>
		/// Generates the list of items that have been Poisson Distributed.
		/// </summary>
		/// <returns>retList</returns>
		private List<String> GenerateList(int expectedRegistrants) {
			List<String> retList = new List<String>();
			for(int i = 1; i <= Poisson(expectedRegistrants); i++) {
				retList.Add(i.ToString().PadLeft(4, '0'));
			}
			return retList;
		}
		/// <summary>
		/// Gets the list boxes from the form.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <returns>retList</returns>
		private List<ListBox> GetListBoxes(MainForm form) {
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
