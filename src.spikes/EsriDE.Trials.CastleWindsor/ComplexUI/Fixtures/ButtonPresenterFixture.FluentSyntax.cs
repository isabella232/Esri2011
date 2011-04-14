using EsriDE.Trials.CastleWindsor.ComplexUI.AA;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.Buttons;
using EsriDE.Trials.CastleWindsor.ComplexUI.Contracts.DomainModel;
using EsriDE.Trials.CastleWindsor.ComplexUI.Implementations.Buttons;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace EsriDE.Trials.CastleWindsor.ComplexUI.Fixtures
{
	[TestFixture]
	public partial class ButtonPresenterFixture
	{
		[Test]
		public void ConnectingView()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.Stub<IToggleModel>();

			var presenter = new ButtonPresenter(model);

			With.Mocks(mockRepository)
				.Expecting(delegate { Expect.Call(() => view.Clicked += null).IgnoreArguments(); })
				.Verify(delegate { presenter.ConnectView(view); });
		}

		[Test]
		public void ConstructingConnectingHandlingWorks_Fluent2()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.DynamicMock<IButtonView>();
			var model = mockRepository.DynamicMock<IToggleModel>();

			IEventRaiser clicked = null;

			With.Mocks(mockRepository)
				.Expecting(delegate
				           	{
				           		Expect.Call(() => model.VisibilityStateChanged += null).IgnoreArguments();

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
		public void ConstructingConnectingHandling_WithRaisingTwoEvents_Works_Fluent()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.StrictMock<IToggleModel>();

			IEventRaiser clicked = null;
			IEventRaiser visibilityChanged = null;

			With.Mocks(mockRepository)
				.Expecting(delegate
				           	{
				           		visibilityChanged =
				           			Expect.Call(() => model.VisibilityStateChanged += null).IgnoreArguments().GetEventRaiser();
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
				        		visibilityChanged.Raise(VisibilityState.Visible);
				        	});
		}

		[Test]
		public void ConstructorConnects_Fluent()
		{
			var mockRepository = new MockRepository();
			var model = mockRepository.StrictMock<IToggleModel>();

			With.Mocks(mockRepository)
				.Expecting(delegate { Expect.Call(() => model.VisibilityStateChanged += null).IgnoreArguments(); })
				.Verify(delegate { new ButtonPresenter(model); });
		}
	}
}