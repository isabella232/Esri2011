using System;
using System.Diagnostics;
using App;
using EsriDE.Samples.ContentFinder.DomainModel;
using Application = System.Windows.Forms.Application;

namespace EsriDE.Samples.ContentFinder.App
{
	internal static class Program
	{
		/// <summary>
		///   The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			LicenseUtil.InitializeEngineLicense();

			if (0 == args.Length)
			{
				StartGui();
			}
			else
			{
				StartConsole(args);
			}
		}

		private static void StartConsole(string[] args)
		{
			Console.WriteLine("Console startet.");
		}

		private static void StartGui()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var builder = new Builder();

			var portal = builder.GetPortal();
				portal.ContentSelected += HandleSelectedContent;
			var uc = builder.GetPortalControl();

			var form = new WinFormsForm(uc);
			Application.Run(form);
		}

		private static void HandleSelectedContent(Content content)
		{
			Debug.WriteLine(content);
			Console.WriteLine(content);
		}
	}
}

