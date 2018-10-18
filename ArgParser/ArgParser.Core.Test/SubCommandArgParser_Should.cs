using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class SubCommandArgParser_Should
    {
        private class BaseOptions
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private class WhateverOptions : BaseOptions
        {
            public string OtherThing { get; set; }
        }

        [Fact]
        public void Throw_If_Not_Configured_To_Parse_All_Args()
        {
            // arrange
            var parser = new SubCommandArgParser<WhateverOptions, BaseOptions>(() => new WhateverOptions());
            Action throws = () => parser.Parse("this is something".Split(' '));

            // act
            // assert
            throws.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Fallback_On_The_Parent_Switch_Strategy()
        {
            // arrange
            var whateverParser = new SubCommandArgParser<WhateverOptions, BaseOptions>(() => new WhateverOptions())
                .WithPositional(new Positional<WhateverOptions>()
                {
                    TakeWhile = (info, element, number) => number == 0
                })
                .WithSwitch(new Switch<WhateverOptions>()
                {
                    IsToken = info => info.Cur == "-a",
                    TakeWhile = (info, element, number) => number == 0,
                    Transformer = (info, instance, strings) => instance.OtherThing = strings[0]
                });

            var baseParser = new ArgParser<BaseOptions>(() => new BaseOptions())
                .WithSwitch(new Switch<BaseOptions>()
                {
                    IsToken = info => info.Cur == "-b",
                    TakeWhile = (info, element, number) => true,
                    Transformer = (info, instance, strings) => instance.Name = strings[0]
                })
                .WithSwitch(new Switch<BaseOptions>()
                {
                    TakeWhile = (info, element, number) => number == 0,
                    GroupLetter = 'i',
                    Transformer = (info, instance, strings) => instance.Age = Convert.ToInt32(strings[0]),
                })
                .WithSubCommand(new SubCommand<WhateverOptions,BaseOptions>()
                {
                    IsCommand = info => info.Cur == "add",
                    ArgParser = whateverParser
                });
            bool isParsed = false;
            bool isParsed2 = false;

            // act
            baseParser.Parse("add -a something -b other things".Split(' '))
                .When<WhateverOptions>(options =>
                {
                    isParsed = true;
                    options.OtherThing.Should().Be("something");
                    options.Name.Should().Be("other");
                });

            baseParser.Parse("add -a something -b other -i 1".Split(' '))
                .When<WhateverOptions>(options =>
                {
                    isParsed2 = true;
                    options.Age.Should().Be(1);
                    options.Name.Should().Be("other");
                    options.OtherThing.Should().Be("something");
                });

            // assert
            isParsed.Should().BeTrue();
            isParsed2.Should().BeTrue();
        }

        [Fact]
        public void Also_Use_Parent_Validation()
        {
            // arrange
            var whateverParser = new SubCommandArgParser<WhateverOptions, BaseOptions>(() => new WhateverOptions())
                .WithPositional(new Positional<WhateverOptions>()
                {
                    TakeWhile = (info, element, number) => number == 0
                })
                .WithSwitch(new Switch<WhateverOptions>()
                {
                    IsToken = info => info.Cur == "-a",
                    TakeWhile = (info, element, number) => number == 0,
                    Transformer = (info, instance, strings) => instance.OtherThing = strings[0]
                });

            var baseParser = new ArgParser<BaseOptions>(() => new BaseOptions())
                .WithSwitch(new Switch<BaseOptions>()
                {
                    IsToken = info => info.Cur == "-b",
                    TakeWhile = (info, element, number) => true,
                    Transformer = (info, instance, strings) => instance.Name = strings[0]
                })
                .WithSwitch(new Switch<BaseOptions>()
                {
                    TakeWhile = (info, element, number) => number == 0,
                    GroupLetter = 'i',
                    Transformer = (info, instance, strings) => instance.Age = Convert.ToInt32(strings[0]),
                })
                .WithSubCommand(new SubCommand<WhateverOptions, BaseOptions>()
                {
                    IsCommand = info => info.Cur == "add",
                    ArgParser = whateverParser
                })
                .WithValidation((info, instance) =>
                {
                    if (instance.Name == "other")
                    {
                        info.Errors.Add(new FormatError("Name cannot be other for some reason"));
                    }
                });
            bool isParsed = false;

            // act
            baseParser.Parse("add -a something -b other things".Split(' '))
                .WhenErrored(info => 
                {
                    isParsed = true;
                    info.Errors.Should().HaveCount(1);
                });


            // assert
            isParsed.Should().BeTrue();
        }
    }
}
