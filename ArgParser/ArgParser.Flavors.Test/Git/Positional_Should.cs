using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class Positional_Should
    {
        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            Action mightThrow0 = () => new Positional(null);

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_If_Not_Enough_Values()
        {
            // arrange
            var strings = new List<string>();
            var positional = new Positional((o, s) => strings.AddRange(s))
            {
                Min = 3
            };
            var fac = new GitIterationInfoFactory();
            var info = fac.Create("p0 p1".Split(' '));
            Action mightThrow = () => positional.Consume(new object(), info);
            
            // act
            // assert
            mightThrow.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Indicate_It_Cant_Consume_If_It_Already_Has_Consume()
        {
            // arrange
            var strings = new List<string>();
            var positional = new Positional((o, s) => strings.AddRange(s));
            var fac = new GitIterationInfoFactory();
            var info = fac.Create("p0 p1".Split(' '));

            // act
            bool before = positional.CanConsume(new object(), info);
            info = positional.Consume(new object(), info);

            // assert
            before.Should().BeTrue();
            positional.CanConsume(new object(), info).Should().BeFalse();
            strings.Should().BeEquivalentTo("p0 p1".Split(' '));
        }

        [Fact]
        public void Consume_The_Correct_Amount_Of_Values()
        {
            // arrange
            var strings = new List<string>();
            var positional = new Positional((o, s) => strings.AddRange(s))
            {
                Max = 1
            };
            var fac = new GitIterationInfoFactory();
            var info = fac.Create("p0 p1".Split(' '));

            // act
            info = positional.Consume(new object(), info);

            // assert
            positional.CanConsume(new object(), info);
            strings.Should().BeEquivalentTo("p0".Split(' '));
        }
    }
}
