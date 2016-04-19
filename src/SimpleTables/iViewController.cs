using System;
#if __IOS__ || __TV__
using UIKit;
#endif

namespace Xamarin.Tables
{
	public interface IViewController
	{
		UINavigationController NavigationController { get;
#if Android
			set;
#endif
		}
		string Title {get;set;}
	}
}

