using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace ArgParser.Flavors.Test
{
    public class GitFlavorRepository_Should
    {
        [Fact]
        public void Throw_If_Attempting_To_Create_Duplicate_Flavor_Names()
        {
            // arrange
            var repo = new GitFlavorRepository();
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
            var repo = new GitFlavorRepository();
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
        public void Throw_If_Asking_For_A_Flavor_That_Doesnt_Exist()
        {
            // arrange
            var repo = new GitFlavorRepository();
            Action mightThrow = () => repo.Get("doesntexist");
            
            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Throw_If_Asking_For_A_Parent_But_The_Child_Doesnt_Exist()
        {
            // arrange
            var repo = new GitFlavorRepository();
            Action mightThrow = () => repo.GetParent("doesntexist", false);

            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }
    }
}
