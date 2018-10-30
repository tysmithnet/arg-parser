using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitToken_Should
    {
        [Fact]
        public void Have_The_Correct_Key()
        {
            // arrange
            var token = new GitToken("--something=other");

            // act
            // assert
            token.Key.Should().Be("something");
            token.Value.Should().Be("other");
        }
    }
}
