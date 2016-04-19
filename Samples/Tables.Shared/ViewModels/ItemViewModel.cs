using System;
using System.Collections.Generic;
using SimpleTables;
using System.Linq;
namespace Tables.Sample
{
	public class ItemViewModel : BaseTableViewModel<Item>
	{
		List<Item> Items = new List<Item> ();
		public ItemViewModel ()
		{
			Items = Enumerable.Range (0, 100).Select (x => new Item { Title = x.ToString (), Details = $"{x} Details" }).ToList ();
		}

		public override string HeaderForSection (int section)
		{
			return section.ToString ();
		}

		public override Item ItemFor (int section, int row)
		{
			return Items [row];
		}

		public override int NumberOfSections ()
		{
			return 1;
		}

		public override int RowsInSection (int section)
		{
			return Items.Count;
		}
	}
}

