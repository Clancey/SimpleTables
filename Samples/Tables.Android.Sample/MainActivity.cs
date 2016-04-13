using Android.App;
using Android.Widget;
using Android.OS;

namespace Tables.Sample
{
	[Activity (Label = "Tables.Android.Sample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity :ListActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			App.Invoker = RunOnUiThread;
			App.Init ();
			base.OnCreate (bundle);
			ListAdapter = new ItemViewModel {
				Context = this,
				ListView = ListView,
			};
		}
	}
}


