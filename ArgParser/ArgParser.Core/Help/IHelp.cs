namespace ArgParser.Core.Help
{
    public interface IHelp
    {
        string Name { get; }
        string ShortDescription { get; }
        string Description { get; }
        string Version { get; }
        /// <summary>
        /// Gets the several character text representation of command template
        /// </summary>
        /// <value>The synopsis.</value>
        string Synopsis { get; }
        string Url { get; }
    }
}