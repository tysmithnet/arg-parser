using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class ChainIdentificationResult
    {
        public IList<Parser> Chain { get; set; }
        public string[] ConsumedArgs { get; set; }
        public Parser IdentifiedParser => Chain.Last();

        public ChainIdentificationResult(IEnumerable<Parser> chain, string[] consumedArgs)
        {
            Chain = chain.ThrowIfArgumentNull(nameof(chain)).ToList();
            ConsumedArgs = consumedArgs ?? throw new ArgumentNullException(nameof(consumedArgs));
        }
    }
}