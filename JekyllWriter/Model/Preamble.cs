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

        public List<string> Categories { get; set; }
        public string Layout { get; set; }
        public string Title { get; set; }
        // TODO: Hvorfor dukker ikke denne opp??
        public string Meta_Description { get; set; }
        public DateTime Date { get; set; }
        public List<string> Tags { get; set; }

        public override string ToString() => parser.Parse(this);
    }
}
