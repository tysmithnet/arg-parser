using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Core;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class ValuesSwitch_Should
    {
        [Fact]
        public void Consume_MaxAllowed_If_Request_Is_Unbounded()
        {
            // arrange
            var list = new List<string[]>();
            var sw = new ValuesSwitch(new Parser("a"), 'v', "values", (o, strings) => list.Add(strings));
            var info = new IterationInfo("-v v0 v1 v2 v3 -s".Split(' '), 0);
            var request = new ConsumptionRequest(info, int.MaxValue);

            // act
            var result = sw.Consume(new object(), request);

            // assert
            list.Should().HaveCount(1);
            list[0].Should().BeEquivalentTo("v0 v1 v2 v3 -s".Split(' '));
            result.NumConsumed.Should().Be(6);
        }

        [Fact]
        public void Consume_Only_The_Maximum_Allowed_From_The_Request()
        {
            // arrange
            var list = new List<string[]>();
            var sw = new ValuesSwitch(new Parser("a"), 'v', "values", (o, strings) => list.Add(strings));
            var info = new IterationInfo("-v v0 v1 v2 v3 -s".Split(' '), 0);
            var request = new ConsumptionRequest(info, 3);

            // act
            var result = sw.Consume(new object(), request);

            // assert
            list.Should().HaveCount(1);
            list[0].Should().BeEquivalentTo("v0 v1".Split(' '));
            result.NumConsumed.Should().Be(3);
        }

        [Fact]
        public void Not_Throw_If_Not_Given_Enough_Args_But_Return_Them_In_The_Result()
        {
            // arrange
            var list = new List<string[]>();
            var sw = new ValuesSwitch(new Parser("a"), 'v', "values", (o, strings) => list.Add(strings))
            {
                MinRequired = 5
            };
            var info = new IterationInfo("-v v0 v1 v2 v3 -s".Split(' '), 0);
            var request = new ConsumptionRequest(info, 3);
            ConsumptionResult res = null;
            Action mightThrow = () => res = sw.Consume(new object(), request);

            // act
            // assert
            mightThrow.Should().NotThrow();
            res.ParseExceptions.Should().HaveCount(1).And.Subject.Single().Should().BeOfType<MissingValueException>();
        }
    }
}