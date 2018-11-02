using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitBuilder
    {
        public ParserBuilder AddParser(string name)
        {
            name.ThrowIfArgumentNull(nameof(name));
            var parser = Context.ParserRepository.Create(name);
            parser.Context = Context;
            return new ParserBuilder(name, this, Context);
        }

        public ParserBuilder AddParser<T>(string name)
        {
            name.ThrowIfArgumentNull(nameof(name));
            var parser = Context.ParserRepository.Create(name);
            parser.Context = Context;
            return new ParserBuilder<T>(name, this, Context);
        }

        public GitBuilder AddSubCommand(string parent, string child)
        {
            parent.ThrowIfArgumentNull(nameof(parent));
            child.ThrowIfArgumentNull(nameof(child));
            Context.ParserRepository.EstablishParentChildRelationship(parent, child);
            return this;
        }

        public IParseResult Parse(string parserName, string[] args)
        {
            parserName.ThrowIfArgumentNull(nameof(parserName));
            args.ThrowIfArgumentNull(nameof(args));
            var parser = Context.ParserRepository.Get(parserName);
            return parser.Parse(args);
        }

        public IGitContext Context { get; set; } = new GitContext();
    }
}