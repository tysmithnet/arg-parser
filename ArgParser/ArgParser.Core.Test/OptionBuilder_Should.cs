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
            options.Source.Should().Be("source");
            options.OtherGuys.Should().BeEquivalentTo("otherguy1", "otherguy2");
            options.Things.Should().BeEquivalentTo("thing1", "thing2");
            options.OtherThings.Should().BeEquivalentTo(new[] {1, 2, 3});

            options2.Verbose.Should().BeTrue();
            options2.Source.Should().Be("source");
            options2.OtherGuys.Should().BeEquivalentTo("otherguy1", "otherguy2");
            options2.Things.Should().BeEquivalentTo("thing1", "thing2");
            options2.OtherThings.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public void Return_Only_As_Many_As_Asked_For()
        {
            // arrange
            var options = new WhateverOptions();

            var builder = new OptionsBuilder<WhateverOptions>()
                .WithSingleSwitch('s', (whateverOptions, s) => whateverOptions.Source = s)
                .WithMultipleSwitch('t', (whateverOptions, strings) => whateverOptions.Things = strings, count: 3)
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
    }
}