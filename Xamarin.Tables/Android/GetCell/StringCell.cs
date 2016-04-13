using System;
using Android.Views;
using Android.Content;
using Android.Widget;
using Android.Graphics;

namespace Xamarin.Tables
{
	public partial class StringCell : ICell
	{
		public override View GetCell (View convertView, ViewGroup parent, Context context, LayoutInflater inflater)
		{
			this.Detail = this.Value;
			return base.GetCell (convertView, parent, context,inflater);
		}
	}
}

