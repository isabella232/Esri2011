using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Impl;
using Rhino.Mocks.Interfaces;

namespace EsriDE.Trials.CastleWindsor
{
	[TestFixture]
	public partial class ButtonPresenterFixture
	{
		
		[Test]
		public void Do3Fluent()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.StrictMock<IToggleFormVisibilityModel>();

			var presenter = new ButtonPresenter(model);
			presenter.ConnectView(view);

			//IEventRaiser clicked = null;
			//view.Clicked += null;
			//clicked = LastCall.GetEventRaiser();

			IEventSubscriber subscriber = mockRepository.StrictMock<IEventSubscriber>();
			IWithEvents events = new WithEvents();
			// This doesn't create an expectation because no method is called on subscriber!! 
			events.Blah += subscriber.Handler;
			subscriber.Handler(Visibility.Visible); 


			With.Mocks(mockRepository)
				.ExpectingInSameOrder(delegate
				{
					Expect.Call(model.ToggleVisibility);
					Expect.Call(() => model.VisibilityChanged += null).IgnoreArguments();
					Expect.Call(() => view.SetCheckedState(CheckedState.Checked));
				})
				.Verify(delegate
				{
					events.RaiseEvent(Visibility.Visible);
					//clicked.Raise();
				});
		}

		[Test]
		public void Do3Classic()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.StrictMock<IToggleFormVisibilityModel>();

			model.VisibilityChanged += null;
			IEventRaiser visibilityChanged = LastCall.GetEventRaiser();

			view.Clicked += null;
			IEventRaiser clicked = LastCall.GetEventRaiser();

			var presenter = new ButtonPresenter(model);
			presenter.ConnectView(view);

			mockRepository.BackToRecordAll();
			//mockRepository.Playback();

			Expect.Call(model.ToggleVisibility);
			Expect.Call(() => model.VisibilityChanged += null).IgnoreArguments();
			Expect.Call(() => view.SetCheckedState(CheckedState.Checked));

			mockRepository.ReplayAll();

			clicked.Raise();
			visibilityChanged.Raise(Visibility.Visible);

			mockRepository.VerifyAll();
		}

		[Test]
		public void Do3Using()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.StrictMock<IToggleFormVisibilityModel>();

			model.VisibilityChanged += null;
			IEventRaiser visibilityChanged = LastCall.GetEventRaiser();

			view.Clicked += null;
			IEventRaiser clicked = LastCall.GetEventRaiser();

			var presenter = new ButtonPresenter(model);
			presenter.ConnectView(view);
			mockRepository.BackToRecordAll();

			using (mockRepository.Record())
			{
				Expect.Call(model.ToggleVisibility);
				Expect.Call(() => model.VisibilityChanged += null).IgnoreArguments();
				Expect.Call(() => view.SetCheckedState(CheckedState.Checked));
			}

			using (mockRepository.Playback())
			{
				clicked.Raise();
				visibilityChanged.Raise(Visibility.Visible);
			}
		}

		[Test]
		public void Do3()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.StrictMock<IToggleFormVisibilityModel>();

			var presenter = new ButtonPresenter(model);
			presenter.ConnectView(view);

			model.VisibilityChanged += null;
			IEventRaiser visibilityChanged = LastCall.GetEventRaiser();

			view.Clicked += null;
			IEventRaiser clicked = LastCall.GetEventRaiser();

			With.Mocks(mockRepository)
				.Expecting(delegate
				{
					Expect.Call(model.ToggleVisibility);
					Expect.Call(() => view.SetCheckedState(CheckedState.Checked));
				})
				.Verify(delegate
				{
					clicked.Raise();
					visibilityChanged.Raise(Visibility.Visible);
				});
		}
		
	}
}