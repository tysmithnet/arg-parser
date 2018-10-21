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

        private class ChildOptions : BaseOptions
        {
            public string Thing { get; set; }
        }

        private class GrandChildOptions : ChildOptions
        {
            public string SpecialThing { get; set; }
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

        [Fact]
        public void Parse_A_Hierarchy()
        {
            // arrange
            var parentParser = new DefaultParser<BaseOptions>();
            parentParser.AddParameter(new Parameter<BaseOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw == "-h" || info.Current.Raw == "--help",
                Handle = (instance, info) =>
                {
                    instance.HelpRequested = true;
                    return info.Consume(1);
                }
            });

            var childParser = new DefaultParser<ChildOptions>();
            childParser.AddParameter(new Parameter<ChildOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw.StartsWith("thing="),
                Handle = (instance, info) =>
                {
                    instance.Thing = info.Current.Raw.Substring("thing=".Length);
                    return info.Consume(1);
                }
            });

            var grandChildParser = new DefaultParser<GrandChildOptions>();
            grandChildParser.AddParameter(new Parameter<GrandChildOptions>()
            {
                CanHandle = (instance, info) => info.Current.Raw == "--special" && info.Next != null,
                Handle = (instance, info) =>
                {
                    instance.SpecialThing = info.Next.Raw;
                    return info.Consume(2);
                }
            });
            grandChildParser.BaseParser = childParser;
            childParser.BaseParser = parentParser;

            var strat = new DefaultParseStrategy(new Func<object>[] { () => new BaseOptions(), () => new ChildOptions(), () => new GrandChildOptions(),  });

            // act
            var result = strat.Parse(new IParser[] { parentParser, childParser, grandChildParser }, "--help thing=duke --special corgi".Split(' '));

            // assert
            int baseParsed = 0;
            int childParsed = 0;
            int grandChildParsed = 0;
            result.When<BaseOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                baseParsed++;
            });
            result.When<ChildOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                options.Thing.Should().Be("duke");
                childParsed++;
            });
            result.When<GrandChildOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                options.Thing.Should().Be("duke");
                options.SpecialThing.Should().Be("corgi");
                grandChildParsed++;
            });
            baseParsed.Should().Be(1);
            childParsed.Should().Be(1);
            grandChildParsed.Should().Be(1);
        }
    }
}
