using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace SimpleTables.Cells
{
	public abstract partial class BoolBaseCell : ICell
	{
		public abstract UITableViewCell GetCell (UITableView tv);
	}
	public partial class BooleanCell : BoolBaseCell {
		static NSString bkey = new NSString ("BooleanElement");
		UISwitch sw;
		public UIColor TextColor = UIColor.Black;

		public override UITableViewCell GetCell (UITableView tv)
		{
			if (sw == null){
				sw = new UISwitch (){
					BackgroundColor = UIColor.Clear,
					Tag = 1,
					On = Value
				};
				sw.AddTarget (delegate {
					Value = sw.On;
				}, UIControlEvent.ValueChanged);
			} else
				sw.On = Value;
			
			var cell = tv.DequeueReusableCell (bkey);
			if (cell == null){
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, bkey);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			} 
			
			cell.TextLabel.Text = Caption;
			cell.TextLabel.TextColor = TextColor;
			cell.AccessoryView = sw;
			
			
			return cell;
		}
		
		protected override void Dispose (bool disposing)
		{
			if (disposing){
				if (sw != null){
					sw.Dispose ();
					sw = null;
				}
			}
		}

	}



	public abstract partial class BaseBooleanImageCell : BoolBaseCell {
		static NSString key = new NSString ("BooleanImageElement");
		
		public class TextWithImageCellView : UITableViewCell {
			const int fontSize = 17;
			static UIFont font = UIFont.BoldSystemFontOfSize (fontSize);
			BaseBooleanImageCell parent;
			UILabel label;
			UIButton button;
			const int ImageSpace = 32;
			const int Padding = 8;
			
			public TextWithImageCellView (BaseBooleanImageCell parent) : base (UITableViewCellStyle.Value1, key)
			{
				this.parent = parent;
				label = new UILabel () {
					TextAlignment = UITextAlignment.Left,
					Text = parent.Caption,
					Font = font,
				};
				button = UIButton.FromType (UIButtonType.Custom);
				button.TouchDown += delegate {
					parent.Value = !parent.Value;
					UpdateImage ();
					if (parent.Tapped != null)
						parent.Tapped ();
				};
				ContentView.Add (label);
				ContentView.Add (button);
				UpdateImage ();
			}
			
			public void UpdateImage ()
			{
				button.SetImage (parent.GetImage (), UIControlState.Normal);
			}
			
			public override void LayoutSubviews ()
			{
				base.LayoutSubviews ();
				var full = ContentView.Bounds;
				var frame = full;
				frame.Height = 22;
				frame.X = Padding;
				frame.Y = (full.Height-frame.Height)/2;
				frame.Width -= ImageSpace+Padding;
				label.Frame = frame;
				
				button.Frame = new CGRect (full.Width-ImageSpace, -3, ImageSpace, 48);
			}
			
			public void UpdateFrom (BaseBooleanImageCell newParent)
			{
				parent = newParent;
				UpdateImage ();
				label.Text = parent.Caption;
				SetNeedsDisplay ();
			}
		}
		protected abstract UIImage GetImage ();
		public UITableViewCell cell;
		public override UITableViewCell GetCell (UITableView tv)
		{
			cell = tv.DequeueReusableCell (key) as TextWithImageCellView;
			//if (cell == null)
			cell = new TextWithImageCellView (this);
			//else
			//	cell.UpdateFrom (this);
			cell.SelectionStyle =  UITableViewCellSelectionStyle.None;
			return cell;
		}
	}
	
	public partial class BooleanImageCell : BaseBooleanImageCell {
		UIImage onImage, offImage;
		
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
		
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			onImage = offImage = null;
		}
	}


}

