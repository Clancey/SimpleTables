using System;

namespace SimpleTables.Cells
{
	
	/// <summary>
	///   The string element can be used to render some text in a cell 
	///   that can optionally respond to tap events.
	/// </summary>
	public partial class StringCell : Cell {
		public string Value;
		
		public StringCell (string caption) : base (caption) {}
		
		public StringCell (string caption, string value) : base (caption)
		{
			this.Value = value;
		}
		
		public StringCell (string caption,  Action tapped) : base (caption)
		{
			Tapped += tapped;
		}
		
		public StringCell (string caption,string value,  Action tapped) : base (caption)
		{
			this.Value = value;
			Tapped += tapped;
		}

		
		public event Action Tapped;
		
		public override string Summary ()
		{
			return Caption;
		}
		public bool ShouldDeselect = true;

		
		public override bool Matches (string text)
		{
			return (Value != null ? Value.IndexOf (text, StringComparison.CurrentCultureIgnoreCase) != -1: false) || base.Matches (text);
		}
		public override void Selected ()
		{
			Tapped?.Invoke();
		}
		public override string ToString ()
		{
			return $"{Caption} {Value}";
		}
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (disposing)
				Tapped = null;
		}
	}

}

