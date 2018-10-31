using System;
using FluentAssertions;
using Xunit;

namespace ArgParser.Core.Test
{
    public class DefaultParameter_Should
    {
        [Fact]
        public void Call_The_Callback_When_Asked()
        {
            // arrange
            var isCanConsumeCalled = false;
            var isConsumeCalled = false;
            Func<object, IIterationInfo, bool> canConsume = (s, info) => isCanConsumeCalled = true;
            Func<object, IIterationInfo, IIterationInfo> consume = (s, info) =>
            {
                isConsumeCalled = true;
                return info;
            };
            var param = new DefaultParameter(canConsume, consume);

            // act
            param.CanConsume("", new DefaultIterationInfo());
            param.Consume("", new DefaultIterationInfo());

            // assert
            isCanConsumeCalled.Should().BeTrue();
            isConsumeCalled.Should().BeTrue();
            param.Reset();
        }

        [Fact]
        public void Call_The_Generic_Callback_When_Asked()
        {
            // arrange
            var isCanConsumeCalled = false;
            var isConsumeCalled = false;
            Func<string, IIterationInfo, bool> canConsume = (s, info) => isCanConsumeCalled = true;
            Func<string, IIterationInfo, IIterationInfo> consume = (s, info) =>
            {
                isConsumeCalled = true;
                return info;
            };
            var param = new DefaultParameter<string>(canConsume, consume);

            // act
            param.CanConsume("", new DefaultIterationInfo());
            param.Consume("", new DefaultIterationInfo());

            // assert
            isCanConsumeCalled.Should().BeTrue();
            isConsumeCalled.Should().BeTrue();
        }

        [Fact]
        public void Gracefully_Degrade_If_Bad_Object_Are_Passed()
        {
            // arrange
            var param = new DefaultParameter<string>((s, info) => true, (s, info) => info);
            Action mightThrow0 = () => param.CanConsume(new object(), new DefaultIterationInfo());
            Action mightThrow1 = () => param.Consume(new object(), new DefaultIterationInfo());

            // act
            // assert
            mightThrow0.Should().NotThrow();
            mightThrow1.Should().NotThrow();
        }

        [Fact]
        public void Throw_If_Constructor_Arguments_Are_Null()
        {
            // arrange
            Action mightThrow0 = () => new DefaultParameter((o, info) => true, null);
            Action mightThrow1 = () => new DefaultParameter(null, (o, info) => info);
            Action mightThrow2 = () => new DefaultParameter<string>((s, info) => true, null);
            Action mightThrow3 = () => new DefaultParameter<string>(null, (s, info) => info);

            // act
            // assert
            mightThrow0.Should().Throw<ArgumentNullException>();
            mightThrow1.Should().Throw<ArgumentNullException>();
            mightThrow2.Should().Throw<ArgumentNullException>();
            mightThrow3.Should().Throw<ArgumentNullException>();
        }
    }
}