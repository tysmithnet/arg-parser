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
        #region Options
        private class SomethingOptions
        {
            public string Something { get; set; }
            public bool IsSomething { get; set; }
        }

        private class SomethingElseOptions : SomethingOptions
        {
            public string SomethingElse { get; set; }
        }

        #endregion

        #region Builders
        private class TableHelpBuilder : IHelpBuilder<SomethingOptions>, IHelpful
        {


            /// <inheritdoc />
            public IHelpBuilder<SomethingOptions> AddSwitch(Switch<SomethingOptions> @switch)
            {
                return this;
            }

            /// <inheritdoc />
            public IHelpBuilder<SomethingOptions> AddSubCommand<TSub>(ISubCommand subCommand) where TSub : SomethingOptions
            {
                return this;
            }

            /// <inheritdoc />
            public IHelpBuilder<SomethingOptions> AddPositional(Positional<SomethingOptions> positional)
            {
                return this;
            }

            /// <inheritdoc />
            public IHelpBuilder<SomethingOptions> AddHelp(IHelp help)
            {
                Help = help;
                return this;
            }

            /// <inheritdoc />
            public Node Build()
            {
                var table = new TableNode();
                table.Children = new List<Node>()
                {
                    new TableRowNode()
                    {
                        Children = new List<Node>()
                        {
                            new TableCellNode()
                            {
                                Children = new List<Node>()
                                {
                                    new TextSnippetNode("Name")
                                }
                            },
                            new TableCellNode()
                            {
                                Children = new List<Node>()
                                {
                                    new TextSnippetNode(Help.Name)
                                }
                            }
                        }
                    },
                    new TableRowNode()
                    {
                        Children = new List<Node>()
                        {
                            new TableCellNode()
                            {
                                Children = new List<Node>()
                                {
                                    new TextSnippetNode("Version")
                                }
                            },
                            new TableCellNode()
                            {
                                Children = new List<Node>()
                                {
                                    new TextSnippetNode(Help.Version)
                                }
                            }
                        }
                    }
                };
                return table;
            }

            /// <inheritdoc />
            public IHelp Help { get; private set; } = new Help.HelpInfo()
            {
                Name = "clip",
                ShortDescription = "Interact with the items in the clipboard",
                Description = "",
                Synopsis = "clip sort",
                Url = "http://www.example.org",
                Version = "v1.2.3.4"
            }; 
        }
        
        #endregion

        [Fact]
        public void Allow_Tables()
        {
            // arrange
            var builder = new TableHelpBuilder();

            // act
            // assert
            if (builder.Build() is TableNode table)
            {
                table.Children.Should().HaveCount(2);
            }
            else
            {
                true.Should().BeFalse("We were expecting a TableNode but did not get one");
            }
        }

        [Fact]
        public void Pass_Basic_Use_Cases()
        {
            // arrange
            var subMock = new Mock<ISubCommand>();
            subMock.Setup(command => command.Help).Returns(new Help.HelpInfo()
            {
                Synopsis = "se",
                ShortDescription = "Set something else instead"
            });
            var builder = new DefaultHelpBuilder<SomethingOptions>()
                .AddHelp(new HelpInfo()
                {
                    Name = "doit",
                    ShortDescription = "do what?",
                    Description = "do something",
                    Version = "1.2.3.4",
                    Synopsis = "doit [se] -blah",
                    Url = "www.example.org"
                })
                .AddSwitch(new Switch<SomethingOptions>()
                {
                    Help = new Help.HelpInfo()
                    {
                        Synopsis = "-s, --something something",
                        ShortDescription = "Set something to some value"
                    }
                }).AddSwitch(new Switch<SomethingOptions>()
                {
                    Help = new Help.HelpInfo()
                    {
                        Synopsis = "-s, --something something",
                        ShortDescription = "Set something to some value"
                    }
                })
                .AddSubCommand<SomethingElseOptions>(subMock.Object)
                .AddPositional(new Positional<SomethingOptions>()
                {
                    Help = new Help.HelpInfo()
                    {
                        Synopsis = "thing1 thing2 ...",
                        ShortDescription = "The things"
                    }
                });

            // act
            // assert
            if (builder.Build() is TextSnippetNode snippet)
            {
                snippet.Text.Trim().Should().Be(@"doit - 1.2.3.4
do what?

Synopsis:
    doit [se] -blah [se]

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
