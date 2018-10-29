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
        public List<string> CurrentWorkingPaths { get; set; } = new List<string>();
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
        public void Parse_Boolean_Switches_In_A_Hierarchy()
        {
            // arrange
            var git = new GitFlavor();
            git.AddBooleanSwitch('h', "help", o =>
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
            int optionsParsedCount = 0;
            int commitParsedCount = 0;
            result.When<GitOptions>(options =>
            {
                optionsParsedCount++;
                options.IsHelpRequested.Should().BeTrue();
            });
            result.When<CommitOptions>(options =>
            {
                commitParsedCount++;
                options.IsAddAll.Should().BeTrue();
            });
            optionsParsedCount.Should().Be(1);
            commitParsedCount.Should().Be(1);
        }

        [Fact]
        public void Parse_Switches_In_A_Hierarchy()
        {
            // arrange
            var git = new GitFlavor();
            git.AddSingleValueSwitch('C', null, (o, s) =>
            {
                if(o is GitOptions casted)
                    casted.CurrentWorkingPaths.Add(s);
            });

            var commit = new GitFlavor();
            commit.AddSingleValueSwitch('m', "message", (o, s) =>
            {
                if (o is CommitOptions casted)
                    casted.Message = s;
            });

            git.AddSubCommand("commit", commit);
            git.AddFactoryMethods(() => new GitOptions(), () => new CommitOptions());

            // act
            var result = git.Parse("commit -C path1 -C path2 -m something".Split(' '));

            // assert
            int optionsParsedCount = 0;
            int commitParsedCount = 0;
            result.When<GitOptions>(options =>
            {
                optionsParsedCount++;
            });
            result.When<CommitOptions>(options =>
            {
                commitParsedCount++;
                options.IsAddAll.Should().BeTrue();
                options.CurrentWorkingPaths.Should().BeEquivalentTo(new[] { "path1", "path2" });

            });
            optionsParsedCount.Should().Be(0);
            commitParsedCount.Should().Be(1);
        }
    }
}
