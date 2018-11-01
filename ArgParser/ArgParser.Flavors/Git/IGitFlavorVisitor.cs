namespace ArgParser.Flavors.Git
{
    public interface IGitFlavorVisitor
    {
        void Visit(GitParser gitParser);
    }
}