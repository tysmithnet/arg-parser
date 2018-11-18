using System;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ContextBuilder
    {
        public ContextBuilder(string rootParserId)
        {
            RootParserId = rootParserId.ThrowIfArgumentNull(nameof(rootParserId));
            Context = new Context
            {
                HierarchyRepository = HierarchyRepository,
                ParserRepository = ParserRepository
            };
        }

        public event EventHandler<ParseStrategyCreatedEventArgs> ParseStrategyCreated;

        public ParserBuilder AddParser(string id, Action<ParserHelpBuilder> helpSetupCallback = null)
        {
            var parser = ParserRepository.Create(id);
            HierarchyRepository.AddParser(id);
            if (helpSetupCallback != null)
            {
                var builder = new ParserHelpBuilder(parser);
                helpSetupCallback(builder);
                parser.Help = builder.Build();
            }

            return new ParserBuilder(this, parser);
        }

        public ParserBuilder<T> AddParser<T>(string id, Action<ParserHelpBuilder> helpSetupCallback = null)
        {
            var parser = ParserRepository.Create<T>(id);
            HierarchyRepository.AddParser(id);

            if (helpSetupCallback != null)
            {
                var builder = new ParserHelpBuilder(parser);
                helpSetupCallback(builder);
                parser.Help = builder.Build();
            }

            return new ParserBuilder<T>(this, parser);
        }

        public ContextBuilder CreateParentChildRelationship(string parent, string child)
        {
            HierarchyRepository.EstablishParentChildRelationship(parent, child);
            return this;
        }

        public IParseResult Parse(string[] args)
        {
            var context = Context;
            var strat = new ParseStrategy(context, RootParserId);
            OnParseStrategyCreated(strat);
            return strat.Parse(args, context);
        }

        protected virtual void OnParseStrategyCreated(ParseStrategy parseStrategy)
        {
            ParseStrategyCreated?.Invoke(this, new ParseStrategyCreatedEventArgs
            {
                ParseStrategy = parseStrategy.ThrowIfArgumentNull(nameof(parseStrategy))
            });
        }

        public Context Context { get; protected internal set; }
        protected internal HierarchyRepository HierarchyRepository { get; set; } = new HierarchyRepository();
        protected internal ParserRepository ParserRepository { get; set; } = new ParserRepository();
        protected internal string RootParserId { get; set; }
    }
}