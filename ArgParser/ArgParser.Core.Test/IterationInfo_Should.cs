using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class IterationInfo_Should
    {
        [Fact]
        public void Return_The_Current_Arg()
        {
            // arrange
            var info = new IterationInfo("a b c".Split(' '), 0);

            // act
            // assert
            info.Current.Should().Be("a");
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
            a.Equals((object) "").Should().BeFalse();
            a.Equals(null).Should().BeFalse();
            a.Equals((object) null).Should().BeFalse();
            a.Equals(b).Should().BeFalse();
            a.Equals(c).Should().BeFalse();
            a.Equals(d).Should().BeTrue();
            a.Equals((object)b).Should().BeFalse();
            a.Equals((object)c).Should().BeFalse();
            a.Equals((object)d).Should().BeTrue();
            a.Equals(a).Should().BeTrue();
            a.Equals((object)a).Should().BeTrue();
            (a != b).Should().BeTrue();
            (a == d).Should().BeTrue();
            (b != a).Should().BeTrue();
            (d == a).Should().BeTrue();
        }
    }

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
    }
}

