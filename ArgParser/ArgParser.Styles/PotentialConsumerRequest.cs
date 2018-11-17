using ArgParser.Core;

namespace ArgParser.Styles
{
    public class PotentialConsumerRequest
    {
        public ChainIdentificationResult ChainIdentificationResult { get; set; }
        public IterationInfo Info { get; set; }
        public object Instance { get; set; }
    }
}