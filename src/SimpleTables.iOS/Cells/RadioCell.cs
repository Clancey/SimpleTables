using System;
using UIKit;
using Foundation;

namespace SimpleTables.Cells
{
	public partial class RadioCell
	{
		
		static NSString skeyvalue = new NSString ("StringElementValue");
		public override UIKit.UITableViewCell GetCell (UIKit.UITableView tv)
		{
			if(selectedIndex < Options.Length)
				Detail = Options[selectedIndex].Value;

			var cell = tv.DequeueReusableCell (skeyvalue);
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Value1, skeyvalue);
				cell.SelectionStyle = UITableViewCellSelectionStyle.Blue;
			}
			cell.Accessory = UITableViewCellAccessory.None;
			cell.TextLabel.Text = Caption;
			cell.TextLabel.BackgroundColor = UIColor.Clear;

			
			// The check is needed because the cell might have been recycled.
			if (cell.DetailTextLabel != null)
				cell.DetailTextLabel.Text =  Detail;
			
			return cell;
		}
		public override void Selected (UITableView tableView, NSIndexPath path)
		{
			tableView.DeselectRow (path, true);
		}
	}
}

