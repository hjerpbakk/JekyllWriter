using System;
using System.Collections.Generic;
using JekyllWriter.Parsers;

namespace JekyllWriter.Model
{
    public class Preamble {
        readonly PreambleParser parser;

        public Preamble()
        {
            parser = new PreambleParser(); 
        }

        public List<string> categories { get; set; }
        public string layout { get; set; }
        public string title { get; set; }
        public string meta_description { get; set; }
        public DateTime date { get; set; }
        public List<string> tags { get; set; }

        public override string ToString() => parser.Parse(this);
    }
}
