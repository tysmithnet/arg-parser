namespace ArgParser.Styles
{
    public interface IParserChainIdentificationStrategy : IParseStrategyUnit
    {
        ChainIdentificationResult Identify(ChainIdentificationRequest request);
    }
}