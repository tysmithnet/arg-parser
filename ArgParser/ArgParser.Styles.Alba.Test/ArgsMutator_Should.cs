using ArgParser.Core;
using ArgParser.Styles.ParseStrategy;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Alba.Test
{
    public class ArgsMutator_Should
    {
        [Fact]
        public void Short_Circuit_If_There_Are_No_Boolean_Parameters()
        {
            // arrange
            var builder = new ContextBuilder("root")
                .AddParser("root")
                .WithPositional((o, s) => { })
                .Finish;
            var mutator = new ArgsMutator(builder.Context);
            var request = new MutateArgsRequest
            {
                Context = builder.Context,
                Args = "a b".Split(' '),
                Chain = builder.Context.PathToRoot("root")
            };

            // act
            // assert
            mutator.Mutate(request).Should().BeEquivalentTo("a", "b");
        }
    }
}