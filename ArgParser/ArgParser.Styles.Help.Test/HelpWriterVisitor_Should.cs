using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba.CsConsoleFormat;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Help.Test
{
    public class HelpWriterVisitor_Should
    {
        private class FakeNode : HelpNode
        {
            public override T Accept<T>(IHelpNodeVisitor<T> visitor)
            {
                return visitor.Visit(this);
            }
        }

        private class FakeNode1 : HelpNode
        {

        }

        [Fact]
        public virtual void Throw_If_Given_Bad_Values()
        {
            // arrange
            var visitor = new HelpWriterVisitor();
            Action mightThrow0 = () => visitor.Visit((RootNode) null);
            Action mightThrow1 = () => visitor.Visit((BlockNode)null);
            Action mightThrow2 = () => visitor.Visit((HeadingNode)null);
            Action mightThrow3 = () => visitor.Visit((TextNode)null);
            Action mightThrow4 = () => visitor.Visit((CodeNode)null);
            Action mightThrow5 = () => visitor.Visit((HelpNode)null);
            Action mightThrow6 = () => visitor.Visit((GridNode)null);

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
            mightThrow3.Should().Throw<ArgumentNullException>();
            mightThrow4.Should().Throw<ArgumentNullException>();
            mightThrow5.Should().Throw<ArgumentNullException>();
            mightThrow6.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Use_The_Most_Base_Overload_For_New_Types()
        {
            // arrange
            var node = new FakeNode()
            {
                Children =
                {
                    new TextNode("a")
                }
            };
            var node1 = new FakeNode1()
            {
                Children =
                {
                    new TextNode("b")
                }
            };
            var visitor = new HelpWriterVisitor();
            var fake = node.Accept(visitor);
            var fake1 = node1.Accept(visitor);

            // act
            // assert
            fake.Should().BeOfType<Span>().Which.Children.Should().HaveCount(1);
            fake1.Should().BeOfType<Span>().Which.Children.Should().HaveCount(1);
        }

        [Fact]
        public void Return_A_Document_For_RootNode()
        {
            // arrange
            var root = new RootNode();
            var visitor = new HelpWriterVisitor();

            // act
            // assert
            root.Accept(visitor).Should().BeOfType<Document>();
        }

        [Fact]
        public void Return_A_Span_When_Faced_With_A_CodeNode()
        {
            // arrange
            var code = new CodeNode("a");
            var visitor = new HelpWriterVisitor();

            // act
            // assert
            code.Accept(visitor).Should().BeOfType<Span>().Which.Text.Should().Be("a");
        }
    }
}
