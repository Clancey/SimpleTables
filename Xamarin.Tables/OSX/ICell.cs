using System;
using AppKit;

namespace Xamarin.Tables
{
	public partial interface ICell
	{
		NSView GetCell (NSTableView tableView, NSTableColumn tableColumn, Foundation.NSObject owner);
		string GetCellText (NSTableColumn tableColumn);
	}
}

