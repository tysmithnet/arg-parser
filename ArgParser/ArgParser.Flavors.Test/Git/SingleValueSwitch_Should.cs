using System;
using System.Collections.Generic;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class SingleValueSwitch_Should
    {
        [Fact]
        public void Only_Consume_Two_Tokens()
        {
            // arrange
            var values = new List<string>();
            var singleValueSwitch = new SingleValueSwitch('v', "value", (o, s) => { values.Add(s); });
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-v value".Split());

            // act
            // assert
            singleValueSwitch.Consume(new object(), info0).Index.Should().Be(2);
            values.Should().BeEquivalentTo("value");
        }

        [Fact]
        public void Allow_Null_For_The_Letter()
        {
            // arrange
            var values = new List<string>();
            var singleValueSwitch = new SingleValueSwitch(null, "version", (o, s) => { values.Add(s); });
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("--version value".Split());

            // act
            // assert
            singleValueSwitch.Consume(new object(), info0).Index.Should().Be(2);
            values.Should().BeEquivalentTo("value");
        }

        [Fact]
        public void Return_True_If_The_Letter_Or_Word_Matches()
        {
            // arrange
            var values = new List<string>();
            var singleValueSwitch = new SingleValueSwitch('v', "value", (o, s) => { values.Add(s); });
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-v value".Split());
            var info1 = fac.Create("--value value".Split());

            // act
            // assert
            singleValueSwitch.CanConsume(new object(), info0).Should().BeTrue();
            singleValueSwitch.CanConsume(new object(), info1).Should().BeTrue();
        }

        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            Action mightThrow0 = () => new SingleValueSwitch('v', null, null);
            Action mightThrow1 = () => new SingleValueSwitch('v', "value", null);
            Action mightThrow2 = () => new SingleValueSwitch('v', null, (o, s) => { });

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_If_No_Next_Token()
        {
            // arrange
            var values = new List<string>();
            var singleValueSwitch = new SingleValueSwitch('v', "value", (o, s) => { values.Add(s); });
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-v".Split());
            Action mightThrow = () => singleValueSwitch.Consume(new object(), info0);

            // act
            // assert
            mightThrow.Should().Throw<IndexOutOfRangeException>();
        }
    }
}