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

namespace Project4
{
    class ConventionRegistration
    {
        public DateTime TimeStarted { get; set; }

        public DateTime ClosingTime { get; set; }

        public DateTime CurrentTime { get; set; }


        //public PriorityQueue<Events> RegistrationQueue { get; set; }

        public ConventionRegistration()
        {
            TimeStarted = DateTime.Today;
            TimeStarted = TimeStarted.AddHours(8.0);

            ClosingTime = DateTime.Today;
            ClosingTime = ClosingTime.AddHours(18.0);


            DateTime CurrentTime = TimeStarted;

            //RegistrationQueue = new ProrityQueue<Events>();

            while (CurrentTime != ClosingTime)
            {
                //if enough time has passed for an arrival of a registrant occurs, then add registrant to shortest line
            }

            //if tick occurs, check priority queue for ending registration time

            //if

        }
    }
}
