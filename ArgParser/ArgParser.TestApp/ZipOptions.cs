namespace ArgParser.TestApp
{
    public class ZipOptions : ClipboardOptions
    {
        public string[] Globs { get; set; }
        public string ZipFile { get; set; }
    }
}