using System;
using System.Threading;
using AppKit;
using JekyllWriter.Files;
using JekyllWriter.Model;
using Timer = System.Timers.Timer;

namespace JekyllWriter.ViewControllers
{
    public class PostController : IDisposable
    {
        // TODO: Hva er rett lagrings-sleep
        const double SaveWaitPeriod = 2000D;

        readonly JekyllFileSystem jekyllFileSystem;
        readonly NSTextView textView;

        Post post;

        readonly Timer timer;
        readonly ManualResetEventSlim manualResetEvent;
        readonly Thread fileThread;

        // TODO: Lagring må tas ut herfra... Kun kobling mot view her, ditto i den andre kontrolleren
        public PostController(JekyllFileSystem jekyllFileSystem, NSTextView textView, Post post)
        {
            this.jekyllFileSystem = jekyllFileSystem ?? throw new ArgumentNullException(nameof(jekyllFileSystem));
            this.textView = textView ?? throw new ArgumentNullException(nameof(textView));
            this.post = post;

            timer = new Timer(SaveWaitPeriod)
            {
                AutoReset = false,
            };
            timer.Elapsed += Timer_Elapsed;
            textView.Value = post.Content.Text;
            textView.TextDidChange += TextView_TextDidChange;

            manualResetEvent = new ManualResetEventSlim(false);
            fileThread = new Thread(new ThreadStart(FileThread));
            fileThread.Start();
        }

        public void Dispose()
        {
            fileThread.Abort();
            Save();
        }

        void Save() {
            var content = textView.Value;
            post = post.UpdatedPost(content);
            jekyllFileSystem.SavePost(post);
        }

#pragma warning disable RECS0135 // Function does not reach its end or a 'return' statement by any of possible execution paths
        void FileThread()
        {
            while (true) {
                manualResetEvent.Wait();
                string content = null;
                textView.InvokeOnMainThread(() => content = textView.Value);
                post = post.UpdatedPost(content);
                jekyllFileSystem.SavePost(post);
                manualResetEvent.Reset();
            }
        }
#pragma warning restore RECS0135 // Function does not reach its end or a 'return' statement by any of possible execution paths

        void TextView_TextDidChange(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            manualResetEvent.Set();
        }
    }
}
