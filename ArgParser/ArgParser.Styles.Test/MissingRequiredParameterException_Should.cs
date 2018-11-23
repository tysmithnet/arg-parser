using System;
using ArgParser.Core;
using FluentAssertions;
using Xunit;

namespace ArgParser.Styles.Test
{
    public class MissingRequiredParameterException_Should
    {
        [Fact]
        public void Provide_The_Instance_And_Parameter_That_Caused_The_Error()
        {
            // arrange
            var param = new BooleanSwitch(new Parser("a"), 'h', "help", o => { });
            var instance = new object();
            var ex0 = new MissingRequiredParameterException(param, instance);

            // act
            // assert
            try
            {
                throw ex0;
            }
            catch (MissingRequiredParameterException e)
            {
                e.Instance.Should().BeSameAs(instance);
                e.RequiredParameter.Should().BeSameAs(param);
            }
        }

        [Fact]
        public void Create_A_Message_For_Switch_And_Positional_Differently()
        {
            // arrange
            var param = new BooleanSwitch(new Parser("a"), 'h', "help", o => { });
            var pos = new Positional(new Parser("a"), (o, strings) => { });
            var ex0 = new MissingRequiredParameterException(param, new object());
            var ex1 = new MissingRequiredParameterException(pos, new object());

            // act
            // assert
            ex0.Message.Should().NotBe(ex1.Message);
        }
    }
}