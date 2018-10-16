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

        [Fact]
        public void Allow_Single_Letter_And_Word_Arguments_Single()
        {
            // arrange
            var options0 = new WhateverOptions();
            var options1 = new WhateverOptions();

            var builder = new OptionsBuilder<WhateverOptions>()
                .WithSingleSwitch("source", (o, s) => o.Source = s)
                .WithSingleSwitch('t', "target", (whateverOptions, s) => whateverOptions.Target = s);

            // act
            builder.Parse(options0, "--source hi -t world".Split(' '));
            builder.Parse(options1, "--source hi --target world".Split(' '));

            // assert
            options0.Source.Should().Be("hi");
            options0.Target.Should().Be("world");
            options1.Source.Should().Be("hi");
            options1.Target.Should().Be("world");
        }

        [Fact]
        public void Allow_Single_Letter_And_Word_Arguments_Multiple()
        {
            // arrange
            var options0 = new WhateverOptions();
            var options1 = new WhateverOptions();

            var builder = new OptionsBuilder<WhateverOptions>()
                .WithMultipleSwitch("source", (o, s) => o.Source = s[0], count: 1)
                .WithMultipleSwitch('t', "target", (whateverOptions, s) => whateverOptions.Target = s[0], count:1);

            // act
            builder.Parse(options0, "--source hi -t world".Split(' '));
            builder.Parse(options1, "--source hi --target world".Split(' '));

            // assert
            options0.Source.Should().Be("hi");
            options0.Target.Should().Be("world");
            options1.Source.Should().Be("hi");
            options1.Target.Should().Be("world");
        }

        [Fact]
        public void Allow_Single_Letter_And_Word_Arguments_Boolean()
        {
            // arrange
            var options0 = new WhateverOptions();
            var options1 = new WhateverOptions();

            var builder0 = new OptionsBuilder<WhateverOptions>()
                .WithBoolean("aye", options => options.Target = "a");
            var builder1 = new OptionsBuilder<WhateverOptions>()
                .WithBoolean('a', "aye", options => options.Target = "a");

            // act
            builder0.Parse(options0, "--aye".Split(' '));
            builder1.Parse(options1, "--aye".Split(' '));

            // assert
            options0.Target.Should().Be("a");
            options1.Target.Should().Be("a");
        }

        [Fact]
        public void Pass_Basic_Test_Cases()
        {
            // arrange
            var options = new WhateverOptions();
            var options2 = new WhateverOptions();
            var builder = new OptionsBuilder<WhateverOptions>()
                .WithBoolean('v', whateverOptions => whateverOptions.Verbose = true)
                .WithMultipleSwitch('t', (whateverOptions, strings) => whateverOptions.Things = strings)
                .WithMultipleSwitch('o',
                    (whateverOptions, strings) =>
                        whateverOptions.OtherThings = strings.Select(x => Convert.ToInt32(x)).ToArray())
                .WithPositional(1, (whateverOptions, strings) => whateverOptions.Source = strings[0])
                .WithPositional(-1, (whateverOptions, strings) => whateverOptions.OtherGuys = strings);

            // act
            builder.Parse(options, "-v source otherguy1 otherguy2 -t thing1 thing2 -o 1 2 3".Split(' '));
            builder.Parse(options2, "source otherguy1 otherguy2 -o 1 2 3 -t thing1 thing2 -v".Split(' '));

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
        public void Return_Only_As_Many_As_Asked_For()
        {
            // arrange
            var options = new WhateverOptions();

            var builder = new OptionsBuilder<WhateverOptions>()
                .WithSingleSwitch('s', (whateverOptions, s) => whateverOptions.Source = s)
                .WithMultipleSwitch('t', (whateverOptions, strings) => whateverOptions.Things = strings, 3)
                .WithMultipleSwitch('o',
                    (whateverOptions, strings) =>
                        whateverOptions.OtherThings = strings.Select(x => Convert.ToInt32(x)).ToArray())
                .WithPositional(-1, (whateverOptions, strings) => whateverOptions.Target = string.Join(", ", strings));

            // act
            builder.Parse(options, "-t a b c d e f -s g h -o 1 2 3".Split(' '));

            // assert
            options.Things.Should().BeEquivalentTo("a b c".Split(' '));
            options.Source.Should().Be("g");
            options.OtherThings.Should().BeEquivalentTo(new[] {1, 2, 3});
            options.Target.Should().Be("d, e, f");
        }

        [Fact]
        public void Throw_If_Missing_Values()
        {
            // arrange
            var options = new WhateverOptions();
            var builder = new OptionsBuilder<WhateverOptions>()
                .WithMultipleSwitch('t', (whateverOptions, strings) => whateverOptions.Things = strings, 5)
                .WithSingleSwitch('s', (whateverOptions, s) => whateverOptions.Source = s)
                .WithPositional(10, (whateverOptions, strings) => whateverOptions.OtherGuys = strings);
            Action throws0 = () => builder.Parse(options, "-t a b c -s source a b c d e f g h i j k l m n".Split(' '));
            Action throws1 = () => builder.Parse(options, "-t a b c d e f f g h i j k l m n -s".Split(' '));
            Action throws2 = () => builder.Parse(options, "-t a b c d e f -s g h i j -t".Split(' '));
            Action throws3 = () => builder.Parse(null, new[] {""});
            Action throws4 = () => builder.Parse(new WhateverOptions(), null);
            Action throws5 = () => builder.Parse(options, "-t a b -s source".Split(' '));
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
            var builder = new OptionsBuilder<WhateverOptions>()
                .WithMultipleSwitch('o', (whateverOptions, strings) => whateverOptions.OtherGuys = strings, 4)
                .WithSingleSwitch('s', (whateverOptions, s) => whateverOptions.Source = s)
                .WithSingleSwitch('t', (whateverOptions, s) => whateverOptions.Target = s);
            Action throws0 = () => builder.Parse(options, "-s -t".Split(' '));
            Action throws1 = () => builder.Parse(options, "-o hi world".Split(' '));

            // act
            // assert
            throws0.Should().Throw<MissingValueException>();
            throws1.Should().Throw<MissingValueException>();
        }
    }
}