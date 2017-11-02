// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace JekyllWriter
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSOutlineView postsView { get; set; }

		[Outlet]
		AppKit.NSTextView textView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (postsView != null) {
				postsView.Dispose ();
				postsView = null;
			}

			if (textView != null) {
				textView.Dispose ();
				textView = null;
			}
		}
	}
}
