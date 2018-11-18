using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class PotentialConsumerRequest
    {
        public ChainIdentificationResult ChainIdentificationResult { get; set; }
        public IterationInfo Info { get; set; }
        public object Instance { get; set; }
    }
}