using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.Windsor;
using EsriDE.Samples.ContentFinder.UI.Contract;

namespace EsriDE.Samples.ContentFinder.LegacyAgdAdapter
{
	public class Builder
	{
		private IWindsorContainer _container;
		private IToggleablePresenter _toggleablePresenter;

		public Builder(IShifterView view, int parentWindowHandle)
		{
			ConfigureIocContainer();

			ConnectShifterPresenterTo(view);
			ConnectMeAsModelObserver();

			var v = _container.Resolve<IWindowInformation>();
			v.WindowHandle = parentWindowHandle;
		}

		~Builder()
		{
			Console.WriteLine("Builder.~");
		}

		private void ConnectMeAsModelObserver()
		{
			var model = _container.Resolve<IShifterModel>();
			model.ShifterStateChanged += ManageFormSubsystem;
		}

		private void ManageFormSubsystem(ShifterState state)
		{
			switch (state)
			{
				case ShifterState.On:
					ConstructSystem();
					break;
				case ShifterState.Off:
					DestroySystem();
					break;
				default:
					throw new ArgumentOutOfRangeException("state");
			}
		}

		private void ConstructSystem()
		{
			Console.WriteLine("Construct system");
			_toggleablePresenter = _container.Resolve<IToggleablePresenter>();
			//var formVisibilityModel = _container.Resolve<IToggleModel>();
			//_toggleablePresenter.SetModel(formVisibilityModel);
		}

		private void DestroySystem()
		{
			Console.WriteLine("Destroy system");
			//_toggleablePresenter.UnsetModel();
			//_container.Release(_toggleablePresenter);
			_toggleablePresenter = null;
		}

		private void ConfigureIocContainer()
		{
			try
			{
				var filename = GetFullName("CastleWindsor.config");
				_container = new WindsorContainer(filename);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void ConnectShifterPresenterTo(IShifterView view)
		{
			var buttonPresenter = _container.Resolve<IShifterPresenter>();
			buttonPresenter.ConnectView(view);
		}

		private string GetFullName(string filename)
		{
			//Assembly callingAssembly = AssemblyUtil.GetCallingAssembly();
			//string assemlblyLocation = callingAssembly.Location;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string assemlblyLocation = executingAssembly.Location;
			string assemlblyPath = (Directory.GetParent(assemlblyLocation)).FullName;
			return Path.Combine(assemlblyPath, filename);
		}
	}

	internal static class AssemblyUtil
	{
		internal static Assembly GetCallingAssembly()
		{
			var thisAssembly = Assembly.GetExecutingAssembly();

			var stackTrace = new StackTrace(false);
			var stackframes = stackTrace.GetFrames();
			if (null == stackframes)
			{
				return null;
			}

			return (from stackFrame in stackTrace.GetFrames()
			        select stackFrame.GetMethod()
			        into methodBase
			        where
			        	null != methodBase &&
			        	thisAssembly != methodBase.DeclaringType.Assembly
			        select methodBase.DeclaringType.Assembly).FirstOrDefault();
		}
	}

	//public class Activator : IComponentActivator
	//{
	//    public object Create(CreationContext context)
	//    {
	//        context.
	//        throw new NotImplementedException();
	//    }

	//    public void Destroy(object instance)
	//    {
	//        throw new NotImplementedException();
	//    }
	//}
}