﻿using System;
using System.Collections.Generic;
using ArgParser.Flavors.Git;
using FluentAssertions;
using Xunit;

namespace ArgParser.Flavors.Test.Git
{
    public class GitValidatorRepository_Should
    {
        [Fact]
        public void Return_The_Correct_Validators()
        {
            // arrange
            var validator = new RequiredParameterValidator(new BooleanSwitch());
            var repo = new GitValidatorRepository();

            // act
            repo.AddValidator("test", validator);
            var validators = repo.GetValidators("test");

            // assert
            validators.Should().BeEquivalentTo(validator);
        }

        [Fact]
        public void Return_True_If_The_Parser_Has_Been_Registered()
        {
            // arrange
            var repo = new GitValidatorRepository();
            repo.AddValidator("test", new RequiredParameterValidator(new BooleanSwitch()));

            // act
            // assert
            repo.Contains("test").Should().BeTrue();
        }

        [Fact]
        public void Throw_If_Given_Null_Values()
        {
            // arrange
            var repo = new GitValidatorRepository();
            Action mightThrow0 = () => repo.AddValidator(null, null);
            Action mightThrow1 = () => repo.AddValidator("a", null);
            Action mightThrow2 = () => repo.AddValidator(null, new RequiredParameterValidator(new BooleanSwitch()));
            Action mightThrow3 = () => repo.GetValidators(null);

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
            mightThrow3.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throw_If_Validator_Cannot_Be_Found()
        {
            // arrange
            var repo = new GitValidatorRepository();
            Action mightThrow = () => repo.GetValidators("notfound");

            // act
            // assert
            mightThrow.Should().Throw<KeyNotFoundException>();
        }
    }
}