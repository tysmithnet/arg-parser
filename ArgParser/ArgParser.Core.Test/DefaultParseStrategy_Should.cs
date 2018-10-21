using System;
using System.Collections;
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
            public string LogFile { get; set; }
        }

        private class DeleteOptions : BaseOptions
        {
            public string[] Files { get; set; }
        }
        
        [Fact]
        public void Parse_A_Single_Type()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddParameter(new Parameter<BaseOptions>()
            {
                CanHandle = (instance, info) => info.Current?.Raw == "-l",
                Handle = (instance, info) =>
                {
                    instance.LogFile = info.Next?.Raw;
                    return info.Consume(info.Next == null ? 1 : 2);
                }
            });
            var strategy = new DefaultParseStrategy<BaseOptions>(() => new BaseOptions());

            // act
            var result = strategy.Parse<BaseOptions>(new[] {parser}, new[] {"-l", "log.txt"});

            // assert
            bool isParsed = false;
            result.When<BaseOptions>(options =>
            {
                isParsed = true;
                options.LogFile.Should().Be("log.txt");
            });
            isParsed.Should().BeTrue();
        }

        [Fact]
        public void Parse_Multiple_Types()
        {
            // arrange
            var parent = new DefaultParser<BaseOptions>();
            var child = new DefaultParser<DeleteOptions, BaseOptions>();
            child.BaseParser = parent;
            parent.AddParameter(new Parameter<BaseOptions>()
            {
                CanHandle = (instance, info) => info.Current?.Raw == "-l",
                Handle = (instance, info) =>
                {
                    instance.LogFile = info.Next?.Raw;
                    return info.Consume(info.Next == null ? 1 : 2);
                }
            });
            child.AddParameter(new Parameter<DeleteOptions>()
            {
                CanHandle = (instance, info) => info.Current?.Raw == "-f",
                Handle = (instance, info) =>
                {
                    instance.Files = info.Rest.Select(x => x.Raw).ToArray();
                    return info.Consume(1 + info.Rest.Count());
                }
            });
            var strategy = new DefaultParseStrategy<BaseOptions>(() => new BaseOptions());

            IList<IParser<DeleteOptions>> parsers = new List<IParser<DeleteOptions>>();
            parsers.Add(child);
            // act
            var result = strategy.Parse<BaseOptions>(parsers, new[] { "-l", "log.txt", "-f", "a.txt", "b.txt", "c.txt", "d.txt" });

            // assert
            bool isParsed = false;
            result.When<DeleteOptions>(options =>
            {
                isParsed = true;
                options.LogFile.Should().Be("log.txt");
                options.Files.Should().BeEquivalentTo(new[] {"a.txt", "b.txt", "c.txt", "d.txt"});
            });
            isParsed.Should().BeTrue();
        }
    }
}
