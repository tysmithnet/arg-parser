using System;
using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultSubCommandStrategy<T> : ISubCommandStrategy<T>
    {
        /// <inheritdoc />
        public DefaultSubCommandStrategy()
        {
        }


        /// <inheritdoc />
        public bool IsSubCommand(IList<ISubCommand> subCommands, IterationInfo info)
        {
            return subCommands.Any(s => s.IsCommand(info));
        }

        /// <inheritdoc />
        public ParseResult Parse(IList<ISubCommand> subCommands, IterationInfo info)
        {
            var first = subCommands.First(s => s.IsCommand(info));
            return first.Parse(info.AllArgs);
        }

        /// <inheritdoc />
        public void Reset()
        {
            
        }
    }
}