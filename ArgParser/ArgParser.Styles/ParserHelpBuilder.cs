using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ParserHelpBuilder
    {
        public ParserHelpBuilder(Parser parser)
        {
            Parser = parser.ThrowIfArgumentNull(nameof(parser));
        }

        public ParserHelpBuilder AddExample(string name, string description, string usage, string result)
        {
            Help.AddExample(new Example
            {
                Name = name,
                ShortDescription = description,
                Usage = usage,
                Result = result
            });
            return this;
        }

        public ParserHelp Build() => Help;

        public ParserHelpBuilder SetLongDescription(string desc)
        {
            Help.LongDescription = desc;
            return this;
        }

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