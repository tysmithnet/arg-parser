namespace ArgParser.Styles
{
    public interface IPotentialConsumerStrategy
    {
        PotentialConsumerResult IdentifyPotentialConsumer(PotentialConsumerRequest request);
    }
}