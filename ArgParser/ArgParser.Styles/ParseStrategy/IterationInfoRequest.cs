using System;

namespace ArgParser.Styles.ParseStrategy
{
    public class IterationInfoRequest
    {
        public ChainIdentificationResult ChainIdentificationResult { get; set; }
        public string[] MutatedArgs { get; set; }
        public string[] OriginalArgs { get; set; }

        public IterationInfoRequest(ChainIdentificationResult chainIdentificationResult, string[] mutatedArgs,
            string[] originalArgs)
        {
            ChainIdentificationResult = chainIdentificationResult ??
                                        throw new ArgumentNullException(nameof(chainIdentificationResult));
            MutatedArgs = mutatedArgs ?? throw new ArgumentNullException(nameof(mutatedArgs));
            OriginalArgs = originalArgs ?? throw new ArgumentNullException(nameof(originalArgs));
        }
    }
}