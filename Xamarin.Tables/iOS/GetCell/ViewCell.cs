using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace Xamarin.Tables
{
	public partial class ViewCell : Cell, ICell
	{
		static int count;
		NSString key;
		protected UIView View;
		public CellFlags Flags;
		
		public enum CellFlags {
			Transparent = 1,
			DisableSelection = 2
		}
		
		/// <summary>
		///   Constructor
		/// </summary>
		/// <param name="caption">
		/// The caption, only used for RootElements that might want to summarize results
		/// </param>
		/// <param name="view">
		/// The view to display
		/// </param>
		/// <param name="transparent">
		/// If this is set, then the view is responsible for painting the entire area,
		/// otherwise the default cell paint code will be used.
		/// </param>
		public ViewCell (string caption, UIView view, bool transparent) : base (caption) 
		{
			this.View = view;
			this.Flags = transparent ? CellFlags.Transparent : 0;
			key = new NSString ("UIViewElement" + count++);
		}
		
		public UITableViewCell GetCell (UITableView tv)
		{
			var cell = tv.DequeueReusableCell (key);
			if (cell == null){
				cell = new UITableViewCell (UITableViewCellStyle.Default, key);
				if ((Flags & CellFlags.Transparent) != 0){
					cell.BackgroundColor = UIColor.Clear;
					
					// 
					// This trick is necessary to keep the background clear, otherwise
					// it gets painted as black
					//
					cell.BackgroundView = new UIView (CGRect.Empty) { 
						BackgroundColor = UIColor.Clear 
					};
				}
				if ((Flags & CellFlags.DisableSelection) != 0)
					cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				var frame = View.Frame;
				frame.Width = tv.Frame.Width;
				View.Frame = frame;
				cell.ContentView.AddSubview (View);
			} 
			return cell;
		}
		
		public nfloat GetHeight (UITableView tableView, NSIndexPath indexPath)
		{
			return View.Bounds.Height;
		}
		
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (disposing){
				if (View != null){
					View.Dispose ();
					View = null;
				}
			}
		}
	}
}

