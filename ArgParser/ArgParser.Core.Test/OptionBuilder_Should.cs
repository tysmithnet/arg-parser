using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class OptionBuilder_Should
    {
        private class WhateverOptions : IOptions
        {
            public string[] OtherGuys { get; set; }
            public int[] OtherThings { get; set; }
            public bool Quiet { get; set; }
            public string Source { get; set; }
            public string Target { get; set; }
            public string[] Things { get; set; }
            public bool Verbose { get; set; }
        }

        private class ManyBoolOptions : IOptions
        {
            public bool AnotherThing { get; set; }
            public bool Something { get; set; }
            public bool SomethingElse { get; set; }
        }

        [Fact]
        public void Allow_For_Validation_Routines()
        {
            // arrange
            var options = new ManyBoolOptions();
            var parser = new ArgParser<ManyBoolOptions>()
                .WithBoolean(new BooleanSwitch<ManyBoolOptions>
                {
                    Letter = 's',
                    Transformer = opts => opts.Something = true
                })
                .WithBoolean(new BooleanSwitch<ManyBoolOptions>
                {
                    Letter = 'e',
                    Transformer = opts => opts.SomethingElse = true
                }).WithBoolean(new BooleanSwitch<ManyBoolOptions>
                {
                    Letter = 'a',
                    Transformer = opts => opts.AnotherThing = true
                })
                .WithValidation((boolOptions, errors) =>
                {
                    if (boolOptions.AnotherThing && boolOptions.Something)
                        errors.Add("Cannot have another thing and something for some reason");
                })
                .WithValidation((boolOptions, errors) =>
                {
                    if (boolOptions.SomethingElse) errors.Add("Cannot have something else either");
                });
            Action throws = () => parser.Parse(options, "-sea".Split(' '));

            // act
            // assert
            throws.Should().Throw<ValidationFailureException>()
                .Where(exception => exception.ValidationMessages.Count == 2);
        }

        [Fact]
        public void Allow_Multiple_Booleans_To_Share_The_Same_Dash()
        {
            // arrange
            var options0 = new ManyBoolOptions();
            var options1 = new ManyBoolOptions();
            var options2 = new ManyBoolOptions();
            var parser = new ArgParser<ManyBoolOptions>()
                .WithBoolean(new BooleanSwitch<ManyBoolOptions>
                {
                    Letter = 's',
                    Transformer = options => options.Something = true
                })
                .WithBoolean(new BooleanSwitch<ManyBoolOptions>
                {
                    Letter = 'e',
                    Transformer = options => options.SomethingElse = true
                }).WithBoolean(new BooleanSwitch<ManyBoolOptions>
                {
                    Letter = 'a',
                    Transformer = options => options.AnotherThing = true
                });

            // act
            parser.Parse(options0, "-sea".Split(' '));
            parser.Parse(options1, "-eas".Split(' '));
            parser.Parse(options2, "-ase".Split(' '));

            // assert
            options0.AnotherThing.Should().BeTrue();
            options0.SomethingElse.Should().BeTrue();
            options0.Something.Should().BeTrue();

            options1.AnotherThing.Should().BeTrue();
            options1.SomethingElse.Should().BeTrue();
            options1.Something.Should().BeTrue();

            options2.AnotherThing.Should().BeTrue();
            options2.SomethingElse.Should().BeTrue();
            options2.Something.Should().BeTrue();
        }

        [Fact]
        public void Allow_Single_Letter_And_Word_Arguments_Boolean()
        {
            // arrange
            var options0 = new WhateverOptions();
            var options1 = new WhateverOptions();

            var parser = new ArgParser<WhateverOptions>()
                .WithBoolean(new BooleanSwitch<WhateverOptions>
                {
                    Word = "aye",
                    Transformer = opts => opts.Target = "a"
                });
            var optionsParser = new ArgParser<WhateverOptions>()
                .WithBoolean(new BooleanSwitch<WhateverOptions>
                {
                    Letter = 'a',
                    Word = "aye",
                    Transformer = opts => opts.Target = "a"
                });

            // act
            parser.Parse(options0, "--aye".Split(' '));
            optionsParser.Parse(options1, "--aye".Split(' '));

            // assert
            options0.Target.Should().Be("a");
            options1.Target.Should().Be("a");
        }

        [Fact]
        public void Allow_Single_Letter_And_Word_Arguments_Multiple()
        {
            // arrange
            var options0 = new WhateverOptions();
            var options1 = new WhateverOptions();

            var parser = new ArgParser<WhateverOptions>()
                .WithMultipleSwitch(new MultipleSwitch<WhateverOptions>
                {
                    Word = "source",
                    Transformer = (o, s) => o.Source = s[0]
                })
                .WithMultipleSwitch(new MultipleSwitch<WhateverOptions>
                {
                    Letter = 't',
                    Word = "target",
                    Transformer = (o, s) => o.Target = s[0]
                });

            // act
            parser.Parse(options0, "--source hi -t world".Split(' '));
            parser.Parse(options1, "--source hi --target world".Split(' '));

            // assert
            options0.Source.Should().Be("hi");
            options0.Target.Should().Be("world");
            options1.Source.Should().Be("hi");
            options1.Target.Should().Be("world");
        }

        [Fact]
        public void Allow_Single_Letter_And_Word_Arguments_Single()
        {
            // arrange
            var options0 = new WhateverOptions();
            var options1 = new WhateverOptions();

            var parser = new ArgParser<WhateverOptions>()
                .WithSingleSwitch(new SingleSwitch<WhateverOptions>
                {
                    Word = "source",
                    Transformer = (options, s) => options.Source = s
                })
                .WithSingleSwitch(new SingleSwitch<WhateverOptions>
                {
                    Word = "target",
                    Letter = 't',
                    Transformer = (options, s) => options.Target = s
                });

            // act
            parser.Parse(options0, "--source hi -t world".Split(' '));
            parser.Parse(options1, "--source hi --target world".Split(' '));

            // assert
            options0.Source.Should().Be("hi");
            options0.Target.Should().Be("world");
            options1.Source.Should().Be("hi");
            options1.Target.Should().Be("world");
        }

        [Fact]
        public void Pass_Basic_Test_Cases()
        {
            // arrange
            var options = new WhateverOptions();
            var options2 = new WhateverOptions();
            var parser = new ArgParser<WhateverOptions>()
                .WithBoolean(new BooleanSwitch<WhateverOptions>
                {
                    Letter = 'v',
                    Transformer = opts => opts.Verbose = true
                })
                .WithMultipleSwitch(new MultipleSwitch<WhateverOptions>
                {
                    Letter = 't',
                    Transformer = (opts, strings) => opts.Things = strings
                })
                .WithMultipleSwitch(new MultipleSwitch<WhateverOptions>
                {
                    Letter = 'o',
                    Transformer = (opts, strings) =>
                        opts.OtherThings = strings.Select(x => Convert.ToInt32(x)).ToArray()
                })
                .WithPositional(new PositionalValues<WhateverOptions>
                {
                    Min = 1,
                    Max = 1,
                    Transformer = (opts, strings) => opts.Source = strings[0]
                })
                .WithPositional(new PositionalValues<WhateverOptions>
                {
                    Transformer = (opts, strings) => opts.OtherGuys = strings
                });

            // act
            parser.Parse(options, "-v source otherguy1 otherguy2 -t thing1 thing2 -o 1 2 3".Split(' '));
            parser.Parse(options2, "source otherguy1 otherguy2 -o 1 2 3 -t thing1 thing2 -v".Split(' '));

            // assert
            options.Verbose.Should().BeTrue();
            options.Source.Should().Be("source",
                "source is the first positional argument and the source positional was registered first");
            options.OtherGuys.Should().BeEquivalentTo(new[] {"otherguy1", "otherguy2"},
                "the other guy positional arguments are the second and third but the fourth is a registered switch so we stop after 2");
            options.Things.Should().BeEquivalentTo(new[] {"thing1", "thing2"},
                "things is a multiple value switch and the args leading up to the next switch are thing1 and thing2");
            options.OtherThings.Should().BeEquivalentTo(new[] {1, 2, 3}, "There are three ints that follow -o");

            options2.Verbose.Should().BeTrue();
            options2.Source.Should().Be("source");
            options2.OtherGuys.Should().BeEquivalentTo("otherguy1", "otherguy2");
            options2.Things.Should().BeEquivalentTo("thing1", "thing2");
            options2.OtherThings.Should().BeEquivalentTo(new[] {1, 2, 3});
        }

        [Fact]
        public void Recognize_Compound_Bool_Switches_When_Taking_Multiple_Values()
        {
            // arrange
            var options = new WhateverOptions();
            var parser = new ArgParser<WhateverOptions>()
                .WithBoolean(new BooleanSwitch<WhateverOptions>
                {
                    Letter = 'v',
                    Transformer = opts => opts.Verbose = true
                })
                .WithBoolean(new BooleanSwitch<WhateverOptions>
                {
                    Letter = 'q',
                    Transformer = opts => opts.Quiet = true
                })
                .WithPositional(new PositionalValues<WhateverOptions>
                {
                    Transformer = (opts, strings) => opts.Things = strings
                });

            // act
            parser.Parse(options, "hello world -vq hi again".Split(' '));

            // assert
            options.Things.Should().BeEquivalentTo("hello", "world");
            options.Verbose.Should().BeTrue();
            options.Quiet.Should().BeTrue();
        }

        [Fact]
        public void Return_Only_As_Many_As_Asked_For()
        {
            // arrange
            var options = new WhateverOptions();

            var parser = new ArgParser<WhateverOptions>()
                .WithSingleSwitch(new SingleSwitch<WhateverOptions>
                {
                    Letter = 's',
                    Transformer = (opts, s) => opts.Source = s
                })
                .WithMultipleSwitch(new MultipleSwitch<WhateverOptions>
                {
                    Letter = 't',
                    Transformer = (whateverOptions, strings) => whateverOptions.Things = strings,
                    Max = 3,
                    Min = 3
                })
                .WithMultipleSwitch(new MultipleSwitch<WhateverOptions>
                {
                    Letter = 'o',
                    Transformer = (whateverOptions, strings) =>
                        whateverOptions.OtherThings = strings.Select(x => Convert.ToInt32(x)).ToArray(),
                    Max = 3,
                    Min = 3
                })
                .WithPositional(new PositionalValues<WhateverOptions>
                {
                    Transformer = (whateverOptions, strings) => whateverOptions.Target = string.Join(", ", strings)
                });

            // act
            parser.Parse(options, "-t a b c d e f -s g h -o 1 2 3".Split(' '));

            // assert
            options.Things.Should().BeEquivalentTo("a b c".Split(' '));
            options.Source.Should().Be("g");
            options.OtherThings.Should().BeEquivalentTo(new[] {1, 2, 3});
            options.Target.Should().Be("d, e, f");
        }

        [Fact]
        public void Throw_If_A_Required_Parameter_Is_Not_Present()
        {
            // arrange
            var options = new WhateverOptions();
            var parser = new ArgParser<WhateverOptions>()
                .WithSingleSwitch(new SingleSwitch<WhateverOptions>
                {
                    Word = "verbose",
                    Required = true
                });
            Action throws0 = () => parser.Parse(options, "".Split(' '));

            // act
            // assert
            throws0.Should().Throw<ValidationFailureException>()
                .Where(exception => exception.ValidationMessages.Count == 1);
        }

        [Fact]
        public void Throw_If_Missing_Values()
        {
            // arrange
            var options = new WhateverOptions();
            var parser = new ArgParser<WhateverOptions>()
                .WithMultipleSwitch(new MultipleSwitch<WhateverOptions>()
                {
                    Letter = 't',
                    Transformer = (opts, strings) => opts.Things = strings,
                    Min = 5,
                    Max = 5
                })
                .WithSingleSwitch(new SingleSwitch<WhateverOptions>
                {
                    Letter = 's',
                    Transformer = (opts, s) => opts.Source = s
                })
                .WithPositional(new PositionalValues<WhateverOptions>
                {
                    Min = 10,
                    Transformer = (opts, strings) => opts.OtherGuys = strings
                });

            Action throws0 = () => parser.Parse(options, "-t a b c -s source a b c d e f g h i j k l m n".Split(' '));
            Action throws1 = () => parser.Parse(options, "-t a b c d e f f g h i j k l m n -s".Split(' '));
            Action throws2 = () => parser.Parse(options, "-t a b c d e f -s g h i j -t".Split(' '));
            Action throws3 = () => parser.Parse(null, new[] {""});
            Action throws4 = () => parser.Parse(new WhateverOptions(), null);
            Action throws5 = () => parser.Parse(options, "-t a b -s source".Split(' '));
            // act
            // assert
            throws0.Should().Throw<MissingValueException>();
            throws1.Should().Throw<MissingValueException>();
            throws2.Should().Throw<MissingValueException>();
            throws3.Should().Throw<ArgumentNullException>();
            throws4.Should().Throw<ArgumentNullException>();
            throws5.Should().Throw<MissingValueException>();
        }

        [Fact]
        public void Throw_If_Missing_Values2()
        {
            // arrange
            var options = new WhateverOptions();
            var parser = new ArgParser<WhateverOptions>()
                .WithMultipleSwitch(new MultipleSwitch<WhateverOptions>()
                {
                    Letter = 'o',
                    Transformer =  (opts, strings) => opts.OtherGuys = strings,
                    Min = 4,
                    Max = 4
                })  
                .WithSingleSwitch(new SingleSwitch<WhateverOptions>
                {
                    Letter = 's',
                    Transformer = (opts, s) => opts.Source = s
                })
                .WithSingleSwitch(new SingleSwitch<WhateverOptions>
                {
                    Letter = 't',
                    Transformer = (opts, s) => opts.Target = s
                });

            Action throws0 = () => parser.Parse(options, "-s -t".Split(' '));
            Action throws1 = () => parser.Parse(options, "-o hi world".Split(' '));

            // act
            // assert
            throws0.Should().Throw<MissingValueException>();
            throws1.Should().Throw<MissingValueException>();
        }
    }
}