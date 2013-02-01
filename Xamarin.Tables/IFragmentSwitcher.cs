using System;
using Android.App;

namespace Xamarin.Tables
{
	public interface IFragmentSwitcher
	{
		void SwitchContent (Fragment fragment, bool animated, bool removed = false);
	}
}

