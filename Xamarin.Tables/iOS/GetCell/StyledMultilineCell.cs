using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace Xamarin.Tables
{
	public partial class StyledMultilineCell : StyledStringCell, ICellSizing {
		public StyledMultilineCell (string caption) : base (caption) {}
		public StyledMultilineCell (string caption, string value) : base (caption, value) {}
		public StyledMultilineCell (string caption, Action tapped) : base (caption, tapped) {}
		public float Height {get;set;}
		
		public virtual float GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			if(Height != 0)
				return Height;
			SizeF size = new SizeF (280, float.MaxValue);
			
			var font = Font ?? UIFont.SystemFontOfSize (14);
			var height = tableView.StringSize (Caption, font, size, LineBreakMode).Height;
			height *= 1.5f;
			return Math.Max(44,height);
		}
	}

}
