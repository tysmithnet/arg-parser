using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Parse_A_Single_Type()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddSwitch(new Switch<BaseOptions>()
            {
                CanHandle = (options, iterationInfo) => iterationInfo.Current.Raw == "-d",
                Handle = (options, iterationInfo) =>
                {
                    options.DryRun = true;
                    return iterationInfo.Consume(1);
                }
            });
            parser.AddSwitch(new Switch<BaseOptions>()
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
            var args = new [] {"-d", "-f", "file1", "file2"};
            var tokens = args.Select(s => new Token(s)).ToList();
            IIterationInfo info = CreateInfo(args: args, tokens: tokens);

            // act
            int i = 0;
            while (!info.IsComplete)
            {
                info = parser.Handle(instance, info);
                if (info.Index == i)
                    true.Should().BeFalse("No progress made");
                i = info.Index;
            }

            // assert
            instance.DryRun.Should().BeTrue();
            instance.Files.Should().BeEquivalentTo(new[] {"file1", "file2"});
        }

        private IterationInfo CreateInfo(string[] args = null, IReadOnlyList<IToken> tokens = null, int index = 0)
        {
            var info = new IterationInfo()
            {
                Args = args ?? new string[0],
                Tokens = tokens ?? new List<IToken>(),
                Index = index
            };
            return info;
        }
    }
}
