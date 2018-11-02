using System;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class CallbackExtensions_Should
    {
        [Fact]
        public void Convert_Actions_To_Actions_Of_Object()
        {
            // arrange
            var booleanCount = 0;
            var singleCount = 0;
            var multiCount = 0;
            Action<string> boolean = s => { booleanCount++; };
            Action<string, string> single = (o, s) => { singleCount++; };
            Action<string, string[]> multi = (o, s) => { multiCount++; };

            // act
            var booleanConv = boolean.ToBaseCallback();
            var singleConv = single.ToBaseCallback();
            var multiConv = multi.ToBaseCallback();
            booleanConv(new object());
            booleanConv("");
            singleConv(new object(), "");
            singleConv("", "");
            multiConv(new object(), "".Split(' '));
            multiConv("", "".Split(' '));

            // assert
            booleanCount.Should().Be(1);
            singleCount.Should().Be(1);
            multiCount.Should().Be(1);
        }
    }
}