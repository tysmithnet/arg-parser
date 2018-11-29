using System;
using System.Collections.Generic;
using System.Text;
using ArgParser.Core;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test
{
    public class SingleValueSwitch_Should
    {
        [Fact]
        public void Only_Consume_Two_Strings()
        {
            // arrange
            var values = new List<string>();
            var sw = new SingleValueSwitch(new Parser("a"), 'l', "log", (o, s) => values.Add(s));
            var info = new IterationInfo("-l log.txt -o other".Split(' '), 0);

            // act
            var result = sw.Consume(new object(), new ConsumptionRequest(info, 2));

            // assert
            values.Should().BeEquivalentTo("log.txt".Split(' '));
            result.NumConsumed.Should().Be(2);
        }

        [Fact]
        public void Provide_A_Generic_Version()
        {
            // arrange
            var values = new List<string>();
            var sw = new SingleValueSwitch<StringBuilder>(new Parser("a"), 'l', "log", (o, s) => values.Add(s));
            var info = new IterationInfo("-l log.txt -o other".Split(' '), 0);

            // act
            var result = sw.Consume(new StringBuilder(), new ConsumptionRequest(info, 2));

            // assert
            values.Should().BeEquivalentTo("log.txt".Split(' '));
            result.NumConsumed.Should().Be(2);
        }

        [Fact]
        public void Not_Throw_If_Bad_Object_Given_To_Generic_Type()
        {
            // arrange
            var values = new List<string>();
            var sw = new SingleValueSwitch<StringBuilder>(new Parser("a"), 'l', "log", (o, s) => values.Add(s));
            var info = new IterationInfo("-l log.txt -o other".Split(' '), 0);
            Action mightThrow = () => sw.Consume(new object(), new ConsumptionRequest(info, 2));

            // act
            // assert
            mightThrow.Should().NotThrow();
        }
    }
}