using System;
using UIKit;
using Foundation;

namespace SimpleTables.Cells
{
	public partial class Cell : ICell
	{
		const string Key = "defaultCell";
		public virtual UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (Key) ?? new UITableViewCell (UITableViewCellStyle.Default, Key);
			cell.TextLabel.Text = this.Caption;
			return cell ;
		}
		public virtual void Selected (UITableView tableView, NSIndexPath path)
		{
			Selected();
		}

		protected virtual void NativeDispose ()
		{

		}
	}
}

