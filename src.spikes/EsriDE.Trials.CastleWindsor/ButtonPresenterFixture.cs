using Moq;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Impl;
using Rhino.Mocks.Interfaces;
using MockRepository = Rhino.Mocks.MockRepository;

namespace EsriDE.Trials.CastleWindsor
{
	[TestFixture]
	public partial class ButtonPresenterFixture
	{

		[Test]
		public void Do3Fluent_A()
		{
			var mockRepository = new MockRepository();
			var view = mockRepository.StrictMock<IButtonView>();
			var model = mockRepository.DynamicMock<IToggleFormVisibilityModel>();

			view.Clicked += null;
			IEventRaiser clicked = LastCall.GetEventRaiser();

			var presenter = new ButtonPresenter(model);
			presenter.ConnectView(view);

			clicked = Expect.Call(() => view.Clicked += null).IgnoreArguments().GetEventRaiser();

			view.BackToRecord();
			model.BackToRecord();

			With.Mocks(mockRepository)
				.Expecting(delegate
				{
					Expect.Call(model.ToggleVisibility);
					//Expect.Call(() => model.VisibilityChanged += null).IgnoreArguments();
					//Expect.Call(() => view.SetCheckedState(CheckedState.Checked));
				})
				.Verify(delegate
				{
					model.ToggleVisibility();
					model.ToggleVisibility();
					//events.RaiseEvent(Visibility.Visible);
					clicked.Raise();
				});
		}


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
				.Expecting(delegate
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

			//IEventRaiser raiser = new EventRaiser((IMockedObject)view, "Clicked");
			//onCreateFeatureEventRaiser.Raise(_objectMock);

			var presenter = new ButtonPresenter(model);
			presenter.ConnectView(view);
			//mockRepository.BackToRecordAll();

			//productDetailView.Raise(view => view.EditClick += null).Raise(sender, EventArgs.Empty);

			view.BackToRecord();
			model.BackToRecord();

			using (mockRepository.Record())
			{
				var v = Expect.Call(() => model.VisibilityChanged += null).IgnoreArguments();

				Expect.Call(model.ToggleVisibility);
				//Expect.Call(() => view.SetCheckedState(CheckedState.Checked));
			}

			using (mockRepository.Playback())
			{
				//raiser.Raise();
				//view.Raise(v => v.Clicked += null);
				//model.Raise(m => m.VisibilityChanged += null).Raise(Visibility.Visible);
				clicked.Raise();
				//visibilityChanged.Raise(Visibility.Visible);
			}
		}

		public delegate void MyEventHandler();
		public interface IFoo
		{
			event MyEventHandler MyEvent;
		}

		
		
	}
}