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
        public void Lex_Tokens_Correctly()
        {
            // arrange
            var git = new GitFlavor<GitOptions>();
            git.AddBooleanParameter('h', "help", options => options.IsHelpRequested = true);

            var commit = new GitFlavor<CommitOptions>();
            commit.AddBooleanParameter('a', "all", options => options.IsAddAll = true);
            commit.AddValueSwitch('m', "message", (options, s) => options.Message = s);
            
            git.AddSubCommand("commit", commit);
            git.AddFactoryMethods(() => new GitOptions(), () => new CommitOptions());

            // act
            

            // assert
        }
    }
}
