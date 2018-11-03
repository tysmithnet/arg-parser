using ArgParser.Core.Help.Dom;

namespace ArgParser.Core.Help
{
    public interface IHelpBuilder
    {
        IHelpNode Build();
    }
}