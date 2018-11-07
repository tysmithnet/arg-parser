using ArgParser.Core;
using ArgParser.Core.Help;

namespace ArgParser.Styles.Default
{
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