using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class ChainIdentificationResult
    {
        public IEnumerable<Parser> Chain { get; set; }
        public string[] ConsumedArgs { get; set; }
        public Parser IdentifiedParser { get; set; }
    }
}