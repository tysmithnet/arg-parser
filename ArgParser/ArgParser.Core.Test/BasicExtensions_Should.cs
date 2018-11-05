using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class BasicExtensions_Should
    {
        [Fact]
        public void Identify_Null_Or_WhiteSpace_Strings()
        {
            // arrange
            string sNull = null;

            // act
            // assert
            sNull.IsNullOrWhiteSpace().Should().BeTrue();
            "".IsNullOrWhiteSpace().Should().BeTrue();
            "\t".IsNullOrWhiteSpace().Should().BeTrue();
        }

        [Fact]
        public void Join_All_Strings_Not_Null_Or_WhiteSpace()
        {
            // arrange
            // act
            // assert
            new[] {null, "a", null, null, "b", "c"}.JoinNonNullOrWhiteSpace(",").Should().Be("a,b,c");
        }

        [Fact]
        public void Join_Strings_On_A_Separator()
        {
            // arrange
            // act
            // assert
            new[] {"a", "b", "c"}.Join(",").Should().Be("a,b,c");
        }

        [Fact]
        public void Return_An_Empty_Enumerable_When_Preventing_Null()
        {
            // arrange
            IEnumerable<string> s = null;

            // act
            // assert
            s.PreventNull().Should().NotBeNull();
        }

        [Fact]
        public void Throw_ArgumentNullException_If_Argument_Is_Null()
        {
            // arrange
            string s = null;
            Action mightThrow = () => s.ThrowIfArgumentNull(nameof(s));
            Action mightThrow1 = () => s.ThrowIfArgumentNull(nameof(s), "test");

            // act
            // assert
            mightThrow.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
        }
    }
}