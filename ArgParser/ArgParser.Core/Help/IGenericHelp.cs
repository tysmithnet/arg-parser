using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public interface IGenericHelp
    {
        string Author { get; }
        string Description { get; }
        IList<IExample> Examples { get; }
        string Name { get; }
        string ShortDescription { get; }
        string Version { get; }
    }
}