using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Tables.Sample
{
	[Activity (Label = "Tables.Android.Sample", MainLauncher = true)]
	public class Activity1 : ListActivity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ListAdapter = new RootViewModel (this, ListView);
		}
	}
}


