using ArgParser.Core;

namespace ArgParser.Styles
{
    public interface ISwitchStrategy
    {
        bool IsWordMatch(IterationInfo info);
        bool IsLetterMatch(IterationInfo info);
    }
}