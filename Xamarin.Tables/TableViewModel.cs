using System;

namespace Xamarin.Tables
{
	public abstract partial class TableViewModel <T>
	{
		public delegate ICell GetCellEventHandler(T item);

		public event GetCellEventHandler CellFor;

		public event EventHandler<EventArgs<T>> ItemSelected;

		public event EventHandler<EventArgs<T>> ItemLongPressed;

		public abstract int RowsInSection (int section);

		public abstract int NumberOfSections ();
		
		public virtual int GetItemViewType (int section, int row)
		{
			return 0;
		}

		public virtual ICell GetICell (int section, int row)
		{
			var item = ItemFor (section, row);
			if (CellFor != null)
				return CellFor (item);
			return item  == null ? null : new StringCell(item.ToString());
		}

		public abstract string HeaderForSection(int section);
		
		public virtual string[] SectionIndexTitles ()
		{
			return null;
		}
		
		public virtual void RowSelected(T item)
		{
			if(ItemSelected != null)
				ItemSelected(this,new EventArgs<T>(item));
		}
		
		public virtual void LongPressOnItem(T item)
		{
			if (ItemLongPressed != null)
				ItemLongPressed (this, new EventArgs<T> (item));
		}
		
		public abstract T ItemFor (int section, int row);
		
		public virtual ICell GetHeaderICell (int section)
		{
			return null;
		}
	}
}

