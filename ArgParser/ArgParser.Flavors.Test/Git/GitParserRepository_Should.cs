using System;
using System.Collections.Generic;
using System.Linq;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class GitParserRepository_Should
    {
        [Fact]
        public void Correctly_Establish_Parent_Child_Relationships()
        {
            // arrange
            var repo = new GitParserRepository();
            var a = repo.Create("a");
            var b = repo.Create("b");
            var c = repo.Create("c");
            var d = repo.Create("d");
            var e = repo.Create("e");

            // act
            repo.EstablishParentChildRelationship("a", "b");
            repo.EstablishParentChildRelationship("a", "c");
            repo.EstablishParentChildRelationship("c", "d");
            repo.EstablishParentChildRelationship("c", "e");

            // assert
            repo.GetChildren("a", false).Select(x => x.Name).Should().BeEquivalentTo("b c".Split(' '));
            repo.GetChildren("a", true).Select(x => x.Name).Should().BeEquivalentTo("b c d e".Split(' '));
            repo.GetParent("a")?.Name.Should().BeNull();
            repo.GetParent("e").Name.Should().Be("c");
            repo.GetAncestors("e").Select(x => x.Name).Should().BeEquivalentTo("c a".Split(' '));
        }

        [Fact]
        public void Gracefully_Exit_If_Adding_The_Same_Relationship_Multiple_Times()
        {
            // arrange
            var repo = new GitParserRepository();
            repo.Create("a");
            repo.Create("b");
            Action add = () => repo.EstablishParentChildRelationship("a", "b");

            // act
            // assert
            for (var i = 0; i < 3; i++) add.Should().NotThrow();
        }

        [Fact]
        public void Provide_The_Ability_To_Get_SubCommands()
        {
            // arrange
            var repo = new GitParserRepository();
            repo.Create("a");
            repo.Create("b");
            repo.EstablishParentChildRelationship("a", "b");

            // act
            // assert
            repo.IsSubCommand("a", "b").Should().BeTrue();
            repo.IsSubCommand("a", "c").Should().BeFalse();
            repo.GetSubCommand("a", "b").Should().BeSameAs(repo.Get("b"));
        }

        [Fact]
        public void Return_The_Correct_Flavor_When_Asked_For_By_Name()
        {
            // arrange
            var repo = new GitParserRepository();

            // act
            var a = repo.Create("a");
            var a2 = repo.Get("a");

            // assert
            a.Should().BeSameAs(a2);
        }

        [Fact]
        public void Throw_If_Asking_For_A_Flavor_That_Doesnt_Exist()
        {
            // arrange
            var repo = new GitParserRepository();
            Action mightThrow = () => repo.Get("doesntexist");

            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Asking_For_A_Parent_But_The_Child_Doesnt_Exist()
        {
            // arrange
            var repo = new GitParserRepository();
            Action mightThrow = () => repo.GetParent("doesntexist");

            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Indicate_It_Contains_Parser_When_Something_Has_Been_Created_With_That_Name()
        {
            // arrange
            var repo = new GitParserRepository();
            repo.Create("a");

            // act
            // assert
            repo.Contains("a").Should().BeTrue();
            repo.Contains("b").Should().BeFalse();
        }

        [Fact]
        public void Throw_If_Attempting_To_Create_Duplicate_Flavor_Names()
        {
            // arrange
            var repo = new GitParserRepository();
            Action mightThrow0 = () => repo.Create("a");
            Action mightThrow1 = () => repo.Create("a");

            // act
            // assert
            mightThrow0.Should().NotThrow();
            mightThrow1.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Throw_If_Cant_Find_Parent_Or_Child_When_Creating_Relationships()
        {
            // arrange
            var repo = new GitParserRepository();
            repo.Create("parent");
            //repo.Create("child");
            Action mightThrow0 = () => repo.EstablishParentChildRelationship("parent", "child");
            Action mightThrow1 = () => repo.EstablishParentChildRelationship("grandparent", "parent");

            // act
            // assert
            mightThrow0.Should().Throw<KeyNotFoundException>();
            mightThrow1.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Cant_Find_Parent_Or_Child_When_Getting_Ancestors()
        {
            // arrange
            var repo = new GitParserRepository();
            //repo.Create("child");
            Action mightThrow0 = () => repo.GetAncestors("parent");

            // act
            // assert
            mightThrow0.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Cant_Find_Parent_Or_Child_When_Getting_Children()
        {
            // arrange
            var repo = new GitParserRepository();
            //repo.Create("child");
            Action mightThrow0 = () => repo.GetChildren("parent", false);

            // act
            // assert
            mightThrow0.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Cant_Find_Parent_Or_Child_When_Getting_Parent()
        {
            // arrange
            var repo = new GitParserRepository();
            //repo.Create("child");
            Action mightThrow0 = () => repo.GetParent("parent");

            // act
            // assert
            mightThrow0.Should().Throw<KeyNotFoundException>();
        }
    }
}