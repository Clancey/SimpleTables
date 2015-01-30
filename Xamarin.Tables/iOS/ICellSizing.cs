using System;
using UIKit;
using Foundation;

namespace Xamarin.Tables
{
	public interface ICellSizing {
		nfloat GetHeight (UITableView tableView, NSIndexPath indexPath);
	}
}

