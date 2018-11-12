using ArgParser.Core;
using ArgParser.Styles.Default;
using Moq;
using Xunit;

namespace ArgParser.Styles.Help.Alba.Test
{
    public class HelpExtensions_Should
    {
        [Fact]
        public void Render_Help()
        {
            // arrange
            var mockFac = new Mock<IHelpNodeFactory>();
            mockFac.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<IContext>())).Returns(new RootNode());
            var mockWriter = new Mock<IHelpWriter>();
            mockWriter.Setup(writer => writer.RenderHelp(It.IsAny<RootNode>(), It.IsAny<int>()));
            HelpExtensions.HelpNodeFactory = mockFac.Object;
            HelpExtensions.HelpWriter = mockWriter.Object;
            var context = new ContextBuilder()
                .AddParser("base")
                .Finish
                .BuildContext();

            // act
            context.RenderHelp("base");

            // assert
            mockFac.Verify(factory => factory.Create("base", context), Times.Once);
            mockWriter.Verify(writer => writer.RenderHelp(It.IsAny<RootNode>(), 80), Times.Once);
        }
    }
}