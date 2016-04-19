using System;
using AppKit;

namespace SimpleTables
{
	public interface ICollectionCell
	{
		NSCollectionViewItem GetCollectionCell (NSCollectionView collectionView, Foundation.NSIndexPath indexPath);
	}
}

