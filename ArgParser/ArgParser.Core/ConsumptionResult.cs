namespace ArgParser.Core
{
    public class ConsumptionResult
    {
        public ConsumptionResult(IterationInfo originalInfo, int numConsumed, Parameter consumingParameter)
        {
            NumConsumed = numConsumed;
            Info = originalInfo.Consume(numConsumed);

            // can be null, means there was no consumption
            ConsumingParameter = consumingParameter;
        }

        public IterationInfo Info { get; protected internal set; }
        public Parameter ConsumingParameter { get; protected  internal set; }
        public int NumConsumed { get; protected internal set; }
    }
}