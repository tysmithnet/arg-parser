namespace ArgParser.TestApp
{
    public abstract class FireWallOptions : UtilOptions
    {
        public bool IsInbound { get; set; }
        public bool IsOutbound { get; set; }
        public int Port { get; set; }
        public string Program { get; set; }
    }
}