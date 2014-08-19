using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace Xamarin.Tables
{
	public partial class StyledMultilineCell : StyledStringCell, ICellSizing {
		public StyledMultilineCell (string caption) : base (caption) {}
		public StyledMultilineCell (string caption, string value) : base (caption, value) {}
		public StyledMultilineCell (string caption, Action tapped) : base (caption, tapped) {}
		public nfloat Height {get;set;}
		
		public virtual nfloat GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			if(Height != 0)
				return Height;
			CGSize size = new CGSize (280, float.MaxValue);
			
			var font = Font ?? UIFont.SystemFontOfSize (14);
			var height = tableView.StringSize (Caption, font, size, LineBreakMode).Height;
			height *= 1.5f;
			return NMath.Max(44,height);
		}
	}

}

