using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class HelpNodeFactory_Should
    {
        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            var fac = new HelpNodeFactory();
            Action mightThrow0 = () => fac.Create(null, null);
            Action mightThrow1 = () => fac.Create("base", null);
            Action mightThrow2 = () => fac.Create(null, new Context());

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
        }
    }
}
