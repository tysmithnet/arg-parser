using ArgParser.Core;

namespace ArgParser.Styles.Default
{
    public class ParameterBuilder
    {
        public ParameterBuilder(ParserBuilder parent, Parameter parameter)
        {
            Finish = parent.ThrowIfArgumentNull(nameof(parent));
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
        }

        public ParserBuilder Finish { get; protected internal set; }
        public Parameter Parameter { get; protected internal set; }
    }
}