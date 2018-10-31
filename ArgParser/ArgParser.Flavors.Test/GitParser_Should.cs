using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitParser_Should
    {
        [Fact]
        public void Throw_If_There_Is_No_Way_To_Consume()
        {
            // arrange
            var flavor = new GitFlavor();
            var parser = new GitParser(flavor); // todo: need abstraction
            Action mightThrow = () => parser.Consume(new object(), new DefaultIterationInfo());

            // act
            // assert
            mightThrow.Should().Throw<InvalidOperationException>();
        }
    }
}
