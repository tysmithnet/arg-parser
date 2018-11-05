﻿using ArgParser.Core;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class BooleanSwitch_Should
    {
        [Fact]
        public void Only_Consume_One_String()
        {
            // arrange
            var parseCount = 0;
            var sw = new BooleanSwitch('h', "help", o => { parseCount++; });
            var info = new IterationInfo("-h -o other stuff".Split(' '), 0);

            // act
            var result = sw.Consume(new object(), new ConsumptionRequest(info));

            // assert
            parseCount.Should().Be(1);
            result.NumConsumed.Should().Be(1);
        }
    }
}