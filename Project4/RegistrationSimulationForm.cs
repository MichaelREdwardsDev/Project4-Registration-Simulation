/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		RegistrationSimulationForm.cs
//	Description:	
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Michael Edwards, edwardsmr@etsu.edu, Elizabeth Jennings, jenningsel@etsu.edu, William Jennings, jenningsw@etsu.edu
//	Created:		Sunday November 14, 2019
//	Copyright:		Michael Edwards, 2019
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project4 {
	public partial class RegistrationSimulationForm:Form {
		public RegistrationSimulationForm() {
			InitializeComponent();
		}



		private void RegistrationSimulationForm_Load(object sender, EventArgs e) {

		}

		/// <summary>
		/// Exits the application when the button has been clicked (Liz)
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event<see cref="EventArgs"/> instance containing the event data.</param>
		private void ButtonExit_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void ButtonStart_Click(object sender, EventArgs e) {
		}

		private void ButtonClear_Click(object sender, EventArgs e) {
			
		}
	}
}
