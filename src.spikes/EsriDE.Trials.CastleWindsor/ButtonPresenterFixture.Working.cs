using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace EsriDE.Trials.CastleWindsor
{
	[TestFixture]
	public partial class ButtonPresenterFixture
	{
		[Test]
		public void ConstructorConnects()
		{
			var mockRepository = new MockRepository();
			var model = mockRepository.StrictMock<IToggleFormVisibilityModel>();

			With.Mocks(mockRepository)
				.Expecting(delegate
				{
					Expect.Call(() => model.VisibilityChanged += null).IgnoreArguments();

				})
				.Verify(delegate
				{
					var presenter = new ButtonPresenter(model);
				});
		}

		[Test]
		public void ConstructingConnectingHandlingWorks_Fluent2()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.DynamicMock<IButtonView>();
			var model = mockRepository.DynamicMock<IToggleFormVisibilityModel>();

			IEventRaiser clicked = null;
			With.Mocks(mockRepository)
				.Expecting(delegate
				{
					Expect.Call(() => model.VisibilityChanged += null).IgnoreArguments();

					clicked = Expect.Call(() => view.Clicked += null).IgnoreArguments().GetEventRaiser();
					Expect.Call(model.ToggleVisibility);
				})
				.Verify(delegate
				{
					var presenter = new ButtonPresenter(model);
					presenter.ConnectView(view);
					clicked.Raise();
				});
		}

		[Test]
		public void ConstructingConnectingHandlingWorks_Fluent()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.StrictMock<IToggleFormVisibilityModel>();

			IEventRaiser clicked = null;
			IEventRaiser visibilityChanged = null;

			With.Mocks(mockRepository)
				.Expecting(delegate
				{
					visibilityChanged = Expect.Call(() => model.VisibilityChanged += null).IgnoreArguments().GetEventRaiser();
					clicked = Expect.Call(() => view.Clicked += null).IgnoreArguments().GetEventRaiser();

					Expect.Call(model.ToggleVisibility);
					Expect.Call(() => view.SetCheckedState(CheckedState.Checked));
				});

			With.Mocks(mockRepository)
				.Verify(delegate
				{
					var presenter = new ButtonPresenter(model);
					presenter.ConnectView(view);

					clicked.Raise();
					visibilityChanged.Raise(Visibility.Visible);
				});
		}

		[Test]
		public void ConnectingView()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.Stub<IToggleFormVisibilityModel>();

			var presenter = new ButtonPresenter(model);

			With.Mocks(mockRepository)
				.Expecting(delegate
				{
					Expect.Call(() => view.Clicked += null).IgnoreArguments();
				})
				.Verify(delegate
				{
					presenter.ConnectView(view);
				});
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
		public void DoClassic()
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
}