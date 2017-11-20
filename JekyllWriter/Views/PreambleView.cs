using System;
using System.Linq;
using AppKit;
using CoreGraphics;
using JekyllWriter.Model;

namespace JekyllWriter.Views
{
    public class PreambleView
    {
        readonly NSStackView stackView;

        // TODO: Hvordan få alignet kollonnene?
        public PreambleView(NSStackView stackView)
        {
            this.stackView = stackView;
            var properties = typeof(Preamble).GetProperties();

           // var chars = properties.Max(p => p.Name.Length);


            var entries = new NSTextField[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                var name = properties[i].Name;
                //name = name.PadLeft(chars, ' ');
                var entry = AddField(name);
                entries[i] = entry;
            }

            //stackView.LayoutSubtreeIfNeeded();

            //var maxX = -1;
            //foreach (var entry in entries)
            //{
            //    var x = entry.Frame.X;
            //    if (x > maxX)
            //    {
            //        maxX = (int)x;
            //    }
            //}

            //foreach (var entry in entries)
            //{
            //    var prevFrame = entry.Frame;
            //    entry.Frame = new CGRect(maxX, prevFrame.Y, prevFrame.Width - (maxX - prevFrame.X), prevFrame.Height);
            //}

            //stackView.LayoutSubtreeIfNeeded();
        }

        NSTextField AddField(string label) {
            var formStackView = new NSStackView
            {
                Orientation = NSUserInterfaceLayoutOrientation.Horizontal,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            var labelView = new NSTextField
            {
                Editable = false,
                Bezeled = false,
                DrawsBackground = false,
                Selectable = false,
                StringValue = label,
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            var entryField = new NSTextField
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            formStackView.AddView(labelView, NSStackViewGravity.Leading);
            formStackView.AddView(entryField, NSStackViewGravity.Trailing);

            stackView.AddView(formStackView, NSStackViewGravity.Top);
            return entryField;
        }
    }
}
