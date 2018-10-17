using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class ArgParser_Should
    {
        private class BaseOptions
        {
            public IList<int> Numbers { get; set; }
            public IList<string> Things { get; set; }
        }

        private class CommitOptions : BaseOptions
        {
            public bool All { get; set; }
            public string Message { get; set; }
        }

        private class AddOptions : BaseOptions
        {
            public bool All { get; set; }
            public IList<string> Files { get; set; } = new List<string>();
        }

        [Fact]
        public void Pass_The_ReadMe_Example()
        {
            // arrange
            var addParser = new SubCommandArgParser<AddOptions, BaseOptions>(() => new AddOptions())
                .WithName("add")
                .WithPositional(new Positional<AddOptions>()
                {
                    TakeWhile = (info, s, arg3) => arg3 == 0
                })
                .WithSwitch(new Switch<AddOptions>()
                {
                    GroupLetter = 'a',
                    IsToken = info => info.Cur == "-a" || info.Cur == "--all",
                    TakeWhile = (info, s, arg3) => false,
                    Transformer = (info, options, arg3) => options.All = true
                })
                .WithPositional(new Positional<AddOptions>()
                {
                    TakeWhile = (info, s, arg3) => true,
                    Transformer = (info, options, arg3) => options.Files = arg3.ToList()
                });

            var commitParser = new SubCommandArgParser<CommitOptions, BaseOptions>(() => new CommitOptions())
                .WithName("commit")
                .WithPositional(new Positional<CommitOptions>()
                {
                    TakeWhile = (info, s, i) => i == 0,
                    Transformer = (info, options, arg3) => { }
                })
                .WithSwitch(new Switch<CommitOptions>()
                {
                    GroupLetter = 'a',
                    IsToken = info => info.Cur == "-a" || info.Cur == "--all",
                    TakeWhile = (info, e, i) => false,
                    Transformer = (info, opts, strings) => opts.All = true,
                })
                .WithSwitch(new Switch<CommitOptions>()
                {
                    GroupLetter = 'm',
                    IsToken = info => info.Cur == "-m" || info.Cur == "--message",
                    TakeWhile = (info, e, i) => i < 1,
                    Transformer = (info, opts, strings) => opts.Message = strings[0],

                });
                
            var parser = new ArgParser<BaseOptions>(() => new BaseOptions())
                .WithName("base")
                .WithTokenSwitch(new Switch<BaseOptions>()
                {
                    GroupLetter = 'n',
                    IsToken = info => new[] {"-n", "--numbers"}.Contains(info.Cur),
                    TakeWhile = (info, e, i) => int.TryParse(e, out var throwAway) && i < 5,

                    Transformer = (info, opts, strings) =>
                        opts.Numbers = strings.Select(x => Convert.ToInt32(x)).ToList(),
                })
                .WithPositional(new Positional<BaseOptions>()
                {
                    TakeWhile = (info, e, i) => true,
                    Transformer = (info, opts, strings) => opts.Things = strings.ToList(),
                })
                .WithSubCommand(new SubCommand<CommitOptions, BaseOptions>()
                {
                    IsCommand = info => info.Cur == "commit",
                    ArgParser = commitParser
                })
                .WithSubCommand(new SubCommand<AddOptions,BaseOptions>()
                {
                    IsCommand = info => info.Cur == "add",
                    ArgParser = addParser
                });


            // act
            // assert
            bool isAddParsed = false;
            bool isCommitParsed = false;
            parser
                .Parse("add -a file1 file2".Split(' '))
                .When<AddOptions>(opts =>
                {
                    isAddParsed = true;
                    opts.All.Should().BeTrue();
                    opts.Files.Should().BeEquivalentTo(new[] {"file1", "file2"});
                });

            parser
                .Parse("commit -am something -n 1 2 3 thing1 thing2".Split(' '))
                .When<CommitOptions>(opts =>
                {
                    isCommitParsed = true;
                    opts.All.Should().BeTrue();
                    opts.Numbers.Should().BeEquivalentTo(new[] {1, 2, 3});
                    opts.Message.Should().Be("something");
                    opts.Things.Should().BeEquivalentTo(new[] {"thing1", "thing2"});
                });

            isAddParsed.Should().BeTrue();
            isCommitParsed.Should().BeTrue();
        }
    }
}