namespace ArgParser.Core.Help
{
    public class GenericHelp : IGenericHelp
    {
        /// <inheritdoc />
        public string Version { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string ShortDescription { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public string Author { get; set; }
    }
}