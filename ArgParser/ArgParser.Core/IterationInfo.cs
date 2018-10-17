using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgParser.Core
{
    public class IterationInfo
    {
        /// <inheritdoc />
        public IterationInfo(string[] allArgs, int index)
        {
            AllArgs = allArgs ?? throw new ArgumentNullException(nameof(allArgs));
            Index = index;
        }

        internal IList<ParsingError> Errors { get; set; } = new List<ParsingError>();
        public string[] AllArgs { get; internal set; }
        public int Index { get; internal set; }
        public string Cur => AllArgs[Index];
        public string[] Rest => AllArgs.Skip(Index + 1).ToArray();
        public bool IsEnd => Index >= AllArgs.Length;
    }
}
