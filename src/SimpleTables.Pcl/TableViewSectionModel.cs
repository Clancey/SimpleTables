using System;
namespace SimpleTables
{
	public partial class TableViewSectionModel
	{
		protected virtual void OnSectionAdded (Section section)
		{
			throw new Exception (WrongVersion);
		}
		protected virtual void OnSectionRemoved (int index)
		{
			throw new Exception (WrongVersion);
		}
	}
}

