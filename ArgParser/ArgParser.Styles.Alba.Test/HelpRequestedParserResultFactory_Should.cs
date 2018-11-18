using System.Collections.Generic;
using ArgParser.Core;
using ArgParser.Styles.ParseStrategy;
using ArgParser.Testing.Common;
using Moq;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class HelpRequestedParserResultFactory_Should
    {
        [Fact]
        public void Defer_To_The_Inner_Factory_For_Creation()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var mock = new Mock<IParseResultFactory>();
            mock.SetupAllProperties();
            mock.Setup(f => f.Create(It.IsAny<Dictionary<object, Parser>>(), It.IsAny<IEnumerable<ParseException>>()))
                .Returns(new ParseResult(null, null));
            var fac = new HelpRequestedParseResultFactory(mock.Object, (parsers, exceptions) => null, builder.Context);

            // act
            fac.Create(null, null);

            // assert
            mock.Verify(f => f.Create(It.IsAny<Dictionary<object, Parser>>(), It.IsAny<IEnumerable<ParseException>>()),
                Times.Once);
        }
    }
}