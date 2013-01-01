using System;

namespace Xamarin.Tables
{
	public partial interface ICell
	{
		MonoTouch.UIKit.UITableViewCell GetCell (MonoTouch.UIKit.UITableView tv);
	}
}

