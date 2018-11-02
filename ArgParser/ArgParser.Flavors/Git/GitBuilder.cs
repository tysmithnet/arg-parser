using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitBuilder
    {
        public ParserBuilder AddParser(string name) => new ParserBuilder(name, this, Context);

        public GitBuilder AddSubCommand(string parent, string child)
        {
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