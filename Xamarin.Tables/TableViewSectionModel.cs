using System;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Tables
{
	public partial class TableViewSectionModel : TableViewModel<Cell>
	{
		public List<Section> Sections 
		{
			get{return sections;}
			set{
				sections = value;
				ReloadData();
			}
		}
		List<Section> sections = new List<Section>();
		
		public event EventHandler<EventArg<Cell>> RowTapped;
		public event EventHandler<EventArg<Cell>> RowLongPress;
		#region implemented abstract members of TableViewModel

		public override int RowsInSection (int section)
		{
			return Sections[section].Count;
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
				RowTapped (this, new EventArg<Cell> (item));
		}
		public override void LongPressOnItem (Cell item)
		{
			if (RowLongPress != null)
				RowLongPress (this, new EventArg<Cell> (item));
		}

		public override Cell ItemFor (int section, int row)
		{
			return Sections [section] [row];
		}

		#endregion


	}
}

