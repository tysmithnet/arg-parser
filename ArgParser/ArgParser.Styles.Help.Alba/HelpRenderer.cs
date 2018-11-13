using ArgParser.Core;

namespace ArgParser.Styles.Help.Alba
{
    public class HelpRenderer
    {
        public HelpNodeFactory HelpNodeFactory { get; set; } = new HelpNodeFactory();
        public HelpWriter HelpWriter { get; set; } = new HelpWriter();

        public void Render(IContext context, string parserId, int width)
        {
            var root = HelpNodeFactory.Create(parserId, context);
            HelpWriter.RenderHelp(root, width);
        }
    }
}