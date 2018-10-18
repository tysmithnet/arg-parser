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
        public void Allow_For_Single_Letter_Grouping()
        {
            // arrange
            var isParsed = false;
            var isSingleParsed = false;

            var parser = new ArgParser<CommitOptions>(() => new CommitOptions())
                .WithSwitch(new Switch<CommitOptions>
                {
                    GroupLetter = 'a',
                    TakeWhile = (info, s, arg3) => false,
                    Transformer = (info, options, arg3) => options.All = true
                })
                .WithSwitch(new Switch<CommitOptions>
                {
                    GroupLetter = 'm',
                    IsToken = info => info.Cur == "--message",
                    TakeWhile = (info, s, arg3) => arg3 == 0,
                    Transformer = (info, options, arg3) => options.Message = arg3[0]
                }).WithPositional(new Positional<CommitOptions>
                {
                    TakeWhile = (info, s, arg3) => true,
                    Transformer = (info, options, arg3) => options.Things = arg3.ToList()
                });

            // act
            // assert
            parser.Parse("-am something".Split(' '))
                .When<CommitOptions>(options =>
                {
                    isParsed = true;
                    options.All.Should().BeTrue();
                    options.Message.Should().Be("something");
                });

            parser.Parse("-m something".Split(' '))
                .When<CommitOptions>(options =>
                {
                    isSingleParsed = true;
                    options.All.Should().BeFalse();
                    options.Message.Should().Be("something");
                });

            isParsed.Should().BeTrue();
            isSingleParsed.Should().BeTrue();
        }

        [Fact]
        public void Work_When_Only_A_Single_Group_Letter_Exists()
        {
            // arrange
            var isParsed = false;
            var isParsed2 = false;
            var parser = new ArgParser<CommitOptions>(() => new CommitOptions())
                .WithSwitch(new Switch<CommitOptions>()
                {
                    GroupLetter = 'a',
                    TakeWhile = (info, element, number) => false,
                    Transformer =(info, instance, strings) => instance.All = true
                });


            // act
            // assert
            parser.Parse("-a".Split(' '))
                .When<CommitOptions>(options =>
                {
                    isParsed = true;
                    options.All.Should().BeTrue();
                });

            parser.Parse("-aaa".Split(' '))
                .When<CommitOptions>(options =>
                {
                    isParsed2 = true;
                    options.All.Should().BeTrue();
                });

            isParsed.Should().BeTrue();
            isParsed2.Should().BeTrue();
        }

        [Fact]
        public void Throw_If_The_Configuration_Is_Insufficient_For_Args()
        {
            // arrange
            var parser = new ArgParser<CommitOptions>(() => new CommitOptions());
            Action throws = () => parser.Parse("-t this is something".Split(' '));
            
            // act
            // assert
            throws.Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public void Pass_A_Basic_Use_Case()
        {
            // arrange
            var isParsed = false;
            var isValidated = false;
            var parser = new ArgParser<CommitOptions>(() => new CommitOptions())
                .WithSwitch(new Switch<CommitOptions>
                {
                    GroupLetter = 'a',
                    IsToken = info => info.Cur == "-a",
                    TakeWhile = (info, s, arg3) => false,
                    Transformer = (info, options, takenStrings) => options.All = true
                })
                .WithSwitch(new Switch<CommitOptions>
                {
                    IsToken = info => info.Cur == "--message",
                    TakeWhile = (info, s, arg3) => arg3 == 0,
                    Transformer = (info, options, takenStrings) => options.Message = takenStrings[0]
                }).WithPositional(new Positional<CommitOptions>
                {
                    TakeWhile = (info, s, arg3) => true,
                    Transformer = (info, options, takenStrings) => options.Things = takenStrings.ToList()
                })
                .WithValidation((info, instance) =>
                {
                    if (instance.Message.Length < 10)
                    {
                        info.AddError(new FormatError("Message is too short"));
                    }
                });

            // act
            // assert
            parser.Parse("-aaa --message something thing1 thing2".Split(' '))
                .When<CommitOptions>(options =>
                {
                    isParsed = true;
                    options.All.Should().BeTrue();
                    options.Message.Should().Be("something");
                    options.Things.Should().BeEquivalentTo("thing1", "thing2");
                });
            parser.Parse("--message four".Split(' '))
                .WhenErrored(info =>
                {
                    isValidated = true;
                    ((IterationInfo) info).Errors.Should().HaveCount(1);
                });

            isParsed.Should().BeTrue();
            isValidated.Should().BeTrue();
        }

        [Fact]
        public void Pass_The_ReadMe_Example()
        {
            // arrange
            var addParser = new SubCommandArgParser<AddOptions, BaseOptions>(() => new AddOptions())
                .WithName("add")
                .WithPositional(new Positional<AddOptions>
                {
                    TakeWhile = (info, s, arg3) => arg3 == 0
                })
                .WithSwitch(new Switch<AddOptions>
                {
                    GroupLetter = 'a',
                    IsToken = info => info.Cur == "-a" || info.Cur == "--all",
                    TakeWhile = (info, s, arg3) => false,
                    Transformer = (info, options, arg3) => options.All = true
                })
                .WithPositional(new Positional<AddOptions>
                {
                    TakeWhile = (info, s, arg3) => true,
                    Transformer = (info, options, arg3) => options.Files = arg3.ToList()
                })
                .WithValidation((info, instance) =>
                {
                    if (!instance.All && instance.Files == null || instance.Files.Count == 0)
                        ((IterationInfo)info).Errors.Add(new CardinalityError($"You must either specify all or identify files"));
                });

            var commitParser = new SubCommandArgParser<CommitOptions, BaseOptions>(() => new CommitOptions())
                .WithName("commit")
                .WithPositional(new Positional<CommitOptions>
                {
                    TakeWhile = (info, s, i) => i == 0,
                    Transformer = (info, options, arg3) => { }
                })
                .WithSwitch(new Switch<CommitOptions>
                {
                    GroupLetter = 'a',
                    IsToken = info => info.Cur == "-a" || info.Cur == "--all",
                    TakeWhile = (info, e, i) => false,
                    Transformer = (info, opts, strings) => opts.All = true
                })
                .WithSwitch(new Switch<CommitOptions>
                {
                    GroupLetter = 'm',
                    IsToken = info => info.Cur == "-m" || info.Cur == "--message",
                    TakeWhile = (info, e, i) => i < 1,
                    Transformer = (info, opts, strings) => opts.Message = strings[0]
                })
                .WithValidation((info, instance) =>
                {
                    if (instance.Message.Length > 10)
                        ((IterationInfo)info).Errors.Add(
                            new FormatError("Expected message to be 10 or less characters for some reason"));
                });

            var parser = new ArgParser<BaseOptions>(() => new BaseOptions())
                .WithName("base")
                .WithSwitch(new Switch<BaseOptions>
                {
                    GroupLetter = 'n',
                    IsToken = info => new[] {"-n", "--numbers"}.Contains(info.Cur),
                    TakeWhile = (info, e, i) => int.TryParse(e, out var throwAway) && i < 5,

                    Transformer = (info, opts, strings) =>
                        opts.Numbers = strings.Select(x => Convert.ToInt32(x)).ToList()
                })
                .WithPositional(new Positional<BaseOptions>
                {
                    TakeWhile = (info, e, i) => true,
                    Transformer = (info, opts, strings) => opts.Things = strings.ToList()
                })
                .WithSubCommand(new SubCommand<CommitOptions, BaseOptions>
                {
                    IsCommand = info => info.Cur == "commit",
                    ArgParser = commitParser
                })
                .WithSubCommand(new SubCommand<AddOptions, BaseOptions>
                {
                    IsCommand = info => info.Cur == "add",
                    ArgParser = addParser
                });

            // act
            // assert
            var isAddParsed = false;
            var isCommitParsed = false;
            var isAddValidated = false;
            var isCommitValidated = false;

            parser
                .Parse("add -a file1 file2".Split(' '))
                .When<AddOptions>(opts =>
                {
                    isAddParsed = true;
                    opts.All.Should().BeTrue();
                    opts.Files.Should().BeEquivalentTo("file1", "file2");
                });

            parser
                .Parse("add".Split(' '))
                .When<AddOptions>(opts => { })
                .WhenErrored(info =>
                {
                    isAddValidated = true;
                    ((IterationInfo)info).Errors.Should().HaveCount(1);
                });

            parser
                .Parse("commit -am something -n 1 2 3 thing1 thing2".Split(' '))
                .When<CommitOptions>(opts =>
                {
                    isCommitParsed = true;
                    opts.All.Should().BeTrue();
                    opts.Numbers.Should().BeEquivalentTo(new[] {1, 2, 3});
                    opts.Message.Should().Be("something");
                    opts.Things.Should().BeEquivalentTo("thing1", "thing2");
                });

            parser
                .Parse("commit -m somethingreallynotthatlong -n 1 2 3 thing1 thing2".Split(' '))
                .WhenErrored(info =>
                {
                    isCommitValidated = true;
                    ((IterationInfo)info).Errors.Should().HaveCount(1);
                });

            isAddParsed.Should().BeTrue();
            isAddValidated.Should().BeTrue();
            isCommitParsed.Should().BeTrue();
            isCommitValidated.Should().BeTrue();
        }
    }
}