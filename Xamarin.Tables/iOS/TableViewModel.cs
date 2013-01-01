using System;
using MonoTouch.UIKit;

namespace Xamarin.Tables
{
	public abstract partial class TableViewModel <T> : UITableViewSource 
	{
		public override int RowsInSection (UITableView tableview, int section)
		{
			return RowsInSection (section);
		}
		public override int NumberOfSections (UITableView tableView)
		{
			return NumberOfSections ();
		}
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			return GetICell (indexPath.Section, indexPath.Row).GetCell(tableView);
		}
		public override void RowSelected (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var item = ItemFor (indexPath.Section, indexPath.Row);
			if (item is Cell)
				(item as Cell).Selected (tableView, indexPath);
			RowSelected (item);
		}	
		
		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return SectionIndexTitles ();
		}
		
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return HeaderForSection(section);
		}
		public override UIView GetViewForHeader (UITableView tableView, int section)
		{
			var header = GetHeaderICell (section);
			if (header == null)
				return null;
			return header.GetCell(tableView);
		}
	}
}

