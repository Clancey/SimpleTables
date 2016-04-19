using System;
using UIKit;
using CoreGraphics;
using SimpleTables.Cells;

namespace SimpleTables
{
	public abstract partial class TableViewModel <T> : UITableViewSource 
	{
		public TableViewModel ()
		{
			
		}
		bool hasBoundLongTouch;
		WeakReference tableView;

		protected UITableView TableView {
			get {
				return tableView?.Target as UITableView;
			}

			set {
				tableView = new WeakReference(value);
			}
		}

		UILongPressGestureRecognizer gesture;
		void bindLongTouch()
		{
			if (hasBoundLongTouch || TableView == null)
				return;
			hasBoundLongTouch = true;
			gesture = new UILongPressGestureRecognizer (LongPress){
				ShouldRecognizeSimultaneously = (s,e)=> true,
			};
			TableView.AddGestureRecognizer (gesture);
		}
		bool shouldBind;
		void updateLongPress()
		{
			var count = itemLongPress == null ? 0 : itemLongPress.GetInvocationList ().Length;
			if (count == 1 && hasBoundLongTouch)
				return;
			if (count == 1) {
				shouldBind = true;
				bindLongTouch ();
			}
			if (count == 0 && hasBoundLongTouch) {
				TableView.RemoveGestureRecognizer (gesture);
				gesture = null;
				hasBoundLongTouch = false;
				return;
			}

		}
		public void LongPress(UILongPressGestureRecognizer gesture)
		{
			var point = gesture.LocationInView (TableView);
			LongPress (point);
		}

		public void LongPress(CGPoint point)
		{
			var indexPath = TableView.IndexPathForRowAtPoint (point);
			if (indexPath == null)
				return;
			LongPressOnItem (ItemFor(indexPath.Section,indexPath.Row));
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return (nint)RowsInSection ((int)section);
		}
		public override nint NumberOfSections (UITableView tableView)
		{
			TableView = tableView;
			updateLongPress ();
			return NumberOfSections ();
		}
		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var icell = GetICell (indexPath.Section, indexPath.Row);
			if(icell == null)
				return null;
			return icell.GetCell(tableView);
		}
		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var item = ItemFor (indexPath.Section, indexPath.Row);
			if (item is Cell)
				(item as Cell).Selected (tableView, indexPath);
			tableView.DeselectRow (indexPath, true);
			RowSelected (item);
		}	
		
		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return SectionIndexTitles ();
		}
		
		
		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return HeaderForSection((int)section);
		}

		public override bool RespondsToSelector (ObjCRuntime.Selector sel)
		{
			if (sel.Name == "tableView:viewForHeaderInSection:") {
				return GetViewForHeader (TableView, 0) != null;
			}
			return base.RespondsToSelector (sel);

		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			var header = GetHeaderICell ((int)section);
			if (header == null)
				return null;
			return header.GetCell(tableView);
		}

		public void ReloadData()
		{
			if (TableView != null)
				TableView.ReloadData ();
		}

		protected virtual void ClearNativeEvents ()
		{

			if (gesture != null) {
				TableView.RemoveGestureRecognizer (gesture);
				gesture = null;
				hasBoundLongTouch = false;
			}

			TableView = null;
		}
	}
}

