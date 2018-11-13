//using System.Linq;
//using ArgParser.Core;
//using ArgParser.Core.Help;
//using FluentAssertions;
//using Xunit;

//namespace ArgParser.Styles.Help.Test
//{
//    public class UsageFactory_Should
//    {
//        [Fact]
//        public void Join_Parser_Ids_As_SubCommand_Usage()
//        {
//            // arrange
//            var fac = new UsageFactory();
//            var builder = new ContextBuilder()
//                .AddParser("base", help => { help.SetName("Base"); })
//                .Finish
//                .AddParser("child", help => { help.SetName("Child"); })
//                .Finish
//                .AddParser("otherchild", help => { help.SetName("Other Child"); })
//                .Finish
//                .CreateParentChildRelationship("base", "child")
//                .CreateParentChildRelationship("base", "otherchild");

//            // act
//            var res = fac.CreateSubCommandUsage("base", builder.BuildContext());

//            // assert
//            res.Should().BeOfType<SpanNode>().Which.Children.Should().HaveCount(3);
//        }

//        [Fact]
//        public void Use_Just_The_Letter_If_The_Word_Is_Not_Set()
//        {
//            // arrange
//            var fac = new UsageFactory();

//            // act
//            var res = fac.CreateSwitchUsage(new SingleValueSwitch(new Parser("a"), 'v', null, (o, s) => { }),
//                new Context());

//            // assert
//            res.Should().BeOfType<SpanNode>();
//            res.Children[0].Should().BeOfType<CodeNode>().Which.Text.Should().Be("-v");
//        }

//        [Fact]
//        public void Use_The_Value_Alias_If_It_Is_Set()
//        {
//            // arrange
//            var parent = new Parser("parent");
//            var singleValue = new SingleValueSwitch(parent, 'v', "value", (o, s) => { })
//            {
//                Help = new ParameterHelp
//                {
//                    ValueAlias = "val"
//                }
//            };
//            var multiValue = new ValuesSwitch(parent, 'v', "val", (o, s) => { })
//            {
//                Help = new ParameterHelp
//                {
//                    ValueAlias = "val"
//                }
//            };
//            var booleanSwitch = new BooleanSwitch(parent, 'h', "help", o => { });
//            var fac = new UsageFactory();

//            // act
//            var res0 = fac.CreateUsageAlias(singleValue, new Context());
//            var res1 = fac.CreateUsageAlias(multiValue, new Context());
//            var res2 = fac.CreateUsageAlias(booleanSwitch, new Context());

//            // assert
//            res0.Should().BeOfType<CodeNode>().Which.Text.Should().Be("val");
//            res1.Should().BeOfType<SpanNode>().Which.Children.Should().HaveCount(3).And.Subject.Cast<TextNode>()
//                .Select(x => x.Text).Should().BeEquivalentTo("val1", "..", "valN");
//            res2.Should().BeOfType<TextNode>().Which.Text.Should().Be("");
//        }
//    }
//}