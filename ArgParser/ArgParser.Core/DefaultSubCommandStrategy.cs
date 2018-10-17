using System.Collections.Generic;
using System.Linq;

namespace ArgParser.Core
{
    public class DefaultSubCommandStrategy<T> : ISubCommandStrategy<T>
    {
        protected internal IList<ISubCommand> SubCommands { get; set; }

        /// <inheritdoc />
        public bool IsSubCommand(IterationInfo info)
        {
            return SubCommands.Any(s => s.IsCommand(info));
        }

        /// <inheritdoc />
        public ParseResult Parse(IterationInfo info)
        {
            var first = SubCommands.First(s => s.IsCommand(info));
            return first.Parse(info.AllArgs);
        }

        /// <inheritdoc />
        public void Reset()
        {
            
        }
    }
}