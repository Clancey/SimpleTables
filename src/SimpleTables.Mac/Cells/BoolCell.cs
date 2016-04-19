using System;
using AppKit;
using Foundation;

namespace SimpleTables.Cells
{
	public abstract partial class BaseBooleanImageCell : BoolBaseCell
	{
		static NSString key = new NSString ("BooleanImageElement");


		protected abstract NSImage GetImage ();

	}

	public partial class BooleanImageCell : BaseBooleanImageCell
	{
		NSImage onImage, offImage;

		public BooleanImageCell (string caption, bool value, NSImage onImage, NSImage offImage) : base (caption, value)
		{
			this.onImage = onImage;
			this.offImage = offImage;
		}

		protected override NSImage GetImage ()
		{
			if (Value)
				return onImage;
			else
				return offImage;
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			onImage = offImage = null;
		}
	}

}

