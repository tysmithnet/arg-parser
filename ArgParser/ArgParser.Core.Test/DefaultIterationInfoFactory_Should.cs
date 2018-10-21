using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultIterationInfoFactory_Should
    {
        [Fact]
        public void Create_Default_Info()
        {
            // arrange
            var fac = new DefaultIterationInfoFactory();

            // act
            var info = fac.Create(new[] {"a", "b"});

            // assert
            info.Tokens.Should().HaveCount(2);
            info.Current.Raw.Should().Be("a");
            info.Index.Should().Be(0);
            info.IsComplete.Should().BeFalse();
            info.Next.Raw.Should().Be("b");
            info.Rest.Should().HaveCount(1);
        }
    }
}
