using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using Foundation;
using JekyllWriter.Model;

namespace JekyllWriter.Views.Posts
{
    public class PostsDataSouce : NSOutlineViewDataSource
    {
        readonly Folder[] folders;

        public PostsDataSouce(params Folder[] folders)
        {
            this.folders = folders;
        }

        // TODO: Noe paging el, trenger vel ikke vise alle hele tiden???
        public override nint GetChildrenCount(NSOutlineView outlineView, NSObject item)
        {
            if (item == null)
            {
                return folders.Length;
            }

            return ((Folder)item).Files.Length;
        }

        public override NSObject GetChild(NSOutlineView outlineView, nint childIndex, NSObject item)
        {
            if (item == null)
            {
                return folders[childIndex];
            }

            return ((Folder)item).Files[childIndex];
        }

        public override bool ItemExpandable(NSOutlineView outlineView, NSObject item) => item is Folder;
    }
}
