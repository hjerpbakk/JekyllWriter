using System;
using AppKit;
using Foundation;
using JekyllWriter.Model;

namespace JekyllWriter.Views.Posts
{
    public class PostsDelegate : NSOutlineViewDelegate
    {
        readonly Action<PostFile> selctionChangedAction;

        public PostsDelegate(Action<PostFile> selctionChangedAction)
        {
            this.selctionChangedAction = selctionChangedAction ?? throw new ArgumentNullException(nameof(selctionChangedAction));
        }

        public override NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            string identifier;
            string value;
            var fileItem = item as PostFile;
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

        public override bool ShouldSelectItem(NSOutlineView outlineView, NSObject item) => item is PostFile;

        public override void SelectionDidChange(NSNotification notification)
        {
            var outlineView = notification.Object as NSOutlineView;
            if (outlineView == null) {
                return;
            }

            selctionChangedAction((PostFile)outlineView.ItemAtRow(outlineView.SelectedRow));
        }
    }
}
