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
        public void Correctly_Parse_Groups()
        {
            // arrange
            var git = new GitFlavor<GitOptions>(() => new GitOptions());
            git.AddBooleanSwitch('h', "help", o => o.IsHelpRequested = true);

            var commit = new GitFlavor<CommitOptions>(() => new CommitOptions());
            commit.AddBooleanSwitch('a', "all", o => o.IsAddAll = true);
            commit.AddValueSwitch('m', "message", (o, v) => o.Message = v);
            commit.AddPositionalList((o, v) => o.Files = v);
            commit.AddValueSwitch('C', "reuse-message", (o, v) => o.ReuseMessageCommit = v, separator: "=", regex: new Regex("^[a-fA-F0-9]$"));

            git.AddSubCommand("commit", commit);

            // act
            var result = git.Parse("commit -am \"something\"".Split(' '));

            // assert
            bool isParsed = false;
            result.When<CommitOptions>(o => {
                isParsed = true;
                o.IsAddAll.Should().BeTrue();
                o.Message.Should().Be("something");
            });
            isParsed.Should().BeTrue();
        }
    }
}
