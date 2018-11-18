namespace ArgParser.Styles.ParseStrategy
{
    public interface IParserChainIdentificationStrategy : IParseStrategyUnit
    {
        ChainIdentificationResult Identify(ChainIdentificationRequest request);
    }
}