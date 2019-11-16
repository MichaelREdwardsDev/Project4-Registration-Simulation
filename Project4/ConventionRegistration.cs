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
using System.Windows.Forms;
using static Utils.Probability;

namespace Project4
{
    class ConventionRegistration
    {
		private List<String> PossibleIDs = GenerateList();
		public List<Line> Lines = OpenLines(11);
		public List<ListBox> ListBoxes;
        public DateTime TimeStarted { get; set; }

        public DateTime ClosingTime { get; set; }

        public DateTime CurrentTime { get; set; }

		public Action<String> str { get; set; }
		//public PriorityQueue<Events> RegistrationQueue { get; set; }

        public ConventionRegistration(RegistrationSimulationForm form)
        {
			ListBoxes = GetListBoxes(form);
            TimeStarted = DateTime.Today;
            TimeStarted = TimeStarted.AddHours(8.0);

			ClosingTime = DateTime.Today;
			ClosingTime = ClosingTime.AddHours(18.0);
			Registrant currReg;
			DateTime CurrentTime = TimeStarted;
			int i = 0;
			while(/*CurrentTime < ClosingTime*/i++ < PossibleIDs.Count) {
				int idIndex = Rand.Next(PossibleIDs.Count);
				String ID = PossibleIDs[idIndex];
				PossibleIDs.Remove(ID);
				currReg = new Registrant(ID);
				int lineID = currReg.Pickline(Lines);
				ListBoxes[lineID].Items.Add(currReg.RegistrantID);
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

		private List<ListBox> GetListBoxes(Form form) {
			List<ListBox> retList = new List<ListBox>();
			foreach(Control box in form.Controls) {
				if(box.GetType().Name == "ListBox")
					retList.Add(box as ListBox);
			}
			return retList;
		}
	}
}
