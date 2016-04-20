using System;
namespace SimpleTables
{
	public partial class TableViewModel <T>
	{

		public const string WrongVersion = "You're referencing the Portable version in your App - you need to reference the platform (iOS/Android) version";
		protected virtual void ClearNativeEvents ()
		{

		}
		private void updateLongPress ()
		{

			throw new Exception (WrongVersion);
		}
		public void ReloadData ()
		{
			throw new Exception (WrongVersion);
		}
	}
}

