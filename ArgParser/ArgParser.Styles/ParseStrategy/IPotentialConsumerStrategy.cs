namespace ArgParser.Styles.ParseStrategy
{
    public interface IPotentialConsumerStrategy : IParseStrategyUnit
    {
        PotentialConsumerResult IdentifyPotentialConsumer(PotentialConsumerRequest request);
    }
}