using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class BasicExtensions_Should
    {
        [Fact]
        public void Convert_Generic_Actions_Into_NonGeneric_Actions()
        {
            // arrange
            var isExecuted0 = false;
            var isExecuted1 = false;
            Action<string, string[]> action0 = (s1, strings) => { isExecuted0 = true; };
            Action<string> action1 = s => { isExecuted1 = true; };
            // act
            var converted0 = action0.ToNonGenericAction();
            var converted1 = action1.ToNonGenericAction();

            converted0("", new string[0]);
            converted1("");

            // assert
            isExecuted0.Should().BeTrue();
            isExecuted1.Should().BeTrue();
        }

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

        [Fact]
        public void Throw_If_Given_Bad_Value_To_Converted_Action_If_Strict_Is_Enabled()
        {
            // arrange
            var isExecuted0 = false;
            var isExecuted1 = false;
            Action<string, string[]> action0 = (s1, strings) => { isExecuted0 = true; };
            Action<string> action1 = s => { isExecuted1 = true; };

            // act
            var converted0 = action0.ToNonGenericAction(true);
            var converted1 = action1.ToNonGenericAction(true);
            Action mightThrow0 = () => converted0(new object(), new string[0]);
            Action mightThrow1 = () => converted1(new object());

            // assert
            mightThrow0.Should().Throw<ArgumentException>();
            mightThrow1.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Not_Throw_If_Given_Bad_Value_To_Converted_Action()
        {
            // arrange
            var isExecuted0 = false;
            var isExecuted1 = false;
            Action<string, string[]> action0 = (s1, strings) => { isExecuted0 = true; };
            Action<string> action1 = s => { isExecuted1 = true; };

            // act
            var converted0 = action0.ToNonGenericAction();
            var converted1 = action1.ToNonGenericAction();
            Action mightThrow0 = () => converted0(new object(), new string[0]);
            Action mightThrow1 = () => converted1(new object());

            // assert
            mightThrow0.Should().NotThrow();
            mightThrow1.Should().NotThrow();
        }
    }
}