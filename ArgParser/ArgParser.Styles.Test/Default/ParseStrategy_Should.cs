using System;
using System.Linq;
using ArgParser.Core;
using ArgParser.Styles.Default;
using ArgParser.Testing.Common;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class ParseStrategy_Should
    {
        private class BackwardsParameter : Parameter
        {
            /// <inheritdoc />
            public override ConsumptionResult CanConsume(object instance, IterationInfo info) =>
                new ConsumptionResult(info, 1, this);

            /// <inheritdoc />
            public override ConsumptionResult Consume(object instance, ConsumptionRequest request) =>
                new ConsumptionResult(request.Info, -1, this);
        }

        private ContextBuilder CreateDefaultBuilder()
        {
            return new ContextBuilder()
                .AddParser<UtilOptions>("util")
                .WithBooleanSwitch('h', "help", o => o.IsHelpRequested = true, help =>
                {
                    help
                        .SetName("Help")
                        .SetShortDescription("Get help on commands");
                })
                .WithBooleanSwitch(null, "version", o => o.IsVersionRequested = true, help =>
                {
                    help
                        .SetName("Version")
                        .SetShortDescription("Display the current version");
                })
                .Finish
                .AddParser<ClipboardOptions>("clip", help =>
                {
                    help
                        .SetName("Clipboard")
                        .SetShortDescription("Interact with the clipboard");
                })
                .WithBooleanSwitch('o', "overwrite", o => o.IsOverwriteClipboard = true, help =>
                {
                    help
                        .SetName("Overwrite")
                        .SetShortDescription("Overwrite the contents of the clipboard");
                })
                .Finish
                .AddParser<SortOptions>("sort", help =>
                {
                    help
                        .SetName("Sort")
                        .SetShortDescription("Sort the lines of text on the clipboard");
                })
                .WithFactoryFunction(() => new SortOptions())
                .WithBooleanSwitch('r', "reverse", o => o.IsReversed = true, help =>
                {
                    help
                        .SetName("Reverse")
                        .SetShortDescription("Reverse the lines of sorted text");
                })
                .Finish
                .AddParser<ZipOptions>("zip", help =>
                {
                    help
                        .SetName("Zip")
                        .SetShortDescription("Zip the files currently on the clipboard");
                })
                .WithFactoryFunction(() => new ZipOptions())
                .WithPositional((o, s) => o.ZipFile = s, help =>
                {
                    help
                        .SetName("Output File")
                        .SetShortDescription("The zip file to create");
                })
                .WithPositionals((o, s) => o.Globs = s, helpSetupCallback: help =>
                {
                    help
                        .SetName("Glob Patterns")
                        .SetShortDescription("Optional list of glob patterns to use to zip only some of the files");
                })
                .Finish
                .AddParser<FireWallOptions>("firewall", help =>
                {
                    help
                        .SetName("Firewall")
                        .SetShortDescription("Interact with the the local firewall");
                })
                .WithSingleValueSwitch('p', "port", (o, s) => o.Port = Convert.ToInt32(s), help =>
                {
                    help
                        .SetName("Port")
                        .SetShortDescription("The port on which to act");
                })
                .WithSingleValueSwitch('m', "mode", (o, s) =>
                {
                    o.IsInbound = s.Contains("i");
                    o.IsOutbound = s.Contains("o");
                }, help =>
                {
                    help
                        .SetName("Mode")
                        .SetShortDescription("Set whether inbound or outbound traffic should be blocked");
                })
                .WithPositional((o, s) => o.Program = s, help =>
                {
                    help
                        .SetName("Program")
                        .SetShortDescription("Which program to set the rule on");
                })
                .Finish
                .AddParser<BlockProgramOptions>("block", help =>
                {
                    help
                        .SetName("Block")
                        .SetShortDescription("Block a program in/out on a specified port");
                })
                .WithFactoryFunction(() => new BlockProgramOptions())
                .Finish
                .AddParser<UnblockProgramOptions>("unblock", help =>
                {
                    help
                        .SetName("Unblock")
                        .SetShortDescription("Unblock a program in/out on a specified port");
                })
                .WithFactoryFunction(() => new UnblockProgramOptions())
                .Finish
                .AddParser<ConvertOptions>("convert", help =>
                {
                    help
                        .SetName("Convert")
                        .SetShortDescription("Convert files to another format");
                })
                .WithFactoryFunction(() => new ConvertOptions())
                .WithSingleValueSwitch('f', "format", (o, s) => o.Format = s, help =>
                {
                    help
                        .SetName("Format")
                        .SetShortDescription("What format to conver the files to");
                })
                .WithPositionals((o, s) => o.InputFiles = s, helpSetupCallback: help =>
                {
                    help
                        .SetName("Input Files")
                        .SetShortDescription("Input files to convert");
                })
                .Finish
                .CreateParentChildRelationship("util", "clip")
                .CreateParentChildRelationship("util", "firewall")
                .CreateParentChildRelationship("util", "convert")
                .CreateParentChildRelationship("clip", "sort")
                .CreateParentChildRelationship("clip", "zip")
                .CreateParentChildRelationship("firewall", "block")
                .CreateParentChildRelationship("firewall", "unblock");
        }

        [Fact]
        public void Allow_A_Switch_To_Be_Greedy_When_No_Conflicting_Switch_Values()
        {
            // arrange
            var strat = new ParseStrategy("base");
            var context = new Context();
            var parser = context.ParserRepository.Create("base");
            parser.AddParameter(new BooleanSwitch(null, "help", o => { }));
            parser.AddParameter(new BooleanSwitch(null, "verbose", o => { }));
            var valuesSwitch = new ValuesSwitch('d', "data", (o, strings) => { });
            parser.AddParameter(valuesSwitch);
            var info = new IterationInfo("-d d0 d1 d2 d3 -v -h".Split(' '));
            var result = new ConsumptionResult(info, 7, valuesSwitch);

            // act
            var request = strat.CreateCanConsumeRequest("", parser.ToEnumerableOfOne().ToList(), info, result);

            // assert
            request.Max.Should().Be(7);
            request.Info.Should().BeSameAs(info);
        }

        [Fact]
        public void Get_The_Correct_Parser_Tree()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .Finish
                .AddParser("child")
                .Finish
                .AddParser("gchild")
                .Finish
                .CreateParentChildRelationship("base", "child")
                .CreateParentChildRelationship("child", "gchild");

            var context = builder.BuildContext();
            var strat = new ParseStrategy("base");
            var gchild = context.ParserRepository.Get("gchild");
            var child = context.ParserRepository.Get("child");
            var baze = context.ParserRepository.Get("base");

            // act
            var chain = strat.GetParserFamily(context, gchild);

            // assert
            chain.Should().BeEquivalentTo(gchild, child, baze);
        }

        [Fact]
        public void Identify_The_Correct_Parser_Based_Off_Which_Commands_Are_Children()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .Finish
                .AddParser("child")
                .Finish
                .AddParser("gchild")
                .Finish
                .CreateParentChildRelationship("base", "child")
                .CreateParentChildRelationship("child", "gchild");

            var context = builder.BuildContext();
            var strat = new ParseStrategy("base");
            var gchild = context.ParserRepository.Get("gchild");

            // act
            var identifiedParser = strat.IdentifyRelevantParser("child gchild".Split(' '), context);

            // assert
            identifiedParser.Should().BeSameAs(gchild);
        }

        [Fact]
        public void Identify_When_Forward_Progress_Has_Been_Violated()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .Finish
                .AddParser("child")
                .Finish
                .AddParser("gchild")
                .WithFactoryFunction(() => "")
                .Finish
                .CreateParentChildRelationship("base", "child")
                .CreateParentChildRelationship("child", "gchild");

            var context = builder.BuildContext();
            var parser = builder.ParserRepository.Get("gchild");
            parser.AddParameter(new BackwardsParameter());
            var strat = new ParseStrategy("base");

            // act
            var parsed = false;
            var result = strat.Parse("child gchild -h".Split(' '), context);
            result.WhenError(exceptions =>
            {
                exceptions.Single().Should().BeOfType<ForwardProgressException>();
                parsed = true;
            });

            // assert
            parsed.Should().BeTrue();
        }

        [Fact]
        public void Limit_A_Switches_Consumption_If_It_Tries_To_Consume_Another_Switch()
        {
            // arrange
            var strat = new ParseStrategy("base");
            var context = new Context();
            var parser = context.ParserRepository.Create("base");
            parser.AddParameter(new BooleanSwitch('h', "help", o => { }));
            parser.AddParameter(new BooleanSwitch('v', "verbose", o => { }));
            var valuesSwitch = new ValuesSwitch('d', "data", (o, strings) => { });
            parser.AddParameter(valuesSwitch);
            var info = new IterationInfo("-d d0 d1 d2 d3 -v -h".Split(' '));
            var result = new ConsumptionResult(info, 7, valuesSwitch);

            // act
            var request = strat.CreateCanConsumeRequest("", parser.ToEnumerableOfOne().ToList(), info, result);

            // assert
            request.Max.Should().Be(5);
            request.Info.Should().BeSameAs(info);
        }

        [Fact]
        public void Not_Throw_A_Parse_Exception_But_Rather_Send_It_To_The_Response()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base") // todo: should have AddBaseParser and remove parser name from strat
                .Finish
                .AddParser("child")
                .Finish
                .AddParser("gchild")
                .WithFactoryFunction(() => "")
                .Finish
                .CreateParentChildRelationship("base", "child")
                .CreateParentChildRelationship("child", "gchild");

            var context = builder.BuildContext();
            var strat = new ParseStrategy("base");
            IParseResult result = null;
            Action mightThrow = () => result = strat.Parse("child gchild -X".Split(' '), context);

            // act
            // assert
            mightThrow.Should().NotThrow();
            result.WhenError(exceptions => { exceptions.Single().Should().BeOfType<UnexpectedArgException>(); });
        }

        [Fact]
        public void Parse_A_Complex_Hierarchy()
        {
            // arrange
            var args = "firewall block -p 8080 -m io firefox.exe".Split(' ');
            var builder = CreateDefaultBuilder();
            var strat = new ParseStrategy("util");

            // act
            var ids = strat.GetCommandIdentifyingSubsequence(args, builder.BuildContext());

            // assert
            ids.Should().BeEquivalentTo("firewall block".Split(' '));
        }

        [Fact]
        public void Parse_Options_Correctly()
        {
            // arrange
            var isHelp = false;
            var builder = new ContextBuilder()
                .AddParser("base")
                .Finish
                .AddParser("child")
                .Finish
                .AddParser("gchild")
                .WithFactoryFunction(() => "")
                .WithBooleanSwitch('h', "help", o => isHelp = true)
                .Finish
                .CreateParentChildRelationship("base", "child")
                .CreateParentChildRelationship("child", "gchild");

            var context = builder.BuildContext();
            var strat = new ParseStrategy("base");

            // act
            var isParsed = false;
            var result = strat.Parse("child gchild -h".Split(' '), context);
            result.When<string>(s => isParsed = true);

            // assert
            isParsed.Should().BeTrue();
            isHelp.Should().BeTrue();
        }

        [Fact]
        public void Parse_Values_Irrespective_Of_Order()
        {
            // arrange
            var builder = CreateDefaultBuilder();

            // act
            var parseCount = 0;
            var res0 = builder.Parse("util", "firewall block -p 8080 -m io firefox.exe".Split(' '));
            var res1 = builder.Parse("util", "firewall block -m io firefox.exe -p 8080".Split(' '));
            var res2 = builder.Parse("util", "firewall block firefox.exe -m io -p 8080".Split(' '));

            // assert
            res0.When<BlockProgramOptions>(options =>
            {
                parseCount++;
                options.Port.Should().Be(8080);
                options.IsInbound.Should().BeTrue();
                options.IsOutbound.Should().BeTrue();
                options.Program.Should().Be("firefox.exe");
            });
            res1.When<BlockProgramOptions>(options =>
            {
                parseCount++;
                options.Port.Should().Be(8080);
                options.IsInbound.Should().BeTrue();
                options.IsOutbound.Should().BeTrue();
                options.Program.Should().Be("firefox.exe");
            });
            res2.When<BlockProgramOptions>(options =>
            {
                parseCount++;
                options.Port.Should().Be(8080);
                options.IsInbound.Should().BeTrue();
                options.IsOutbound.Should().BeTrue();
                options.Program.Should().Be("firefox.exe");
            });
            parseCount.Should().Be(3);
        }

        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            Action mightThrow = () => new ParseStrategy(null);

            // act
            // assert
            mightThrow.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_If_No_Factory_Function()
        {
            // arrange
            var isHelp = false;
            var builder = new ContextBuilder()
                .AddParser("base")
                .Finish
                .AddParser("child")
                .Finish
                .AddParser("gchild")
                .WithBooleanSwitch('h', "help", o => isHelp = true)
                .Finish
                .CreateParentChildRelationship("base", "child")
                .CreateParentChildRelationship("child", "gchild");

            var context = builder.BuildContext();
            var strat = new ParseStrategy("base");
            Action mightThrow = () => strat.Parse("child gchild -h".Split(' '), context);

            // act
            // assert
            isHelp.Should().BeFalse();
            mightThrow.Should().Throw<NoFactoryFunctionException>();
        }
    }
}