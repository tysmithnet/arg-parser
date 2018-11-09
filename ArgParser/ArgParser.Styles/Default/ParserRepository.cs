using System;
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParserRepository : IParserRepository
    {
        public Parser Create(string id)
        {
            if (Parsers.ContainsKey(id))
                throw new ArgumentException($"Parser already exists with id={id}");
            var parser = new Parser(id);
            if (FirstAdded == null)
                FirstAdded = parser;
            Parsers.Add(id, parser);
            return parser;
        }

        public Parser<T> Create<T>(string id)
        {
            if (Parsers.ContainsKey(id)) // todo: duplicate code
                throw new ArgumentException($"Parser already exists with id={id}");
            var parser = new Parser<T>(id);
            if (FirstAdded == null)
                FirstAdded = parser;
            Parsers.Add(id, parser);
            return parser;
        }

        public Parser Get(string id)
        {
            if (!Parsers.ContainsKey(id))
                throw new KeyNotFoundException(
                    $"Unable to find parser with id={id}, are you sure it was added and you are using the correct id?");
            return Parsers[id];
        }

        public Parser<T> Get<T>(string id)
        {
            var parser = Get(id);
            return (Parser<T>) parser;
        }

        public IEnumerable<Parser> GetAll() => Parsers.Values;

        protected internal Parser FirstAdded { get; set; }

        protected internal Dictionary<string, Parser> Parsers { get; set; } = new Dictionary<string, Parser>();
    }
}