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
            var commitParser = new ArgParser<CommitOptions>(() => new CommitOptions())
                .WithName("commit")
                .WithPositional(new Positional<CommitOptions>()
                {
                    TakeWhile = (info, s, i) => i == 0,
                    Transformer = (info, options, arg3) => { }
                })
                .WithTokenSwitch(new Switch<CommitOptions>()
                {
                    GroupLetter = 'a',
                    IsToken = info => info.Cur == "-a" || info.Cur == "--all",
                    TakeWhile = (info, e, i) => false,
                    Transformer = (info, opts, strings) => opts.All = true,
                })
                .WithTokenSwitch(new Switch<CommitOptions>()
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
                        opts.Numbers = strings.Skip(1).Select(x => Convert.ToInt32(x)).ToList(),
                })
                .WithPositional(new Positional<BaseOptions>()
                {
                    TakeWhile = (info, e, i) => i < 1,
                    Transformer = (info, opts, strings) => opts.Things = strings.ToList(),
                })
                .WithSubCommand(new SubCommand<CommitOptions>()
                {
                    IsCommand = info => info.Cur == "commit",
                    ArgParser = commitParser
                });


            // act
            bool isAll = false;
            string message = null;
            IList<int> numbers = null;
            IList<string> things = null;
            parser
                .Parse("commit -am something -n 1 2 3 thing1 thing2".Split(' '))
                .When<CommitOptions>(opts =>
                {
                    isAll = opts.All;
                    message = opts.Message;
                    numbers = opts.Numbers;
                    things = opts.Things;
                });

            // assert
            isAll.Should().BeTrue();
            message.Should().Be("something");
            numbers.Should().BeEquivalentTo(new[]{1,2,3});
            things.Should().BeEquivalentTo(new[] {"thing1", "thing2"});
        }
    }
}