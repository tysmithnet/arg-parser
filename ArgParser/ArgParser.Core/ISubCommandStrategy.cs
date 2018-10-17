using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface ISubCommandStrategy<T> : IParsingStrategy<T>
    {
        bool IsSubCommand(IList<ISubCommand> subCommands, IterationInfo info);
        ParseResult Parse(IList<ISubCommand> subCommands, IterationInfo info);
    }
}