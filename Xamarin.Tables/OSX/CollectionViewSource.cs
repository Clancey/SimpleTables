using System;
using AppKit;
using System.Linq;
using Foundation;
using CoreGraphics;

namespace Xamarin.Tables
{
	public class CollectionViewSource<T> : NSCollectionViewDataSource, INSCollectionViewDelegate
	{
		public delegate ICollectionCell GetCollectionCellForEventHandler (T item);

		public event GetCollectionCellForEventHandler CollectionCellFor;

		WeakReference _model;

		protected TableViewModel<T> Model {
			get{ return _model.Target as TableViewModel<T>; }
			set{ _model = new WeakReference (value); }
		}

		public CollectionViewSource (TableViewModel<T> model)
		{
			Model = model;
		}
		public virtual ICollectionCell GetICollectionCell (int section, int row)
		{
			var item = Model.ItemFor (section, row);
			var cell = item as ICollectionCell;
			if (cell != null)
				return cell;
			return GetCellFromEvent (item) ?? new StringCell(item?.ToString() ?? "");
		}


		protected ICollectionCell GetCellFromEvent (T item)
		{
			return CollectionCellFor?.Invoke (item);
		}

		#region implemented abstract members of NSCollectionViewDataSource

		public override nint GetNumberOfSections (NSCollectionView collectionView)
		{
			var sections = Model.NumberOfSections ();
			return sections;
		}

		public override NSCollectionViewItem GetItem (NSCollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = GetICollectionCell ((int)indexPath.Section, (int)indexPath.Item);
			return cell.GetCollectionCell (collectionView, indexPath);
		}

		public override nint GetNumberofItems (NSCollectionView collectionView, nint section)
		{
			var items = Model.RowsInSection ((int)section);
			return items;
		}

		#endregion

		[Foundation.Export ("collectionView:didSelectItemsAtIndexPaths:")]
		public virtual void ItemsSelected (AppKit.NSCollectionView collectionView, Foundation.NSSet indexPaths)
		{
			indexPaths.OfType<NSIndexPath> ().ToList ().ForEach (indexPath => {
				var item = Model.ItemFor ((int)indexPath.Section, (int)indexPath.Item);
				Model.RowSelected (item);
			});

		}
//		[Export("collectionView:layout:sizeForItemAtIndexPath:")]
//		public CGSize SizeForItem(NSCollectionView collectionView, NSCollectionViewLayout layout, NSIndexPath indexpath)
//		{
//			return new CGSize (100, 100);
//		}
	}
}

