using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            bool code = false;
            bool heading = false;
            bool row = false;
            bool table = false;
            bool td = false;
            bool text = false;
            bool root = false;

            var help = new RootNode()
            {
                Children = new IHelpNode[]
                {
                    new TextNode("hi"), 
                    new CodeNode("blah"), 
                    new HeadingNode("big"), 
                    new TableNode()
                    {
                        Rows = new TableRowNode[]
                        {
                            new TableRowNode()
                            {
                                TableDataNodes = new List<TableDataNode>()
                                {
                                    new TableDataNode("nested")
                                }
                            }, 
                        }
                    },
                }
            };


            var visitor = new DefaultHelpNodeVisitor()
            {
                RootNode = node => root = true,
                CodeNode = node => code = true,
                HeadingNode = node => heading = true,
                TableDataNode = node => td = true,
                TableNode = node => table = true,
                TableRowNode = node => row = true,
                TextNode = node => text = true,
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
