using System;
using AppKit;
using Foundation;
using JekyllWriter.Model;

namespace JekyllWriter.Views.Posts
{
    public class PostsDelegate : NSOutlineViewDelegate
    {
        readonly Action selectionIsChanging;
        readonly Action<File> selctionChangedAction;

        public PostsDelegate(Action selectionIsChanging, Action<File> selctionChangedAction)
        {
            this.selectionIsChanging = selectionIsChanging ?? throw new ArgumentNullException(nameof(selectionIsChanging));
            this.selctionChangedAction = selctionChangedAction ?? throw new ArgumentNullException(nameof(selctionChangedAction));
        }

        public override NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            string identifier;
            string value;
            var fileItem = item as File;
            if (fileItem == null) {
                identifier = "HeaderCell";
                value = ((Folder)item).Name;
            } else {
                identifier = "DataCell";
                value = fileItem.Name;
            }

            var view = (NSTableCellView)outlineView.MakeView(identifier, this);
            view.TextField.StringValue = value;
            return view;
        }

        public override bool IsGroupItem(NSOutlineView outlineView, NSObject item) => item is Folder;

        public override bool ShouldSelectItem(NSOutlineView outlineView, NSObject item) => item is File;

        public override void SelectionIsChanging(NSNotification notification) => selectionIsChanging();

        public override void SelectionDidChange(NSNotification notification)
        {
            var postFile = GetPostFileFromSelectedRow(notification);
            if (postFile == null) {
                return;
            }

            selctionChangedAction(postFile);
        }

        File GetPostFileFromSelectedRow(NSNotification notification) {
            var outlineView = notification.Object as NSOutlineView;
            if (outlineView == null)
            {
                return null;
            }

            return (File)outlineView.ItemAtRow(outlineView.SelectedRow);
        }
    }
}
