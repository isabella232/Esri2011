using System;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace EsriDE.Trials.EmbeddingManager
{
	public partial class FixtureCastle
	{
		private int TimerInterval;

		[Test]
		public void Do_Variante_1()
		{
			var container = new WindsorContainer();

			container.Register(Component
								.For<IWorker>()
								.ImplementedBy<TimedWorker>()
								.DependsOn(new { timerInterval = TimerInterval })
								.LifeStyle.Transient);

			container.Register(Component.For<Action>()
								.Instance(() => Resolve<SomeService>().SomeMethod())
								.LifeStyle.Transient);

			container.Register(Component.For<ILogger>()
								.ImplementedBy<TraceLogProvider>()
								.LifeStyle.Singleton);
		}

		[Test]
		public void Do_Variante_2_WithDynamicParameters()
		{
			var container = new WindsorContainer();

			container.Register(Component
								.For<IWorker>()
								.ImplementedBy<TimedWorker>()
								.DynamicParameters((kernel, parameters) =>
								{
									parameters["timerInterval"] = TimerInterval;
									parameters["execute"] = new Action(kernel.Resolve<SomeService>().SomeMethod);
								})
								.LifeStyle.Transient);
		}

		private interface IWorker
		{
		}

		private SomeService Resolve<U>()
		{
			return new SomeService();
		}

		internal class SomeService
		{
			internal void SomeMethod()
			{
			}
		}

		private class TimedWorker : IWorker
		{
			public TimedWorker(int timerInterval, Action execute, ILogger logger)
			{
			}
		}

		private class TraceLogProvider : ILogger
		{
			#region ILogger Members
			public void Debug(string message)
			{
				throw new NotImplementedException();
			}

			public void Debug(string message, Exception exception)
			{
				throw new NotImplementedException();
			}

			public void Debug(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void DebugFormat(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void DebugFormat(Exception exception, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void Info(string message)
			{
				throw new NotImplementedException();
			}

			public void Info(string message, Exception exception)
			{
				throw new NotImplementedException();
			}

			public void Info(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void InfoFormat(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void InfoFormat(Exception exception, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void Warn(string message)
			{
				throw new NotImplementedException();
			}

			public void Warn(string message, Exception exception)
			{
				throw new NotImplementedException();
			}

			public void Warn(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void WarnFormat(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void WarnFormat(Exception exception, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void Error(string message)
			{
				throw new NotImplementedException();
			}

			public void Error(string message, Exception exception)
			{
				throw new NotImplementedException();
			}

			public void Error(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void ErrorFormat(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void ErrorFormat(Exception exception, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void Fatal(string message)
			{
				throw new NotImplementedException();
			}

			public void Fatal(string message, Exception exception)
			{
				throw new NotImplementedException();
			}

			public void Fatal(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void FatalFormat(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void FatalFormat(Exception exception, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public void FatalError(string message)
			{
				throw new NotImplementedException();
			}

			public void FatalError(string message, Exception exception)
			{
				throw new NotImplementedException();
			}

			public void FatalError(string format, params object[] args)
			{
				throw new NotImplementedException();
			}

			public ILogger CreateChildLogger(string loggerName)
			{
				throw new NotImplementedException();
			}

			public bool IsDebugEnabled
			{
				get { throw new NotImplementedException(); }
			}

			public bool IsInfoEnabled
			{
				get { throw new NotImplementedException(); }
			}

			public bool IsWarnEnabled
			{
				get { throw new NotImplementedException(); }
			}

			public bool IsErrorEnabled
			{
				get { throw new NotImplementedException(); }
			}

			public bool IsFatalEnabled
			{
				get { throw new NotImplementedException(); }
			}

			public bool IsFatalErrorEnabled
			{
				get { throw new NotImplementedException(); }
			}
			#endregion
		}

		
	}
}