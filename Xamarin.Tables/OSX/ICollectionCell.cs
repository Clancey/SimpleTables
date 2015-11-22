using System;
using AppKit;

namespace Xamarin.Tables
{
	public interface ICollectionCell
	{
		NSCollectionViewItem GetCollectionCell (NSCollectionView collectionView, Foundation.NSIndexPath indexPath);
	}
}

