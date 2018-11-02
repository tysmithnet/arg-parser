namespace ArgParser.IntegrationTests.Options.MadeUpUtility
{
    public class ZipOptions : ClipboardOptions
    {
        public string[] FilterGlobs { get; set; }
        public string ZipFileName { get; set; }
    }
}