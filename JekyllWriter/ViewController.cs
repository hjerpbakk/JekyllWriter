using System;
using System.Linq;
using System.Collections.Generic;
using AppKit;
using Foundation;
using JekyllWriter.Views.Posts;
using JekyllWriter.Files;
using JekyllWriter.Model;
using JekyllWriter.ViewControllers;

namespace JekyllWriter
{
    public partial class ViewController : NSViewController
    {
        readonly JekyllFileSystem jekyllFileSystem;

        PostController postController;

        public ViewController(IntPtr handle) : base(handle)
        {
            // TODO: From install or something
            // TODO: Sandboxing
            jekyllFileSystem = new JekyllFileSystem("/Users/sankra/projects/sankra.github.io");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // TODO: Selection and UX etc. Would like to mimic Finder.
            postsView.DataSource = new PostsDataSouce(jekyllFileSystem.GetDrafts(), jekyllFileSystem.GetPosts());
            postsView.Delegate = new PostsDelegate(SelectionIsChanging, SelctionChanged);
            postsView.ExpandItem(null, true);
        }

        public override void ViewWillDisappear()
        {
            SelectionIsChanging();
            base.ViewWillDisappear();
        }

        public override NSObject RepresentedObject
        {
            get { return base.RepresentedObject; }
            set { base.RepresentedObject = value; }
        }

        void SelectionIsChanging() {
            if (postController != null)
            {
                postController.Dispose();
                postController = null;
            }
        }

        void SelctionChanged(File file) {
            try
            {
                postController = new PostController(jekyllFileSystem, textView, file);
            }
            catch (Exception ex)
            {
                var alert = new NSAlert
                {
                    MessageText = "Something went wrong",
                    InformativeText = ex.Message
                };
                alert.RunModal();
            }
        }
    }
}
