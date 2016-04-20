using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using SimpleTables.Cells;
using System.Collections.ObjectModel;

namespace SimpleTables
{
	public partial class TableViewSectionModel : TableViewModel<Cells.Cell>,  IEnumerable, IEnumerable<Section>
	{
		public TableViewSectionModel (List<Section> sections)
		{
			Sections = sections ?? new List<Section> ();
		}

		public TableViewSectionModel ()
		{
		}


		List<Section> sections = new List<Section> ();
		public List<Section> Sections 
		{
			get{return sections;}
			set { 
				sections = value;
				ReloadData ();
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
			OnSectionAdded (section);
		}

		public void Remove (Section section)
		{
			if (section == null)
				return;
			var index = sections.IndexOf (section);
			Sections.Remove (section);
			OnSectionRemoved (index);
		}


		public void Remove (int index)
		{
			Sections.RemoveAt (index);
			OnSectionRemoved (index);
		}

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

		public override Cell ItemFor (int section, int row)
		{
			return Sections [section] [row];
		}

		#endregion


		#region IEnumerable implementation
		public IEnumerator GetEnumerator ()
		{
			return sections?.GetEnumerator ();
		}
		#endregion

		#region IEnumerable implementation

		IEnumerator<Section> IEnumerable<Section>.GetEnumerator ()
		{
			return sections?.GetEnumerator ();
		}

		#endregion


	}
}

