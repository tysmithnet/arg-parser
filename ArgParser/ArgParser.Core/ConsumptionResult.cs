namespace ArgParser.Core
{
    public class ConsumptionResult
    {
        public ConsumptionResult(IterationInfo info, int numConsumed)
        {
            NumConsumed = numConsumed;
            Info = info.Consume(numConsumed);
        }

        public int NumConsumed { get; protected internal set; }
        public IterationInfo Info { get; protected internal set; }
    }
}