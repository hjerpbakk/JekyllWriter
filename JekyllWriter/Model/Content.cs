namespace JekyllWriter.Model
{
    public struct Content
    { 
        public Content(string text) {
            Text = text;
        }

        public string Text { get; }

        public override string ToString() => Text;
    }
}
