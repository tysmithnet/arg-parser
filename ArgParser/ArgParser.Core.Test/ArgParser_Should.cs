using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArgParser.Core.Test
{
    public class ArgParser_Should
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
        public void Not_Crash_When_Given_Nothing()
        {
            // arrange
            var parser = new Parser<BaseOptions>();

            // act
            

            // assert
        }
    }
}
