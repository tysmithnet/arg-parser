using System;
using ArgParser.Core;

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
            ChainIdentificationResult = chainIdentificationResult.ThrowIfArgumentNull(nameof(chainIdentificationResult));
            MutatedArgs = mutatedArgs.ThrowIfArgumentNull(nameof(mutatedArgs));
            OriginalArgs = originalArgs.ThrowIfArgumentNull(nameof(originalArgs));
        }
    }
}