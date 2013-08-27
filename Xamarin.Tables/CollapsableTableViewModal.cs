using System;
using System.Collections.Generic;

namespace Xamarin.Tables
{
	public abstract partial class CollapsableTableViewModal<T> : TableViewModel<T>
	{
		bool defaultState;
		public bool DefaultState {
			get {
				return defaultState;
			}
			set {
				defaultState = value;
			}
		}
		public void CollapseAll()
		{
			BeginAnimation();
			int sections = NumberOfSections();
			for (int section = 0; section < sections; section++)
			{
				SetCollapsed(section, false,false);
			}
			EndAnimation();
		}
		public void ExpandAll()
		{
			BeginAnimation();
			int sections = NumberOfSections();
			for (int section = 0; section < sections; section++)
			{
				SetCollapsed(section, true,false);
			}
			EndAnimation();
		}
		public bool IsCollapsed(int section)
		{
			bool isCollapsed = defaultState;
			if(collapsed.ContainsKey(section))
				collapsed.TryGetValue(section,out isCollapsed);
			return isCollapsed;
		}
		public void SetCollapsed(int section,bool state, bool animate = true)
		{
			if(state == IsCollapsed(section))
				return;
			collapsed[section] = state;
			ReloadData(state,section,animate);
		}
		public void SetDefaultState(int section,bool state)
		{
			collapsed[section] = state;
		}
		public void ToggleState(int section)
		{
			SetCollapsed(section,!IsCollapsed(section));
		}
		Dictionary<int,bool> collapsed = new Dictionary<int, bool>();

	}
}

