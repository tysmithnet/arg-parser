using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Core.Help;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultHelpBuilder_Should
    {
        private class SomethingOptions
        {
            public string Something { get; set; }
            public bool IsSomething { get; set; }
        }

        private class SomethingElseOptions : SomethingOptions
        {
            public string SomethingElse { get; set; }
        }

        [Fact]
        public void Pass_Basic_Use_Cases()
        {
            // arrange
            var subMock = new Mock<ISubCommand>();
            subMock.Setup(command => command.HelpHints).Returns(new HelpHints()
            {
                Synopsis = "se",
                ShortDescription = "Set something else instead"
            });
            var builder = new DefaultHelpBuilder<SomethingOptions>()
                .AddSwitch(new Switch<SomethingOptions>()
                {
                    HelpHints = new HelpHints()
                    {
                        Synopsis = "-s, --something something",
                        ShortDescription = "Set something to some value"
                    }
                }).AddSwitch(new Switch<SomethingOptions>()
                {
                    HelpHints = new HelpHints()
                    {
                        Synopsis = "-s, --something something",
                        ShortDescription = "Set something to some value"
                    }
                })
                .AddSubCommand<SomethingElseOptions>(subMock.Object)
                .AddIdentityInfomation(new IdentityInformation("something")
                {
                    Version = "v1.2.3.4",
                    Name = "something",
                    ShortDescription = "Everybody's looking for somethin"
                })
                .AddPositional(new Positional<SomethingOptions>()
                {
                    HelpHints = new HelpHints()
                    {
                        Synopsis = "thing1 thing2 ...",
                        ShortDescription = "The things"
                    }
                });

            // act
            // assert
            if (builder.Build() is TextSnippetNode snippet)
            {
                snippet.Text.Trim().Should().Be(@"something - v1.2.3.4
Everybody's looking for somethin
Sub Commands:
	se - Set something else instead
Switches:
	-s, --something something
	Set something to some value

	-s, --something something
	Set something to some value

Positionals:
	thing1 thing2 ...
	The things
".Trim());
            }
            else
            {
                true.Should().BeFalse("The builder did not return a TextSnippetNode");
            }
        }
    }
}
