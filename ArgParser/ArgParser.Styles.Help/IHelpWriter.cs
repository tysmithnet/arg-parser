namespace ArgParser.Styles.Help
{
    public interface IHelpWriter
    {
        string CreateHelpText(RootNode rootNode, int width = 80);
        void RenderHelp(RootNode rootNode, int width = 80);
    }
}