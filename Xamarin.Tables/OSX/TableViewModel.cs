using System;
using AppKit;
using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace Xamarin.Tables
{
	public partial class TableViewModel <T> : NSTableViewSource
	{
		WeakReference _tableView;
		public NSTableView TableView
		{
			get{ return _tableView?.Target as NSTableView; }
			set{ 
				_tableView = new WeakReference (value);
				isSet = _tableView != null;
			}
		}
		bool isSet;
		void SetTable(NSTableView table)
		{
			if (isSet)
				return;
			TableView = table;
		}
		public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
		{
			SetTable (tableView);
			var icell = GetICell (row);
			return icell.GetCell (tableView, tableColumn, this);

		}
		public override Foundation.NSObject GetObjectValue (NSTableView tableView, NSTableColumn tableColumn, nint row)
		{
			SetTable (tableView);
			var item = GetICell (row)?.GetCellText(tableColumn) ?? "";
			return new NSString (item);
		}
		public override nint GetRowCount (NSTableView tableView)
		{
			SetTable (tableView);
			if (sectionRows.Count == 0)
				ReloadData ();
			return sectionRows.LastOrDefault ()?.IndexEnd ?? 0;
		}

		public override void SelectionDidChange (NSNotification notification)
		{
			var table = TableView;
			if (table == null)
				return;
			var row = table.SelectedRow;
			var item = GetItem (row);
			RowSelected (item);
		}

		List<SectionRows> sectionRows = new List<SectionRows> ();

		void updateLongPress ()
		{
//			var count = itemLongPress == null ? 0 : itemLongPress.GetInvocationList ().Length;
//			if (count == 1 && hasBoundLongTouch)
//				return;
//			if (count == 1) {
//				shouldBind = true;
//				bindLongTouch ();
//			}
//			if (count == 0 && hasBoundLongTouch) {
//				tv.RemoveGestureRecognizer (gesture);
//				gesture = null;
//				hasBoundLongTouch = false;
//				return;
//			}

		}

		ICell GetICell (nint row)
		{
			foreach (var s in sectionRows) {
				if (s.Contains (row)) {
					return GetICell(s.Section,s.GetSectionRow(row));
				}

			}
			return null;
		}

		T GetItem(nint row)
		{
			foreach (var s in sectionRows) {
				if (s.Contains (row)) {
					return ItemFor(s.Section,s.GetSectionRow(row));
				}
			}
			return default(T);
		}

		class SectionRows
		{
			public int Section { get; set; }

			public int Rows { get; set; }

			public int IndexStart { get; set; }

			public int IndexEnd { get; set; }

			public bool Contains(long index)
			{
				var contains = IndexEnd >= index &&  IndexStart <= index;
				return contains;
			}

			public int GetSectionRow(nint row)
			{
				int realRow = (int)(row - IndexStart);
				return realRow;
			}


		}

		public void BeginAnimation ()
		{

		}

		public void EndAnimation ()
		{

		}

		public void ReloadData ()
		{
			sectionRows.Clear ();
			var sections = NumberOfSections ();
			var index = 0;

			foreach (var s in Enumerable.Range(0,sections).ToList()) {
				var rows = RowsInSection (s);
				if (rows == 0)
					continue;
				var section = new SectionRows {
					IndexStart = index,
					Rows = rows,
					Section = s,
					IndexEnd = index + rows -1,
				};
				index += rows;
				sectionRows.Add (section);
			}
		}

		public virtual void ClearEvents()
		{
			
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

