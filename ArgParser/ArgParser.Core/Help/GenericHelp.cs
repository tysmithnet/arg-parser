using System.Collections.Generic;

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

        /// <inheritdoc />
        public IList<IExample> Examples { get; set; }
    }
}