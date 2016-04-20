using System;
using System.Collections.Generic;
using SimpleTables.Cells;

namespace SimpleTables
{
	public partial class TableViewCellModel : TableViewListModel<Cells.Cell>
	{

		#region implemented abstract members of TableViewModel

		public override ICell GetICell (int section, int position)
		{
			if (section > 0)
				return null;
			return (ICell)Items [position];
		}
		#endregion
	}
}

