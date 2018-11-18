using System;
using ArgParser.Core;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class Positional_Should
    {
        [Fact]
        public void Consume_The_Min_Of_The_Number_It_Wants_Vs_The_Number_It_Can_Have()
        {
            // arrange
            var positional = new Positional<string>(new Parser("a"), (o, strings) => { })
            {
                MaxAllowed = 3
            };
            var info = new IterationInfo("a b c d e f".Split(' '));

            // act
            var res0 = positional.Consume("", new ConsumptionRequest(info, int.MaxValue));
            var res1 = positional.Consume("", new ConsumptionRequest(info, 2));

            // assert
            res0.NumConsumed.Should().Be(3);
            res1.NumConsumed.Should().Be(2);
        }

        [Fact]
        public void Indicate_It_Cannot_Consume_If_It_Has_Already_Consumed()
        {
            // arrange
            var positional = new Positional(new Parser("a"), (o, strings) => { })
            {
                HasBeenConsumed = true
            };
            var info = new IterationInfo("a b c".Split(' '));

            // act
            var res = positional.CanConsume("", info);

            // assert
            res.NumConsumed.Should().Be(0);
        }

        [Fact]
        public void Provide_A_Generic_Version()
        {
            // arrange
            var positional = new Positional<string>(new Parser("a"), (o, strings) => { });
            var info = new IterationInfo("a b c".Split(' '));

            // act
            var res = positional.CanConsume("", info);

            // assert
            res.NumConsumed.Should().Be(3);
        }

        [Fact]
        public void Throw_If_Given_An_Incompatible_Instance()
        {
            // arrange
            var positional = new Positional<string>(new Parser("a"), (o, strings) => { });
            var info = new IterationInfo("a b c".Split(' '));
            Action mightThrow = () => positional.Consume(new object(), new ConsumptionRequest(info));

            // act
            // assert
            mightThrow.Should().Throw<ArgumentException>();
        }
    }
}