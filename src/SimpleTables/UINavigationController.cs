using System;
using Android.App;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace Xamarin.Tables
{
	public class UINavigationController : Fragment
	{
		public List<Fragment> ControllerStack = new List<Fragment> ();
		public IFragmentSwitcher Parent;
		protected Button rightButton;
		public Button RightButton
		{
			get{ return rightButton;}
			set{
				if(value == null){
					rightButton.RemoveFromParent();
					return;
				}else if(rightButton != value)
					rightButton = value;
				if(rightButton.Parent == null)
					RightButtonLayout.AddView(rightButton);
			}
		}
		protected Button leftButton;
		public Button LeftButton
		{
			get{ return leftButton;}
			set{
				if(value == null)
				{
					leftButton.RemoveFromParent();
					return;
				}
				else if(leftButton != value)
					leftButton = value;
				if(leftButton.Parent == null)
					LeftButtonLayout.AddView(leftButton);
			}
		}

		protected TextView TitleTv;
		protected LinearLayout LeftButtonLayout;
		protected LinearLayout RightButtonLayout;
		public UINavigationController() : base()
		{

		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			if(ControllerStack.Count > 1)
				SwitchContent (CurrentFragment, false);
		}
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View v = inflater.Inflate (Resource.Layout.anav, container, false);
			LeftButtonLayout = v.FindViewById<LinearLayout> (Resource.Id.uinavigationLeftButtonLayout);
			RightButtonLayout = v.FindViewById<LinearLayout> (Resource.Id.uinavigationRightButtonLayout);
			rightButton = v.FindViewById<Button> (Resource.Id.uinavigationrightbtn);
			leftButton = v.FindViewById<Button> (Resource.Id.uinavigationleftbtn);
			leftButton.SetTextColor (Color.White);
			leftButton.Click += LeftClicked;
			TitleTv = v.FindViewById<TextView> (Resource.Id.title);
			var curFrag = CurrentFragment;
			if (curFrag != null) {
				try{
					if(!curFrag.IsAdded)
						FragmentManager.BeginTransaction ().Add (Resource.Id.uinavigationcontent, CurrentFragment).Commit ();
					else
						FragmentManager.BeginTransaction ().Replace (Resource.Id.uinavigationcontent, CurrentFragment).Commit ();
						
				if(curFrag is IViewController)
					TitleTv.Text = ((IViewController)curFrag).Title;
				}
				catch(Exception ex)
				{
				}
			}
			SetLeftButton ();
			return v;
		}
		public override void OnDetach ()
		{
			var curFrag = CurrentFragment;
			if (curFrag != null) {
				if (curFrag.IsAdded)
				{
				
					try{
					FragmentManager.BeginTransaction ().Remove (CurrentFragment).Commit ();
					}
					catch(Exception ex)
					{
					}
				}
			}
			base.OnDetach ();
		}
		public void SetLeftButton()
		{
			RightButton = null;
			if(ControllerStack.Count > 1)
			{
				//LeftButton.SetBackgroundResource(Resource.Drawable.back);
				LeftButton = leftButton;
				LeftButton.Text = "Back";

			} else {
				//LeftButton.SetBackgroundResource(Resource.Drawable.menuButton);
				LeftButton = null;;
			}
		}

		public void LeftClicked (object sender, EventArgs e)
		{
			if (ControllerStack.Count > 1) {
				PopViewController (true);
			}else {

//					if(Parent is Com.Slidingmenu.Lib.App.SlidingFragmentActivity)
//						((Com.Slidingmenu.Lib.App.SlidingFragmentActivity)Parent).SlidingMenu.Toggle();

			}
		}

		public override void OnResume ()
		{
			base.OnResume ();
		}

		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			base.OnActivityCreated (savedInstanceState);
		}

		public UINavigationController (Fragment root)
		{
			ControllerStack.Add (root);
			if (root is IViewController) {
				((IViewController)root).NavigationController = this;
			}
		}
		public Fragment CurrentFragment
		{
			get{ 
				return ControllerStack.LastOrDefault ();
			}
		}
		public virtual void PushViewController(Fragment fragment,bool animated)
		{
			ControllerStack.Add (fragment);
			if (fragment is IViewController)
				((IViewController)fragment).NavigationController = this;
			SwitchContent (CurrentFragment,animated);
		}
		protected virtual void SwitchContent (Fragment fragment, bool animated, bool removed = false)
		{
			if (fragment == null)
				return;
			var ft = FragmentManager.BeginTransaction ();
			if (animated) {
				if(removed)
					ft.SetCustomAnimations (Resource.Animation.slide_in_left, Resource.Animation.slide_out_right);
				else
					ft.SetCustomAnimations (Resource.Animation.slide_in_right, Resource.Animation.slide_out_left);
				
			}
			try{
			ft.Replace (Resource.Id.uinavigationcontent, fragment).Commit ();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
			}
			SetLeftButton ();
			if (fragment is IViewController) {
				TitleTv.Text = ((IViewController)fragment).Title;
			}
		}
		public virtual bool PopViewController(bool animated)
		{
			if (ControllerStack.Count <= 1)
				return false;
			var last = CurrentFragment;
			ControllerStack.Remove (last);
			if (CurrentFragment is IViewController)
				((IViewController)CurrentFragment).NavigationController = this;
			SwitchContent (CurrentFragment,animated,true);
			return true;
		}
	}
}

