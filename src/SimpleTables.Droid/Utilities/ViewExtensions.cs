using System;
using Android.Views;

namespace SimpleTables
{
	public static class ViewExtensions
	{
		public static void RemoveFromParent (this View view)
		{
			((ViewGroup)view.Parent).RemoveView (view);
		}
	}
}

