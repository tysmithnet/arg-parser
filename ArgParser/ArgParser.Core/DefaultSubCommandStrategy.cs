using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultSubCommandStrategy<T> : ISubCommandStrategy<T>
    {
        /// <inheritdoc />
        public bool IsSubCommand(IList<ISubCommand> subCommands, IterationInfo info, ISubCommandStrategy<T> parent = null)
        {
            return subCommands.Any(s => s.IsCommand(info)) || (parent?.IsSubCommand(subCommands, info) ?? false);
        }

        /// <inheritdoc />
        public ParseResult Parse(IList<ISubCommand> subCommands, IterationInfo info, ISubCommandStrategy<T> parent = null)
        {
            
            var first = subCommands.FirstOrDefault(s => s.IsCommand(info));
            return first != null ? first.Parse(info.AllArgs) : parent?.Parse(subCommands, info) ?? throw new InvalidOperationException("Check to find subcommand indicated there was one, but then could not find it when called upon to parse. Make sure your command logic is sound.");
        }

        /// <inheritdoc />
        public void Reset()
        {
            
        }
    }
}