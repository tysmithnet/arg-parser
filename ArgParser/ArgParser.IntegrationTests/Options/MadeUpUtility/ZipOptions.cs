namespace ArgParser.IntegrationTests.Options.MadeUpUtility
{
    public class ZipOptions : ClipboardOptions
    {
        public string ZipFileName { get; set; }
        public string[] FilterGlobs { get; set; }
    }
}