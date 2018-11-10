using System;
using ArgParser.Core.Help;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class FullHelp_Should
    {
        [Fact]
        public void Allow_Examples_To_Be_Added()
        {
            // arrange
            var help = new FullHelp();

            // act
            help.AddExample(new Example());

            // assert
            help.Examples.Should().HaveCount(1);
        }

        [Fact]
        public void Only_Allow_Examples_To_Be_Added_More_Than_Once()
        {
            // arrange
            var help = new FullHelp();

            // act
            var example = new Example();
            help.AddExample(example);
            help.AddExample(example);

            // assert
            help.Examples.Should().HaveCount(1);
        }

        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            var help = new FullHelp();
            Action mightThrow = () => help.AddExample(null);

            // act
            // assert
            mightThrow.Should().Throw<ArgumentNullException>();
        }
    }
}