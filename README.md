Simple Tables
================
Simple Tables is a way to to quick way to to build super fast and responsive ListViews/Tables in a cross platorm manner. The main focus of Simple Tables is performance and maximum code sharing.



Available on Nuget
================

https://www.nuget.org/packages/Clancey.SimpleTables/

PCL vs Shared Projects
===
Can you use a PCL? Yes!  However Simple Tables is built around maximum code share and is geared more towards Shared Projects.

TableViewModel&lt;T&gt;
===
Using clever tricks, the TableViewModel&lt;T&gt; can be directly assigned to a UITableView.Source and ListView.Adapter

```cs

public class BaseViewModel<T> : TableViewModel<T> where T : new()
{
	
	#region implemented abstract members of TableViewModel

	public override int RowsInSection(int section)
	{
		if (Database.Main == null)
			return 0;
		int rows = Database.Main.RowsInSection<T>(CurrentGroupInfo, section);
		return rows;
	}

	public override int NumberOfSections()
	{
		if (Database.Main == null)
			return 0;
		int sections = Database.Main.NumberOfSections<T>(CurrentGroupInfo);
		return sections;
	}
	
	public override string HeaderForSection(int section)
	{
		if (Database.Main == null)
			return "";
		return Database.Main.SectionHeader<T>(CurrentGroupInfo, section);
	}

	public override string[] SectionIndexTitles()
	{
		if (Database.Main == null)
			return null;
		return Database.Main.QuickJump<T>(CurrentGroupInfo);
	}

	public override T ItemFor(int section, int row)
	{
		return Database.Main.ObjectForRow<T>(CurrentGroupInfo, section, row);
	}

	#endregion
}
```

iOS

```cs
class MyViewController : UITableViewController
{
	public MyViewController
	{
		TableView.Source = new MyViewModel();
	}
}
```

On Android you need to set the ListView, and the Context.

```cs
class MyListFragment : ListFragment
{
	public override void OnViewCreated (Android.Views.View view, Android.OS.Bundle savedInstanceState)
	{
		base.OnViewCreated (view, savedInstanceState);
		ListAdapter = new MyViewModel
		{
			Context = App.Context,
			ListView = ListView,
		};
	}
}
```


ICell
================

iOS ICell

```cs
public partial interface ICell
{
	UIKit.UITableViewCell GetCell (UIKit.UITableView tv);
}
```

Android ICell

```cs
public partial interface ICell
{
	View GetCell (View convertView, ViewGroup parent, Context context, LayoutInflater inflator);
}
```

CellRegistrar
===
To get the most code share use the CellRegistrar to tie Models to Cells

```cs
CellRegistrar.Register<Song, SongCell>();
```

IBindingCell
===
When you use the CellRegistrar and your Cell implements IBindingCell the BindingContext is set automatically by the TableViewModel

```cs
public interface IBindingCell : ICell
{
	object BindingContext { get; set; }
}
```


TableViewSectionModel
===
TableViewSectionModel is heavily patterned after MonoTouch.Dialog
The cell implementions are not yet complete.

```cs
public class RootViewModel : TableViewSectionModel
{
	public RootViewModel ()
	{
		Sections = new List<Section> (){
			new Section ("Element API"){
				new RadioCell("Test Radio",TestEnum.Value1),
				new StringCell ("iPhone Settings Sample"),
				new StringCell ("Dynamically load data"),
				new StringCell ("Add/Remove demo"),
				new StringCell ("Assorted cells"),
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
```

TODO
===
Port more cells

iOS: <https://github.com/migueldeicaza/MonoTouch.Dialog/>

Android: <https://github.com/Clancey/MonoDroid.Dialog/>