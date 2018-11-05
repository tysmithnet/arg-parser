namespace ArgParser.Core
{
    public class ConsumptionResult
    {
        public ConsumptionResult(IterationInfo originalInfo, int numConsumed)
        {
            NumConsumed = numConsumed;
            Info = originalInfo.Consume(numConsumed);
        }

        public IterationInfo Info { get; protected internal set; }

        public int NumConsumed { get; protected internal set; }
    }
}