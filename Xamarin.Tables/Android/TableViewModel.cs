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
	public abstract partial class TableViewModel<T> : BaseAdapter<object>, ISectionIndexer
	{
		public override bool AreAllItemsEnabled()
		{
			return false;
		}

		#region ISectionIndexer implementation
		public int GetPositionForSection(int section)
		{
			try
			{
				return SectionsData[section].Start;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return 0;
			}
		}

		public int GetSectionForPosition(int position)
		{
			var data = GetSectionData(position);
			if (data.Item1 == null)
				return 0;
			return data.Item1.Section;
		}

		public Java.Lang.Object[] GetSections()
		{
			List<Java.Lang.Object> sections = new List<Java.Lang.Object>();
			foreach (var section in SectionIndexTitles())
				sections.Add(section);
			return sections.ToArray();
		}
		#endregion

		const int TYPE_SECTION_HEADER = 0;

		Context context;
		LayoutInflater inflater;
		int sectionedListSeparator = 0;
		ListView listView;
		public TableViewModel(int sectionedListSeparatorLayout = global::Android.Resource.Layout.SimpleListItem1)
		{
			this.sectionedListSeparator = sectionedListSeparatorLayout;
		}

		public Context Context
		{
			get
			{
				return context ?? global::Android.App.Application.Context;
			}
			set
			{
				context = value;
				if (value != null)
					this.inflater = LayoutInflater.From(context);
			}
		}

		public ListView ListView
		{
			get
			{
				return listView;
			}
			set
			{
				UpdateListView(value);
			}
		}

		void HandleItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
		{
			ItemClicked(e.Position, true);
		}

		void HandleItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			ItemClicked(e.Position);
		}

		public void UpdateListView(ListView listview)
		{
			if (this.listView != null)
			{
				this.listView.ItemClick -= HandleItemClick;
				this.listView.ItemLongClick += HandleItemLongClick;
			}
			listView = listview;
			listView.ItemClick += HandleItemClick;
			listView.ItemLongClick += HandleItemLongClick;
		}
		public void ReloadData()
		{
			this.NotifyDataSetChanged();
		}
		public override void NotifyDataSetChanged()
		{
			count = -1;
			SectionsData.Clear();
			base.NotifyDataSetChanged();
		}
		public void updateLongPress()
		{

		}

		public void ItemClicked(int position, bool isLongPress = false)
		{
			var item = (T)this[position];
			if (item is Cell)
				(item as Cell).Selected();
			if (isLongPress)
				LongPressOnItem(item);
			else
				RowSelected(item);
		}

		List<SectionData> SectionsData = new List<SectionData>();
		int count;
		public override int Count
		{
			get
			{
				if (count >= 0)
					return count;

				count = 0;
				var section = NumberOfSections();
				for (int i = 0; i < section; i++)
				{
					var rowCount = RowsInSection(i);
					SectionsData.Add(new SectionData
					{
						Section = i,
						Start = count,
						RowCount = rowCount
					});
					count += rowCount;
				}

				return count;
			}
		}
		LayoutInflater _layoutInflator;

		protected LayoutInflater Inflator {
			get {
				return _layoutInflator ?? (_layoutInflator = LayoutInflater.FromContext(Context));
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var data = GetSectionData(position);
			var item = GetICell(data.Item1.Section, data.Item2);
			if (item == null)
				return new View(Context);
				
			if (item is ICell)
				return ((ICell)item).GetCell(convertView, parent, Context,Inflator);
			var cell = new Cell(item.ToString()) { BackGroundColor = Color.Gray, TextColor = Color.White };
			return cell.GetCell(convertView, parent, Context,Inflator);
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public virtual void ClearEvents()
		{
			if (this.listView != null)
			{
				this.listView.ItemClick -= HandleItemClick;
				this.listView.ItemLongClick += HandleItemLongClick;
			}
			if (CellFor != null)
				foreach (var d in CellFor.GetInvocationList())
					CellFor -= (GetCellEventHandler)d;

			if (CellForHeader != null)
				foreach (var d in CellForHeader.GetInvocationList())
					CellForHeader -= (GetHeaderCellEventHandler)d;

			if (ItemSelected != null)
				foreach (var d in ItemSelected.GetInvocationList())
					ItemSelected -= (EventHandler<EventArgs<T>>)d;

			if (itemLongPress != null)
				foreach (var d in itemLongPress.GetInvocationList())
					ItemLongPressed -= (EventHandler<EventArgs<T>>)d;
		}


		class SectionData
		{
			public int Section { get; set; }
			public int RowCount { get; set; }
			public int Start { get; set; }
		}

		public override object this[int position]
		{
			get
			{
				var data = GetSectionData(position);
				if (data.Item1 == null)
					return null;
				return ItemFor(data.Item1.Section, data.Item2);
			}
		}

		Tuple<SectionData,int> GetSectionData(int position)
		{
			foreach (var sectionData in SectionsData)
			{

				if (position < sectionData.RowCount)
					return new Tuple<SectionData, int>(sectionData,position);

				position -= sectionData.RowCount;
			}
			return new Tuple<SectionData, int>(null,0);
		}
	}
}

