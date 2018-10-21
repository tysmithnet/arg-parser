using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultParser_Should
    {
        public class BaseOptions
        {
            public bool DryRun { get; set; }
            public string[] Files { get; set; }
        }

        public class CompressOptions : BaseOptions
        {
            public string CompressionType { get; set; }
        }

        private DefaultIterationInfo CreateInfo(string[] args = null, IReadOnlyList<IToken> tokens = null,
            int index = 0)
        {
            var info = new DefaultIterationInfo
            {
                Args = args ?? new string[0],
                Tokens = tokens ?? new List<IToken>(),
                Index = index
            };
            return info;
        }

        private class SpecialCompressOptions : CompressOptions
        {
            public bool IsSpecial { get; set; }
        }

        [Fact]
        public void Not_Throw_When_Given_Nothing()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            var instance = new BaseOptions();
            var info = CreateInfo();

            // act
            Action mightThrow = () => parser.CanConsume(instance, info);

            // assert
            mightThrow.Should().NotThrow();
        }

        [Fact]
        public void Parse_A_Hierarchy()
        {
            // arrange
            var baseParser = new DefaultParser<BaseOptions>();
            var childParser = new DefaultParser<CompressOptions, BaseOptions>();
            var grandChildParser = new DefaultParser<SpecialCompressOptions, CompressOptions>();
            childParser.BaseParser = baseParser;
            grandChildParser.BaseParser = childParser;
            var args = new[] {"-d", "-t", "zip", "-s"};
            var tokens = args.Select(x => new Token(x)).ToArray();
            IIterationInfo curInfo = CreateInfo(args, tokens);
            baseParser.AddParameter(new Parameter<BaseOptions>
            {
                CanHandle = (instance, info) => info.Current.Raw == "-d",
                Handle = (instance, info) =>
                {
                    instance.DryRun = true;
                    return info.Consume(1);
                }
            });

            childParser.AddParameter(new Parameter<CompressOptions>
            {
                CanHandle = (instance, info) => info.Current.Raw == "-t",
                Handle = (instance, info) =>
                {
                    instance.CompressionType = info.Next?.Raw;
                    return info.Next != null ? info.Consume(2) : info.Consume(1);
                }
            });

            grandChildParser.AddParameter(new Parameter<SpecialCompressOptions>
            {
                CanHandle = (instance, info) => info.Current.Raw == "-s",
                Handle = (instance, info) =>
                {
                    instance.IsSpecial = true;
                    return info.Consume(1);
                }
            });

            // act
            // assert
            var options = new SpecialCompressOptions();
            var i = 0;
            while (!curInfo.IsComplete && grandChildParser.CanConsume(options, curInfo))
            {
                curInfo = grandChildParser.Consume(options, curInfo);
                if (curInfo.Index == i)
                    true.Should().BeFalse("No progress made");
                i = curInfo.Index;
            }

            options.DryRun.Should().BeTrue();
            options.CompressionType.Should().Be("zip");
            options.IsSpecial.Should().BeTrue();
        }

        [Fact]
        public void Parse_A_Single_Positional()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            
            // act
            

            // assert
        }

        [Fact]
        public void Parse_A_Single_Type()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddParameter(new Parameter<BaseOptions>
            {
                CanHandle = (options, iterationInfo) => iterationInfo.Current.Raw == "-d",
                Handle = (options, iterationInfo) =>
                {
                    options.DryRun = true;
                    return iterationInfo.Consume(1);
                }
            });
            parser.AddParameter(new Parameter<BaseOptions>
            {
                CanHandle = (options, iterationInfo) => iterationInfo.Current.Raw == "-f",
                Handle = (options, iterationInfo) =>
                {
                    var files = iterationInfo.Rest.Select(x => x.Raw).ToArray();
                    options.Files = files;
                    return iterationInfo.Consume(1 + files.Length);
                }
            });
            var instance = new BaseOptions();
            var args = new[] {"-d", "-f", "file1", "file2"};
            var tokens = args.Select(s => new Token(s)).ToList();
            IIterationInfo info = CreateInfo(args, tokens);

            // act
            var i = 0;
            while (!info.IsComplete)
            {
                info = parser.Consume(instance, info);
                if (info.Index == i)
                    true.Should().BeFalse("No progress made");
                i = info.Index;
            }

            // assert
            instance.DryRun.Should().BeTrue();
            instance.Files.Should().BeEquivalentTo("file1", "file2");
        }
    }
}