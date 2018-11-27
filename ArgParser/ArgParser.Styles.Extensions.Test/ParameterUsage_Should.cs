using Alba.CsConsoleFormat;
using ArgParser.Testing.Common;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArgParser.Styles.Extensions.Test
{
    public class ParameterUsage_Should
    {
        [Fact]
        public void Generate_The_Correct_Output_For_A_Single_Value_Switch()
        {
            // arrange
            var mock = new Mock<IInlineSequence>();
            mock.SetupAllProperties();
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var parameter = new SingleValueSwitch(builder.Context.ParserRepository.Get("util"), 'v', "value",
                (o, s) => { });
            var vm = new ParameterViewModel(parameter, Theme.Default);
            var usage = new ParameterUsage
            {
                ViewModel = vm
            };

            // act
            usage.GenerateSequence(mock.Object);

            // assert
            usage.StringBuilder.ToString().Should().Be("[-v, --value v]");
        }

        [Fact]
        public void Generate_The_Correct_Output_For_A_Single_Value_Switch_With_No_Letter()
        {
            // arrange
            var mock = new Mock<IInlineSequence>();
            mock.SetupAllProperties();
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var parameter = new SingleValueSwitch(builder.Context.ParserRepository.Get("util"), null, "value",
                (o, s) => { });
            parameter.Help.ValueAlias = "val";
            var vm = new ParameterViewModel(parameter, Theme.Default);
            var usage = new ParameterUsage
            {
                ViewModel = vm
            };

            // act
            usage.GenerateSequence(mock.Object);

            // assert
            usage.StringBuilder.ToString().Should().Be("[--value val]");
        }

        [Fact]
        public void Generate_The_Correct_Output_For_A_Single_Value_Switch_With_No_Word()
        {
            // arrange
            var mock = new Mock<IInlineSequence>();
            mock.SetupAllProperties();
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var parameter = new SingleValueSwitch(builder.Context.ParserRepository.Get("util"), 'v', null,
                (o, s) => { });
            parameter.Help.ValueAlias = "val";
            var vm = new ParameterViewModel(parameter, Theme.Default);
            var usage = new ParameterUsage
            {
                ViewModel = vm
            };

            // act
            usage.GenerateSequence(mock.Object);

            // assert
            usage.StringBuilder.ToString().Should().Be("[-v val]");
        }

        [Fact]
        public void Generate_The_Correct_Output_For_Positionals()
        {
            // arrange
            var mock = new Mock<IInlineSequence>();
            mock.SetupAllProperties();
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var parameter = new Positional(builder.Context.ParserRepository.Get("util"), (o, strings) => { });
            parameter.Help.ValueAlias = "val";
            var vm = new ParameterViewModel(parameter, Theme.Default);
            var usage = new ParameterUsage
            {
                ViewModel = vm
            };

            // act
            usage.GenerateSequence(mock.Object);

            // assert
            usage.StringBuilder.ToString().Should().Be("[val1..valN]");
        }

        [Fact]
        public void Generate_The_Correct_Prefix_For_BooleanSwitches()
        {
            // arrange
            var mock = new Mock<IInlineSequence>();
            mock.SetupAllProperties();
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var parameter = new BooleanSwitch(builder.Context.ParserRepository.Get("util"), 'v', "value", o => { });
            parameter.Help.ValueAlias = "val";
            var vm = new ParameterViewModel(parameter, Theme.Default);
            var usage = new ParameterUsage
            {
                ViewModel = vm
            };

            // act
            // assert
            usage.GenerateValueAlias(parameter).Should().Be("");
        }

        [Fact]
        public void Generate_The_Correct_Prefix_For_Positionals()
        {
            // arrange
            var mock = new Mock<IInlineSequence>();
            mock.SetupAllProperties();
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var parameter = new Positional(builder.Context.ParserRepository.Get("util"), (o, strings) => { }, 1, 1);
            parameter.Help.ValueAlias = "val";
            var vm = new ParameterViewModel(parameter, Theme.Default);
            var usage = new ParameterUsage
            {
                ViewModel = vm
            };

            // act
            usage.GenerateSequence(mock.Object);

            // assert
            usage.StringBuilder.ToString().Should().Be("[val]");
        }

        [Fact]
        public void Generate_The_Correct_Prefix_For_Switches()
        {
            // arrange
            var mock = new Mock<IInlineSequence>();
            mock.SetupAllProperties();
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var parameter = new ValuesSwitch(builder.Context.ParserRepository.Get("util"), 'v', "value",
                (o, strings) => { }, 1, 3);
            parameter.Help.ValueAlias = "val";
            var vm = new ParameterViewModel(parameter, Theme.Default);
            var usage = new ParameterUsage
            {
                ViewModel = vm
            };

            // act
            usage.GenerateSequence(mock.Object);

            // assert
            usage.GeneratedText.Should().Be("[-v, --value val1..val3]");
        }
    }
}