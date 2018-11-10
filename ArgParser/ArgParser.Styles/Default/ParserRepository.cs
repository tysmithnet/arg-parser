using System;
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParserRepository : IParserRepository
    {
        public string RootId { get; protected internal set; }

        public Parser Root => Parsers[RootId];

        public Parser Create(string id, bool isRoot = false)
        {
            CheckCreateParameters(id, isRoot);
            var parser = new Parser(id);
            if (FirstAdded == null)
                FirstAdded = parser;
            Parsers.Add(id, parser);
            return parser;
        }

        private void CheckCreateParameters(string id, bool isRoot)
        {
            if (Parsers.ContainsKey(id))
                throw new ArgumentException($"Parser already exists with id={id}");
            if (isRoot)
            {
                if (!RootId.IsNullOrWhiteSpace())
                    RootId = id;
                else
                    throw new InvalidOperationException(
                        $"Root parser is already set to id={RootId}, but attempting to set it to {id}");
            }
        }

        public Parser<T> Create<T>(string id, bool isRoot = false)
        {
            CheckCreateParameters(id, isRoot);
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