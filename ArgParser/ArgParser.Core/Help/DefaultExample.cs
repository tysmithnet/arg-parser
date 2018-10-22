namespace ArgParser.Core.Help
{
    public class DefaultExample : IExample
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string ShortDescription { get; set; }

        /// <inheritdoc />
        public string[] Usage { get; set; }
    }
}