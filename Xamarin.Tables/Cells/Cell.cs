using System;

namespace Xamarin.Tables
{
	public partial class Cell  : IDisposable 
	{
		public string Caption;
		
		public string Detail;
		
		/// <summary>
		///  Initializes the element with the given caption.
		/// </summary>
		/// <param name="caption">
		/// The caption.
		/// </param>
		public Cell (string caption)
		{
			this.Caption = caption;
		}	
		
		public Cell(string caption, string detail)
		{
			this.Caption = caption;
			this.Detail = detail;	
		}
		
		
		public void Dispose ()
		{
			Dispose (true);
		}
		
		protected virtual void Dispose (bool disposing)
		{
		}
		public virtual string Summary ()
		{
			return "";
		}
		public virtual bool Matches (string text)
		{
			if (Caption == null)
				return false;
			return Caption.IndexOf (text, StringComparison.CurrentCultureIgnoreCase) != -1;
		}
	}
}

