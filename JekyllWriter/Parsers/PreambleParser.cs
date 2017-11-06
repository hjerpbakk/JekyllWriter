using System;
using JekyllWriter.Model;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace JekyllWriter.Parsers
{
    public class PreambleParser
    {
        readonly Deserializer deserializer;
        readonly Serializer serializer;

        public PreambleParser()
        {
            var namingConvention = new UnderscoredNamingConvention();
            deserializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention)
                .IgnoreUnmatchedProperties()
                .Build();

            serializer = new SerializerBuilder()
                .WithNamingConvention(namingConvention)
                .Build();
        }

        public Preamble Parse(string preamble) => deserializer.Deserialize<Preamble>(preamble);

        public string Parse(Preamble preamble) => serializer.Serialize(preamble);
    }
}
