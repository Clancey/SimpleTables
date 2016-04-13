using System;
using Android.Views;
using Android.Widget;
using Xamarin.Tables;

namespace Tables.Sample
{
	public class ItemCell : ICell, IBindingContext
	{
		public ItemCell () 
		{
		}


		WeakReference bindingContext;

		public object BindingContext {
			get { return bindingContext?.Target; }

			set { bindingContext = new WeakReference (value); }
		}
		public  Android.Views.View GetCell (Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context, LayoutInflater inflater)
		{
			var item = BindingContext as Item;
			View view = null; // re-use an existing view, if one is available
			int type = global::Android.Resource.Layout.SimpleListItem2;
			if (view == null || view.Id != type) // otherwise create a new one
				view = inflater.Inflate (type, null);
			var textView1 = view.FindViewById<TextView> (global::Android.Resource.Id.Text1);
			textView1.Text = item.Title.ToString();

			var textView2 = view.FindViewById<TextView> (global::Android.Resource.Id.Text2);
			textView2.Text = $"{item.Title} Details";
			return view;
		}
	}
}

