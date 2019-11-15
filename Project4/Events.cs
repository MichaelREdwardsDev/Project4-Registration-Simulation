using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4 {
	class Events {
		private DateTime Time = new DateTime();
		public String EventType { get; set; }
		public int EventPriority { get; set; }
		public int EventID { get; set; }

		public Events(String eventType, int eventID, int eventPriority) {
			EventType = eventType;
			EventID = eventID;
			EventPriority = eventPriority;

		}
	}
}
