using System.Collections.Generic;
using ArgParser.Core.Help;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultHelpNodeVisitor_Should
    {
        [Fact]
        public void Perform_The_Correct_Action_For_Each_Node_Type()
        {
            // arrange
            var code = false;
            var heading = false;
            var row = false;
            var table = false;
            var td = false;
            var text = false;
            var root = false;

            var help = new RootNode
            {
                Children = new IHelpNode[]
                {
                    new TextNode("hi"),
                    new CodeNode("blah"),
                    new HeadingNode("big"),
                    new TableNode
                    {
                        Rows = new[]
                        {
                            new TableRowNode
                            {
                                TableDataNodes = new List<TableDataNode>
                                {
                                    new TableDataNode("nested")
                                }
                            }
                        }
                    }
                }
            };

            var visitor = new DefaultHelpNodeVisitor
            {
                RootNode = node => root = true,
                CodeNode = node => code = true,
                HeadingNode = node => heading = true,
                TableDataNode = node => td = true,
                TableNode = node => table = true,
                TableRowNode = node => row = true,
                TextNode = node => text = true
            };

            // act
            help.Accept(visitor);

            // assert
            code.Should().BeTrue();
            heading.Should().BeTrue();
            row.Should().BeTrue();
            table.Should().BeTrue();
            td.Should().BeTrue();
            text.Should().BeTrue();
            root.Should().BeTrue();
        }
    }
}