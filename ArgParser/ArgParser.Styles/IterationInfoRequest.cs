namespace ArgParser.Styles
{
    public class IterationInfoRequest
    {
        public string[] OriginalArgs { get; set; }
        public string[] MutatedArgs { get; set; }
        public ChainIdentificationResult ChainIdentificationResult { get; set; }
    }
}