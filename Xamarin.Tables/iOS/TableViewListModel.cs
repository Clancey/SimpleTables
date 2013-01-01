using System;
using System.Collections.Generic;

namespace Xamarin.Tables
{
	public partial class TableViewListModel<T>
	{
		public TableViewListModel (List<T> items) 
		{
			Items = items;
		}
	}
}

