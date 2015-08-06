using System;

namespace Xamarin.Tables
{
	public abstract partial class TableViewModel <T>
	{
		public delegate ICell GetCellEventHandler(T item);
		public delegate ICell GetHeaderCellEventHandler(string header);

		public event GetCellEventHandler CellFor;
		public event GetHeaderCellEventHandler CellForHeader;

		public event EventHandler<EventArgs<T>> ItemSelected;

		EventHandler<EventArgs<T>> itemLongPress;
		public event EventHandler<EventArgs<T>> ItemLongPressed
		{
			add{
				itemLongPress += value;
				updateLongPress();
			}
			remove{
				itemLongPress -= value;
				updateLongPress ();
			}
		}

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
			{
				return CellFor(item) ?? new StringCell("");
			}
			return  new StringCell(item == null ? ""  : item.ToString());
		}

		protected ICell GetCellFromEvent(T item)
		{
			return CellFor?.Invoke(item);
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
			if (itemLongPress != null)
				itemLongPress (this, new EventArgs<T> (item));
		}
		
		public abstract T ItemFor (int section, int row);
		
		public virtual ICell GetHeaderICell (int section)
		{
			if (CellForHeader != null)
				return CellForHeader(HeaderForSection(section));
			return null;
		}
	}
}

