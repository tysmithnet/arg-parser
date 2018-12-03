using System;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class IterationInfoExtensions_Should
    {
        [Fact]
        public void Identify_When_There_Is_A_Next_Arg()
        {
            // arrange
            var info0 = new IterationInfo("".Split(' '), 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);

            // act
            // assert
            info0.HasNext().Should().BeFalse();
            info1.HasNext().Should().BeFalse();
            info2.HasNext().Should().BeTrue();
        }

        [Fact]
        public void Return_The_Current_Item_And_All_SubSequent_Items()
        {
            // arrange
            var info0 = new IterationInfo(new string[0], 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 1);

            // act
            // assert
            info0.FromNowOn().Should().BeEmpty();
            info1.FromNowOn().Should().BeEquivalentTo("-h".Split(' '));
            info2.FromNowOn().Should().BeEquivalentTo("-v something".Split(' '));
            info3.FromNowOn().Should().BeEquivalentTo("b c d e".Split(' '));
        }

        [Fact]
        public void Return_The_First_Item()
        {
            // arrange
            var info0 = new IterationInfo(new string[0], 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 1);
            Action mightThrow = () => info0.First();

            // act
            // assert
            mightThrow.Should().Throw<ArgumentOutOfRangeException>();
            info1.First().Should().Be("-h");
            info2.First().Should().Be("-v");
            info3.First().Should().Be("a");
        }

        [Fact]
        public void Return_The_Last_Item()
        {
            // arrange
            var info0 = new IterationInfo(new string[0], 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 1);
            Action mightThrow = () => info0.Last();

            // act
            // assert
            mightThrow.Should().Throw<ArgumentOutOfRangeException>();
            info1.Last().Should().Be("-h");
            info2.Last().Should().Be("something");
            info3.Last().Should().Be("e");
        }

        [Fact]
        public void Return_The_Next_Item()
        {
            // arrange
            var info0 = new IterationInfo(new string[0], 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 1);
            Action mightThrow0 = () => info0.Next();
            Action mightThrow1 = () => info1.Next();

            // act
            // assert
            mightThrow0.Should().Throw<IndexOutOfRangeException>();
            mightThrow1.Should().Throw<IndexOutOfRangeException>();
            info2.Next().Should().Be("something");
            info3.Next().Should().Be("c");
        }

        [Fact]
        public void Return_The_SubSequent_Items()
        {
            // arrange
            var info0 = new IterationInfo(new string[0], 0);
            var info1 = new IterationInfo("-h".Split(' '), 0);
            var info2 = new IterationInfo("-v something".Split(' '), 0);
            var info3 = new IterationInfo("a b c d e".Split(' '), 1);

            // act
            // assert
            info0.Rest().Should().BeEmpty();
            info1.Rest().Should().BeEmpty();
            info2.Rest().Should().BeEquivalentTo("something".Split(' '));
            info3.Rest().Should().BeEquivalentTo("c d e".Split(' '));
        }
    }
}