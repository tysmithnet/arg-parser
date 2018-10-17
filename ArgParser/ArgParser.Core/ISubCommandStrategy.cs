using System.Collections.Generic;

namespace ArgParser.Core
{
    public interface ISubCommandStrategy<T> : IParsingStrategy<T>
    {
        bool IsSubCommand(IList<ISubCommand> subCommands, IterationInfo info, ISubCommandStrategy<T> parent = null);
        ParseResult Parse(IList<ISubCommand> subCommands, IterationInfo info, ISubCommandStrategy<T> parent = null);
    }
}