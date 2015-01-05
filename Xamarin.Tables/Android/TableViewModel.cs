using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Xamarin.Tables
{
	public abstract partial class TableViewModel <T> :  BaseAdapter<object> , ISectionIndexer
	{
		public override bool AreAllItemsEnabled()
		{
			return false;
		}
		
		#region ISectionIndexer implementation
		public int GetPositionForSection (int section)
		{
			int count = 0;
			for(int i = 0; i < section; i++)
			{
				count += RowsInSection(i);
			}
			Console.WriteLine ("GetPositionForSection;  {0},{1}", section, count);
			return count;
  		}

		public int GetSectionForPosition (int position)
		{
			int index = 0;
			int rowCount = 0;
			while (rowCount < position) {
				rowCount += RowsInSection(index);
				index ++;
			}
			Console.WriteLine ("Section for position;  {0},{1}", position, index);
			return index;
  		}

		public Java.Lang.Object[] GetSections ()
		{
			List<Java.Lang.Object> sections = new List<Java.Lang.Object> ();
			foreach(var section in SectionIndexTitles())
				sections.Add (section);
			return sections.ToArray ();
  		}
		#endregion

		const int TYPE_SECTION_HEADER = 0;
		
		Context Context;
		LayoutInflater inflater;
		int sectionedListSeparator = 0;
		
		public TableViewModel(Context context,ListView listView, int sectionedListSeparatorLayout = Android.Resource.Layout.SimpleListItem1)
		{
			this.Context = context;
			this.inflater = LayoutInflater.From (context);
			listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => ItemClicked (e.Position);
			listView.LongClick += (object sender, View.LongClickEventArgs e) => {

			};
			this.sectionedListSeparator = sectionedListSeparatorLayout;
		}

		public void ReloadData()
		{
			this.NotifyDataSetChanged ();
		}
		public void updateLongPress()
		{

		}

		public void ItemClicked (int position, bool isLongPress = false)
		{
			var sectionCount = NumberOfSections();
			for(int sectionIndex = 0; sectionIndex < sectionCount; sectionIndex ++)
			{
				if (position == 0) 
				{
					return;
				}
				
				int size = RowsInSection(sectionIndex) + 1;
				
				if (position < size)
				{
					var item = ItemFor(sectionIndex,position - 1);
					if(item is Cell)
						(item as Cell).Selected();
					if(isLongPress)
						LongPressOnItem(item);
					else
						RowSelected(item);
					return;
				}
				
				position -= size;
			}
		}
		public override int Count
		{
			get 
			{
				int count = 0;
				
				//Get each adapter's count + 1 for the header
				var section = NumberOfSections();
				for(int i = 0; i < section; i++)
					count += RowsInSection(i) + 1;
				
				return count;
			}
		}

		public override int ViewTypeCount
		{
			get
			{
				//The headers count as a view type too
				int viewTypeCount = 1;
				
				//Get each adapter's ViewTypeCount
				var section = NumberOfSections();
				for(int i = 0; i < section; i++)
					viewTypeCount += RowsInSection(i);
				
				return viewTypeCount;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}
		
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = this[position];
			if (item == null)
				return new View(Context) ;
			if(item is ICell)
				return ((ICell)item).GetCell(convertView, parent, Context);
			var cell = new Cell (item.ToString ()){BackGroundColor = Color.Gray, TextColor = Color.White};
			return cell.GetCell (convertView, parent, Context);
		}

		public virtual void ClearEvents()
		{
//			if (listView != null) {
//				tv.RemoveGestureRecognizer (gesture);
//				gesture = null;
//				tv = null;
//				hasBoundLongTouch = false;
//			}

			if(CellFor != null)
				foreach (var d in CellFor.GetInvocationList())
					CellFor -= (GetCellEventHandler)d;

			if(CellForHeader != null)
				foreach (var d in CellForHeader.GetInvocationList())
					CellForHeader -= (GetHeaderCellEventHandler)d;

			if(ItemSelected != null)
				foreach (var d in ItemSelected.GetInvocationList())
					ItemSelected -= (EventHandler<EventArgs<T>>)d;

			if(itemLongPress != null)
				foreach (var d in itemLongPress.GetInvocationList())
					ItemLongPressed -= (EventHandler<EventArgs<T>>)d;
		}

		public override object this [int position] {
			get {
				var sectionCount = NumberOfSections();
				for(int sectionIndex = 0; sectionIndex < sectionCount; sectionIndex ++)
				{
					if (position == 0) 
					{
						var header = GetHeaderICell(sectionIndex);
						if(header != null)
							return header;
						return HeaderForSection(sectionIndex);
					}
					
					int size = RowsInSection(sectionIndex) + 1;
					
					if (position < size)
						return GetICell(sectionIndex,position - 1);
					
					position -= size;
				}
				
				return null;
			}
		}
	}
}

