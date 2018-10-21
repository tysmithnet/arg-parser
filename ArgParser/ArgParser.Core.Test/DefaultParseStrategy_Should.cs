using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultParseStrategy_Should
    {
        private class BaseOptions
        {
            public bool HelpRequested { get; set; }
        }

        [Fact]
        public void Parse_A_Single_Type()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddParameter(new Parameter<BaseOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw == "-h" || info.Current.Raw == "--help",
                Handle = (instance, info) =>
                {
                    instance.HelpRequested = true;
                    return info.Consume(1);
                }
            });
            var strat = new DefaultParseStrategy(new Func<object>[]{ () => new BaseOptions() });

            // act
            var result = strat.Parse(new[] {parser}, "--help".Split(' '));

            // assert
            bool isParsed = false;
            result.When<BaseOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                isParsed = true;
            });
            isParsed.Should().BeTrue();
        }
    }
}
