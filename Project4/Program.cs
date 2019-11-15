using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using static Utils.Probability;

namespace Project4 {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			/*Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new RegistrationSimulationForm());*/
			Console.WriteLine(Probability.Poisson(1000));
		}
	}
}