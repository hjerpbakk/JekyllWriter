using System;
using JekyllWriter.Model;

namespace JekyllWriter.Parsers
{
    public class PostParser
    {
        readonly PreambleParser preambleParser;

        public PostParser()
        {
            preambleParser = new PreambleParser();
        }

        public Post Parse(string fullText, File file) {
            var indexOfFirstLineBreak = fullText.IndexOf('\n');
            var indexOfPreambleEnd = fullText.IndexOf("---\n", indexOfFirstLineBreak, StringComparison.InvariantCulture);
            if (indexOfPreambleEnd == -1)
            {
                throw CreateNotValidPostException(fullText);
            }

            var preambleEnd = indexOfPreambleEnd + 3;
            var preambleText = fullText.Substring(0, preambleEnd).Trim('-');
            var preamble = preambleParser.Parse(preambleText);
            var contentStart = preambleEnd + 2;
            var contentText = fullText.Substring(contentStart);
            var content = new Content(contentText);
            return new Post(preamble, content, file);
        }

        static ArgumentException CreateNotValidPostException(string fullText) => new ArgumentException($"Text is not a valid Jekyll post:\n{fullText}", nameof(fullText));
    }
}
