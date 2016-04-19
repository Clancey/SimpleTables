using System;
using AppKit;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using SimpleTables.Cells;

namespace SimpleTables
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
		public bool ShowHeaders { get; set; }

		bool isSet;
		protected void SetTable(NSTableView table)
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
				SetupSections ();
			return sectionRows.LastOrDefault ()?.IndexEnd + 1?? 0;
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
			
		protected List<SectionRows> sectionRows = new List<SectionRows> ();

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

		public ICell GetICell (nint row)
		{
			foreach (var s in sectionRows) {
				if (s.Contains (row)) {
					if (ShowHeaders && s.IndexStart == row)
						return  GetHeaderICell (s.Section) ?? new StringCell(HeaderForSection(s.Section));
					return GetICell(s.Section,s.GetSectionRow(row));
				}

			}
			return null;
		}

		public T GetItem(nint row)
		{
			foreach (var s in sectionRows) {
				if (s.Contains (row)) {
					return ItemFor(s.Section,s.GetSectionRow(row));
				}
			}
			return default(T);
		}

		protected class SectionRows
		{
			public bool IncludesHeader { get; set; }
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
				int realRow = (int)(row - IndexStart - (IncludesHeader ? 1 : 0));
				return realRow;
			}


		}

		public void BeginAnimation ()
		{

		}

		public void EndAnimation ()
		{

		}

		void SetupSections()
		{
			sectionRows.Clear ();
			var sections = NumberOfSections ();
			var index = 0;

			foreach (var s in Enumerable.Range(0,sections).ToList()) {
				var rows = RowsInSection (s);
				if (rows == 0)
					continue;
				var section = new SectionRows {
					IncludesHeader = ShowHeaders,
					IndexStart = index,
					Rows = rows,
					Section = s,
					IndexEnd = index + rows - (ShowHeaders ? 0 : 1),
				};
				index += rows;
				if(ShowHeaders)
					index++;
				sectionRows.Add (section);
			}
		}

		public void ReloadData ()
		{
			SetupSections ();
			TableView?.ReloadData ();
		}

		protected virtual void ClearNativeEvents ()
		{
			TableView = null;
		}

	}
}

