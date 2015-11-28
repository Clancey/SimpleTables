using System;
using AppKit;
using Foundation;
using CoreGraphics;

namespace Xamarin.Tables
{
	public partial class Cell : ICell, ICollectionCell
	{
		#region ICell implementation

		public AppKit.NSView GetCell (AppKit.NSTableView tableView, AppKit.NSTableColumn tableColumn, Foundation.NSObject owner)
		{
			var cell = tableView.MakeView("TextField",owner)as NSTextField ??new NSTextField{Identifier = "TextField"};
			cell.StringValue = Caption ?? "";
			return cell;
		}

		public string GetCellText (AppKit.NSTableColumn tableColumn)
		{
			return Caption;
		}
		#endregion

		public NSCollectionViewItem GetCollectionCell (NSCollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			var cell = collectionView.MakeItem ("cell", indexPath);
			if (cell == null) {
				cell = new NSCollectionViewItem (){

				};
				cell.View = new NSTextField (new CGRect (0, 0, 100, 100)) {
					Editable = false,
				};
			}
			var text = cell.View as NSTextField;
			//var cell = collectionView.NewItemForRepresentedObject ((NSString)Caption);
			//var cell = collectionView.GetItem ( indexPath) ;
			cell.Identifier = "cell";
			text.StringValue = Caption;
			return cell;
		}
	}
}

