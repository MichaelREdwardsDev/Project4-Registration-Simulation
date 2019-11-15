using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils {
	public static class Probability {
		public static Random Rand = new Random();
		public static double NegEx(double ExpectedValue) {
			return -ExpectedValue * Math.Log(Rand.NextDouble(), Math.E);
		}

		public static int Poisson(double expectedValue) {
			double dLimit = -expectedValue;
			double dSum = Math.Log(Rand.NextDouble());
			int count;
			for(count = 0; dSum > dLimit; count++)
				dSum += Math.Log(Rand.NextDouble());
			return count;
		}
	}
}
