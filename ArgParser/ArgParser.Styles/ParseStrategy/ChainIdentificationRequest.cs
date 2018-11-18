using ArgParser.Core;

namespace ArgParser.Styles.ParseStrategy
{
    public class ChainIdentificationRequest
    {
        public string[] Args { get; set; }
        public IContext Context { get; set; }
    }
}