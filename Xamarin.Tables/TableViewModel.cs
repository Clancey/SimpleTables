using System;

namespace Xamarin.Tables
{
	public abstract partial class TableViewModel <T>
	{
		public abstract int RowsInSection (int section);
		
		public abstract int NumberOfSections ();
		
		public abstract int GetItemViewType (int section, int row);
		
		public abstract ICell GetICell (int section, int position);
		
		public abstract string HeaderForSection(int section);
		
		public abstract string[] SectionIndexTitles ();
		
		public abstract void RowSelected(T item);
		
		public abstract T ItemFor (int section, int row);
		
		public virtual ICell GetHeaderICell (int section)
		{
			return null;
		}
	}
}

