using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace SimpleTables.Cells
{
	public partial class RadioCell : Cell
	{
		int selectedIndex = 0;
		RadioCellOptions[] Options;
		public RadioCell (string caption, Enum values) : base(caption)
		{
			Options = ParseEnum (values,out selectedIndex);
		}
		public RadioCell(string caption, int index, params RadioCellOptions[] options) : base(caption)
		{
			selectedIndex = index;
			Options = options;
		}
		static RadioCellOptions[] ParseEnum(Enum value, out int index)
		{
			index = 0;
			int idx = 0;
			var mType = value.GetType();
			List<RadioCellOptions> foundOptions = new List<RadioCellOptions> ();
			foreach (var fi in mType.GetFields (BindingFlags.Public | BindingFlags.Static)){
				Enum v = (Enum)GetValue (fi, null);
				if (v == value)
					index = idx;
				foundOptions.Add (new RadioCellOptions(MakeCaption (fi.Name),fi.Name));
				idx++;
			}
			return foundOptions.ToArray ();
		}
		
		static object GetValue (MemberInfo mi, object o)
		{
			var fi = mi as FieldInfo;
			if (fi != null)
				return fi.GetValue (o);
			var pi = mi as PropertyInfo;
			
			var getMethod = pi.GetGetMethod ();
			return getMethod.Invoke (o, new object [0]);
		}
		static string MakeCaption (string name)
		{
			var sb = new StringBuilder (name.Length);
			bool nextUp = true;
			
			foreach (char c in name){
				if (nextUp){
					sb.Append (Char.ToUpper (c));
					nextUp = false;
				} else {
					if (c == '_'){
						sb.Append (' ');
						continue;
					}
					if (Char.IsUpper (c))
						sb.Append (' ');
					sb.Append (c);
				}
			}
			return sb.ToString ();
		}
	}
}

