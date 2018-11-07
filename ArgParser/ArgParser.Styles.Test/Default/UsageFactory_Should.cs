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
    public class UsageFactory_Should
    {
        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .Finish;
            Action mightThrow0 = () => new UsageFactory().Create(null, null);
            Action mightThrow1 = () => new UsageFactory().Create("base", null);
            Action mightThrow2 = () => new UsageFactory().Create(null, builder.BuildContext());
            Action mightThrow3 = () => new UsageFactory().Create("madeup", builder.BuildContext());

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
            mightThrow3.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Return_The_Root_Parser_Id_If_Nothing_Configured()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .Finish;
            var fac = new UsageFactory();

            // act
            var node = fac.Create("base", builder.BuildContext());

            // assert
            node.Text.Should().Be("base");
        }

        [Fact]
        public void Group_Boolean_Switches()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .WithBooleanSwitch('h', "help", o => { })
                .WithBooleanSwitch('v', "version", o => { })
                .Finish
                .AddParser("child")
                .WithBooleanSwitch('l', "log", o => { })
                .Finish
                .CreateParentChildRelationship("base", "child");

            var fac = new UsageFactory();

            // act
            var node = fac.Create("child", builder.BuildContext());

            // assert
            node.Text.Should().Be("child [-hlv]");
        }

        [Fact]
        public void Group_Single_Value_Switches()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .WithSingleValueSwitch('a', "accept", (o, s) => { })
                .WithSingleValueSwitch('r', "reject", (o, s) => { })
                .Finish
                .AddParser("child")
                .WithSingleValueSwitch('c', "capture", (o, s) => { })
                .Finish
                .CreateParentChildRelationship("base", "child");

            var fac = new UsageFactory();

            // act
            var node = fac.Create("child", builder.BuildContext());

            // assert
            node.Text.Should().Be("child [-acr v1]");
        }

        [Fact]
        public void Group_Value_Switches_With_The_Same_Max()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .WithValuesSwitch('a', "accept", (o, s) => { }, 1, 2)
                .WithValuesSwitch('r', "reject", (o, s) => { }, 2, 2)
                .WithValuesSwitch('m', "monitor", (o, s) => { }, 2, 5)
                .Finish
                .AddParser("child")
                .WithValuesSwitch('c', "capture", (o, s) => { })
                .Finish
                .CreateParentChildRelationship("base", "child");

            var fac = new UsageFactory();

            // act
            var node = fac.Create("child", builder.BuildContext());

            // assert
            node.Text.Should().Be("child [-ar v1..v2] [-m v1..v5] [-c v1..vN]");
        }

        [Fact]
        public void List_Switch_Word_When_No_Letter()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .WithBooleanSwitch(null, "help", o => { })
                .WithBooleanSwitch(null, "version", o => { })
                .WithSingleValueSwitch(null, "reject", (o, s) => { })
                .WithValuesSwitch(null, "monitor", (o, s) => { }, 2, 5)
                .Finish
                .AddParser("child")
                .WithValuesSwitch(null, "capture", (o, s) => { },1, 5)
                .Finish
                .CreateParentChildRelationship("base", "child");

            var fac = new UsageFactory();

            // act
            var node = fac.Create("child", builder.BuildContext());

            // assert
            node.Text.Should().Be("child [--help|version] [--reject v1] [--capture|monitor v1..v5]");
        }

        [Fact]
        public void Not_Group_Positionals()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .WithPositional((o, s) => { })
                .WithPositionals((o, s) => { }, 1, 5)
                .WithPositionals((o, s) => { }, 1, 3)
                .Finish
                .AddParser("child")
                .WithPositionals((o, s) => { })
                .Finish
                .CreateParentChildRelationship("base", "child");

            var fac = new UsageFactory();

            // act
            var node = fac.Create("child", builder.BuildContext());

            // assert
            node.Text.Should().Be("child [p1..pN] [p1] [p1..p5] [p1..p3]"); // todo: should we warn the user about this?
        }

        [Fact]
        public void List_Sub_Commands_First()
        {
            // arrange
            var builder = new ContextBuilder()
                .AddParser("base")
                .Finish
                .AddParser("child0")
                .Finish
                .AddParser("child1")
                .Finish
                .AddParser("child2")
                .Finish
                .CreateParentChildRelationship("base", "child0")
                .CreateParentChildRelationship("base", "child1")
                .CreateParentChildRelationship("base", "child2")
                ;

            var fac = new UsageFactory();

            // act
            var node = fac.Create("base", builder.BuildContext());

            // assert
            node.Text.Should().Be("base [child0|child1|child2]"); 
        }
    }
}
