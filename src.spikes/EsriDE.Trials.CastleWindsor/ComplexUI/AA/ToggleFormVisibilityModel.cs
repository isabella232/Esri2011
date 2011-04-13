using System;
using EsriDE.Trials.CastleWindsor.ComplexUI.Forms;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.AA
{
	public class ToggleFormVisibilityModel : IModel
	{
		private VisibilityState _visibilityState = VisibilityState.Invisible;

		public ToggleFormVisibilityModel()
		{
			Console.WriteLine("ToggleFormVisibilityModel.ctor()");
		}

		#region IModel Members
		public VisibilityState VisibilityState
		{
			get { return _visibilityState; }
			set
			{
				if (value != _visibilityState)
				{
					_visibilityState = value;
					VisibilityChanged(_visibilityState);
				}
			}
		}

		public void ToggleVisibility()
		{
			switch (VisibilityState)
			{
				case VisibilityState.Visible:
					VisibilityState = VisibilityState.Invisible;
					break;
				case VisibilityState.Invisible:
					VisibilityState = VisibilityState.Visible;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public event Action<VisibilityState> VisibilityChanged;
		public IBuilder Builder { get; set; }
		#endregion

		~ToggleFormVisibilityModel()
		{
			Console.WriteLine("ToggleFormVisibilityModel.~()");
		}
	}
}