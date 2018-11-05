﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgParser.Styles.Default;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test.Default
{
    public class HierarchyRepository_Should
    {
        [Fact]
        public void Throw_If_Given_Bad_Values()
        {
            // arrange
            var repo = new HierarchyRepository();
            Action mightThrow0 = () => repo.GetAncestors(null);
            Action mightThrow1 = () => repo.IsParent(null, null);
            Action mightThrow2 = () => repo.IsParent(null, "something");
            Action mightThrow3 = () => repo.EstablishParentChildRelationship(null, null);
            Action mightThrow4 = () => repo.EstablishParentChildRelationship(null, "");
            Action mightThrow5 = () => repo.EstablishParentChildRelationship("", null);

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().NotThrow();
            mightThrow2.Should().NotThrow();
            mightThrow3.Should().Throw<ArgumentNullException>();
            mightThrow4.Should().Throw<ArgumentNullException>();
            mightThrow5.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_If_Requested_Parser_Does_Not_Exist()
        {
            // arrange
            var repo = new HierarchyRepository();
            repo.AddParser("base");
            repo.AddParser("child");
            repo.EstablishParentChildRelationship("base", "child");
            Action mightThrow0 = () => repo.GetAncestors("blah");
            Action mightThrow1 = () => repo.IsParent("base", "blah");
            Action mightThrow2 = () => repo.IsParent("blah", "blah");
            Action mightThrow3 = () => repo.EstablishParentChildRelationship("base", "blah");
            Action mightThrow4 = () => repo.EstablishParentChildRelationship("blah", "base");

            // act
            // assert
            mightThrow0.Should().Throw<KeyNotFoundException>();
            mightThrow1.Should().NotThrow();
            mightThrow2.Should().NotThrow();
            mightThrow3.Should().Throw<KeyNotFoundException>();
            mightThrow4.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Correctly_Create_And_Recall_Parent_Child_Relationships()
        {
            // arrange
            var repo = new HierarchyRepository();

            // act
            repo.AddParser("base");
            repo.AddParser("child");
            repo.EstablishParentChildRelationship("base", "child");

            // assert
            repo.IsParent("base", "child").Should().BeTrue();
            repo.IsParent("base", null).Should().BeFalse();
            repo.IsParent("child", "base").Should().BeFalse();
            repo.IsParent("base", "base").Should().BeFalse();
            repo.IsParent(null, "base").Should().BeTrue();
        }

        [Fact]
        public void Only_Create_A_Relationship_Once()
        {
            // arrange
            var repo = new HierarchyRepository();

            // act
            repo.AddParser("base");
            repo.AddParser("child");
            repo.EstablishParentChildRelationship("base", "child");
            repo.EstablishParentChildRelationship("base", "child");

            // assert
            repo.Nodes["base"].Children.Should().HaveCount(1);
        }

        [Fact]
        public void Only_Create_A_Parser_Once()
        {
            // arrange
            var repo = new HierarchyRepository();

            // act
            repo.AddParser("base");
            repo.AddParser("base");

            // assert
            repo.Nodes.Should().HaveCount(1);
        }

        [Fact]
        public void Correctly_Get_A_Parsers_Ancestors()
        {
            // arrange
            var repo = new HierarchyRepository();

            // act
            repo.AddParser("base");
            repo.AddParser("child0");
            repo.AddParser("child1");
            repo.AddParser("child0child0");
            repo.AddParser("child0child1");
            repo.AddParser("child1child0");
            repo.AddParser("child1child1");
            repo.EstablishParentChildRelationship("base", "child0");
            repo.EstablishParentChildRelationship("base", "child1");
            repo.EstablishParentChildRelationship("child0", "child0child0");
            repo.EstablishParentChildRelationship("child0", "child0child1");
            repo.EstablishParentChildRelationship("child1", "child1child0");
            repo.EstablishParentChildRelationship("child1", "child1child1");

            // assert
            repo.GetAncestors("child0").Should().BeEquivalentTo("base".Split(' '));
            repo.GetAncestors("child1child1").Should().BeEquivalentTo("child1 base".Split(' '));
        }
    }
}
