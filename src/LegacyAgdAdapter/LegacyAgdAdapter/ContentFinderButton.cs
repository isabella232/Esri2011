using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.LegacyAgdAdapter
{
	[Guid("54021017-4865-407D-AC9F-E082038AA7E2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("LegacyAgdAdapter.ContentFinderButton")]
	[ComVisible(true)]
	public partial class ContentFinderButton : BaseCommand, IShifterView
	{
		public ContentFinderButton()
		{
			Trace.WriteLine("ContentFinderButton.ctor()");

			m_category = "ESRI2 Deutschland GmbH"; 
			m_caption = "Content Finder (COM)";  
			m_message = "ICommand-based Content Finder";
			m_toolTip = m_message;
			m_name = "LegacyAgdAdapter_ContentFinderButton";

			try
			{
				string bitmapResourceName = GetType().Name + ".bmp";
				m_bitmap = new Bitmap(GetType(), bitmapResourceName);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.Message, "Invalid Bitmap");
			}
		}

		public override void OnCreate(object hook)
		{
			var mxApplication = hook as IMxApplication;

			if (null == mxApplication)
			{
				return;
			}

			m_enabled = true;

			var application = hook as IApplication;
			new Builder(this, application.hWnd);
		}

		public void SetShifterState(ShifterState shifterState)
		{
			m_checked = ShifterState.On == shifterState;
		}

		public void SetEnabledState(EnabledState enabledState)
		{
			m_enabled = EnabledState.Enabled == enabledState;
		}

		private event Action _clicked;
		public event Action Clicked
		{
			add { _clicked += value; }
			remove { _clicked -= value; }
		}

		public override void OnClick()
		{
			OnClicked();
		}

		protected virtual void OnClicked()
		{
			var clicked = _clicked;
			if (null != clicked)
			{
				clicked();
			}
		}
	}
}