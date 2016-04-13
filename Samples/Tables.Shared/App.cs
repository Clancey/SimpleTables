using System;
using System.Threading;

namespace Tables.Sample
{
	public static class App
	{
		public static Action<Action> Invoker { get; set; }
		static Thread MainThread;
		public static void Init ()
		{
			MainThread = Thread.CurrentThread;
			RegisterCells ();
		}

		static void RegisterCells ()
		{
			CellRegistrar.Register<Item, ItemCell> ();
		}

		public static void EnsureInvokeOnMainThread (Action action)
		{
			if (MainThread == Thread.CurrentThread)
				action ();
			else
				Invoker (action);
		}
	}
}

