using System;
using AppKit;

namespace Xamarin.Tables
{
	public partial class Cell : ICell
	{
		#region ICell implementation

		public AppKit.NSView GetCell (AppKit.NSTableView tableView, AppKit.NSTableColumn tableColumn, Foundation.NSObject owner)
		{
			var cell = tableView.MakeView("TextField",owner)as NSTextField ??new NSTextField();
			cell.StringValue = Caption;
			return cell;
		}

		public string GetCellText (AppKit.NSTableColumn tableColumn)
		{
			return Caption;
		}
		#endregion
	}
}

