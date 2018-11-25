using ArgParser.Core;
using ArgParser.Testing.Common;
using Xunit;

namespace ArgParser.Styles.Test
{
    public class IterationInfoFactory_Should
    {
        [Fact]
        public void Create_The_Correct_Info_For_The_Request()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var fac = new IterationInfoFactory(builder.Context);
            var parsers = builder.Context.ParserRepository.Get("util").ToEnumerableOfOne();
            var res = new ChainIdentificationResult(parsers, new string[0]);
            var req = new IterationInfoRequest(res, "-h".Split(' '), "-hlep".Split(' '));

            // act
            var result = fac.Create(req);

            // assert
        }
    }
}