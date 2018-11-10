using System;
using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ContextBuilder
    {
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

        public ParserBuilder AddRoot(Action<ParserHelpBuilder> helpSetupCallback = null)
        {
            var parser = ParserRepository.Create("root", true);
            HierarchyRepository.AddParser("root");
            if (helpSetupCallback != null)
            {
                var builder = new ParserHelpBuilder(parser);
                helpSetupCallback(builder);
                parser.Help = builder.Build();
            }

            return new ParserBuilder(this, parser);
        }

        public ParserBuilder<T> AddRoot<T>(Action<ParserHelpBuilder> helpSetupCallback = null)
        {
            var parser = ParserRepository.Create<T>("root", true);
            HierarchyRepository.AddParser("root");

            if (helpSetupCallback != null)
            {
                var builder = new ParserHelpBuilder(parser);
                helpSetupCallback(builder);
                parser.Help = builder.Build();
            }

            return new ParserBuilder<T>(this, parser);
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

        public Context BuildContext() => new Context
        {
            HierarchyRepository = HierarchyRepository,
            ParserRepository = ParserRepository
        };

        public ContextBuilder CreateParentChildRelationship(string child)
        {
            HierarchyRepository.EstablishParentChildRelationship(ParserRepository.RootId, child);
            return this;
        }

        public ContextBuilder CreateParentChildRelationship(string parent, string child)
        {
            HierarchyRepository.EstablishParentChildRelationship(parent, child);
            return this;
        }

        public IParseResult Parse(string[] args)
        {
            var context = BuildContext();
            var root = context.HierarchyRepository.Root;
            var strat = new ParseStrategy();
            return strat.Parse(args, context);
        }

        public IParseResult Parse(string parser, string[] args)
        {
            var strat = new ParseStrategy(parser);
            var context = BuildContext();
            return strat.Parse(args, context);
        }

        protected internal HierarchyRepository HierarchyRepository { get; set; } = new HierarchyRepository();
        protected internal ParserRepository ParserRepository { get; set; } = new ParserRepository();
    }
}