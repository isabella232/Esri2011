using System;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.AgdAdapterCommons
{
	public class ShifterModel : IShifterModel
	{
		private ShifterState _shifterState = ShifterState.Off;
		private event Action<ShifterState> _shifterStateChanged;

		public ShifterState ShifterState
		{
			get { return _shifterState; }
			set
			{
				if (value != _shifterState)
				{
					_shifterState = value;
					OnShifterStateChanged(_shifterState);
				}
			}
		}

		public virtual void ToggleShifter()
		{
			switch (ShifterState)
			{
				case ShifterState.On:
					ShifterState = ShifterState.Off;
					break;
				case ShifterState.Off:
					ShifterState = ShifterState.On;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public event Action<ShifterState> ShifterStateChanged
		{
			add { _shifterStateChanged += value; }
			remove { _shifterStateChanged -= value; }
		}

		protected virtual void OnShifterStateChanged(ShifterState shifterState)
		{
			var shifterStateChanged = _shifterStateChanged;
			if (null != shifterStateChanged)
			{
				shifterStateChanged(shifterState);
			}
		}
	}
}