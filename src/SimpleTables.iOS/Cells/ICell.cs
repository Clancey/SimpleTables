using System;

namespace SimpleTables
{
	public partial interface ICell
	{
		UIKit.UITableViewCell GetCell (UIKit.UITableView tv);
	}
}

