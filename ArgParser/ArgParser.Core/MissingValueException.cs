namespace ArgParser.Core
{
    public class MissingValueException : ParseException
    {
        public MissingValueException(Parameter parameter, string message) : base(message)
        {
            Parameter = parameter.ThrowIfArgumentNull(nameof(parameter));
        }

        public Parameter Parameter { get; set; }
    }
}