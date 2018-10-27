using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitOptions
    {
        public bool IsHelpRequested { get; set; }
        public bool IsVersionRequested { get; set; }
    }

    public class CommitOptions : GitOptions
    {
        public bool IsAddAll { get; set; }
        public string Message { get; set; }
    }

    public class GitFlavor_Should
    {

        [Fact]
        public void Identify_Help_Requested()
        {
            // arrange
            var flavor = new GitFlavor<CommitOptions>(new Func<CommitOptions>[] {() => new CommitOptions(), });
            flavor.Parameters.Add(new GitParameter<CommitOptions>()
            {
                Letter = 'h',
                Word = "help",
                ConsumeCallback = (o, info) =>
                {
                    o.
                }
            });

            // act
            var result = flavor.Parse("--help".Split(' '));

            // assert
            bool isParsed = false;
            result.When<CommitOptions>(options =>
            {
                isParsed = true;
                options.IsHelpRequested.Should().BeTrue();
            });
            isParsed.Should().BeTrue();
        }
    }
}
