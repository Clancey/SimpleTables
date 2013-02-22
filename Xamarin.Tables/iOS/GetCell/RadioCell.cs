using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Xamarin.Tables
{
	public partial class RadioCell
	{
		
		static NSString skeyvalue = new NSString ("StringElementValue");
		public override MonoTouch.UIKit.UITableViewCell GetCell (MonoTouch.UIKit.UITableView tv)
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
		public override void Selected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);
		}
	}
}

