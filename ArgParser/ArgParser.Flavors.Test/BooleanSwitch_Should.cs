using System;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class BooleanSwitch_Should
    {
        [Fact]
        public void Only_Consume_One_Token()
        {
            // arrange
            var consumeCount = 0;
            var booleanSwitch = new BooleanSwitch('h', "help", o => { consumeCount++; });
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-h".Split());

            // act
            // assert
            booleanSwitch.Consume(new object(), info0).Index.Should().Be(1);
            consumeCount.Should().Be(1);
        }

        [Fact]
        public void Return_True_If_The_Letter_Or_Word_Matches()
        {
            // arrange
            var booleanSwitch = new BooleanSwitch('h', "help", o => { });
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-h".Split());
            var info1 = fac.Create("--help".Split());

            // act
            // assert
            booleanSwitch.CanConsume(new object(), info0).Should().BeTrue();
            booleanSwitch.CanConsume(new object(), info1).Should().BeTrue();
        }

        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            Action mightThrow0 = () => new BooleanSwitch('h', null, null);
            Action mightThrow1 = () => new BooleanSwitch('h', "help", null);
            Action mightThrow2 = () => new BooleanSwitch('h', null, o => { });

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
        }
    }
}