namespace ArgParser.Core.Help
{
    public class HelpHints : IHelpHints
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string ShortDescription { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public string Synopsis { get; set; }

        /// <inheritdoc />
        public string Url { get; set; }
    }
}