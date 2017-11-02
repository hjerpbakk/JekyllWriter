using System;

namespace JekyllWriter.Model
{
    public class Post
    {
        public Post(string fullText, PostFile file)
        {
            if (fullText == null)
            {
                throw new ArgumentNullException(nameof(fullText));
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            Name = file.Name;
            Path = file.Path;
            var parsedPost = ParsePost(fullText);
            Preamble = parsedPost.Preamble;
            Content = parsedPost.Content;
        }

        public string Name { get; }
        public string Path { get; }
        public string Preamble { get; }
        public string Content { get; }

        (string Preamble, string Content) ParsePost(string fullText)
        {
            var indexOfFirstLineBreak = fullText.IndexOf('\n');
            var indexOfPreambleEnd = fullText.IndexOf("---\n", indexOfFirstLineBreak, StringComparison.InvariantCulture);
            if (indexOfPreambleEnd == -1)
            {
                throw CreateNotValidPostException(fullText);
            }

            var preambleEnd = indexOfPreambleEnd + 3;
            var preamble = fullText.Substring(0, preambleEnd);
            var contentStart = preambleEnd + 2;
            var content = fullText.Substring(contentStart);
            return (preamble, content);
        }

        ArgumentException CreateNotValidPostException(string fullText) => new ArgumentException($"Text is not a valid Jekyll post:\n{fullText}", nameof(fullText));
    }
}
