using ArgParser.Core;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Extensions.Test
{
    public class ArgsMutator_Should
    {
        [Fact]
        public void Short_Circuit_If_There_Are_No_Boolean_Parameters()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("root")
                .WithPositional((o, s) => { })
                .Finish;
            var mutator = new ArgsMutator(builder.Context);
            var request = new MutateArgsRequest("a b".Split(' '), builder.Context.PathToRoot("root"), builder.Context);

            // act
            // assert
            mutator.Mutate(request).Should().BeEquivalentTo("a", "b");
        }

        [Fact]
        public void Short_Circuit_If_There_Are_No_Boolean_Letters()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("root")
                .WithBooleanSwitch(null, "whatever", o => {})
                .Finish;
            var mutator = new ArgsMutator(builder.Context);
            var request = new MutateArgsRequest("-a -b".Split(' '), builder.Context.PathToRoot("root"), builder.Context);

            // act
            // assert
            mutator.Mutate(request).Should().BeEquivalentTo("-a", "-b");
        }

        [Fact]
        public void Only_Group_If_The_Entire_Arg_Matches_And_Not_When_A_Subtring_Matches()
        {
            // arrange
            var builder = DefaultBuilder.CreateDefaultBuilder();
            var request = new MutateArgsRequest("--help".Split(' '), builder.Context.ParserRepository.Get("util").ToEnumerableOfOne(), builder.Context);
            var mutator = new ArgsMutator(builder.Context);

            // act
            // assert
            mutator.Mutate(request).Should().BeEquivalentTo("--help".ToEnumerableOfOne(), "Although the arg contains -h, it is not the entire arg and thus does not match");
        }
    }
}