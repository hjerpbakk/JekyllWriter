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
        AppKit.NSDatePicker Date { get; set; }

        [Outlet]
        AppKit.NSTextField DateLabel { get; set; }

        [Outlet]
        AppKit.NSOutlineView postsView { get; set; }

        [Outlet]
        AppKit.NSTextView textView { get; set; }

        [Outlet]
        AppKit.NSLayoutConstraint TextViewTopConstraint { get; set; }

        [Outlet]
        AppKit.NSTextField Title { get; set; }

        [Outlet]
        AppKit.NSTextField TitleLabel { get; set; }

        [Action ("ShowOrHidePreamble:")]
        partial void ShowOrHidePreamble (AppKit.NSButtonCell sender);
        
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

            if (TitleLabel != null) {
                TitleLabel.Dispose ();
                TitleLabel = null;
            }

            if (Title != null) {
                Title.Dispose ();
                Title = null;
            }

            if (DateLabel != null) {
                DateLabel.Dispose ();
                DateLabel = null;
            }

            if (Date != null) {
                Date.Dispose ();
                Date = null;
            }

            if (TextViewTopConstraint != null) {
                TextViewTopConstraint.Dispose ();
                TextViewTopConstraint = null;
            }
        }
    }
}
