using System;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;

namespace SimpleTables
{
	public partial class TableViewSectionModel
	{
		protected virtual void OnSectionAdded (Section section)
		{
			ReloadData ();
		}

		protected virtual void OnSectionRemoved (int index)
		{
			ReloadData ();
		}

	}
}

