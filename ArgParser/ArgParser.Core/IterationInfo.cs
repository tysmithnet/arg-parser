using System;

namespace ArgParser.Core
{
    public class IterationInfo
    {
        public string[] Args { get; protected internal set; }
        public int Index { get; set; }
        public string Current => Args[Index];
        public IterationInfo(string[] args, int index)
        {
            Args = args ?? throw new ArgumentNullException(nameof(args));
            Index = index;
        }

        public IterationInfo Consume(int num)
        {
            return new IterationInfo(Args, Index + num);
        }
    }
}