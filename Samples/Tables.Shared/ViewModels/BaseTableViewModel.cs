using System;
using SimpleTables;
namespace Tables.Sample
{
	public abstract class BaseTableViewModel<T> : TableViewModel<T>
	{
		public override ICell GetICell (int section, int row)
		{
			var item = ItemFor (section, row);
			var cell = CellRegistrar.GetCell (item.GetType ());

			var binding = cell as IBindingContext;
			if (binding != null)
				binding.BindingContext = item;
			
			if (cell != null)
				return cell;
			return base.GetICell (section, row);
		}
	}
}

