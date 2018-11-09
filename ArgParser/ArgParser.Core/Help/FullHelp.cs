using System.Collections.Generic;

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

        public IList<Example> Examples { get; set; } = new List<Example>();
        public string LongDescription { get; set; }
    }
}