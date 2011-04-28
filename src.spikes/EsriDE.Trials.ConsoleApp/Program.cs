using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EsriDE.Trials.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			if (0 == args.Length)
			{
				StartGui();
			}
			else
			{
				StartConsole();
			}
		}

		private static void StartConsole()
		{
			Console.WriteLine("Console startet.");
		}

		private static void StartGui()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
