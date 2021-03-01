using System;
using System.Threading;
using System.Windows.Forms;
using ThreadAbort;
using Timer = System.Threading.Timer;

namespace LoadingIndicator.WinForms
{
	internal static class SelectControlExtensions2
	{
		private const string AbortReason = "Deadlock in Control.Select detected. Raised ThreadAbort to exit from deadlock.";


		// Try to avoid deadlock on Select method
		public static void SafeSelect(int sleep)
		{
			// using Dispose to call ResetAbort always, if it was requested, even if no exception is thrown yet
			using (var threadWrapper = new ThreadWrapper(Thread.CurrentThread))
			{
				try
				{
					using (CreateTimer(threadWrapper))
					{
						//TimeSlice t = TimeSlice.Start(sleep);
						//while (!t.HasExpired)
						//{

						//}
						Thread.Sleep(sleep);
					}
				}
			catch (ThreadAbortException ex)
			{
				threadWrapper.ResetAbort();
			}
		}

	}

		private static IDisposable CreateTimer(ThreadWrapper threadWrapper)
		{

			return new Timer(CancelSelect, threadWrapper, TimeSpan.FromMilliseconds(50), Timeout.InfiniteTimeSpan);
		}

		private static void CancelSelect(object state)
		{
			var threadWrapper = (ThreadWrapper)state;

			threadWrapper.AbortThread();
		}

		private class ThreadWrapper : IDisposable
		{
			private readonly Thread _thread;

			public ThreadWrapper(Thread thread)
			{
				_thread = thread;
			}

			public void AbortThread()
			{
				if (_thread.ThreadState != ThreadState.AbortRequested)
				{
					_thread.Abort(AbortReason);
				}
			}

			public void ResetAbort()
			{
				if (_thread == Thread.CurrentThread  && _thread.ThreadState == ThreadState.AbortRequested)
				{
					Thread.ResetAbort();
				}
			}

			public void Dispose()
			{
				ResetAbort();
			}
		}
	}
}