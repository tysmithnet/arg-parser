using System;
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParserRepository : IParserRepository
    {
        protected internal Parser FirstAdded { get; set; }
        
        protected internal Dictionary<string, Parser> Parsers { get; set; } = new Dictionary<string, Parser>();

        public Parser Create(string id)
        {
            if(Parsers.ContainsKey(id))
                throw new ArgumentException($"Parser already exists with id={id}");
            var parser = new Parser(id);
            if (FirstAdded == null)
                FirstAdded = parser;
            Parsers.Add(id, parser);
            return parser;
        }

        public Parser GetParser(string id)
        {
            if(!Parsers.ContainsKey(id))
                throw new KeyNotFoundException($"Unable to find parser with id={id}, are you sure it was added and you are using the correct id?");
            return Parsers[id];
        }

        public IEnumerable<Parser> GetAll()
        {
            return Parsers.Values;
        }
    }
}