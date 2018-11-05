using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParseResult : IParseResult
    {
        protected internal IList<object> ParsedInstances { get; set; }
        protected internal IList<ParseException> ParseExceptions { get; set; }

        public ParseResult(IEnumerable<object> parsedInstances, IEnumerable<ParseException> parseExceptions)
        {
            ParsedInstances = parsedInstances.PreventNull().ToList();
            ParseExceptions = parseExceptions.PreventNull().ToList();
        }

        public void When<T>(Action<T> handler)
        {
            foreach (var instance in ParsedInstances.OfType<T>())
            {
                handler(instance);
            }
        }

        public void WhenError(Action<IEnumerable<ParseException>> handler)
        {
            if (ParseExceptions.Any())
                handler(ParseExceptions);
        }
    }

    public class ParseStrategy : IParseStrategy
    {
        public string RootParserId { get; protected internal set; }

        public ParseStrategy(string rootParserId)
        {
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
        }

        public virtual IParseResult Parse(string[] args, IContext context)
        {
            var parsers = IdentifyRelevantParsers(args, context).ToList();
            var factoryFuncs = IdentifyFactoryFunctions(args, context).ToList();
            try
            {
                
                return new ParseResult(results, null);
            }
            catch (ParseException e)
            {
                return new ParseResult(null, e.ToEnumerableOfOne());
            }
        }

        public virtual IEnumerable<Func<object>> IdentifyFactoryFunctions(string[] args, IContext context)
        {
            var parsers = IdentifyRelevantParsers(args, context).ToList();
            var factoryFuncs = parsers.SelectMany(x => x.FactoryFunctions).ToList();
            return factoryFuncs;
        }

        public virtual IEnumerable<Parser> IdentifyRelevantParsers(string[] args, IContext context)
        {
            var ids = new List<string>();
            ids.Add(RootParserId);
            for (int i = 1; i < args.Length; i++)
            {
                string left = i == 0 ? RootParserId : args[i - 1];
                string right = args[i];
                if (context.HierarchyRepository.IsParent(left, right))
                {
                    ids.Add(right);
                }
            }

            return ids.Select(x => context.ParserRepository.Get(x));
        }
    }
}