using System;
using Android.Graphics;
using Android.Widget;
using Android.Views;
using Android.Content;

namespace SimpleTables.Cells
{
	public partial class BooleanCell : CompoundButton.IOnCheckedChangeListener
	{
		private ToggleButton _toggleButton;
		private TextView _caption;
		private TextView _subCaption;

		public override View GetCell (View convertView, ViewGroup parent, Context context, LayoutInflater inflater)
		{
			View layout = inflater.Inflate (Resource.Layout.dialog_onofffieldright, null);
			if (layout != null)
			{

				_caption = layout.FindViewById<TextView>(Resource.Id.dialog_LabelField);
				_caption.Text = Caption;
				_toggleButton = layout.FindViewById<ToggleButton>(Resource.Id.dialog_BoolField);
				_toggleButton.Checked = Value;
				_subCaption = layout.FindViewById<TextView>(Resource.Id.dialog_LabelSubtextField);
				if(_subCaption != null)
					_subCaption.Text = Detail;

			}
			else
			{
				_caption = null;
				_toggleButton = null;
				_subCaption = null;
			}
			return layout;

		}
		public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
		{
			this.Value = isChecked;
		}
		protected override void NativeDispose ()
		{
			base.NativeDispose ();
			_toggleButton = null;
			_caption = null;
			_subCaption = null;

		}
	}

	public partial class BooleanImageCell
	{
		Bitmap onImage, offImage;
		
		public BooleanImageCell (string caption, bool value, Bitmap onImage, Bitmap offImage) : base (caption, value)
		{
			this.onImage = onImage;
			this.offImage = offImage;
		}
		protected override void NativeDispose ()
		{
			base.NativeDispose ();
			onImage = null;
			offImage = null;
		}
	}
}

