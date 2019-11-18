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

namespace Project4
{
    /// <summary>
    /// Customized queue that accounts for the priority of the objects contained
    /// </summary>
    public class ConventionRegistration
    {
        public RegistrationSimulationForm Simulation { get; set; }
        /// <summary>
        /// The number of lines
        /// </summary>
        private static int NumOfLines = 6;
        /// <summary>
        /// The possible i ds
        /// </summary>
        private List<String> PossibleIDs = GenerateList();
        /// <summary>
        /// The lines
        /// </summary>
        public List<Line> Lines = OpenLines(NumOfLines);
        /// <summary>
        /// The list boxes
        /// </summary>
        public List<ListBox> ListBoxes;
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
        public PriorityQueue<Event> Events { get; set; }
        /// <summary>
        /// The event count
        /// </summary>
        public int EventCount = 0, ArrivalCount = 0, DepartureCount = 0, LongestQueue = 0;
        /// <summary>
        /// The sim running
        /// </summary>
        private bool SimRunning = true;
        private Random ran { get; set; } = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConventionRegistration"/> class.
        /// </summary>
        /// <param name="form">The form.</param>
        public ConventionRegistration(RegistrationSimulationForm form)
        {
            Simulation = form;
            ListBoxes = GetListBoxes(form);
            TimeStarted = DateTime.Today.AddHours(8.0);
            ClosingTime = DateTime.Today.AddHours(18.0);
        }

        public Task RunSimulation()
        {
            Task run = Task.Factory.StartNew(() =>
            {
                CurrentTime = TimeStarted;

                Events = new PriorityQueue<Event>();

                GenerateRegistrantEvents();

                Registrant currReg;

                while (Events.Count > 0)
                {
					//MessageBox.Show("Mark");
					//Task.Delay(100);

					if(Events.Peek().EventType == "departure" && Events.Peek().Time <= CurrentTime) {
						DepartureCount++;
						Event DepEvent = Events.Dequeue();

						currReg = DepEvent.Registrant;
						Lines[currReg.LineID].Dequeue();
						ListBoxes[currReg.LineID].Invoke((MethodInvoker)delegate {
							ListBoxes[currReg.LineID].Items.RemoveAt(0);
						});

						//MessageBox.Show((CurrentTime).ToString() + "  " + (DepEvent.Time).ToString() + "  " + currReg.RegistrantID);

						try {
							currReg = Lines[currReg.LineID].Peek();
							//MessageBox.Show(tempReg.RegistrantID + " " + tempReg.LineID);
							EventCount++;
							Events.Enqueue(new Event(Int32.Parse(currReg.RegistrantID), "departure", currReg, CurrentTime + currReg.CompletionTime));
						} catch(Exception) {
							continue;
						}
					} else if(Events.Peek().Time <= CurrentTime)
                    {
                        
                        Event ArrEvent = Events.Dequeue();
						if(CurrentTime <= ClosingTime) {
							EventCount++;
							ArrivalCount++;
							currReg = ArrEvent.Registrant;
							currReg.LineID = currReg.PickLine(Lines);
							ListBoxes[currReg.LineID].Invoke((MethodInvoker)delegate {
								ListBoxes[currReg.LineID].Items.Add(currReg.RegistrantID);
							});


							if(Lines[currReg.LineID].Count == 1) {
								currReg = Lines[currReg.LineID].Peek();
								//MessageBox.Show(tempReg.RegistrantID + " " + tempReg.LineID);
								EventCount++;
								Events.Enqueue(new Event(Int32.Parse(currReg.RegistrantID), "departure", currReg, CurrentTime + currReg.CompletionTime));
								//MessageBox.Show((currReg.CompletionTime).ToString() + "  " + (CurrentTime + currReg.CompletionTime).ToString() + "  " + currReg.RegistrantID);

							}
						} else if(ArrivalCount == DepartureCount && CurrentTime > ClosingTime){
							break;
						}
                    }

                    UpdateGUI();

                    foreach (Line line in Lines)
                    {
                        if (line.Count > LongestQueue)
                        {
                            LongestQueue = line.Count;
/*                            Simulation.LongestQueueBox.Invoke((MethodInvoker)delegate {
                                form.LongestQueueBox.Text = LongestQueue.ToString();
                            });
*/                        }
                    }
                    CurrentTime += new TimeSpan(0, 0, 1);
                    Thread.Sleep(1/100);
                }
				MessageBox.Show("Simulation Complete!");
			});
			
            return run;
        }

        private void GenerateRegistrantEvents()
        {
            TimeSpan interval;

            DateTime previousTime = TimeStarted;

            foreach (string regID in PossibleIDs)
            {
                interval = new TimeSpan(0, 0, ran.Next(90));

                Registrant currReg = new Registrant(PossibleIDs[ran.Next(PossibleIDs.Count)]);

                Events.Enqueue(new Event(ran.Next(PossibleIDs.Count), "arrival", currReg, previousTime += interval));

            }
        }

        public void UpdateGUI()
        {
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
            Simulation.EventCountBox.Invoke((MethodInvoker)delegate {
                Simulation.EventCountBox.Text = Events.Count.ToString();
            });
            Simulation.LongestQueueBox.Invoke((MethodInvoker)delegate {
                Simulation.LongestQueueBox.Text = LongestQueue.ToString();
            });
        }

        /// <summary>
        /// Method for available lines in the simulation.
        /// </summary>
        /// <param name="numOfLines">The number of lines.</param>
        /// <returns>retList</returns>
        private static List<Line> OpenLines(int numOfLines)
        {
            List<Line> retList = new List<Line>();
            for (int i = 0; i < numOfLines; i++)
            {
                retList.Add(new Line(i));
            }
            return retList;
        }
        /// <summary>
        /// Generates the list of items that have been Poisson Distributed.
        /// </summary>
        /// <returns>retList</returns>
        private static List<String> GenerateList()
        {
            List<String> retList = new List<String>();
            for (int i = 1; i <= Poisson(1000); i++)
            {
                retList.Add(i.ToString().PadLeft(4, '0'));
            }
            return retList;
        }

        /// <summary>
        /// Gets the list boxes from the form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>retList</returns>
        private List<ListBox> GetListBoxes(RegistrationSimulationForm form)
        {
            List<ListBox> retList = new List<ListBox>();
            foreach (Control box in form.Controls)
            {
                if (box.GetType().Name == "ListBox")
                {
                    retList.Add(box as ListBox);
                }
            }
            return retList.OrderBy(id => id.AccessibleName).ToList();
        }
    }
}
