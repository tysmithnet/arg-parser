using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    #region Git Examples
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
    #endregion

    #region DotNet Core Example

    public class DotNetOptions
    {

    }

    public class DotNetNewOptions : DotNetOptions
    {

    }

    #endregion
    
    #region Trivial Example

    public class BaseOptions
    {
        public bool IsHelpRequested { get; set; }
        public string BasePositional0 { get; set; }
        public string BaseSwitch0 { get; set; }
    }

    public class Child0Options : BaseOptions
    {
        public string Child0Positional0 { get; set; }
        public string[] Child0Positional1 { get; set; }
    }

    public class Child1Options : BaseOptions
    {
        public string Child1Positional0 { get; set; }
    }

    public class Child0Child0Options : Child0Options
    {
        public string[] Child0Child0Positional0 { get; set; }
    }

    public class Child1Child0Options : Child1Options
    {
        public string Child1Child1Positional0 { get; set; }
    }

    public class Child0Child1Options : Child0Options
    {
        public string[] Child0Child1Positional0 { get; set; }
    }

    public class Child1Child1Options : Child1Options
    {
        public string Child1Child1Positional0 { get; set; }
    }

    #endregion


    public class GitFlavor_Should
    {
        [Fact]
        public void Parse_The_Most_Specific_SubCommands_Positionals_First()
        {
            // arrange
            var baze = new GitFlavor();
            baze.Name = "base";
            baze.AddPositional((o, s) =>
            {
                if (o is BaseOptions b)
                    b.BasePositional0 = s;
            });

            var child0 = new GitFlavor();
            child0.Name = "child0";
            child0.AddPositional((o, s) =>
            {
                if (o is Child0Options c)
                    c.Child0Positional0 = s;
            });
            child0.AddPositionals((o, s) =>
            {
                if (o is Child0Options c)
                    c.Child0Positional1 = s;
            }, 2, 2);


            var child1 = new GitFlavor();
            child1.Name = "child1";
            child1.AddPositional((o, s) =>
            {
                if (o is Child1Options c)
                    c.Child1Positional0 = s;
            });

            var child0child0 = new GitFlavor();
            child0child0.Name = "child0child0";
            child0child0.AddPositionals((o, s) =>
            {
                if (o is Child0Child0Options c)
                    c.Child0Child0Positional0 = s;
            }, 2,2);

            var child0child1 = new GitFlavor();
            child0child1.Name = "child0child1";
            child0child1.AddPositionals((o, s) =>
            {
                if (o is Child0Child1Options c)
                    c.Child0Child1Positional0 = s;
            });

            var child1child0 = new GitFlavor();
            child1child0.Name = "child1child0";
            child1child0.AddPositional((o, s) =>
            {
                if (o is Child1Child0Options c)
                    c.Child1Child1Positional0 = s;
            });

            var child1child1 = new GitFlavor();
            child1child1.Name = "child1child1";
            child1child1.AddPositional((o, s) =>
            {
                if (o is Child1Child1Options c)
                    c.Child1Child1Positional0 = s;
            });

            baze.AddSubCommand("child0", child0);
            baze.AddSubCommand("child1", child1);

            child0.AddSubCommand("child0child0", child0child0);
            child0.AddSubCommand("child0child1", child0child1);

            child1.AddSubCommand("child1child0", child1child0);
            child1.AddSubCommand("child1child1", child1child1);
            baze.AddFactoryMethods(() => new Child0Child0Options(), () => new Child0Child1Options(), () => new Child0Options());
            
            // act
            var result = baze.Parse("child0 child0child0 p0 p1 p2 p3 p4".Split(' '));

            // assert
            int baseCount = 0;
            int childCount = 0;
            int childchildCount = 0;
            result.When<BaseOptions>(options => { baseCount++; });
            result.When<Child0Options>(options =>
            {
                childCount++;
            });
            result.When<Child0Child0Options>(options =>
            {
                childchildCount++;
                options.Child0Child0Positional0.Should().BeEquivalentTo(new[] {"p0", "p1"});
                options.Child0Positional0.Should().Be("p2");
                options.Child0Positional1.Should().BeEquivalentTo(new[] {"p3", "p4"});
            });

            baseCount.Should().Be(0);
            childCount.Should().Be(0);
            childchildCount.Should().Be(1);
        }

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