using System;
using ArgParser.Core;
using ArgParser.Core.Help;
using ArgParser.Styles.Default;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class HelpNodeFactory_Should
    {
        [Fact]
        public void Create_A_Description()
        {
            // arrange
            var mock = new Mock<IUsageFactory>();
            mock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<IContext>()))
                .Returns(new CodeNode("mock value"));
            var fac = new HelpNodeFactory();
            fac.UsageFactory = mock.Object;
            var context = new ContextBuilder()
                .AddParser("test", help => { help.SetName("test").SetLongDescription("long desc"); })
                .Finish
                .BuildContext();

            // act
            var block = fac.CreateDescription("test", context);

            // assert
            block.Children[0].Should().BeOfType<BlockNode>().Which.Children.Should().HaveCount(2);
            block.Children[0].Children[0].Should().BeOfType<TextNode>().Which.Text.Should().Be("Usage: ");
            block.Children[0].Children[1].Should().BeOfType<CodeNode>().Which.Text.Should().Be("mock value");
            block.Children[1].Should().BeOfType<TextNode>().Which.Text.Should().Be("long desc");
        }

        [Fact]
        public void Create_A_Heading()
        {
            // arrange
            var fac = new HelpNodeFactory();

            // act
            var heading = fac.CreateHeading("test", new Context());

            // assert
            heading.Should().BeOfType<HeadingNode>().Which.Text.Should().Be("test");
        }

        [Fact]
        public void Create_Help_For_A_Parser()
        {
            // arrange
            var mock = new Mock<IUsageFactory>();
            mock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<IContext>()))
                .Returns(new CodeNode("mock value"));
            var fac = new HelpNodeFactory();
            fac.UsageFactory = mock.Object;
            var context = new ContextBuilder()
                .AddParser("test", help => { help.SetName("test").SetLongDescription("long desc"); })
                .Finish
                .BuildContext();

            // act
            var res = fac.Create("test", context);

            // assert
            res.Children.Should().HaveCount(7);
            res.Children[0].Should().BeOfType<HeadingNode>();
            res.Children[1].Should().BeOfType<HorizontalLineNode>();
            res.Children[2].Should().BeOfType<BlockNode>();
            res.Children[3].Should().BeOfType<HorizontalLineNode>();
            res.Children[4].Should().BeOfType<GridNode>();
            res.Children[5].Should().BeOfType<HorizontalLineNode>();
            res.Children[6].Should().BeOfType<GridNode>();
        }

        [Fact]
        public void Create_Parameters()
        {
            // arrange
            var context = new ContextBuilder()
                .AddParser("base")
                .WithBooleanSwitch('h', "help", o => { },
                    help => { help.SetName("help").SetShortDescription("Get help"); })
                .WithSingleValueSwitch('v', "value", (o, s) => { },
                    help => { help.SetName("value").SetShortDescription("Set a value"); }).Finish.BuildContext();
            var fac = new HelpNodeFactory();

            // act
            var res = fac.CreateParameters("base", context);

            // assert
            res.Children.Should().HaveCount(4);
            var grid = (GridNode) res;
            grid.Columns.Should().Be(2);
            grid.Children[0].Should().BeOfType<TextNode>().Which.Text.Should().Be("-h, --help");
            grid.Children[1].Should().BeOfType<TextNode>().Which.Text.Should().Be("Get help");
            grid.Children[2].Should().BeOfType<TextNode>().Which.Text.Should().Be("-v, --value");
            grid.Children[3].Should().BeOfType<TextNode>().Which.Text.Should().Be("Set a value");
        }

        [Fact]
        public void Create_Sub_Commands()
        {
            // arrange
            var fac = new HelpNodeFactory();
            var context = new ContextBuilder()
                .AddParser("base")
                .Finish
                .AddParser("c0", help => { help.SetName("child0").SetShortDescription("child0 desc"); })
                .Finish
                .AddParser("c1", help => { help.SetName("child1").SetShortDescription("child1 desc"); })
                .Finish
                .CreateParentChildRelationship("base", "c0")
                .CreateParentChildRelationship("base", "c1")
                .BuildContext();

            // act
            var res = fac.CreateSubCommands("base", context);

            // assert
            res.Children.Should().HaveCount(4);
            var grid = (GridNode) res;
            grid.Columns.Should().Be(2);
            grid.Children[0].Should().BeOfType<TextNode>().Which.Text.Should().Be("c0");
            grid.Children[1].Should().BeOfType<TextNode>().Which.Text.Should().Be("child0 desc");
            grid.Children[2].Should().BeOfType<TextNode>().Which.Text.Should().Be("c1");
            grid.Children[3].Should().BeOfType<TextNode>().Which.Text.Should().Be("child1 desc");
        }

        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            var fac = new HelpNodeFactory();
            Action mightThrow0 = () => fac.Create(null, null);
            Action mightThrow1 = () => fac.Create("base", null);
            Action mightThrow2 = () => fac.Create(null, new Context());

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
        }
    }
}