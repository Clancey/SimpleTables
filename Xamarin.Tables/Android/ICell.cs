using System;
using Android.Views;
using Android.Content;

namespace Xamarin.Tables
{
	public partial interface ICell
	{
		View GetCell (View convertView, ViewGroup parent, Context context, LayoutInflater inflator);
	}
}

