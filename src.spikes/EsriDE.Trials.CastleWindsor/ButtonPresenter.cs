using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace EsriDE.Trials.CastleWindsor
{
	public class ButtonPresenterForTest : ButtonPresenter
	{
		public ButtonPresenterForTest(IToggleFormVisibilityModel toggleFormVisibility) : base(toggleFormVisibility)
		{
		}

		public void EmulateEventing(Visibility visibility)
		{
			SetButtonCheckedState(visibility);
		}
	}

	[TestFixture]
	public class ButtonPresenterFixture
	{
		[Test]
		public void DoUsing()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.Stub<IToggleFormVisibilityModel>();

			var presenter = new ButtonPresenterForTest(model);

			using (mockRepository.Record())
			{
				Expect.Call(() => view.Clicked += null).IgnoreArguments();
				Expect.Call(() => view.SetCheckedState(CheckedState.Checked)).IgnoreArguments();
			}

			using (mockRepository.Playback())
			{
				presenter.ConnectView(view);
				presenter.EmulateEventing(Visibility.Visible);
			}
		}

		[Test]
		public void DoFluent()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.Stub<IToggleFormVisibilityModel>();

			var presenter = new ButtonPresenterForTest(model);

			With.Mocks(mockRepository)
				.Expecting(delegate
				           	{
								Expect.Call(() => view.Clicked += null).IgnoreArguments();
								Expect.Call(() => view.SetCheckedState(CheckedState.Checked)).IgnoreArguments();
				           	})
				.Verify(delegate
				        	{
								presenter.ConnectView(view);
								presenter.EmulateEventing(Visibility.Visible);
				        	});
		}

		[Test]
		public void Do()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.Stub<IToggleFormVisibilityModel>();

			var presenter = new ButtonPresenterForTest(model);
			presenter.ConnectView(view);

			mockRepository.Playback();

			Expect.Call(() => view.SetCheckedState(CheckedState.Checked)).IgnoreArguments();

			mockRepository.ReplayAll();
			
			presenter.EmulateEventing(Visibility.Visible);

			mockRepository.VerifyAll();

		}
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
}