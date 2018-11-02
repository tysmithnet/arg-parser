using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class BasicExtensions_Should
    {
        [Fact]
        public void Join_On_The_Separator()
        {
            // arrange
            // act
            // assert
            "a b c".Split(' ').Join(",").Should().Be("a,b,c");
        }

        [Fact]
        public void Join_On_The_Separator_Where_Instances_Are_Non_Null_Or_WhiteSpace()
        {
            // arrange
            // act
            // assert
            new[] {null, "a", "", "  ", "\t", "b"}.JoinNonNullOrWhiteSpace(",").Should().Be("a,b");
        }

        [Fact]
        public void Prevent_Null_Enumerations()
        {
            // arrange
            IEnumerable<string> strings = null;

            // act
            // assert
            strings.PreventNull().Should().HaveCount(0);
        }

        [Fact]
        public void Return_True_If_Null_Or_WhiteSpace()
        {
            // arrange
            string sNull = null;
            var white = "   ";

            // act
            // assert
            sNull.IsNullOrWhiteSpace().Should().BeTrue();
            white.IsNullOrWhiteSpace().Should().BeTrue();
            "a".IsNullOrWhiteSpace().Should().BeFalse();
        }

        [Fact]
        public void Throw_Argument_Null_Exception_If_Argument_Is_Null()
        {
            // arrange
            object oNull = null;
            Action mightThrow = () => oNull.ThrowIfArgumentNull("", "special message");

            // act
            // assert
            mightThrow.Should().Throw<ArgumentNullException>().Which.Message.Should().Be("special message");
        }
    }
}