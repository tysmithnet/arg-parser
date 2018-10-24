using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArgParser.Core.Help;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultParser_Should
    {
        public class BaseOptions
        {
            public bool DryRun { get; set; }
            public List<string> Files { get; set; } = new List<string>();
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
                Args = args.PreventNull().ToArray(),
                Tokens = tokens.PreventNull().ToList(),
                Index = index
            };
            return info;
        }

        private class SpecialCompressOptions : CompressOptions
        {
            public bool IsSpecial { get; set; }
        }

        [Fact]
        public void Add_Help()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            var help = new GenericHelp
            {
                Name = "hi"
            };

            // act
            parser.AddHelp(help);

            // assert
            parser.Help.Name.Should().Be("hi");
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
            var childParser = new DefaultParser<CompressOptions>();
            var grandChildParser = new DefaultParser<SpecialCompressOptions>();
            childParser.BaseParser = baseParser;
            grandChildParser.BaseParser = childParser;
            var args = new[] {"-d", "-t", "zip", "-s"};
            var tokens = args.Select(x => new DefaultToken(x)).ToArray();
            IIterationInfo curInfo = CreateInfo(args, tokens);
            baseParser.AddParameter(new DefaultParameter<BaseOptions>
            (
                (instance, info) => info.Current.Raw == "-d",
                (instance, info) =>
                {
                    instance.DryRun = true;
                    return info.Consume(1);
                }
            ), new GenericHelp
            {
                Name = "Dry Run",
                ShortDescription = "Don't actually do anything, just output what would be done",
                Examples = new List<IExample>
                {
                    new DefaultExample
                    {
                        Name = "Basic",
                        Usage = new[] {"-d", "--dry-run"},
                        ShortDescription = "Just turning on dry run"
                    }
                }
            });

            childParser.AddParameter(new DefaultParameter<CompressOptions>((instance, info) => info.Current.Raw == "-t",
                (instance, info) =>
                {
                    instance.CompressionType = info.Next?.Raw;
                    return info.Next != null ? info.Consume(2) : info.Consume(1);
                }));

            grandChildParser.AddParameter(new DefaultParameter<SpecialCompressOptions>((instance, info) => info.Current.Raw == "-s",
                (instance, info) =>
                {
                    instance.IsSpecial = true;
                    return info.Consume(1);
                }));

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
        public void Parse_A_Single_Type()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddParameter(new DefaultParameter<BaseOptions>
            (
                (options, iterationInfo) => iterationInfo.Current.Raw == "-d",
                (options, iterationInfo) =>
                {
                    options.DryRun = true;
                    return iterationInfo.Consume(1);
                }
            ));
            parser.AddParameter(new DefaultParameter<BaseOptions>
            (
                (options, iterationInfo) => iterationInfo.Current.Raw == "-f",
                (options, iterationInfo) =>
                {
                    var files = iterationInfo.Rest.Select(x => x.Raw).ToArray();
                    options.Files = files.ToList();
                    return iterationInfo.Consume(1 + files.Length);
                }
            ));
            var instance = new BaseOptions();
            var args = new[] {"-d", "-f", "file1", "file2"};
            var tokens = args.Select(s => new DefaultToken(s)).ToList();
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

        [Fact]
        public void Parse_When_Only_Positionals()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            var infoFac = new DefaultIterationInfoFactory();
            parser.AddParameter(new DefaultParameter<BaseOptions>
            (
                (instance, info) => !Regex.IsMatch(info.Current.Raw, "--?[a-zA-Z0-9]+"),
                (instance, info) =>
                {
                    instance.Files.Add(info.Current.Raw);
                    return info.Consume(1);
                }
            ));

            // act
            var options = new BaseOptions();
            var nfo = infoFac.Create("pos1 pos2 pos3".Split(' '));
            while (!nfo.IsComplete) nfo = parser.Consume(options, nfo);

            // assert
            options.Files.Should().BeEquivalentTo("pos1", "pos2", "pos3");
        }
    }
}