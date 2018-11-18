namespace ArgParser.Core
{
    public class MissingValueException : ParseException
    {
        public Parameter Parameter { get; set; }
        public MissingValueException(Parameter parameter, string message) : base(message)
        {
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
        }
    }
}