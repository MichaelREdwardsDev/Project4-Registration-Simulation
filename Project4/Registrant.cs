using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4 {
	class Registrant : IComparable {
		public int Priority { get; set; }
		public int RegistrantID { get; set; }
		public Registrant(int registrantID, int priority) {
			Priority = priority;
			RegistrantID = registrantID;
		}
		public int CompareTo(object obj) {
			if(Priority > (obj as Registrant).Priority) {
				return 1;
			}else if(Priority == (obj as Registrant).Priority) {
				return 0;
			} else {
				return -1;
			}
		}
		public int Pickline(List<Line> lines) {
			Line shortestLine = null;
			Nullable<int> lowestLineCount = null;
			foreach(Line line in lines) {
				if(lowestLineCount == null || lowestLineCount > line.Count) {
					lowestLineCount = line.Count;
					shortestLine = line;
				}
			}
			shortestLine.Enqueue(this);
			return shortestLine.LineID;
		}
	}
}
