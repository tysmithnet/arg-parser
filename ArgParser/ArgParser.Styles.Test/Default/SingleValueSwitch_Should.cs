﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class SingleValueSwitch_Should
    {
        [Fact]
        public void Only_Consume_Two_Strings()
        {
            // arrange
            var values = new List<string>();
            var sw = new SingleValueSwitch('l', "log", (o, s) => values.Add(s));
            var info = new IterationInfo("-l log.txt -o other".Split(' '), 0);

            // act
            sw.Consume(new object(), new ConsumptionRequest(info, 2));

            // assert
            values.Should().BeEquivalentTo("log.txt".Split(' '));
        }
    }
}
