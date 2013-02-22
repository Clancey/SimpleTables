using System;

namespace Xamarin.Tables
{
	public class RadioCellOptions 
	{
		public string Caption { get; set; }
		public string Value { get; set; }
		public RadioCellOptions (string caption, string value)
		{
			Caption = caption;
			Value = value;
		}
	}
}

