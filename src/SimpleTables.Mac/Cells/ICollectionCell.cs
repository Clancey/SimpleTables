using System;
using AppKit;

namespace SimpleTables
{
	public partial interface ICollectionCell
	{
		NSCollectionViewItem GetCollectionCell (NSCollectionView collectionView, Foundation.NSIndexPath indexPath);
	}
}

