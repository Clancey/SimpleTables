using System;
#if iOS
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

