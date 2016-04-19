using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using SimpleTables.Cells;

namespace SimpleTables
{
	public partial class TableViewSectionModel : TableViewModel<Cell>,  IEnumerable, IEnumerable<Section>
	{
		public List<Section> Sections 
		{
			get{return sections;}
			set{
				sections = value;
				ReloadData();
			}
		}
		public Section this [int idx] {
			get {
				return Sections [idx];
			}
		}
		public void Add (Section section)
		{
			if (section == null)
				return;
			
			Sections.Add (section);
//			if (TableView == null)
//				return;
			
			//TableView.InsertSections (MakeIndexSet (Sections.Count-1, 1), UITableViewRowAnimation.None);
		}

		List<Section> sections = new List<Section>();
		
		public event EventHandler<EventArgs<Cell>> RowTapped;
		public event EventHandler<EventArgs<Cell>> RowLongPress;
		
		public event EventHandler<EventArgs<IndexPath>> OnSelection;

		#region implemented abstract members of TableViewModel

		public override int RowsInSection (int section)
		{
			return Sections[(int)section].Count;
		}

		public override ICell GetICell (int section, int position)
		{
			return Sections [section] [position];
		}

		public override int NumberOfSections ()
		{
			return Sections.Count;
		}

		public override int GetItemViewType (int section, int row)
		{
			throw new NotImplementedException ();
		}

		public override string[] SectionIndexTitles ()
		{
			return null;// Sections.Select (x => x.Header).ToArray ();
		}

		public override string HeaderForSection (int section)
		{
			return Sections [section].Header;
		}

		public override void RowSelected (Cell item)
		{
			if (RowTapped != null)
				RowTapped (this, new EventArgs<Cell> (item));
		}
		public override void LongPressOnItem (Cell item)
		{
			if (RowLongPress != null)
				RowLongPress (this, new EventArgs<Cell> (item));
		}

		public override Cell ItemFor (int section, int row)
		{
			return Sections [section] [row];
		}

		#endregion


		#region IEnumerable implementation
		public IEnumerator GetEnumerator ()
		{
			throw new NotImplementedException ();
		}
		#endregion

		#region IEnumerable implementation

		IEnumerator<Section> IEnumerable<Section>.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

