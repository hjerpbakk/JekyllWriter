using System;
using System.Linq;
using System.Collections.Generic;
using AppKit;
using Foundation;
using JekyllWriter.Views.Posts;
using JekyllWriter.Files;
using JekyllWriter.Model;

namespace JekyllWriter
{
    public partial class ViewController : NSViewController
    {
        readonly JekyllFileSystem jekyllFileSystem;

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
            postsView.Delegate = new PostsDelegate(SelctionChanged);
            postsView.ExpandItem(null, true);


        }

        public override NSObject RepresentedObject
        {
            get { return base.RepresentedObject; }
            set { base.RepresentedObject = value; }
        }

        void SelctionChanged(PostFile file) {
            try
            {
                var post = jekyllFileSystem.ReadPost(file);
                textView.Value = post.Content;
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
