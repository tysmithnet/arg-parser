using System;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class IterationInfo_Should
    {
        [Fact]
        public void Be_Comparable()
        {
            // arrange
            var abc = "a b c".Split(' ');
            var a = new IterationInfo(abc, 0);
            var b = new IterationInfo(abc, 1);
            var c = new IterationInfo("def".Split(' '));
            Action mightThrow0 = () =>
            {
                var x = a < c;
            };
            Action mightThrow1 = () =>
            {
                var x = a.CompareTo("");
            };

            // act
            // assert
            (a < b).Should().BeTrue();
            (a <= b).Should().BeTrue();
            (b > a).Should().BeTrue();
            (b >= a).Should().BeTrue();
            a.CompareTo(null).Should().Be(1);
            a.CompareTo((object) null).Should().Be(1);
            a.CompareTo(a).Should().Be(0);
            a.CompareTo((object) a).Should().Be(0);
            a.CompareTo(b).Should().Be(-1);
            a.CompareTo((object) b).Should().Be(-1);
            mightThrow0.Should().Throw<InvalidOperationException>();
            mightThrow1.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Exhibit_Value_Equality()
        {
            // arrange
            var a = new IterationInfo("a b c".Split(' '), 0);
            var b = new IterationInfo("a b".Split(' '), 0);
            var c = new IterationInfo("a b c d".Split(' '), 0);
            var d = new IterationInfo("a b c".Split(' '), 0);

            // act
            // assert
            a.GetHashCode().Should().Be(d.GetHashCode());
            a.Equals("").Should().BeFalse();
            a.Equals(null).Should().BeFalse();
            a.Equals((object) null).Should().BeFalse();
            a.Equals(b).Should().BeFalse();
            a.Equals(c).Should().BeFalse();
            a.Equals(d).Should().BeTrue();
            a.Equals((object) b).Should().BeFalse();
            a.Equals((object) c).Should().BeFalse();
            a.Equals((object) d).Should().BeTrue();
            a.Equals(a).Should().BeTrue();
            a.Equals((object) a).Should().BeTrue();
            (a != b).Should().BeTrue();
            (a == d).Should().BeTrue();
            (b != a).Should().BeTrue();
            (d == a).Should().BeTrue();
        }

        [Fact]
        public void Return_The_Current_Arg()
        {
            // arrange
            var info = new IterationInfo("a b c".Split(' '), 0);

            // act
            // assert
            info.Current.Should().Be("a");
        }
    }
}