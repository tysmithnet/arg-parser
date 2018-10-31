using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public class GenericHelp : IGenericHelp
    {
            
        public string Author { get; set; }

            
        public string Description { get; set; }

            
        public IList<IExample> Examples { get; set; }

            
        public string Name { get; set; }

            
        public string ShortDescription { get; set; }

            
        public string Version { get; set; }
    }
}