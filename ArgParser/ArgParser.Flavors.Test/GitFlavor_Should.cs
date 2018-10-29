using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public string[] Files { get; set; }
        public string ReuseMessageCommit { get; set; }
    }

    public class GitFlavor_Should
    {
        [Fact]
        public void Parse_Non_Generic_Values()
        {
            // arrange
            var git = new GitFlavor();
            git.AddValueSwitch('h', "help", (o, strings) =>
            {
                if (o is GitOptions go)
                {
                    go.IsHelpRequested = true;
                }
            });
            
            var commit = new GitFlavor();
            git.AddBooleanSwitch('a', "all", o =>
            {
                if (o is CommitOptions co)
                {
                    co.IsAddAll = true;
                }
            });

            git.AddSubCommand("commit", commit);
            git.AddFactoryMethods(() => new GitOptions(), () => new CommitOptions());

            // act
            var result = git.Parse("commit -a -h".Split(' '));

            // assert
            bool isOptionsParsed = false;
            bool isCommitParsed = false;
            result.When<GitOptions>(options =>
            {
                isOptionsParsed = true;
                options.IsHelpRequested.Should().BeTrue();
            });
            result.When<CommitOptions>(options =>
            {
                isCommitParsed = true;
                options.IsAddAll.Should().BeTrue();
            });
            isOptionsParsed.Should().BeTrue();
            isCommitParsed.Should().BeTrue();
        }
    }
}
