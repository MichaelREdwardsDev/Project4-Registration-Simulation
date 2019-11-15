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

		public Events(String eventType, Registrant registrant, DateTime currTime) {
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
			throw new NotImplementedException();
		}
	}
}
