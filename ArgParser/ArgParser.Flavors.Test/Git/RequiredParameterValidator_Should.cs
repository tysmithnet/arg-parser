using System.Linq;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class RequiredParameterValidator_Should
    {
        public class BasicOptions
        {
            public string File { get; set; }
        }

        [Fact]
        public void Always_Say_It_Can_Validate()
        {
            // arrange
            var validator = new RequiredParameterValidator(new BooleanSwitch('h', "help", o => { }));

            // act
            // assert
            validator.CanValidate(null).Should().BeTrue();
            validator.CanValidate(new object()).Should().BeTrue();
            validator.CanValidate("").Should().BeTrue();
        }

        [Fact]
        public void Return_Error_If_Parameter_Was_Not_Consumed()
        {
            // arrange
            var gitParameter = new Positional
            {
                ConsumeCallback = (o, strings) =>
                {
                    if (o is BasicOptions b)
                        b.File = strings.First();
                },
                HasBeenConsumed = false
            };
            var validator = new RequiredParameterValidator(gitParameter);
            validator.Parameter = gitParameter;

            // act
            var result = validator.Validate(new BasicOptions());

            // assert
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Return_Success_If_Parameter_Was_Consumed()
        {
            // arrange
            var gitParameter = new Positional
            {
                ConsumeCallback = (o, strings) =>
                {
                    if (o is BasicOptions b)
                        b.File = strings.First();
                },
                HasBeenConsumed = true
            };
            var validator = new RequiredParameterValidator(gitParameter);
            validator.Parameter = gitParameter;

            // act
            var result = validator.Validate(new BasicOptions());

            // assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}