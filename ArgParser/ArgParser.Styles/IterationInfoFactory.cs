using ArgParser.Core;

namespace ArgParser.Styles
{
    public class IterationInfoFactory : IIterationInfoFactory
    {
        public IterationInfo Create(IterationInfoRequest request)
        {
            var consumed = request.ChainIdentificationResult.ConsumedArgs;
            return new IterationInfo(request.MutatedArgs, consumed.Length); // todo: only consume those that still remain in mutated
        }
    }
}