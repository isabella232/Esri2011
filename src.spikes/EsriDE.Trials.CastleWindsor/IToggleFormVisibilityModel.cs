using System;

namespace EsriDE.Trials.CastleWindsor
{
	public interface IBuilderHolder
	{
		IBuilder Builder {get; set;}
	}

	public interface IToggleFormVisibilityModel
	{
		Visibility Visibility { get; }

		void ToggleVisibility();
		event Action<Visibility> VisibilityChanged;
	}

	public interface IModel : IToggleFormVisibilityModel, IBuilderHolder
	{}

	public class ToggleFormVisibilityModel : IModel
	{
		private Visibility _visibility = Visibility.Invisible;

		public ToggleFormVisibilityModel()
		{
			Console.WriteLine("ToggleFormVisibilityModel.ctor()");
		}

		~ToggleFormVisibilityModel()
		{
			Console.WriteLine("ToggleFormVisibilityModel.~()");
		}

		public Visibility Visibility
		{
			get { return _visibility; } 
			set
			{
				if (value != _visibility)
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
					Visibility = Visibility.Invisible;
					break;
				case Visibility.Invisible:
					Visibility = Visibility.Visible;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public event Action<Visibility> VisibilityChanged;
		public IBuilder Builder { get; set; }
	}
}