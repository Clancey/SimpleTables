using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Xamarin.Tables
{
	public interface ICellSizing {
		float GetHeight (UITableView tableView, NSIndexPath indexPath);
	}
}

