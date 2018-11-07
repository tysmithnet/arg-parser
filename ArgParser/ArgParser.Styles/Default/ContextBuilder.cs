using System;
using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Default
{
    public class ContextBuilder
    {
        public ParserBuilder AddParser(string id, Action<ParserHelp> helpSetupCallback = null)
        {
            var parser = ParserRepository.Create(id);
            HierarchyRepository.AddParser(id);
            if (helpSetupCallback != null)
            {
                parser.Help = new ParserHelp();
                helpSetupCallback(parser.Help);
            }

            return new ParserBuilder(this, parser);
        }

        public ParserBuilder<T> AddParser<T>(string id, Action<ParserHelp> helpSetupCallback = null)
        {
            var parser = ParserRepository.Create<T>(id);
            HierarchyRepository.AddParser(id);

            if (helpSetupCallback != null)
            {
                parser.Help = new ParserHelp();
                helpSetupCallback(parser.Help);
            }

            return new ParserBuilder<T>(this, parser);
        }

        public Context BuildContext() => new Context
        {
            HierarchyRepository = HierarchyRepository,
            ParserRepository = ParserRepository
        };

        public ContextBuilder CreateParentChildRelationship(string parent, string child)
        {
            HierarchyRepository.EstablishParentChildRelationship(parent, child);
            return this;
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