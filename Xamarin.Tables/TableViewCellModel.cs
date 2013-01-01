using System;
using System.Collections.Generic;

namespace Xamarin.Tables
{
	public partial class TableViewCellModel : TableViewListModel<Cell>
	{

		#region implemented abstract members of TableViewModel

		public override ICell GetICell (int section, int position)
		{
			if (section > 0)
				return null;
			return Items [position];
		}
		#endregion
	}
}

