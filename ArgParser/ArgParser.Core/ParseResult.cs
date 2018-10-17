using System;

namespace ArgParser.Core
{
    public class ParseResult<T>
    {
        public ParseResult<T> When<TSub>(Action<TSub> handler) where TSub : T
        {
            return this;
        }
    }
}