using ArgParser.Core;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Extensions.Test
{
    public class ParameterViewModel_Should
    {
        [Fact]
        public void Return_A_Check_Mark_If_Required()
        {
            // arrange
            var param = new BooleanSwitch(new Parser("a"), 'b', "bee", o => { })
            {
                IsRequired = true
            };
            var vm = new ParameterViewModel(param, Theme.Default);

            // act
            // assert
            vm.RequiredText.Should().Be("✓");
        }

        [Fact]
        public void Return_EmptyString_If_Not_Required()
        {
            // arrange
            var param = new BooleanSwitch(new Parser("a"), 'b', "bee", o => { })
            {
                IsRequired = false
            };
            var vm = new ParameterViewModel(param, Theme.Default);

            // act
            // assert
            vm.RequiredText.Should().Be("");
        }
    }
}