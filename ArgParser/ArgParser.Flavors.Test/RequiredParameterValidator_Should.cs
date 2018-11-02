using System.Linq;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test
{
    public class RequiredParameterValidator_Should
    {
        public class BasicOptions
        {
            public string File { get; set; }
        }

        [Fact]
        public void Return_Success_If_The_Validation_Succeeds()
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