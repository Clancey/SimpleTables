using System;
using System.Collections.Generic;
using UIKit;

namespace SimpleTables
{
	public partial class TableViewSectionModel
	{
		public TableViewSectionModel (List<Section> sections) 
		{
			Sections = sections ?? new List<Section> ();
		}
		public TableViewSectionModel ()
		{
		}
	}
}

