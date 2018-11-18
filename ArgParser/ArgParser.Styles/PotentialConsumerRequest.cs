using ArgParser.Core;
using ArgParser.Core.Extensions;

namespace ArgParser.Styles
{
    public class PotentialConsumerRequest
    {
        public ChainIdentificationResult ChainIdentificationResult { get; set; }
        public IterationInfo Info { get; set; }
        public object Instance { get; set; }

        public PotentialConsumerRequest(ChainIdentificationResult chainIdentificationResult, IterationInfo info,
            object instance)
        {
            ChainIdentificationResult = chainIdentificationResult.ThrowIfArgumentNull(nameof(chainIdentificationResult));
            Info = info.ThrowIfArgumentNull(nameof(info));
            Instance = instance.ThrowIfArgumentNull(nameof(instance));
        }
    }
}