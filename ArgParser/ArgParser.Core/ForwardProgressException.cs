namespace ArgParser.Core
{
    public class ForwardProgressException : ParseException
    {
        public ForwardProgressException(IterationInfo location, string message) : base(message)
        {
            Location = location.ThrowIfArgumentNull(nameof(location));
        }

        public IterationInfo Location { get; protected internal set; }
    }
}