using System;
using ArgParser.Core;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitParseStrategy_Should
    {
        public class BadInfo : DefaultIterationInfo
        {
                
            public override IIterationInfo Consume(int numTokens) => base.Consume(-1);
        }

        public class BadFactory : DefaultIterationInfoFactory
        {
                
            public override IIterationInfo Create(string[] args) => new BadInfo
            {
                Args = args
            };
        }
    }
}