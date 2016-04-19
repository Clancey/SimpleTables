using System;

namespace SimpleTables.Cells
{
	public partial class Cell  : IDisposable, ICell
	{
		public Cell()
		{

		}
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
		#if !Android
		public void Dispose ()
		{
			Dispose (true);
		}
		#endif

		protected
		#if Android
		override
		#else
		virtual 
		#endif
		void Dispose (bool disposing)
		{
			if (disposing)
				NativeDispose ();
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
		public virtual void Selected()
		{

		}
		public override string ToString ()
		{
			return Caption;
		}
	}
}

