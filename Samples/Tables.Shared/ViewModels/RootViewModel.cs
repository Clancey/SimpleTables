using System;
using SimpleTables;
using System.Collections.Generic;
using SimpleTables.Cells;

namespace Tables.Sample
{
	public class RootViewModel : TableViewSectionModel
	{
		public RootViewModel ()
		{
			Sections = CreateRoot ();
		}
		List<Section> CreateRoot()
		{
			return  new List<Section> (){
				new Section ("Element API"){
					new RadioCell("Test Radio",TestEnum.Value1),
					new StringCell ("iPhone Settings Sample"),
					new StringCell ("Dynamically load data"),
					new StringCell ("Add/Remove demo"),
					new StringCell ("Assorted cells"),
					//new StyledStringCell ("Styled Elements", DemoStyled) { BackgroundUri = new Uri ("file://" + p) },
					new StringCell ("Load More Sample"),
					new StringCell ("Row Editing Support"),
					new StringCell ("Advanced Editing Support"),
					new StringCell ("Owner Drawn Element"),
				},

				new Section ("Container features"){
					new StringCell ("Pull to Refresh"),
					new StringCell ("Headers and Footers"),
					new StringCell ("Root Style"),
					new StringCell ("Index sample"),
				},
				new Section ("Auto-mapped"){
					new StringCell ("Reflection API")
				},
			};
		}
	}
}

