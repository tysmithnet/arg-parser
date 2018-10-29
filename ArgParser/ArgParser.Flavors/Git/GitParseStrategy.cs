using System;
using System.Collections.Generic;
using ArgParser.Core;

namespace ArgParser.Flavors.Git
{
    public class GitParseStrategy : DefaultParseStrategy
    {
        public GitParseStrategy(List<Func<object>> factoryMethods) : base(factoryMethods)
        {
            
        }

        /// <inheritdoc />
        protected override IParseResult CreateParseResult(List<object> results)
        {
            return new GitParseResult(results);
        }
    }
}