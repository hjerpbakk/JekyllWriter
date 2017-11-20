﻿using System;
using System.Linq;
using System.Collections.Generic;
using AppKit;
using Foundation;
using JekyllWriter.Views.Posts;
using JekyllWriter.Files;
using JekyllWriter.Model;
using JekyllWriter.ViewControllers;
using CoreGraphics;
using JekyllWriter.Views;

namespace JekyllWriter
{
    public partial class ViewController : NSViewController
    {
        readonly JekyllFileSystem jekyllFileSystem;

        PostController postController;
        PreambleController preambleController;

        PreambleView preambleView;

        public ViewController(IntPtr handle) : base(handle)
        {
            // TODO: From install or something
            // TODO: Sandboxing
            jekyllFileSystem = new JekyllFileSystem("/Users/sankra/projects/sankra.github.io");
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            // Apply the Dark Interface Appearance
            //View.Window.Appearance = NSAppearance.GetAppearance(NSAppearance.NameVibrantDark);

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // TODO: Selection and UX etc. Would like to mimic Finder. Master detail?
            postsView.DataSource = new PostsDataSouce(jekyllFileSystem.GetDrafts(), jekyllFileSystem.GetPosts());
            postsView.Delegate = new PostsDelegate(SelectionIsChanging, SelctionChanged);
            postsView.ExpandItem(null, true);

            stackView.RemoveView(textViewParent);
            preambleView = new PreambleView(stackView);
            stackView.AddView(textViewParent, NSStackViewGravity.Bottom);
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

        void SelctionChanged(SourceFile file) {
            try
            {
                var post = jekyllFileSystem.ReadPost(file);
                var preamble = post.Preamble;
                postController = new PostController(jekyllFileSystem, textView, post);

                // TODO
                //preambleController = new PreambleController(preambleForm, preamble);

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
