using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
using ArgParser.Core.Validation;

namespace ArgParser.Flavors.Git
{
    public class GitParseStrategy : DefaultParseStrategy
    {
        public GitFlavor RootFlavor { get; set; }

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