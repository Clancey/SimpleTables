using System;
using Android.Views;

namespace Xamarin.Tables
{
	public static class ViewExtensions
	{
		public static void RemoveFromParent (this View view)
		{
			((ViewGroup)view.Parent).RemoveView (view);
		}
	}
}

