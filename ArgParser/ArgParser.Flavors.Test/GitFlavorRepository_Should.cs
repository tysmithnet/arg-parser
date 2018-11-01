using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace ArgParser.Flavors.Test
{
    public class GitFlavorRepository_Should
    {
        [Fact]
        public void Throw_If_Attempting_To_Create_Duplicate_Flavor_Names()
        {
            // arrange
            var repo = new GitFlavorRepository();
            Action mightThrow0 = () => repo.Create("a");
            Action mightThrow1 = () => repo.Create("a");

            // act
            // assert
            mightThrow0.Should().NotThrow();
            mightThrow1.Should().Throw<ArgumentException>();
        }
    }
}
