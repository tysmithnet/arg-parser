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