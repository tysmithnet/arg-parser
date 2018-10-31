using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
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
            /// <inheritdoc />
            public override IIterationInfo Consume(int numTokens)
            {
                return base.Consume(-1);
            }
        }

        public class BadFactory : DefaultIterationInfoFactory
        {
            /// <inheritdoc />
            public override IIterationInfo Create(string[] args)
            {
                return new BadInfo()
                {
                    Args = args
                };
            }
        }

        [Fact]
        public void Not_Add_An_Instance_As_Parsed_If_IterationInfo_Does_Not_Progress()
        {
            // arrange
            var strat = new GitParseStrategy();
            strat.FactoryFunctions.Add(() => new BaseOptions());
            var parser = new Mock<IParser>();
            parser.Setup(p => p.CanConsume(It.IsAny<object>(), It.IsAny<IIterationInfo>())).Returns(true);
            parser.Setup(p => p.Consume(It.IsAny<object>(), It.IsAny<IIterationInfo>())).Returns(new BadInfo()
            {
                Args = "whatever".Split(),
                Index = -1
            });
            Action mightThrow = () => strat.Parse(new[] {parser.Object}, "whatever".Split());

            // act
            // assert
            mightThrow.Should().NotThrow();
        }
    }
}
