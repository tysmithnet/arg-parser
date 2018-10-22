using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public interface IExample
    {
        string Name { get; }
        string[] Usage { get; }
        string ShortDescription { get; }
    }

    public class DefaultExample : IExample
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string[] Usage { get; set; }

        /// <inheritdoc />
        public string ShortDescription { get; set; }
    }

    public interface IGenericHelp
    {
        string Version { get; }
        string Name { get; }
        string ShortDescription { get; }
        string Description { get; }
        string Author { get; }
        IList<IExample> Examples { get; }
    }
}