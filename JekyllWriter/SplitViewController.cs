// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;

namespace JekyllWriter
{
	public partial class SplitViewController : NSSplitViewController
	{
		public SplitViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            // Enable streamlined Toolbars
            View.Window.TitleVisibility = NSWindowTitleVisibility.Hidden;


        }
	}
}