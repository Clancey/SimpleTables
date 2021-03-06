using System;
using Android.Views;
using Android.Content;
using Android.Widget;
using Android.Graphics;

namespace SimpleTables.Cells
{
	public partial class Cell : Java.Lang.Object , ICell
	{
		public int LayoutId { get; private set; }
		public Color BackGroundColor = Color.Black;
		public Color TextColor = Color.White;
		public virtual View GetCell (View convertView, ViewGroup parent, Context context, LayoutInflater inflater)
		{
			View view = null; // re-use an existing view, if one is available
			int type = string.IsNullOrEmpty(Detail) ? global::Android.Resource.Layout.SimpleListItem1 : global::Android.Resource.Layout.SimpleListItem2;
			if (view == null || view.Id != type) // otherwise create a new one
				view = inflater.Inflate (type , null);
			if(!string.IsNullOrEmpty(Caption))
			{
				var textView = view.FindViewById<TextView> (global::Android.Resource.Id.Text1);
				textView.Text = this.Caption;
				textView.SetTextColor (TextColor);
				textView.SetBackgroundColor(Color.Transparent);
			}
			if(!string.IsNullOrEmpty(Detail))
			{
				var textView = view.FindViewById<TextView> (global::Android.Resource.Id.Text2);
				textView.Text = this.Detail;
				textView.SetTextColor (TextColor);
				textView.SetBackgroundColor(Color.Transparent);
			}
			
			view.SetBackgroundColor(BackGroundColor);
			return view;
		}

		protected virtual void NativeDispose ()
		{

		}
	}
}

