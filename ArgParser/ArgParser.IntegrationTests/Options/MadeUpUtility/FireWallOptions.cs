namespace ArgParser.IntegrationTests.Options.MadeUpUtility
{
    public class FireWallOptions : UtilityOptions
    {
        public int Port { get; set; }
        public bool IsInbound { get; set; }
        public bool IsOutbound { get; set; }
        public string Program { get; set; }
    }
}