using System;
using SimpleTables.Cells;

namespace SimpleTables
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
			var cell = item as ICell;
			if (cell != null)
				return cell;
			
			if (item == null)
				return new StringCell ("");

			cell = GetCellFromEvent (item);

			if (cell == null)
				cell = CellRegistrar.GetCell (item.GetType ());

			var binding = cell as IBindingCell;
			if (binding != null)
				binding.BindingContext = item;
			if (cell != null)
				return cell;
			
			return new StringCell(item?.ToString() ?? "");
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


		public virtual void ClearEvents ()
		{
			ClearNativeEvents ();

			if (CellFor != null)
				foreach (var d in CellFor.GetInvocationList ())
					CellFor -= (GetCellEventHandler)d;

			if (CellForHeader != null)
				foreach (var d in CellForHeader.GetInvocationList ())
					CellForHeader -= (GetHeaderCellEventHandler)d;

			if (ItemSelected != null)
				foreach (var d in ItemSelected.GetInvocationList ())
					ItemSelected -= (EventHandler<EventArgs<T>>)d;

			if (itemLongPress != null)
				foreach (var d in itemLongPress.GetInvocationList ())
					ItemLongPressed -= (EventHandler<EventArgs<T>>)d;
		}
	}
}

