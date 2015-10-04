using System;
using Foundation;
using UIKit;

namespace Xamarin.Tables
{
	public partial class StringCell : ICell
	{
		static NSString skey = new NSString ("StringElement");
		static NSString skeyvalue = new NSString ("StringElementValue");
		public UITextAlignment Alignment = UITextAlignment.Left;

		public override UITableViewCell GetCell (UITableView tv)
		{
			var key = Value == null ? skey : skeyvalue;
			var cell = tv.DequeueReusableCell (key);
			if (cell == null){
				cell = new UITableViewCell (Value == null ? UITableViewCellStyle.Default : UITableViewCellStyle.Value1, key);
				cell.SelectionStyle = (Tapped != null) ? UITableViewCellSelectionStyle.Blue : UITableViewCellSelectionStyle.None;
			}
			cell.Accessory = UITableViewCellAccessory.None;
			cell.TextLabel.Text = Caption;
			cell.TextLabel.TextAlignment = Alignment;
			cell.TextLabel.BackgroundColor = UIColor.Clear;

			
			// The check is needed because the cell might have been recycled.
			if (cell.DetailTextLabel != null)
				cell.DetailTextLabel.Text = Value == null ? "" : Value;
			
			return cell;
		}
		public override void Selected (UITableView tableView, NSIndexPath indexPath)
		{
			if(ShouldDeselect)
				tableView.DeselectRow (indexPath, true);
			base.Selected(tableView, indexPath);
		}
	}
}

