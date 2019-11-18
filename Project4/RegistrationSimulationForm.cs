/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 4 - Registration Simulation
//	File Name:		RegistrationSimulationForm.cs
//	Description:	Main form for the simulation - Displays the lines as well as other useful information about the simulation
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Michael Edwards, edwardsmr@etsu.edu, Elizabeth Jennings, jenningsel@etsu.edu, William Jennings, jenningsw@etsu.edu
//	Created:		Sunday November 14, 2019
//	Copyright:		Michael Edwards, Elizabeth Jennings, William Jennings 2019
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
using static Utils.Utility;
using System.Windows.Forms;

namespace Project4 {
	/// <summary>
	/// The main form class, contains the Main Form constructor as well as several other functionalities for the controls on the GUI
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class MainForm:Form {
		public ConventionRegistration Conv;
		/// <summary>
		/// Initializes a new instance of the <see cref="MainForm"/> class.
		/// </summary>
		public MainForm() {

			InitializeComponent();
		}
		/// <summary>
		/// Exits the application when the button has been clicked
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event<see cref="EventArgs"/> instance containing the event data.</param>
		private void ButtonExit_Click(object sender, EventArgs e) {
			Application.Exit();
		}
		/// <summary>
		/// Handles the Click event of the ButtonStart control. Creates a new ConventionRegistration object to run the simulation.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ButtonStart_Click(object sender, EventArgs e) {
			ResetListBoxes(this);
			Conv = new ConventionRegistration(this);
		}
		/// <summary>
		/// Handles the Click event of the ButtonClear control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ButtonClear_Click(object sender, EventArgs e) {
			try {
				if(Conv.SimulationRunning) {
					MessageBox.Show("Please wait for the simulation to end...");
				} else {
					ClearFormControls(this);
					ResetListBoxes(this);
				}
			} catch(NullReferenceException) {
				MessageBox.Show("The simulation must be ran before clearing...");
			}
		}
		/// <summary>
		/// Resets all of the list boxes in the forms' visibility to reset the GUI back to the default look
		/// </summary>
		/// <param name="form"></param>
		private void ResetListBoxes(MainForm form) {
			foreach(Control box in form.Controls) {
				if(box.GetType().Name == "ListBox") {
					box.Visible = false;
				}
			}
		}
    }
}
