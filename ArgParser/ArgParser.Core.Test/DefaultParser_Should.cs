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

        private IterationInfo CreateInfo()
        {
            var info = new IterationInfo()
            {
                Args = new string[0],
                Tokens = new List<IToken>(),
                Index = 0
            };
            return info;
        }
    }
}
