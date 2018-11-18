using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles
{
    public class ChainIdentificationResult
    {
        public IEnumerable<Parser> Chain { get; set; }
        public Parser IdentifiedParser { get; set; }
        public string[] ConsumedArgs { get; set; }
    }
}