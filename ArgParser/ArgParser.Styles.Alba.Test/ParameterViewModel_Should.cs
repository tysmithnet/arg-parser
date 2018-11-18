using ArgParser.Core;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class ParameterViewModel_Should
    {
        [Fact]
        public void Return_A_Check_Mark_If_Required()
        {
            // arrange
            var param = new BooleanSwitch(new Parser("a"), 'b', "bee", o => { });
            param.IsRequired = true;
            var vm = new ParameterViewModel
            {
                Parameter = param,
                Theme = Theme.Default
            };

            // act
            // assert
            vm.RequiredText.Should().Be("✓");
        }

        [Fact]
        public void Return_EmptyString_If_Not_Required()
        {
            // arrange
            var param = new BooleanSwitch(new Parser("a"), 'b', "bee", o => { });
            param.IsRequired = false;
            var vm = new ParameterViewModel
            {
                Parameter = param,
                Theme = Theme.Default
            };

            // act
            // assert
            vm.RequiredText.Should().Be("");
        }
    }
}