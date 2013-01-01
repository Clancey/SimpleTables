using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Xamarin.Tables
{
	public partial class Cell : ICell
	{
		const string Key = "defaultCell";
		public virtual UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (Key) ?? new UITableViewCell (UITableViewCellStyle.Default, Key);
			return cell ;
		}
		public virtual void Selected (UITableView tableView, NSIndexPath path)
		{
		}
	}
}

