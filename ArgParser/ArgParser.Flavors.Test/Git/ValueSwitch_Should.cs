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
    public class ValueSwitch_Should
    {
        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            Action mightThrow0 = () => new ValuesSwitch('v', null, null);
            Action mightThrow1 = () => new ValuesSwitch('v', "value", null);
            Action mightThrow2 = () => new ValuesSwitch('v', null, (o, s) => { });

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_If_Given_Too_Few_Values()
        {
            // arrange
            var values = new List<string>();
            var valuesSwitch = new ValuesSwitch('v', "value", (o, s) => { values.AddRange(s); })
            {
                Min = 100
            };
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-v v0 v1".Split());
            Action mightThrow = () => valuesSwitch.Consume(new object(), info0);
            
            // act
            // assert
            mightThrow.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Consume_One_Plus_The_Number_Of_Consumed_Tokens()
        {
            // arrange
            var values = new List<string>();
            var valuesSwitch = new ValuesSwitch('v', "value", (o, s) => { values.AddRange(s); });
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-v v0 v1".Split());

            // act
            // assert
            valuesSwitch.Consume(new object(), info0).Index.Should().Be(3);
            values.Should().BeEquivalentTo("v0 v1".Split(' '));
        }

        [Fact]
        public void Return_True_If_The_Letter_Or_Word_Matches()
        {
            // arrange
            var values = new List<string>();
            var valuesSwitch = new ValuesSwitch('v', "value", (o, s) => { values.AddRange(s); });
            var fac = new GitIterationInfoFactory();
            var info0 = fac.Create("-v v0 v1".Split());
            var info1 = fac.Create("--value v0 v1".Split());

            // act
            // assert
            valuesSwitch.CanConsume(new object(), info0).Should().BeTrue();
            valuesSwitch.CanConsume(new object(), info1).Should().BeTrue();
        }
    }
}
