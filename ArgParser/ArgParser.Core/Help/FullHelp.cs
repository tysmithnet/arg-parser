using System.Collections.Generic;
using ArgParser.Core.Extensions;

namespace ArgParser.Core.Help
{
    public class FullHelp : SimpleHelp
    {
        public void AddExample(Example example)
        {
            example.ThrowIfArgumentNull(nameof(example));
            if (Examples.Contains(example))
                return;
            Examples.Add(example);
        }

        public IList<Example> Examples { get; protected internal set; } = new List<Example>();
        public string LongDescription { get; set; }
    }
}