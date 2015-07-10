using System;
using UIKit;
using CoreGraphics;

namespace Xamarin.Tables
{
	public abstract partial class TableViewModel <T> : UITableViewSource 
	{
		public TableViewModel ()
		{
			
		}
		bool hasBoundLongTouch;
		protected UITableView tv;
		UILongPressGestureRecognizer gesture;
		void bindLongTouch()
		{
			if (hasBoundLongTouch || tv == null)
				return;
			hasBoundLongTouch = true;
			gesture = new UILongPressGestureRecognizer (LongPress){
				ShouldRecognizeSimultaneously = (s,e)=> true,
			};
			tv.AddGestureRecognizer (gesture);
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
				tv.RemoveGestureRecognizer (gesture);
				gesture = null;
				hasBoundLongTouch = false;
				return;
			}

		}
		public void LongPress(UILongPressGestureRecognizer gesture)
		{
			var point = gesture.LocationInView (tv);
			LongPress (point);
		}

		public void LongPress(CGPoint point)
		{
			var indexPath = tv.IndexPathForRowAtPoint (point);
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
			tv = tableView;
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
				return GetViewForHeader (tv, 0) != null;
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
			if (tv != null)
				tv.ReloadData ();
		}

		public virtual void ClearEvents()
		{
			if (gesture != null) {
				tv.RemoveGestureRecognizer (gesture);
				gesture = null;
				hasBoundLongTouch = false;
			}

			tv = null;
			if(CellFor != null)
			foreach (var d in CellFor.GetInvocationList())
				CellFor -= (GetCellEventHandler)d;

			if(CellForHeader != null)
			foreach (var d in CellForHeader.GetInvocationList())
				CellForHeader -= (GetHeaderCellEventHandler)d;

			if(ItemSelected != null)
			foreach (var d in ItemSelected.GetInvocationList())
				ItemSelected -= (EventHandler<EventArgs<T>>)d;

			if(itemLongPress != null)
				foreach (var d in itemLongPress.GetInvocationList())
					ItemLongPressed -= (EventHandler<EventArgs<T>>)d;
		}


	}
}

