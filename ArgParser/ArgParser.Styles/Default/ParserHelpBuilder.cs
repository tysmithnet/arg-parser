using System;
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

    public class ParameterHelpBuilder
    {
        protected internal ParameterHelp Help { get; set; } = new ParameterHelp();
        protected internal Parameter Parameter { get; set; }

        public ParameterHelpBuilder(Parameter parameter)
        {
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
        }

        public ParameterHelp Build()
        {
            return Help;
        }

        public ParameterHelpBuilder SetName(string name)
        {
            Help.Name = name;
            return this;
        }

        public ParameterHelpBuilder SetShortDescription(string desc)
        {
            Help.ShortDescription = desc;
            return this;
        }
    }
}