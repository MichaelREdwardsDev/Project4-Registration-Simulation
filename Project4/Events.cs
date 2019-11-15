using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4 {
	class Events : IComparable {
		private DateTime Time = new DateTime();
		public String EventType { get; set; }
		public int EventID { get; set; }
		public int Priority { get; set; } = 0;

		public Events(int ID, String eventType, Registrant registrant, DateTime currTime) {
			EventID = ID;
			EventType = eventType;
			Time = DetermineEventTime(eventType, registrant, currTime);
		}

		private DateTime DetermineEventTime(String eventType, Registrant registrant, DateTime currTime) {
			
			if(eventType == "arrival")
				return currTime;
			else
				return currTime + registrant.CompletionTime;
		}

		public int CompareTo(object obj) {
			if(Time < (obj as Events).Time)
				return 1;
			else
				return -1;
		}
	}
}
