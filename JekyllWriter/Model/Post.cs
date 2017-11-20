namespace JekyllWriter.Model
{
    public struct Post
    {
        public Post(Preamble preamble, Content content, SourceFile file)
        {
            Preamble = preamble;
            Content = content;
            File = file;
        }

        public Preamble Preamble { get; }
        public Content Content { get; }
        public SourceFile File { get; }

        public Post UpdatedPost(string content) => new Post(Preamble, new Content(content), File);

        public override string ToString() => $"---\n{Preamble}---\n\n{Content}";
    }
}
