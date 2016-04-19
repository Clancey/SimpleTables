using System;

namespace SimpleTables.Cells
{
	public abstract partial class BoolBaseCell : Cell
	{

		bool val;
		public bool Value {
			get {
				return val;
			}
			set {
				if (val != value)
					return;
				ValueChanged?.Invoke (this, EventArgs.Empty);
				OnValueChanged ();
			}
		}

		public event EventHandler ValueChanged;
		
		public BoolBaseCell (string caption, bool value) : base (caption)
		{
			val = value;
		}
		
		public BoolBaseCell (string caption, string detail, bool value) : base (caption, detail)
		{
			val = value;
		}
		
		public override string Summary ()
		{
			return val ? "On" : "Off";
		}

		protected virtual void OnValueChanged ()
		{

		}
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (disposing) {
				if (ValueChanged != null)
					foreach (var d in ValueChanged.GetInvocationList ())
						ValueChanged -= (EventHandler)d;
			}
		}
	}
	/// <summary>
	/// Used to display switch on the screen.
	/// </summary>
	public partial class BooleanCell : BoolBaseCell {
		public BooleanCell (string caption, bool value) : base (caption, value)
		{  }
		
		public BooleanCell (string caption, bool value, string key) : base (caption, value)
		{  }
		
		public BooleanCell (string caption, string detail, bool value, string key) : base (caption, detail, value)
		{  }

	}
	
	/// <summary>
	///  This class is used to render a string + a state in the form
	/// of an image.  
	/// </summary>
	/// <remarks>
	/// It is abstract to avoid making this element
	/// keep two pointers for the state images, saving 8 bytes per
	/// slot.   The more derived class "BooleanImageElement" shows
	/// one way to implement this by keeping two pointers, a better
	/// implementation would return pointers to images that were 
	/// preloaded and are static.
	/// 
	/// A subclass only needs to implement the GetImage method.
	/// </remarks>
	public abstract partial class BaseBooleanImageCell : BoolBaseCell {

		
		public BaseBooleanImageCell (string caption, bool value)
			: base (caption, value)
		{
		}
		
		public event Action Tapped;

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (disposing)
				Tapped = null;
		}
		

	}
	
	public partial class BooleanImageCell : BaseBooleanImageCell {

	}

}

