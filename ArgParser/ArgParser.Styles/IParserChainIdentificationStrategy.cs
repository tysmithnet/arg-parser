namespace ArgParser.Styles
{
    public interface IParserChainIdentificationStrategy
    {
        ChainIdentificationResult Identify(ChainIdentificationRequest request);
    }
}