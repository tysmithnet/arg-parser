namespace ArgParser.Styles
{
    public interface IPotentialConsumerStrategy : IParseStrategyUnit
    {
        PotentialConsumerResult IdentifyPotentialConsumer(PotentialConsumerRequest request);
    }
}