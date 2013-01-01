using System;
using System.Collections.Generic;

namespace Xamarin.Tables
{
	public abstract partial class TableViewListModel<T> : TableViewModel<T>
	{
		protected List<T> Items;
				
		public event EventHandler<EventArg<T>> RowTapped;
		#region implemented abstract members of TableViewModel

		public override int RowsInSection (int section)
		{
			if (section > 0)
				return 0;
			return Items.Count;
		}

		public override int NumberOfSections ()
		{
			return 1;
		}

		public override int GetItemViewType (int section, int row)
		{
			throw new NotImplementedException ();
		}

		public override string[] SectionIndexTitles ()
		{
			return null;
		}

		public override string HeaderForSection (int section)
		{
			return null;
		}

		public override T ItemFor (int section, int row)
		{
			return Items[row];
		}
		public override void RowSelected (T item)
		{
			if (RowTapped != null)
				RowTapped (this, new EventArg<T> (item));
		}
		#endregion
	}
}

