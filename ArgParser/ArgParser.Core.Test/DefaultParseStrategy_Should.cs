using System;
using System.Collections.Generic;
using ArgParser.Core.Validation;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultParseStrategy_Should
    {
        private class BaseOptions
        {
            public bool HelpRequested { get; set; }
        }

        private class ChildOptions : BaseOptions
        {
            public string Thing { get; set; }
        }

        private class GrandChildOptions : ChildOptions
        {
            public string SpecialThing { get; set; }
        }

        private class BaseOptionsNoHelpValidator : IValidator<BaseOptions>
        {
            /// <inheritdoc />
            public bool CanValidate(object instance)
            {
                if (instance is BaseOptions casted)
                    return CanValidate(casted);
                return false;
            }

            /// <inheritdoc />
            public bool CanValidate(BaseOptions instance) => true;

            /// <inheritdoc />
            public IValidationResult Validate(object instance)
            {
                if (instance is BaseOptions casted)
                    return Validate(casted);
                throw new InvalidOperationException();
            }

            /// <inheritdoc />
            public IValidationResult Validate(BaseOptions instance)
            {
                if (instance.HelpRequested)
                    return new ValidationResult
                    {
                        Errors = new List<ValidationError>
                        {
                            new ValidationError()
                        },
                        IsSuccess = false,
                        Instance = instance
                    };
                return new ValidationResult
                {
                    IsSuccess = true,
                    Instance = instance
                };
            }
        }

        [Fact]
        public void Identify_Lack_Of_Forward_Progress()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddParameter(new DefaultParameter<BaseOptions>(
                (instance, info) => info.Current.Raw == "-h" || info.Current.Raw == "--help",
                (instance, info) =>
                {
                    instance.HelpRequested = true;
                    return info.Consume(-1);
                }));
            var strat = new DefaultParseStrategy(new Func<object>[] {() => new BaseOptions()});

            // act
            var result = strat.Parse(new[] {parser}, "--help".Split(' '));

            // assert
            var isParsed = false;
            result.When<BaseOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                isParsed = true;
            });
            isParsed.Should().BeFalse();
        }

        [Fact]
        public void Parse_A_Hierarchy()
        {
            // arrange
            var parentParser = new DefaultParser<BaseOptions>();
            parentParser.AddParameter(new DefaultParameter<BaseOptions>(
                (instance, info) => info.Current.Raw == "-h" || info.Current.Raw == "--help",
                (instance, info) =>
                {
                    instance.HelpRequested = true;
                    return info.Consume(1);
                }));

            var childParser = new DefaultParser<ChildOptions>();
            childParser.AddParameter(new DefaultParameter<ChildOptions>(
                (instance, info) => info.Current.Raw.StartsWith("thing="),
                (instance, info) =>
                {
                    instance.Thing = info.Current.Raw.Substring("thing=".Length);
                    return info.Consume(1);
                }));

            var grandChildParser = new DefaultParser<GrandChildOptions>();
            grandChildParser.AddParameter(new DefaultParameter<GrandChildOptions>(
                (instance, info) => info.Current.Raw == "--special" && info.Next != null,
                (instance, info) =>
                {
                    instance.SpecialThing = info.Next.Raw;
                    return info.Consume(2);
                }));

            grandChildParser.BaseParser = childParser;
            childParser.BaseParser = parentParser;

            var strat = new DefaultParseStrategy(new Func<object>[]
                {() => new BaseOptions(), () => new ChildOptions(), () => new GrandChildOptions()});

            // act
            var result = strat.Parse(new IParser[] {parentParser, childParser, grandChildParser},
                "--help thing=duke --special corgi".Split(' '));

            // assert
            var baseParsed = 0;
            var childParsed = 0;
            var grandChildParsed = 0;
            result.When<BaseOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                baseParsed++;
            });
            result.When<ChildOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                options.Thing.Should().Be("duke");
                childParsed++;
            });
            result.When<GrandChildOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                options.Thing.Should().Be("duke");
                options.SpecialThing.Should().Be("corgi");
                grandChildParsed++;
            });
            baseParsed.Should().Be(1);
            childParsed.Should().Be(1);
            grandChildParsed.Should().Be(1);
        }

        [Fact]
        public void Parse_A_Single_Type()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddParameter(new DefaultParameter<BaseOptions>(
                (instance, info) => info.Current.Raw == "-h" || info.Current.Raw == "--help",
                (instance, info) =>
                {
                    instance.HelpRequested = true;
                    return info.Consume(1);
                }));
            var strat = new DefaultParseStrategy<BaseOptions>(new Func<BaseOptions>[] {() => new BaseOptions()});

            // act
            var result = strat.Parse(new[] {parser}, "--help".Split(' '));

            // assert
            var isParsed = false;
            result.When<BaseOptions>(options =>
            {
                options.HelpRequested.Should().BeTrue();
                isParsed = true;
            });
            isParsed.Should().BeTrue();
        }

        [Fact]
        public void Validate_Results()
        {
            // arrange
            var parser = new DefaultParser<BaseOptions>();
            parser.AddParameter(new DefaultParameter<BaseOptions>(
                (instance, info) => info.Current.Raw == "-h" || info.Current.Raw == "--help",
                (instance, info) =>
                {
                    instance.HelpRequested = true;
                    return info.Consume(1);
                }));
            var strat = new DefaultParseStrategy(new Func<object>[] {() => new BaseOptions()})
            {
                Validators = new List<IValidator>
                {
                    new BaseOptionsNoHelpValidator()
                }
            };
            // act
            var result = strat.Parse(new[] {parser}, "--help".Split(' '));

            // assert
            var isParsed = false;
            result.When<BaseOptions>(options => { isParsed = true; });
            isParsed.Should().BeFalse();
        }
    }
}