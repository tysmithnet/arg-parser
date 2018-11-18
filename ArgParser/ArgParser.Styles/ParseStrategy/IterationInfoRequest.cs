namespace ArgParser.Styles.ParseStrategy
{
    public class IterationInfoRequest
    {
        public ChainIdentificationResult ChainIdentificationResult { get; set; }
        public string[] MutatedArgs { get; set; }
        public string[] OriginalArgs { get; set; }
    }
}