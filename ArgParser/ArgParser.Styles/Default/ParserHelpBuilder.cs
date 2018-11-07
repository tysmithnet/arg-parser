using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Default
{
    public class ParserHelpBuilder
    {
        public ParserHelpBuilder(Parser parser)
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        public ParserHelp Build() => Help;

        public ParserHelpBuilder SetName(string name)
        {
            Help.Name = name;
            return this;
        }

        public ParserHelpBuilder SetShortDescription(string desc)
        {
            Help.ShortDescription = desc;
            return this;
        }

        public ParserHelpBuilder SetVersion(string version)
        {
            Help.Version = version;
            return this;
        }

        protected internal ParserHelp Help { get; set; } = new ParserHelp();
        protected internal Parser Parser { get; set; }
    }
}