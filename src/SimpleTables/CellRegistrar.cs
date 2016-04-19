using System;
using System.Collections.Generic;


namespace SimpleTables
{
	public static class CellRegistrar
	{
		static readonly Dictionary<Type, Type> RegisteredCells = new Dictionary<Type, Type> ();
		static readonly Dictionary<Type, Type> RegisteredCollectionCells = new Dictionary<Type, Type> ();

		public static void Register<TType, TICell> () where TICell : ICell
		{
			var type = typeof (TICell);
			RegisteredCells [typeof (TType)] = type;

			if (typeof (ICollectionCell).IsAssignableFrom (type))
				RegisteredCollectionCells [typeof (TType)] = type;
		}

		public static void RegisterCell (Type type, Type cell)
		{
			RegisteredCells [type] = cell;

			if (typeof (ICollectionCell).IsAssignableFrom (cell))
				RegisteredCollectionCells [type] = cell;
		}

		public static ICell GetCell (Type type)
		{
			if (type == null)
				return null;
			Type cellType;
			if (!RegisteredCells.TryGetValue (type, out cellType))
				return null;

			var cell = (ICell)Activator.CreateInstance (cellType);

			return cell;
		}


		public static void RegisterCollectionCell<TType, TICell> () where TICell : ICollectionCell
		{
			RegisteredCollectionCells [typeof (TType)] = typeof (TICell);
		}

		public static void RegisterCollectionCell (Type type, Type cell)
		{
			RegisteredCollectionCells [type] = cell;
		}

		public static ICollectionCell GetCollectionCell (Type type)
		{
			Type cellType;
			if (!RegisteredCollectionCells.TryGetValue (type, out cellType))
				return null;

			var cell = (ICollectionCell)Activator.CreateInstance (cellType);

			return cell;
		}
	}
}