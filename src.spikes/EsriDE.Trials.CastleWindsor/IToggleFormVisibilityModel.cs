using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IToggleFormVisibilityModel
	{
		Visibility Visibility { get; }
		void ToggleVisibility();
		event Action<Visibility> VisibilityChanged;
	}

	public class ToggleFormVisibilityModel : IToggleFormVisibilityModel
	{
		private Visibility _visibility;

		public Visibility Visibility
		{
			get { return _visibility; } 
			set
			{
				if (value == _visibility)
				{
					_visibility = value;
					VisibilityChanged(_visibility);
				}
			}
		}

		public void ToggleVisibility()
		{
			switch (Visibility)
			{
				case Visibility.Visible:
					Visibility = Visibility.Unvisible;
					break;
				case Visibility.Unvisible:
					Visibility = Visibility.Visible;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public event Action<Visibility> VisibilityChanged;
	}
}