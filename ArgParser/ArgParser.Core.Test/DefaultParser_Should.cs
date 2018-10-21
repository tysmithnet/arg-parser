﻿using System;
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

        [Fact]
        public void Not_Throw_When_Given_Nothing()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            var instance = new BaseOptions();
            var info = CreateInfo();

            // act
            Action mightThrow = () => parser.CanHandle(instance, info);

            // assert
            mightThrow.Should().NotThrow();
        }

        [Fact]
        public void Parse_A_Hierarchy()
        {
            // arrange
            var baseParser = new DefaultParser<BaseOptions>();
            var childParser = new DefaultParser<CompressOptions, BaseOptions>();
            childParser.BaseParser = baseParser;
            var args = new[] {"-d", "-t", "zip"};
            var tokens = args.Select(x => new Token(x)).ToArray();
            IIterationInfo curInfo = CreateInfo(args, tokens);
            baseParser.AddSwitch(new Switch<BaseOptions>
            {
                CanHandle = (instance, info) => info.Current.Raw == "-d",
                Handle = (instance, info) =>
                {
                    instance.DryRun = true;
                    return info.Consume(1);
                }
            });

            childParser.AddSwitch(new Switch<CompressOptions>
            {
                CanHandle = (instance, info) => info.Current.Raw == "-t",
                Handle = (instance, info) =>
                {
                    instance.CompressionType = info.Next?.Raw;
                    return info.Next != null ? info.Consume(2) : info.Consume(1);
                }
            });

            // act
            // assert
            var options = new CompressOptions();
            var i = 0;
            while (!curInfo.IsComplete && childParser.CanHandle(options, curInfo))
            {
                curInfo = childParser.Handle(options, curInfo);
                if (curInfo.Index == i)
                    true.Should().BeFalse("No progress made");
                i = curInfo.Index;
            }

            options.DryRun.Should().BeTrue();
            options.CompressionType.Should().Be("zip");
        }

        [Fact]
        public void Parse_A_Single_Type()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddSwitch(new Switch<BaseOptions>
            {
                CanHandle = (options, iterationInfo) => iterationInfo.Current.Raw == "-d",
                Handle = (options, iterationInfo) =>
                {
                    options.DryRun = true;
                    return iterationInfo.Consume(1);
                }
            });
            parser.AddSwitch(new Switch<BaseOptions>
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
                info = parser.Handle(instance, info);
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