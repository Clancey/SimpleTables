using System;
using System.Collections.Generic;
using UIKit;

namespace Xamarin.Tables
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

