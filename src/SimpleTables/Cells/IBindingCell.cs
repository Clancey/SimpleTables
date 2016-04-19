using System;

namespace SimpleTables
{
	public interface IBindingCell : ICell
	{
		object BindingContext { get; set; }
	}
}