namespace ArgParser.Testing.Common
{
    public  class FireWallOptions : UtilOptions
    {
        public bool IsInbound { get; set; }
        public bool IsOutbound { get; set; }
        public int Port { get; set; }
        public string Program { get; set; }
    }
}