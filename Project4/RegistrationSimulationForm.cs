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
using static Utils.Utility;
using System.Windows.Forms;

namespace Project4 {
	
	/// <summary>
	/// 
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

		//public List<ListBox>
       
		private void RegistrationSimulationForm_Load(object sender, EventArgs e) {

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
		/// Handles the Click event of the ButtonStart control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ButtonStart_Click(object sender, EventArgs e) {
			//ClearFormControls(this);
			Conv = new ConventionRegistration(this);
			//Task run = Conv.RunSimulation();
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

		private void ResetListBoxes(MainForm form) {
			foreach(Control box in form.Controls) {
				if(box.GetType().Name == "ListBox") {
					box.Visible = false;
				}
			}
		}
	}
}
