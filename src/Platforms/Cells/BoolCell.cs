using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace SimpleTables.Cells
{
	public abstract partial class BoolBaseCell : ICell
	{
	}
	public partial class BooleanCell : BoolBaseCell
	{
		static NSString bkey = new NSString ("BooleanElement");
		UISwitch sw;
		public UIColor TextColor = UIColor.Black;

		public override UITableViewCell GetCell (UITableView tv)
		{
			if (sw == null) {
				sw = new UISwitch () {
					BackgroundColor = UIColor.Clear,
					Tag = 1,
					On = Value
				};
				sw.AddTarget (SwitchChanged, UIControlEvent.ValueChanged);
			} else
				sw.On = Value;

			var cell = tv.DequeueReusableCell (bkey) ?? new UITableViewCell (UITableViewCellStyle.Subtitle, bkey) {
				SelectionStyle = UITableViewCellSelectionStyle.None
			};

			cell.TextLabel.Text = Caption;
			cell.TextLabel.TextColor = TextColor;
			cell.AccessoryView = sw;


			return cell;
		}

		void SwitchChanged (object sender, EventArgs args)
		{
			Value = sw.On;
		}

		protected override void OnValueChanged ()
		{
			base.OnValueChanged ();
			if (sw == null)
				return;
			sw.On = this.Value;
		}
		protected override void NativeDispose ()
		{
			base.NativeDispose ();
			sw?.RemoveTarget (SwitchChanged, UIControlEvent.ValueChanged);
			sw?.Dispose ();
			sw = null;
		}
	}



	public abstract partial class BaseBooleanImageCell : BoolBaseCell
	{
		static NSString key = new NSString ("BooleanImageElement");

		public class TextWithImageCellView : UITableViewCell
		{
			const int fontSize = 17;
			static UIFont font = UIFont.BoldSystemFontOfSize (fontSize);
			WeakReference parent;

			BaseBooleanImageCell Parent {
				get {
					return parent?.Target as BaseBooleanImageCell;
				}

				set {
					parent = new WeakReference (value);
				}
			}

			UILabel label;
			UIButton button;
			const int ImageSpace = 32;
			const int Padding = 8;

			public TextWithImageCellView () : base (UITableViewCellStyle.Value1, key)
			{
				label = new UILabel () {
					TextAlignment = UITextAlignment.Left,
					Text = "",
					Font = font,
				};
				button = UIButton.FromType (UIButtonType.Custom);
				button.TouchDown += delegate {
					Parent.Value = !Parent.Value;
					UpdateImage ();
					if (Parent.Tapped != null)
						Parent.Tapped ();
				};
				ContentView.Add (label);
				ContentView.Add (button);
			}

			public void UpdateImage ()
			{
				button.SetImage (Parent.GetImage (), UIControlState.Normal);
			}

			public override void LayoutSubviews ()
			{
				base.LayoutSubviews ();
				var full = ContentView.Bounds;
				var frame = full;
				frame.Height = 22;
				frame.X = Padding;
				frame.Y = (full.Height - frame.Height) / 2;
				frame.Width -= ImageSpace + Padding;
				label.Frame = frame;

				button.Frame = new CGRect (full.Width - ImageSpace, -3, ImageSpace, 48);
			}

			public void UpdateFrom (BaseBooleanImageCell newParent)
			{
				Parent = newParent;
				UpdateImage ();
				label.Text = Parent.Caption;
				SetNeedsDisplay ();
			}
		}
		protected abstract UIImage GetImage ();

		WeakReference cell;
		TextWithImageCellView Cell {
			get { return cell?.Target as TextWithImageCellView; }
			set { cell = new WeakReference (value); }
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			Cell = tv.DequeueReusableCell (key) as TextWithImageCellView ?? new TextWithImageCellView ();
			Cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			Cell.UpdateFrom (this);
			return Cell;
		}
		protected override void OnValueChanged ()
		{
			Cell?.UpdateImage ();
			base.OnValueChanged ();
		}
	}

	public partial class BooleanImageCell : BaseBooleanImageCell
	{
		UIImage onImage, offImage;
		public BooleanImageCell (string caption, bool value, string onImage, string offImage) : base (caption, value)
		{
			this.onImage = UIImage.FromBundle (onImage);
			this.offImage = UIImage.FromBundle (offImage);
		}
		public BooleanImageCell (string caption, bool value, UIImage onImage, UIImage offImage) : base (caption, value)
		{
			this.onImage = onImage;
			this.offImage = offImage;
		}

		protected override UIImage GetImage ()
		{
			if (Value)
				return onImage;
			else
				return offImage;
		}
		protected override void NativeDispose ()
		{
			base.NativeDispose ();
			offImage?.Dispose ();
			onImage?.Dispose ();

		}
	}
}

