using System;
using AppKit;

namespace SimpleTables
{
	public partial interface ICell
	{
		NSView GetCell (NSTableView tableView, NSTableColumn tableColumn, Foundation.NSObject owner);
		string GetCellText (NSTableColumn tableColumn);
	}
}

