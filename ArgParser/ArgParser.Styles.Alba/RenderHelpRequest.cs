using ArgParser.Core;

namespace ArgParser.Styles.Alba
{
    public class RenderHelpRequest
    {
        public IContext Context { get; set; }
        public string ParserId { get; set; }
        public int Width { get; set; }
        public Template Template { get; set; }
    }
}
