using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Xamarin.Tables
{
	public partial class StringCell : ICell
	{
		static NSString skey = new NSString ("StringElement");
		static NSString skeyvalue = new NSString ("StringElementValue");
		public UITextAlignment Alignment = UITextAlignment.Left;

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (Value == null ? skey : skeyvalue);
			if (cell == null){
				cell = new UITableViewCell (UITableViewCellStyle.Value1, skey);
				cell.SelectionStyle = (Tapped != null) ? UITableViewCellSelectionStyle.Blue : UITableViewCellSelectionStyle.None;
			}
			cell.Accessory = UITableViewCellAccessory.None;
			cell.TextLabel.Text = Caption;
			cell.TextLabel.TextAlignment = Alignment;
			
			// The check is needed because the cell might have been recycled.
			if (cell.DetailTextLabel != null)
				cell.DetailTextLabel.Text = Value == null ? "" : Value;
			
			return cell;
		}
		public override void Selected (UITableView tableView, NSIndexPath indexPath)
		{
			if (Tapped != null)
				Tapped ();
			if(ShouldDeselect)
				tableView.DeselectRow (indexPath, true);
		}
	}
}

