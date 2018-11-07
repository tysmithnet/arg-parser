using System.Collections.Generic;

namespace ArgParser.Core.Help
{
    public class FullHelp : SimpleHelp
    {
        public string LongDescription { get; set; }
        public IList<Example> Examples { get; set; } = new List<Example>();

        public void AddExample(Example example)
        {
            example.ThrowIfArgumentNull(nameof(example));
            if (Examples.Contains(example))
                return;
            Examples.Add(example);
        }
    }
}