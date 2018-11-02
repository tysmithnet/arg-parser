namespace ArgParser.IntegrationTests.Options.MadeUpUtility
{
    public class FireWallOptions : UtilityOptions
    {
        public bool IsInbound { get; set; }
        public bool IsOutbound { get; set; }
        public int Port { get; set; }
        public string Program { get; set; }
    }
}