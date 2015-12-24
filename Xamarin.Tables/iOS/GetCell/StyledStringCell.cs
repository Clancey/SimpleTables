using System;
using Foundation;
using UIKit;

namespace Xamarin.Tables
{
	public interface IColorizeBackground {
		void WillDisplay (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath);
	}
	public partial class StyledStringCell : StringCell, IImageUpdated, IColorizeBackground {
		static NSString [] skey = { new NSString (".1"), new NSString (".2"), new NSString (".3"), new NSString (".4") };
		
		public StyledStringCell (string caption) : base (caption) {}
		public StyledStringCell (string caption, Action tapped) : base (caption, tapped) {}
		public StyledStringCell (string caption, string value) : base (caption, value) 
		{
			style = UITableViewCellStyle.Value1;	
		}
		public StyledStringCell (string caption, string value, UITableViewCellStyle style) : base (caption, value) 
		{ 
			this.style = style;
		}
		
		public UITableViewCellStyle style;
		public UIFont Font;
		public UIColor TextColor;
		public UILineBreakMode LineBreakMode = UILineBreakMode.WordWrap;
		public int Lines = 1;
		public UITableViewCellAccessory Accessory = UITableViewCellAccessory.None;
		
		// To keep the size down for a StyleStringElement, we put all the image information
		// on a separate structure, and create this on demand.
		public ExtraInfo extraInfo;
		
		public class ExtraInfo {
			public UIImage Image; // Maybe add BackgroundImage?
			public UIColor BackgroundColor, DetailColor;
			public Uri Uri, BackgroundUri;
		}
		
		ExtraInfo OnImageInfo ()
		{
			if (extraInfo == null)
				extraInfo = new ExtraInfo ();
			return extraInfo;
		}
		
		// Uses the specified image (use this or ImageUri)
		public UIImage Image {
			get {
				return extraInfo == null ? null : extraInfo.Image;
			}
			set {
				OnImageInfo ().Image = value;
				extraInfo.Uri = null;
			}
		}
		
		// Loads the image from the specified uri (use this or Image)
		public Uri ImageUri {
			get {
				return extraInfo == null ? null : extraInfo.Uri;
			}
			set {
				OnImageInfo ().Uri = value;
				extraInfo.Image = null;
			}
		}
		
		// Background color for the cell (alternative: BackgroundUri)
		public UIColor BackgroundColor {
			get {
				return extraInfo == null ? null : extraInfo.BackgroundColor;
			}
			set {
				OnImageInfo ().BackgroundColor = value;
				extraInfo.BackgroundUri = null;
			}
		}
		
		public UIColor DetailColor {
			get {
				return extraInfo == null ? null : extraInfo.DetailColor;
			}
			set {
				OnImageInfo ().DetailColor = value;
			}
		}
		
		// Uri for a Background image (alternatiev: BackgroundColor)
		public Uri BackgroundUri {
			get {
				return extraInfo == null ? null : extraInfo.BackgroundUri;
			}
			set {
				OnImageInfo ().BackgroundUri = value;
				extraInfo.BackgroundColor = null;
			}
		}
		
		protected virtual string GetKey (int style)
		{
			return skey [style];
		}
		
		public override UITableViewCell GetCell (UITableView tv)		{
			var key = GetKey ((int) style);
			var cell = tv.DequeueReusableCell (key) ?? new UITableViewCell (style, key);
			cell.SelectionStyle = UITableViewCellSelectionStyle.Blue;
			PrepareCell (cell);
			return cell;
		}
		
		void PrepareCell (UITableViewCell cell)
		{
			cell.Accessory = Accessory;
			var tl = cell.TextLabel;
			tl.Text = Caption;
			tl.TextAlignment = Alignment;
			tl.TextColor = TextColor ?? UIColor.Black;
			tl.Font = Font ?? UIFont.BoldSystemFontOfSize (17);
			tl.LineBreakMode = LineBreakMode;
			tl.Lines = 0;			
			
			// The check is needed because the cell might have been recycled.
			if (cell.DetailTextLabel != null)
				cell.DetailTextLabel.Text = Value == null ? "" : Value;
			
			if (extraInfo == null){
				cell.ContentView.BackgroundColor = null;
				tl.BackgroundColor = null;
			} else {
				var imgView = cell.ImageView;
				UIImage img;
				
//				if (extraInfo.Uri != null)
//					img = ImageLoader.DefaultRequestImage (extraInfo.Uri, this);
//				else 
					if (extraInfo.Image != null)
					img = extraInfo.Image;
				else 
					img = null;
				imgView.Image = img;
				
				if (cell.DetailTextLabel != null)
					cell.DetailTextLabel.TextColor = extraInfo.DetailColor ?? UIColor.Black;
			}		
		}
		
		void ClearBackground (UITableViewCell cell)
		{
			cell.BackgroundColor = UIColor.White;
			cell.TextLabel.BackgroundColor = UIColor.Clear;
		}
		
		void IColorizeBackground.WillDisplay (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			if (extraInfo == null){
				ClearBackground (cell);
				return;
			}
			
			if (extraInfo.BackgroundColor != null){
				cell.BackgroundColor = extraInfo.BackgroundColor;
				cell.TextLabel.BackgroundColor = UIColor.Clear;
			} else if (extraInfo.BackgroundUri != null){
				//var img = ImageLoader.DefaultRequestImage (extraInfo.BackgroundUri, this);
//				cell.BackgroundColor = img == null ? UIColor.White : UIColor.FromPatternImage (img);
				cell.TextLabel.BackgroundColor = UIColor.Clear;
			} else 
				ClearBackground (cell);
		}
		
		void IImageUpdated.UpdatedImage (Uri uri)
		{
			if (uri == null || extraInfo == null)
				return;
//			var root = GetImmediateRootElement ();
//			if (root == null || root.TableView == null)
//				return;
//			root.TableView.ReloadRows (new NSIndexPath [] { IndexPath }, UITableViewRowAnimation.None);
		}	
	}
}