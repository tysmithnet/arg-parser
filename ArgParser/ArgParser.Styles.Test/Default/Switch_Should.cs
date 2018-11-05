using System;
using ArgParser.Core;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class Switch_Should
    {
        private class TestSwitch : Switch
        {
            /// <inheritdoc />
            public TestSwitch(char? letter, string word, Action<object, string[]> consumeCallback) : base(letter, word,
                consumeCallback)
            {
            }
        }

        [Fact]
        public void Indicate_It_Can_Consume_The_Max_Allowed_When_Current_Arg_Is_A_Match()
        {
            // arrange
            var switch0 = new TestSwitch('t', "test", (o, strings) => { })
            {
                MaxAllowed = 3
            };
            var info = new IterationInfo("-t a b c d e d f g".Split(' '));

            // act
            // assert
            var result = switch0.CanConsume("", info);
            result.NumConsumed.Should().Be(3);
            result.Info.Current.Should().Be("c");
        }

        [Fact]
        public void Return_A_Human_Friendly_ToString()
        {
            // arrange
            var switch0 = new TestSwitch('t', "test", (o, strings) => { });
            var switch1 = new TestSwitch(null, "test", (o, strings) => { });
            var switch2 = new TestSwitch('t', null, (o, strings) => { });

            // act
            // assert
            switch0.ToString().Should().Be($"-t, --test");
            switch1.ToString().Should().Be($"--test");
            switch2.ToString().Should().Be($"-t");
        }

        [Fact]
        public void Return_No_Consumption_If_Minimum_Number_Of_Values_Are_Not_Met()
        {
            // arrange
            var switch0 = new TestSwitch('t', "test", (o, strings) => { })
            {
                MinRequired = 50
            };
            var info = new IterationInfo("-t a b c d e d f g".Split(' '));

            // act
            var result = switch0.CanConsume("", info);

            // assert
            result.NumConsumed.Should().Be(0);
        }

        [Fact]
        public void Return_No_Consumption_If_Switch_Does_Not_Match()
        {
            // arrange
            var switch0 = new TestSwitch('t', "test", (o, strings) => { });
            var info = new IterationInfo("-x a b c d e d f g".Split(' '));

            // act
            var result = switch0.CanConsume("", info);

            // assert
            result.NumConsumed.Should().Be(0);
        }

        [Fact]
        public void Return_True_When_Current_Arg_Matches()
        {
            // arrange
            var switch0 = new TestSwitch('t', "test", (o, strings) => { });
            var switch1 = new TestSwitch(null, "test", (o, strings) => { });
            var switch2 = new TestSwitch('t', null, (o, strings) => { });

            // act
            // assert
            switch0.IsLetterMatch(new IterationInfo("-t".Split(' '), 0)).Should().BeTrue();
            switch0.IsWordMatch(new IterationInfo("--test".Split(' '), 0)).Should().BeTrue();

            switch1.IsLetterMatch(new IterationInfo("-t".Split(' '), 0)).Should().BeFalse();
            switch1.IsWordMatch(new IterationInfo("--test".Split(' '), 0)).Should().BeTrue();

            switch2.IsLetterMatch(new IterationInfo("-t".Split(' '), 0)).Should().BeTrue();
            switch2.IsWordMatch(new IterationInfo("--test".Split(' '), 0)).Should().BeFalse();
        }

        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            Action mightThrow = () => new TestSwitch(null, null, (o, strings) => { });

            // act
            // assert
            mightThrow.Should().Throw<ArgumentException>();
        }
    }
}