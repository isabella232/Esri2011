using System;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.WpfUI
{
	public class ContentFormPresenter : IToggleablePresenter
	{
		//private readonly IAgdAdapter _agdAdapter;
		private readonly IToggleableForm _form;
		private IShifterModel _model;

		//public ContentFormPresenter(IToggleModel model, IFormView form, IAgdAdapter agdAdapter)
		public ContentFormPresenter(IShifterModel model, IToggleableForm form)
		{
			Console.WriteLine("ContentFormPresenter.ctor()");

			_model = model;
			_model.ShifterStateChanged += ManageVisibility;

			_form = form;
			_form.Closing += HandleFormClosed;
			_form.Show();

			//_agdAdapter = agdAdapter;
		}

		#region IFormPresenter Members
		//public void SetModel(IFormModel model)
		//{
		//    Console.WriteLine("ContentFormPresenter.SetModel()");

		//    _model = model;
		//    _model.VisibilityStateChanged += ManageVisibility;
		//}

		//public void UnsetModel()
		//{
		//    Console.WriteLine("ContentFormPresenter.UnsetModel()");

		//    CleanUp();
		//}
		#endregion

		private void HandleFormClosed()
		{
			_model.ToggleShifter();
		}

		~ContentFormPresenter()
		{
			Console.WriteLine("ContentFormPresenter.~()");
		}

		private void ManageVisibility(ShifterState state)
		{
			switch (state)
			{
				case ShifterState.On:
					Console.WriteLine("ContentFormPresenter.ManageVisibility(visible)");
					break;
				case ShifterState.Off:
					Console.WriteLine("ContentFormPresenter.ManageVisibility(invisible)");
					
					CleanUp();
					break;
				default:
					throw new ArgumentOutOfRangeException("state");
			}
		}

		private void CleanUp()
		{
			Console.WriteLine("ContentFormPresenter.CleanUp()");

			_form.Closing -= HandleFormClosed;
			_form.Hide();

			_model.ShifterStateChanged -= ManageVisibility;
		}
	}
}