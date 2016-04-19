using System;
using UIKit;
using SimpleTables;
namespace Tables.Sample
{
	public class ItemCell : ICell, IBindingContext
	{
		WeakReference bindingContext;

		public object BindingContext {
			get { return bindingContext?.Target; }

			set { bindingContext = new WeakReference (value); }
		}

		public UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell ("ItemCell") ?? new UITableViewCell (UITableViewCellStyle.Subtitle, "ItemCell");
			var item = BindingContext as Item;
			cell.TextLabel.Text = item.Title;
			cell.DetailTextLabel.Text = item.Details;
			return cell;
		}
	}
}

