using ArgParser.Core;

namespace ArgParser.Flavors.Test
{
    public class GitParseStrategy_Should
    {
        public class BadFactory : DefaultIterationInfoFactory
        {
            public override IIterationInfo Create(string[] args) => new BadInfo
            {
                Args = args
            };
        }

        public class BadInfo : DefaultIterationInfo
        {
            public override IIterationInfo Consume(int numTokens) => base.Consume(-1);
        }
    }
}