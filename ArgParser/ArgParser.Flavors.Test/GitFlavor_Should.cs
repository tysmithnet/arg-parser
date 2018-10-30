using System.Collections.Generic;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class GitOptions
    {
        public List<string> CurrentWorkingPaths { get; set; } = new List<string>();
        public bool IsHelpRequested { get; set; }
        public bool IsVersionRequested { get; set; }
    }

    public class CommitOptions : GitOptions
    {
        public string[] Files { get; set; }
        public bool IsAddAll { get; set; }
        public string Message { get; set; }
        public string ReuseMessageCommit { get; set; }
    }

    public class PushOptions : GitOptions
    {
        public List<string> RefSpec { get; set; } = new List<string>();
        public string Repository { get; set; }
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
                if (o is GitOptions go) go.IsHelpRequested = true;
            });

            var commit = new GitFlavor();
            git.AddBooleanSwitch('a', "all", o =>
            {
                if (o is CommitOptions co) co.IsAddAll = true;
            });

            git.AddSubCommand("commit", commit);
            git.AddFactoryMethods(() => new GitOptions(), () => new CommitOptions());

            // act
            var result = git.Parse("commit -a -h".Split(' '));

            // assert
            var optionsParsedCount = 0;
            var commitParsedCount = 0;
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
        public void Parse_Multiple_Positionals()
        {
            // arrange
            var git = new GitFlavor();

            var push = new GitFlavor();
            push.AddPositional((o, s) =>
            {
                if (o is PushOptions po)
                    po.Repository = s;
            });
            push.AddPositionals((o, strings) =>
            {
                if(o is PushOptions po)
                    po.RefSpec.AddRange(strings);
            });

            git.AddFactoryMethods(() => new PushOptions());
            git.AddSubCommand("push", push);

            // act
            var result = git.Parse("push origin master develop".Split(' '));

            // assert
            int parseCount = 0;

            result.When<PushOptions>(options =>
            {
                parseCount++;
                options.Repository.Should().Be("origin");
                options.RefSpec.Should().BeEquivalentTo(new[] {"master", "develop"});
            });

            parseCount.Should().Be(1);
        }

        [Fact]
        public void Parse_Switches_In_A_Hierarchy()
        {
            // arrange
            var git = new GitFlavor();
            git.AddSingleValueSwitch('C', null, (o, s) =>
            {
                if (o is GitOptions casted)
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
            var optionsParsedCount = 0;
            var commitParsedCount = 0;
            result.When<GitOptions>(options => { optionsParsedCount++; });
            result.When<CommitOptions>(options =>
            {
                commitParsedCount++;
                options.CurrentWorkingPaths.Should().BeEquivalentTo("path1", "path2");
                options.Message.Should().Be("something");
            });
            optionsParsedCount.Should().Be(1);
            commitParsedCount.Should().Be(1);
        }
    }
}