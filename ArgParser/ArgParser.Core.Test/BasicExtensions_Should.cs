using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class BasicExtensions_Should
    {
        [Fact]
        public void Identify_Null_Or_WhiteSpace_Strings()
        {
            // arrange
            string sNull = null;

            // act
            // assert
            sNull.IsNullOrWhiteSpace().Should().BeTrue();
        }
    }
}
