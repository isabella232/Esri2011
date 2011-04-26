using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace EsriDE.Trials.ArcMapAdapter
{
	/// <summary>
	/// Summary description for ArcMapCommand.
	/// </summary>
	[Guid("ce36bf0a-2c6c-4b59-89ee-46eac82af980")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("EsriDE.Trials.ArcMapAdapter.ArcMapCommand")]
	public sealed class ArcMapCommand : BaseCommand
	{
		#region COM Registration Function(s)
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType);

			//
			// TODO: Add any COM registration code here
			//
		}

		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryUnregistration(registerType);

			//
			// TODO: Add any COM unregistration code here
			//
		}

		#region ArcGIS Component Category Registrar generated code
		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(Type registerType)
		{
            Trace.WriteLine("Registering '" + registerType.GUID + "' as MxCommand.");
            
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommands.Register(regKey);
		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
            Trace.WriteLine("Unregistering '" + registerType.GUID + "' from MxCommand.");

            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxCommands.Unregister(regKey);
		}

		#endregion
		#endregion

		private IApplication m_application;
		public ArcMapCommand()
		{
            Trace.WriteLine("ArcMapCommand.ctor()");
            //
			// TODO: Define values for the public properties
			//
			base.m_category = "ESRI Deutschland GmbH"; 
            base.m_caption = "ArcMapCommand Spike";  
            base.m_message = "Spike für ArcMapCommand";
			base.m_toolTip = m_message;
            base.m_name = "ArcMapAdapter_ArcMapCommand";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

			try
			{
				//
				// TODO: change bitmap name if necessary
				//
				string bitmapResourceName = GetType().Name + ".bmp";
				base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
			}
		}

		#region Overridden Class Methods

		/// <summary>
		/// Occurs when this command is created
		/// </summary>
		/// <param name="hook">Instance of the application</param>
		public override void OnCreate(object hook)
		{
			if (hook == null)
				return;

			m_application = hook as IApplication;

			//Disable if it is not ArcMap
			if (hook is IMxApplication)
				base.m_enabled = true;
			else
				base.m_enabled = false;

			// TODO:  Add other initialization code
		}

		/// <summary>
		/// Occurs when this command is clicked
		/// </summary>
		public override void OnClick()
		{
			// TODO: Add ArcMapCommand.OnClick implementation
		}

		#endregion
	}
}
