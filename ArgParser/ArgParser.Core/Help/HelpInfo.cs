namespace ArgParser.Core.Help
{
    public class HelpInfo : IHelp
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string ShortDescription { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public string Version { get; set; }

        /// <inheritdoc />
        public string Synopsis { get; set; }

        /// <inheritdoc />
        public string Url { get; set; }
    }
}