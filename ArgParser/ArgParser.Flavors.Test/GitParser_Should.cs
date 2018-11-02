using System;
using ArgParser.Core;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitParser_Should
    {
        [Fact]
        public void Throw_If_There_Is_No_Way_To_Consume()
        {
            // arrange
            var context = new GitContext();
            var parser = context.ParserRepository.Create("base");
            parser.Context = context;
            parser.Reset();

            // *no parameters added*
            Action mightThrow = () => parser.Consume(new object(), new DefaultIterationInfo());

            // act
            // assert
            mightThrow.Should().Throw<InvalidOperationException>();
        }
    }
}