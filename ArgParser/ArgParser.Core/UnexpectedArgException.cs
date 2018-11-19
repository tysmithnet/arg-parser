namespace ArgParser.Core
{
    public class UnexpectedArgException : ParseException
    {
        public UnexpectedArgException(IterationInfo location) : base($"Encountered unexpected argument={location.Current}")
        {
            Location = location.ThrowIfArgumentNull(nameof(location));
        }

        public IterationInfo Location { get; protected internal set; }
    }
}