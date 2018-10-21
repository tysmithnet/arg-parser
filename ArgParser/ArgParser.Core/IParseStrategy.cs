using System.Collections.Generic;
using System.Text;

namespace ArgParser.Core
{
    public interface IParseStrategy<in T>
    {
        void AddParser<TSub>(IParser<TSub> parser) where TSub : T;
        IParseResult Parse(string[] args);
    }

    public class DefaultParseStrategy<T> : IParseStrategy<T>
    {
        public IList<T> Parsers { get; set; }

        /// <inheritdoc />
        public void AddParser<TSub>(IParser<TSub> parser) where TSub : T
        {

        }

        /// <inheritdoc />
        public IParseResult Parse(string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}
