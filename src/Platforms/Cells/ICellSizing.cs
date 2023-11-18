using System;
using UIKit;
using Foundation;

namespace SimpleTables
{
	public interface ICellSizing {
		nfloat GetHeight (UITableView tableView, NSIndexPath indexPath);
	}
}

