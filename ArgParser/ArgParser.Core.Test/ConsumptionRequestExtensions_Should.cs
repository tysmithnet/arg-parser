using System;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class ConsumptionRequestExtensions_Should
    {
        [Fact]
        public void Identify_When_There_Is_A_Next_Arg()
        {
            // arrange
            var info0 = new IterationInfo("".Split(' '), 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var req0 = new ConsumptionRequest(info0, 1);
            var req1 = new ConsumptionRequest(info1, 1);
            var req2 = new ConsumptionRequest(info2, 1);
            var req3 = new ConsumptionRequest(info2, 2);

            // act
            // assert
            req0.HasNext().Should().BeFalse();
            req1.HasNext().Should().BeFalse();
            req2.HasNext().Should().BeFalse();
            req3.HasNext().Should().BeTrue();
        }

        [Fact]
        public void Return_The_Current_Item_And_All_SubSequent_Items()
        {
            // arrange
            var info0 = new IterationInfo("".Split(' '), 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 0);
            var req0 = new ConsumptionRequest(info0, 1);
            var req1 = new ConsumptionRequest(info1, 1);
            var req2 = new ConsumptionRequest(info2, 1);
            var req3 = new ConsumptionRequest(info2, 2);
            var req4 = new ConsumptionRequest(info3, 3);

            // act
            // assert
            req0.AllToBeConsumed().Should().BeEquivalentTo("".Split(' '));
            req1.AllToBeConsumed().Should().BeEquivalentTo("-h".Split(' '));
            req2.AllToBeConsumed().Should().BeEquivalentTo("-v".Split(' '));
            req3.AllToBeConsumed().Should().BeEquivalentTo("-v something".Split(' '));
            req4.AllToBeConsumed().Should().BeEquivalentTo("a b c".Split(' '));
        }

        [Fact]
        public void Return_The_First_Item()
        {
            // arrange
            var info0 = new IterationInfo(new string[0], 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 0);
            var req0 = new ConsumptionRequest(info0, 1);
            var req1 = new ConsumptionRequest(info1, 1);
            var req2 = new ConsumptionRequest(info2, 1);
            var req3 = new ConsumptionRequest(info2, 2);
            var req4 = new ConsumptionRequest(info3, 3);
            Action mightThrow = () => req0.First();

            // act
            // assert
            mightThrow.Should().Throw<ArgumentOutOfRangeException>();
            req1.First().Should().Be("-h");
            req2.First().Should().Be("-v");
            req3.First().Should().Be("-v");
            req4.First().Should().Be("a");
        }

        [Fact]
        public void Return_The_Last_Item()
        {
            // arrange
            var info0 = new IterationInfo(new string[0], 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 0);
            var req0 = new ConsumptionRequest(info0, 1);
            var req1 = new ConsumptionRequest(info1, 1);
            var req2 = new ConsumptionRequest(info2, 1);
            var req3 = new ConsumptionRequest(info2, 2);
            var req4 = new ConsumptionRequest(info3, 3);
            Action mightThrow = () => req0.Last();

            // act
            // assert
            mightThrow.Should().Throw<ArgumentOutOfRangeException>();
            req1.Last().Should().Be("-h");
            req2.Last().Should().Be("-v");
            req3.Last().Should().Be("something");
            req4.Last().Should().Be("c");
        }

        [Fact]
        public void Return_The_Next_Item()
        {
            // arrange
            var info0 = new IterationInfo(new string[0], 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 0);
            var req0 = new ConsumptionRequest(info0, 1);
            var req1 = new ConsumptionRequest(info1, 1);
            var req2 = new ConsumptionRequest(info2, 1);
            var req3 = new ConsumptionRequest(info2, 2);
            var req4 = new ConsumptionRequest(info3, 3);
            Action mightThrow0 = () => req0.Next();
            Action mightThrow1 = () => req1.Next();
            Action mightThrow2 = () => req2.Next();

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentOutOfRangeException>();
            mightThrow1.Should().Throw<ArgumentOutOfRangeException>();
            mightThrow2.Should().Throw<ArgumentOutOfRangeException>();
            req3.Next().Should().Be("something");
            req4.Next().Should().Be("b");
        }

        [Fact]
        public void Return_The_SubSequent_Items()
        {
            // arrange
            var info0 = new IterationInfo("".Split(' '), 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 0);
            var req0 = new ConsumptionRequest(info0, 1);
            var req1 = new ConsumptionRequest(info1, 1);
            var req2 = new ConsumptionRequest(info2, 1);
            var req3 = new ConsumptionRequest(info2, 2);
            var req4 = new ConsumptionRequest(info3, 3);

            // act
            // assert
            req0.Rest().Should().BeEmpty();
            req1.Rest().Should().BeEmpty();
            req2.Rest().Should().BeEmpty();
            req3.Rest().Should().BeEquivalentTo("something".Split(' '));
            req4.Rest().Should().BeEquivalentTo("b c".Split(' '));
        }
    }
}