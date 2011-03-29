using System;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Mapfel.Trials.RhinoMocks
{
	public enum Visibility { Visible, Invisible }
	public enum CheckedState { Checked, Unchecked }

	public interface IToggleFormVisibilityModel
	{
		Visibility Visibility { get; }

		void ToggleVisibility();
		event Action<Visibility> VisibilityChanged;
	}

	public interface IButtonView
	{
		void SetCheckedState(CheckedState checkedState);
		event Action Clicked;
	}

	public interface IButtonPresenter
	{
		void ConnectView(IButtonView buttonView);
	}

	public class ButtonPresenter : IButtonPresenter
	{
		private readonly IToggleFormVisibilityModel _toggleFormVisibilityModel;
		private IButtonView _buttonView;

		public ButtonPresenter(IToggleFormVisibilityModel toggleFormVisibility)
		{
			_toggleFormVisibilityModel = toggleFormVisibility;
			_toggleFormVisibilityModel.VisibilityChanged += SetButtonCheckedState;
		}

		public void ConnectView(IButtonView buttonView)
		{
			_buttonView = buttonView;
			_buttonView.Clicked += ButtonClicked;
		}

		protected void SetButtonCheckedState(Visibility visibility)
		{
			var checkedState = GetCheckedState(visibility);
			_buttonView.SetCheckedState(checkedState);
		}

		protected static CheckedState GetCheckedState(Visibility visibility)
		{
			switch (visibility)
			{
				case Visibility.Visible:
					return CheckedState.Checked;
				case Visibility.Invisible:
					return CheckedState.Unchecked;
				default:
					throw new ArgumentOutOfRangeException("visibility");
			}
		}

		protected void ButtonClicked()
		{
			_toggleFormVisibilityModel.ToggleVisibility();
		}
	}

	[TestFixture]
	public class ButtonPresenterFixture
	{
		[Test]
		public void Do()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.StrictMock<IToggleFormVisibilityModel>();

			var presenter = new ButtonPresenter(model);
			presenter.ConnectView(view);

			model.BackToRecord();
			view.BackToRecord();

			With.Mocks(mockRepository)
				.Expecting(delegate
				{
					Expect.Call(model.ToggleVisibility);
				})
				.Verify(delegate
				{
					view.Raise(x => x.Clicked += null);
				});
		}
	}
}